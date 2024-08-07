﻿namespace Footballers.DataProcessor.ImportDto
{
    using Footballers.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    using static Data.DataConstraints;

    [XmlType(nameof(Footballer))]
    public class ImportFootballersDto
    {
        [XmlElement("Name")]
        [Required]
        [MinLength(FootballerNameMinLenght)]
        [MaxLength(FootballerNameMaxLenght)]
        public string Name { get; set; }

        [XmlElement("ContractStartDate")]
        [Required]
        public string ContractStartDate { get; set; } = null!;

        [XmlElement("ContractEndDate")]
        [Required]
        public string ContractEndDate { get; set; } = null!;

        [XmlElement("BestSkillType")]
        [Required]
        [Range(0, 4)]
        public int BestSkillType { get; set; }

        [Required]
        [XmlElement("PositionType")]
        [Range(0, 3)]
        public int PositionType { get; set; } 
    }

    //ContractStartDate>22/03/2020</ContractStartDate>
    //    <ContractEndDate>24/02/2026</ContractEndDate>
    //    <BestSkillType>2</BestSkillType>
    //    <PositionType>2</PositionType>

}
