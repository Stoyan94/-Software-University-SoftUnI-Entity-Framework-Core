using Invoices.Data.Models;
using System.Xml.Serialization;

namespace Invoices.DataProcessor.ImportDto
{
    [XmlType(nameof(Client))]
    public class ImportClientDto
    {
        [XmlElement(nameof(Name))]
        public string Name { get; set; }

        [XmlElement(nameof(NumberVat))]
        public string NumberVat { get; set; }

        [XmlArray(nameof(Addresses))]
        public ImportAddressDto[] Addresses { get; set; }
    }
}
