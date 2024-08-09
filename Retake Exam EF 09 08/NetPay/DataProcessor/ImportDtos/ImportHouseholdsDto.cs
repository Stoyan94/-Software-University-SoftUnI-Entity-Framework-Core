using NetPay.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace NetPay.DataProcessor.ImportDtos
{
    using static Data.DataConstraints;

    [XmlType(nameof(Household))]
    public class ImportHouseholdsDto
    {
        [XmlElement("ContactPerson")]
        [Required]
        [MinLength(HouseholdContactPersonMinLenght)]
        [MaxLength(HouseholdContactPersonMaxLenght)]
        public string ContactPerson { get; set; } = null!;

        [XmlAttribute("phone")]
        [Required]
        [RegularExpression(RegexHouseholdPhonePattern)]
        public string Phone { get; set; } = null!;

        [XmlElement("Email")]
        [MinLength(HouseholdEmailMinLenght)]
        [MaxLength(HouseholdEmailMaxLenght)]
        public string? Email { get; set; } 
    }


//    •	ContactPerson - text with length[5, 50] (required)
//•	Email – text with length[6, 80] (not required)
//•	PhoneNumber – text with length 15. (required)
//o   The phone number must start with a plus sign, followed by exactly three digits for the country code, a slash, exactly three digits for the area or service provider code, a dash, and exactly six digits for the subscriber number: 
//	Example -> +144/123-123456 
//	Use the following string for correct validation: @"^\+\d{3}/\d{3}-\d{6}$"
//•	Expenses - a collection of type Expense

}
