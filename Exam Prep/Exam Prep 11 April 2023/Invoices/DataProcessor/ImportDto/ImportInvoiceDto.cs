namespace Invoices.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstraints;

    public class ImportInvoiceDto
    {
        [Required]
        [Range(InvoiceNumberMinValue, InvoiceNumberMaxValue)]
        public int Number { get; set; }

        [Required]
        public string IssueDate { get; set; } = null!; // DateTime -> Deserialize as a string

        [Required]
        public string DueDate { get; set; } = null!;

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [Range(InvoiceCurrencyTypeMinValue, InvoiceCurrencyTypeMaxValue)]
        public int CurrencyType { get; set; } // Enum -> Deserialize as an integer

        [Required]
        public int ClientId { get; set; }
    }
}
