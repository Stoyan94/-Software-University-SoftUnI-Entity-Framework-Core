using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using TravelAgency.Data;
using TravelAgency.Data.Models;
using TravelAgency.DataProcessor.ExportDtos;
using TravelAgency.Utilities;

namespace TravelAgency.DataProcessor
{
    public class Serializer
    {
        public static string ExportGuidesWithSpanishLanguageWithAllTheirTourPackages(TravelAgencyContext context)
        {
            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Guides";

            var guidesSpeakingSpanish = context.Guides
                .Where(g => (int)g.Language == 3)
                .Include(g => g.TourPackagesGuides)
                .Select(guiDto => new ExportGuidesDto()
                {
                    FullName = guiDto.FullName,
                    TourPackages = guiDto.TourPackagesGuides.Select(tpDto => new ExportTourPackagesDto()
                    {
                        Name = tpDto.TourPackage.PackageName,
                        Description = tpDto.TourPackage.Description,
                        Price = tpDto.TourPackage.Price
                    })
                    .OrderByDescending(g => g.Price)
                    .ToArray()
                })
                .OrderByDescending(g => g.TourPackages.Count())
                .ThenBy(g => g.FullName)
                .ToArray();

            return xmlHelper.Serialize(guidesSpeakingSpanish, xmlRoot);
        }

        public static string ExportCustomersThatHaveBookedHorseRidingTourPackage(TravelAgencyContext context)
        {
            var customersWithBookedPackage = context.Customers
            .Where(c => c.Bookings.Any(b => b.TourPackage.PackageName == "Horse Riding Tour"))
            .Select(c => new
            {
                c.FullName,
                c.PhoneNumber,
                Bookings = c.Bookings
                    .Where(b => b.TourPackage.PackageName == "Horse Riding Tour")
                    .Select(b => new
                    {
                        b.TourPackage.PackageName,
                        b.BookingDate
                    })
                    .OrderBy(b => b.BookingDate)
                    .ToArray()
            })
            .OrderBy(c => c.FullName)
            .ThenBy(c => c.Bookings.Length)
            .ToArray();


            var validExport = customersWithBookedPackage
                .Select(c => new ExportCustomerDTO
                {
                    FullName = c.FullName,
                    PhoneNumber = c.PhoneNumber,
                    Bookings = c.Bookings
                        .Select(b => new ExportBookingDTO
                        {
                            TourPackageName = b.PackageName,
                            Date = b.BookingDate.ToString("yyyy-MM-dd")
                        })
                        .ToArray()
                })
                .ToArray();
            
            

            return JsonConvert.SerializeObject(validExport, Formatting.Indented);
        }
    }
}
