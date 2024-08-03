namespace Invoices.Data.Models
{
    // System
    using System.ComponentModel.DataAnnotations;

    // NuGet Namespaces

    // Internal project namespaces
    using Enums;
    using static DataConstraints;

    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ProductNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required] // decimal is Required by default
        public decimal Price { get; set; }

        [Required]
        public CategoryType CategoryType { get; set; } // Enumeration is stored in the DB as INT -> By default enum is required

        public virtual ICollection<ProductClient> ProductsClients { get; set; }
            = new HashSet<ProductClient>();
    }
}
