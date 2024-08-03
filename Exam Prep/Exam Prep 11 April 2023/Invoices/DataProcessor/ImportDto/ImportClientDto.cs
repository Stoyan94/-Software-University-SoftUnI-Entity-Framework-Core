    namespace Invoices.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    using Data.Models;
    using static Data.DataConstraints;

    [XmlType(nameof(Client))]
    public class ImportClientDto
    {
        [XmlElement(nameof(Name))]
        [Required]
        [MinLength(ClientNameMinLength)]
        [MaxLength(ClientNameMaxLength)]
        public string Name { get; set; } = null!;

        [XmlElement(nameof(NumberVat))]
        [Required]
        [MinLength(ClientNumberVatMinLength)]
        [MaxLength(ClientNumberVatMaxLength)]
        public string NumberVat { get; set; } = null!;

        [XmlArray(nameof(Addresses))]
        public ImportAddressDto[] Addresses { get; set; } = null!;
    }
}
