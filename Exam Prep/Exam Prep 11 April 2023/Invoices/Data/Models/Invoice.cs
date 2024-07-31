using Invoices.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Invoices.Data.Models
{
    using static DataConstraints;
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(InvoiceNumberMaxLength)]
        public int Number { get; set; }
        public DateTime IssueDate  { get; set; }
        public DateTime DueDate  { get; set; }
        public decimal Amount  { get; set; }
        public CurrencyType CurrencyType { get; set; }

        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }
        public virtual Client Client { get; set; } = null!;
    }
}
