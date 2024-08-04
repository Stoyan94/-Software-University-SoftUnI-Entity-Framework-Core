namespace Boardgames.DataProcessor.ImportDto
{
    using Boardgames.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    using static Data.Models.DataConstraints;

    [XmlType(nameof(Boardgame))]
    public class BoardgameImportDto
    {
        [Required]
        [XmlElement("Name")]
        [MinLength(BoardGameNameMinLenght)]
        [MaxLength(BoardGameNameMaxLenght)]
        public string Name { get; set; } = null!;

        [Required]
        [XmlElement("Rating")]
        [Range(BoardGameRatingMinRange, BoardGameRatingMaxRange)]
        public double Rating { get; set; }

        [Required]
        [XmlElement("YearPublished")]
        [Range(BoardGameYearPublishedMinRange, BoardGameYearPublishedMaxRange)]
        public int YearPublished { get; set; }

        [Required]
        [XmlElement("CategoryType")]
        [Range(0, 4)]
        public int CategoryType { get; set; }

        [Required]
        [XmlElement("Mechanics")]
        public string Mechanics { get; set; } = null!;


    }
}
