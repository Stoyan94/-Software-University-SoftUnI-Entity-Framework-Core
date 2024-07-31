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

        public ICollection<Product> ProductsClients  { get; set; }
            = new HashSet<Product>();

        public ICollection<Address> Addresses { get; set; } 
            = new HashSet<Address>();

    }
}
