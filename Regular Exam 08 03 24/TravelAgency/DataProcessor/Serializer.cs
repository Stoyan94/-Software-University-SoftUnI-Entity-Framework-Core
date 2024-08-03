using Microsoft.EntityFrameworkCore;
using System.Text;
using TravelAgency.Data;
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
            throw new NotImplementedException();
        }
    }
}
