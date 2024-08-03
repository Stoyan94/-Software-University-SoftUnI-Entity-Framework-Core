namespace Invoices.DataProcessor
{
    using System.Globalization;

    using Newtonsoft.Json;

    using ExportDto;
    using Data;
    using Utilities;

    public class Serializer
    {
        public static string ExportClientsWithTheirInvoices(InvoicesContext context, DateTime date)
        {
            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Clients";

            ExportClientDto[] clientsToExport = context.Clients
                .Where(cl => cl.Invoices.Any(i => DateTime.Compare(i.IssueDate, date) > 0))
                .Select(cl => new ExportClientDto()
                {
                    ClientName = cl.Name,
                    VatNumber = cl.NumberVat,
                    Invoices = cl.Invoices
                        .OrderBy(i => i.IssueDate)
                        .ThenByDescending(i => i.DueDate)
                        .Select(i => new ExportClientInvoiceDto()
                        {
                            InvoiceNumber = i.Number,
                            InvoiceAmount = i.Amount,
                            Currency = i.CurrencyType.ToString(),
                            DueDate = i.DueDate.ToString("d", CultureInfo.InvariantCulture)
                        })
                        .ToArray(),
                    InvoicesCount = cl.Invoices.Count
                })
                .OrderByDescending(cl => cl.InvoicesCount)
                .ThenBy(cl => cl.ClientName)
                .ToArray();

            return xmlHelper.Serialize(clientsToExport, xmlRoot);
        }

        public static string ExportProductsWithMostClients(InvoicesContext context, int nameLength)
        {
            ExportProductDto[] productsToExport = context.Products
                .Where(p => p.ProductsClients.Any())
                .Where(p => p.ProductsClients.Any(pc => pc.Client.Name.Length >= nameLength))
                .Select(p => new ExportProductDto()
                {
                    Name = p.Name,
                    Price = p.Price,
                    Category = p.CategoryType.ToString(),
                    Clients = p.ProductsClients
                        .Where(pc => pc.Client.Name.Length >= nameLength)
                        .Select(pc => new ExportProductClientsDto()
                        {
                            Name = pc.Client.Name,
                            NumberVat = pc.Client.NumberVat,
                        })
                        .OrderBy(c => c.Name)
                        .ToArray()
                })
                .OrderByDescending(p => p.Clients.Length)
                .ThenBy(p => p.Name)
                .Take(5)
                .ToArray();

            return JsonConvert.SerializeObject(productsToExport, Formatting.Indented);
        }
    }
}