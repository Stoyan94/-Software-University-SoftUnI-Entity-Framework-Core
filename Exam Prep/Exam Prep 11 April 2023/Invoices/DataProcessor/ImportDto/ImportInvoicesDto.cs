using System.ComponentModel.DataAnnotations;

namespace Invoices.DataProcessor.ImportDto
{
    using static Data.DataConstraints;
    public class ImportInvoicesDto
    {
        [Range(InvoiceNumberMinLength, InvoiceNumberMaxLength)]
        public int Number { get; set; }

        [Required]
        public string IssueDate { get; set; } = null!;

        [Required]
        public string DueDate { get; set; } = null!;

        public decimal Amount { get; set; }

        [Range(InvoiceCurrencyTypeMinValue, InvoiceCurrencyTypeMaxValue)]
        public int CurrencyType { get; set; }

        public int ClientId { get; set; }
    }
}
