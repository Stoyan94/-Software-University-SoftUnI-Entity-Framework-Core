using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema_RepoLearn.Data.Model
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string? CustomerName { get; set; }

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
