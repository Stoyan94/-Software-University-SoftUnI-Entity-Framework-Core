namespace Cinema_RepoLearn.Infrastructure.Data.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        [Required]
        [Column(TypeName = "money")]
        public decimal BasePrice { get; set; }

        [Required]
        [ForeignKey(nameof(Seat))]
        public int SeatId { get; set; }
        public Seat Seat { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Schedule))]
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Tariff))]
        public int TariffId { get; set; }
        public Tariff Tariff { get; set; } = null!;
    }
}
