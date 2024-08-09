using NetPay.Data.Models;
using System.Diagnostics.Metrics;
using System.Xml.Serialization;

namespace NetPay.DataProcessor.ExportDtos
{
    [XmlType(nameof(Household))]
    public class ExporHouseholdsDto
    {
        [XmlElement("ContactPerson")]
        public string ContactPerson { get; set; } = null!;

        [XmlElement("Email")]
        public string Email { get; set; } = null!; // may be null 

        [XmlElement("PhoneNumber")]
        public string PhoneNumber { get; set; } = null!;

        [XmlArray("Expenses")]
        public ExportExpencesDto[] Expenses { get; set; } = null!;
    }

    //ContactPerson>Alexander Ivanov</ContactPerson>
    //<Email>alexander.ivanov @example.com</Email>
    //<PhoneNumber>+144/123-123456</PhoneNumber>
    

}
