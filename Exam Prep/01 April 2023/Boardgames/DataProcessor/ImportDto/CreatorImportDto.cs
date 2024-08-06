namespace Boardgames.DataProcessor.ImportDto
{
    using Boardgames.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using static Data.Models.DataConstraints;

    [XmlType(nameof(Creator))]
    public class CreatorImportDto
    {
        [Required]
        [XmlElement("FirstName")]
        [MinLength(CreatorFirstNameMinLenght)]
        [MaxLength(CreatorFirstNameMaxLenght)]
        public string FirstName { get; set; } = null!;

        [Required]
        [XmlElement("LastName")]
        [MinLength(CreatorLastNameMinLenght)]
        [MaxLength(CreatorLastNameMaxLenght)]
        public string LastName { get; set; } = null!;

        [XmlArray("Boardgames")]
        public BoardgameImporttDto[] Boardgame { get; set; } = null!;
    }
}
