using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using System.Xml.Serialization;
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

            XmlSerializer serializer = new XmlSerializer(typeof(XmlImportCustomerDto[]), new XmlRootAttribute("Customers"));

            using StringReader reader = new StringReader(xmlString);
            XmlImportCustomerDto[] customerDtos = (XmlImportCustomerDto[])serializer.Deserialize(reader);
            List<Customer> customers = new List<Customer>();

            foreach (var cDto in customerDtos)
            {
                if (!IsValid(cDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var customer = new Customer
                {
                    FullName = cDto.FullName,
                    Email = cDto.Email,
                    PhoneNumber = cDto.PhoneNumber
                };

                bool isDuplicationInContext = context.Customers.Any(c => c.FullName == customer.FullName) ||
                                              context.Customers.Any(c => c.Email == customer.Email) ||
                                              context.Customers.Any(c => c.PhoneNumber == customer.PhoneNumber);

                bool isDuplicationInCustomers = customers.Any(c => c.FullName == customer.FullName) ||
                                                customers.Any(c => c.Email == customer.Email) ||
                                                customers.Any(c => c.PhoneNumber == customer.PhoneNumber);

                if (isDuplicationInContext || isDuplicationInCustomers)
                {
                    sb.AppendLine(DuplicationDataMessage);
                    continue;
                }

                customers.Add(customer);
                sb.AppendLine(string.Format(SuccessfullyImportedCustomer, customer.FullName));
            }

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }



        public static string ImportBookings(TravelAgencyContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var bookingDtos = JsonConvert.DeserializeObject<List<JsonImportBooking>>(jsonString);

            List<Booking> validBookings = new List<Booking>();

            foreach (var bookingDto in bookingDtos)
            {
                if (!IsValid(bookingDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime bookingDate;

                if (!DateTime.TryParseExact(bookingDto.BookingDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out bookingDate))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var customerId = context.Customers.FirstOrDefault(c => c.FullName == bookingDto.CustomerName).Id;
                var tourPackageId = context.TourPackages.FirstOrDefault(tp => tp.PackageName == bookingDto.TourPackageName).Id;

                if (customerId == 0 || tourPackageId == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var booking = new Booking
                {
                    CustomerId = customerId,
                    TourPackageId = tourPackageId,
                    BookingDate = bookingDate
                };
                validBookings.Add(booking);
                sb.AppendLine(string.Format(SuccessfullyImportedBooking, bookingDto.TourPackageName, bookingDto.BookingDate));
            }

            context.Bookings.AddRange(validBookings);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
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
