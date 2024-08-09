using NetPay.Data;
using NetPay.Data.Models;
using NetPay.DataProcessor.ExportDtos;
using NetPay.Utilities;
using Newtonsoft.Json;

namespace NetPay.DataProcessor
{
    public class Serializer
    {
        public static string ExportHouseholdsWhichHaveExpensesToPay(NetPayContext context)
        {
            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRootName = "Households";

            var houseExpences = context.Households
                .Select(houseDto => new ExporHouseholdsDto()
                {
                    ContactPerson = houseDto.ContactPerson,
                    Email = houseDto.Email,
                    PhoneNumber = houseDto.PhoneNumber,
                    Expenses = houseDto.Expenses
                    .OrderBy(e => e.DueDate)
                    .ThenBy(e => e.Amount)
                    .Select(exDto => new ExportExpencesDto()
                    {
                        ExpenseName = exDto.ExpenseName,
                        Amount = exDto.Amount,
                        PaymentDate = exDto.DueDate.ToString("yyyy-MM-dd"),
                        ServiceName = exDto.Service.ServiceName
                    })
                    .ToArray()

                })
                .OrderBy(c=> c.ContactPerson)
                .ToArray();

            return xmlHelper.Serialize(houseExpences, xmlRootName);
        }

        public static string ExportAllServicesWithSuppliers(NetPayContext context)
        {
            var servicesWithSup = context.Services
                .Select(s => new
                {
                    ServiceName = s.ServiceName,
                    Suppliers = s.SuppliersServices
                    .Select(sup => new 
                    {
                        SupplierName = sup.Supplier.SupplierName
                    })
                    .OrderBy(sup => sup.SupplierName)
                    .ToArray()

                })
                .OrderBy(s=> s.ServiceName)
                .ToArray();

            return JsonConvert.SerializeObject(servicesWithSup, Formatting.Indented);
        }
    }
}
