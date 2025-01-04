using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTrain.Data.Model
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public decimal BasePrice { get; set; }

        public int TariffId { get; set; }

        [ForeignKey(nameof(TariffId))]
        public Tariff Tariff { get; set; } = null!;

        [Required]
        public int SeatId { get; set; }

        [ForeignKey(nameof(SeatId))]
        public Seat Seat { get; set; } = null!;

        [Required]
        public int ScheduleId { get; set; }

        [ForeignKey(nameof(ScheduleId))]
        public Schedule Schedule { get; set; } = null!;

    }
}
