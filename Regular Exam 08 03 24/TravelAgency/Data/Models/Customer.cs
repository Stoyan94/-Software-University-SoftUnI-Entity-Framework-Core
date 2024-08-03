using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Data.Models
{
    using static DataConstraints;
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CustomerFullNameMaxLenght)]
        public string FullName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(CustomerEmailMaxLenght)]
        public string Email { get; set; } = null!;

        [Required]
        [RegularExpression(CustomerPhoneNumberPattern)]
        public string PhoneNumber { get; set; } = null!;

        public virtual ICollection<Booking> Bookings  { get; set; } 
            = new HashSet<Booking>();
    }
}
