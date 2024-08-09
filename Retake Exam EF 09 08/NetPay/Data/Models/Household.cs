using AutoMapper.Execution;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.Intrinsics.X86;
using System.Threading;

namespace NetPay.Data.Models
{
    using static DataConstraints;
    public class Household
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(HouseholdContactPersonMaxLenght)]
        public string ContactPerson { get; set; } = null!;

        [EmailAddress]
        [MaxLength(HouseholdEmailMaxLenght)]
        public string? Email { get; set; }

        [MaxLength(HouseholdPhoneNumberMaxLenght)]
        [RegularExpression(RegexHouseholdPhonePattern)]
        public string PhoneNumber { get; set; } = null!;

        public virtual ICollection<Expense> Expenses { get; set; }
            = new HashSet<Expense>();
    }
}
