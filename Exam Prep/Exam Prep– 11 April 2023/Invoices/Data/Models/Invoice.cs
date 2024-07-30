using Invoices.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

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

        // TODO: Add navigation properties
    }
}
