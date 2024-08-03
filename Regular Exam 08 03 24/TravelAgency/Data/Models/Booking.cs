namespace TravelAgency.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static DataConstraints;
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        public DateTime BookingDate  { get; set; }

        [Required]
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(TourPackage))]
        public int TourPackageId { get; set; }
        public virtual TourPackage TourPackage { get; set; } = null!;
    }
}
