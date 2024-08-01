using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [MaxLength(AddressCityMaxLength)]
        public string City { get; set; } = null!;

        [MaxLength(AddressCountryMaxLength)]
        public string Country { get; set; } = null!;

        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }
        public virtual Client Client { get; set; } = null!;
    }
}
