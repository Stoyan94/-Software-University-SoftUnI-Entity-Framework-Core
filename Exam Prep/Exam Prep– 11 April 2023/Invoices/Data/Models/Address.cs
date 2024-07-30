using System.ComponentModel.DataAnnotations;

namespace Invoices.Data.Models
{
    using static DataConstraints;
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(AddressStreetNameMaxLength)]
        public string StreetName { get; set; } = null!;

        public int StreetNumber  { get; set; }

        public string PostCode { get; set; } = null!;

        [MaxLength(AdressCityMaxLength)]
        public string City { get; set; } = null!;

        [MaxLength(AdressCountryMaxLength)]
        public string Country { get; set; } = null!;

        // TODO: Add navigation properties
    }
}
