using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using TravelAgency.Data.Models;

namespace TravelAgency.DataProcessor.ImportDtos
{
    using static Data.DataConstraints;

    [XmlType("Customer")]
    public class XmlImportCustomerDto
    {
        [XmlElement("FullName")]
        [Required]
        [MinLength(CustomerFullNameMinLenght)]
        [MaxLength(CustomerFullNameMaxLenght)]
        public string FullName { get; set; } = null!;

        [XmlElement("Email")]
        [Required]
        [MinLength(CustomerEmailMinLenght)]
        [MaxLength(CustomerEmailMaxLenght)]
        public string Email { get; set; } = null!;

        [XmlAttribute("phoneNumber")]
        [Required]
        [RegularExpression(CustomerPhoneNumberPattern)]
        public string PhoneNumber { get; set; } = null!;
    }
}
