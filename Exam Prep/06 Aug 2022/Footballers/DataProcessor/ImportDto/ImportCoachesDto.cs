using Footballers.Data.Models;
namespace Footballers.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    using static Data.DataConstraints;

    [XmlType(nameof(Coach))]
    public class ImportCoachesDto
    {
        [XmlElement("Name")]
        [Required]
        [MinLength(CoachNameMinLenght)]
        [MaxLength(CoachNameMaxLenght)]
        public string Name { get; set; } = null!;

        [XmlElement("Nationality")]
        [Required]
        public string Nationality { get; set; } = null!;
        public ImportFootballersDto[] Footballers { get; set; } = null!;
    }
}
