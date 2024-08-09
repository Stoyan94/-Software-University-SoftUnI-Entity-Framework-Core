namespace NetPay.DataProcessor.ImportDtos
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstraints;
    using static System.Net.Mime.MediaTypeNames;

    public class ImportExpensesDto
    {
        [Required]
        [MinLength(ExpenseNameMinLenght)]
        [MaxLength(ExpenseNameMaxLenght)]
        public string ExpenseName { get; set; } = null!;

        [Range(ExpenAmountMinRange, ExpenAmountMaxRange)]
        public decimal Amount { get; set; }

        [Required]
        public string DueDate { get; set; } = null!;

        [Required]
        public string PaymentStatus { get; set; } = null!;

        [Required]
        public int HouseholdId { get; set; }
        [Required]
        public int ServiceId { get; set; }
    }

}
