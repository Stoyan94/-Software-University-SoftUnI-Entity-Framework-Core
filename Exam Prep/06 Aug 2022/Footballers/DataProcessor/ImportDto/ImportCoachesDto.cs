﻿using Footballers.Data.Models;
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
        public string Name { get; set; }

        [XmlElement("Nationality")]
        public string Nationality { get; set; }

        [XmlArray("Footballers")]
        public ImportFootballersDto[] Footballers { get; set; } = null!;
    }
}
