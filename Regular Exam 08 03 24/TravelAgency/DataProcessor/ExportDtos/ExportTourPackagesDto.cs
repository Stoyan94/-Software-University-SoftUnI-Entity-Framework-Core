using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using TravelAgency.Data.Models;

namespace TravelAgency.DataProcessor.ExportDtos
{
    using static Data.DataConstraints;

    [XmlType(nameof(TourPackage))]
    public class ExportTourPackagesDto
    {
        [Required]
        [MinLength(TourPackageNameMinLenght)]
        [MaxLength(TourPackageNameMaxLenght)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }
    }
}
