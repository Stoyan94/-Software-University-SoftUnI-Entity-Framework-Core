namespace Invoices.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using Invoices.Data;
    using Invoices.Data.Models;
    using Invoices.Data.Models.Enums;
    using Invoices.DataProcessor.ImportDto;
    using Invoices.Utilities;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedClients
            = "Successfully imported client {0}.";

        private const string SuccessfullyImportedInvoices
            = "Successfully imported invoice with number {0}.";

        private const string SuccessfullyImportedProducts
            = "Successfully imported product - {0} with {1} clients.";


        public static string ImportClients(InvoicesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlHepler xmlHepler = new XmlHepler();
            const string xmlRoot = "Clients";

            // Valid models to import into the DB!
            ICollection<Client> clientsToImport = new List<Client>();

            ImportClientDto[] deserializeClient =
                xmlHepler.Deserialize<ImportClientDto[]>(xmlString, xmlRoot);

            foreach (ImportClientDto clientDto in deserializeClient)
            {
                if (!IsValid(clientDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                ICollection<Address> addressesToImport = new List<Address>();

                foreach (var addressDto in clientDto.Addresses)
                {
                    if (!IsValid(addressDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Address newAddress = new Address()
                    {
                        StreetName = addressDto.StreetName,
                        StreetNumber = addressDto.StreetNumber,
                        PostCode = addressDto.PostCode,
                        City = addressDto.City,
                        Country = addressDto.Country                        
                    };
                    addressesToImport.Add(newAddress);
                }

                Client newClient = new Client()
                {
                    Name = clientDto.Name,
                    NumberVat = clientDto.NumberVat,
                    Addresses = addressesToImport
                };

                clientsToImport.Add(newClient);
                sb.AppendLine(String.Format(SuccessfullyImportedClients, clientDto.Name));
            }

            context.Clients.AddRange(clientsToImport);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }


        public static string ImportInvoices(InvoicesContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            ICollection<Invoice> invoicesToImport = new List<Invoice>();

            ImportInvoicesDto[] deserializedInvoices = 
                JsonConvert.DeserializeObject<ImportInvoicesDto[]>(jsonString)!;

            foreach (var invoicesDto in deserializedInvoices)
            {
                if (!IsValid(invoicesDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isIssueDateValid = DateTime
                    .TryParse(invoicesDto.IssueDate, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, 
                    out DateTime issueDate);

                bool isDueDateValid = DateTime
                    .TryParse(invoicesDto.DueDate, CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime dueDate);

                if (isIssueDateValid == false || isDueDateValid == false ||
                    DateTime.Compare(dueDate, issueDate ) < 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (!context.Clients.Any(cl => cl.Id == invoicesDto.ClientId))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Invoice newInvoice = new Invoice()
                {
                    Number = invoicesDto.Number,
                    IssueDate = issueDate,
                    DueDate = dueDate,
                    Amount = invoicesDto.Amount,
                    CurrencyType = (CurrencyType)invoicesDto.CurrencyType,
                    ClientId = invoicesDto.ClientId
                };

                invoicesToImport.Add(newInvoice);
                sb.AppendLine(String.Format(SuccessfullyImportedInvoices, invoicesDto.Number));
            }

            context.Invoices.AddRange(invoicesToImport);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportProducts(InvoicesContext context, string jsonString)
        {


            throw new NotImplementedException();
        }

        public static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    } 
}
