namespace Invoices.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static DataConstraints;

    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(AddressStreetNameMaxLength)]
        public string StreetName { get; set; } = null!; // NVARCHAR(20)

        [Required]
        public int StreetNumber { get; set; }

        [Required]
        public string PostCode { get; set; } = null!; // NVARCHAR(MAX)

        [Required]
        [MaxLength(AddressCityMaxLength)]
        public string City { get; set; } = null!; // NVARCHAR(15)

        [Required]
        [MaxLength(AddressCountryMaxLength)]
        public string Country { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }

        [Required]
        public virtual Client Client { get; set; } = null!;
    }
}
