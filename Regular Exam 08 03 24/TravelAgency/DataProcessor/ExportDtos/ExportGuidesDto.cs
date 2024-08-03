namespace TravelAgency.DataProcessor.ExportDtos
{
    using System.Xml.Serialization;
    using TravelAgency.Data.Models;
    using static Data.DataConstraints;

    [XmlType(nameof(Guide))]
    public class ExportGuidesDto
    {
        [XmlElement("FullName")]
        public string FullName { get; set; } = null!;

        [XmlArray("TourPackages")]
        public ExportTourPackagesDto[] TourPackages { get; set; } = null!;
    }
}
