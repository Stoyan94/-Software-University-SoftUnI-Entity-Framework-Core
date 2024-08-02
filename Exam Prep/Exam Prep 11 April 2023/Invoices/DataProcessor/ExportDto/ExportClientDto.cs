using Invoices.Data.Models;
using System.Xml.Serialization;

namespace Invoices.DataProcessor.ExportDto
{
    [XmlType(nameof(Client))]
    public class ExportClientDto
    {
        [XmlElement(nameof(ClientName))]
        public string ClientName { get; set; } = null!;

        [XmlElement(nameof(VatNumber))]
        public string VatNumber { get; set; } = null!;

        [XmlArray(nameof(Invoices))]
        public ExportClientInvoiceDto[] Invoices { get; set; } = null!;
    }
}
