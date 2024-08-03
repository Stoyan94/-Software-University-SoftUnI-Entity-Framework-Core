using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using TravelAgency.Data;
using TravelAgency.Data.Models;
using TravelAgency.DataProcessor.ImportDtos;
using TravelAgency.Utilities;

namespace TravelAgency.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data format!";
        private const string DuplicationDataMessage = "Error! Data duplicated.";
        private const string SuccessfullyImportedCustomer = "Successfully imported customer - {0}";
        private const string SuccessfullyImportedBooking = "Successfully imported booking. TourPackage: {0}, Date: {1}";

        public static string ImportCustomers(TravelAgencyContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlHelper xmlHelper = new XmlHelper();

            const string xmlRootName = "Customers";

            List<Customer> customerToImport = new List<Customer>();

            XmlImportCustomerDto[] importCustomers = xmlHelper.
                Deserialize<XmlImportCustomerDto[]>(xmlString, xmlRootName);

            foreach (var customerDto in importCustomers)
            {
                
                    if (!IsValid(customerDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                Customer newCustomer = new Customer()
                {
                    FullName = customerDto.FullName,
                    Email = customerDto.Email,
                    PhoneNumber = customerDto.PhoneNumber
                };

                if (customerToImport.Any(n => n.FullName == customerDto.FullName) ||
                    customerToImport.Any(e => e.Email == customerDto.Email ||
                    customerToImport.Any(p => p.PhoneNumber == customerDto.PhoneNumber ||
                    context.Customers.Any(n => n.FullName == customerDto.FullName) ||
                    context.Customers.Any(e => e.Email == customerDto.Email ||
                    context.Customers.Any(p => p.PhoneNumber == customerDto.PhoneNumber)))))
                {
                    sb.AppendLine(DuplicationDataMessage);
                    continue;
                }




                customerToImport.Add(newCustomer);
                sb.AppendLine(string.Format(SuccessfullyImportedCustomer, newCustomer.FullName));
            }

            context.Customers.AddRange(customerToImport);
            context.SaveChanges();

            return sb.ToString();

        }

        

        public static string ImportBookings(TravelAgencyContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            
            ICollection<Booking> bookingsToImport = new List<Booking>();

            JsonImportBooking[] deserializedBookings =
               JsonConvert.DeserializeObject<JsonImportBooking[]>(jsonString)!;

            foreach (var bookingDto in deserializedBookings)
            {
                if (!IsValid(bookingDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime parsedDate;

                if (DateTime.TryParse(bookingDto.BookingDate, out parsedDate))
                {
                    string formattedDate = parsedDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                }

                var customer = context.Customers.SingleOrDefault(c => c.FullName == bookingDto.CustomerName);
                var tourPackage = context.TourPackages.SingleOrDefault(tp => tp.PackageName == bookingDto.TourPackageName);

                if (customer != null && tourPackage != null)
                {
                    var booking = new Booking
                    {
                        BookingDate = parsedDate,
                        CustomerId = customer.Id,
                        TourPackageId = tourPackage.Id
                    };

                    context.Bookings.Add(booking);
                    context.SaveChanges();

                    bookingsToImport.Add(booking);
                }


            }

            throw new NotImplementedException();
        }

        public static bool IsValid(object dto)
        {
            var validateContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(dto, validateContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                string currValidationMessage = validationResult.ErrorMessage;
            }

            return isValid;
        }
    }
}
