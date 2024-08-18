using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema_RepoLearn.Data.Model
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        [Required]
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
