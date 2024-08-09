using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NetPay.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace NetPay.Data.Models
{
    using static DataConstraints;
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(ExpenseNameMaxLenght)]
        public string ExpenseName { get; set; } = null!;
                
        public decimal Amount { get; set; }

        public DateTime DueDate { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        [ForeignKey(nameof(Household))]
        public int HouseholdId { get; set; }
        public virtual Household Household { get; set; } = null!;

        [ForeignKey(nameof(Service))]
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; } = null!;

    }
}
