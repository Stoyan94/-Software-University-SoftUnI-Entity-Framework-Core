using Invoices.Data.Models;
using System.Xml.Serialization;

namespace Invoices.DataProcessor.ImportDto
{
    [XmlType(nameof(Address))]
    public class ImportAddressDto
    {
        [XmlElement(nameof(StreetName))]
        public string StreetName { get; set; }

        [XmlElement(nameof(StreetNumber))]
        public int StreetNumber { get; set; }

        [XmlElement(nameof(PostCode))]
        public string PostCode { get; set; }

        [XmlElement(nameof(City))]
        public string City { get; set; }

        [XmlElement(nameof(Country))]
        public string Country { get; set; }
    }
}
