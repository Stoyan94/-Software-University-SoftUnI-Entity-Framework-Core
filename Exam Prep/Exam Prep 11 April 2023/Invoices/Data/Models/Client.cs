using System.ComponentModel.DataAnnotations;

namespace Invoices.Data.Models
{
    using static DataConstraints;
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(ClientNameMaxLength)]
        public string Name { get; set; } = null!;

        [MaxLength(ClientNumberVatMaxLength)]
        public string NumberVat { get; set; } = null!;

        public virtual ICollection<ProductClient> ProductsClients  { get; set; }
            = new HashSet<ProductClient>();

        public virtual ICollection<Address> Addresses { get; set; } 
            = new HashSet<Address>();

        public virtual ICollection<Invoice> Invoices { get; set; }
            = new HashSet<Invoice>();

    }
}
