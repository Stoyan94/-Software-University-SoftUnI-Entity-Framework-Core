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

        // TODO: Add navigations properties 
    }
}
