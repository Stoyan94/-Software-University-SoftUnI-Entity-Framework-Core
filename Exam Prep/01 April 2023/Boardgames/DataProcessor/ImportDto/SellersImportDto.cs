namespace Boardgames.DataProcessor.ImportDto
{
    using Boardgames.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using static Data.Models.DataConstraints;

    [XmlType(nameof(Seller))]
    public class SellersImportDto
    {
        [Required]        
        [MinLength(SellerNameMinLenght)]
        [MaxLength(SellerNameMaxLenght)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(SellerAddressMinLenght)]
        [MaxLength(SellerAddressMaxLenght)]
        public string Address { get; set; } = null!;

        [Required]
        public string Country { get; set; } = null!;

        [Required]
        [RegularExpression(SellerWebsiteRegexPattern)]
        public string Website { get; set; } = null!;

        public int[] Boardgames { get; set; } = null!;
    }
}
