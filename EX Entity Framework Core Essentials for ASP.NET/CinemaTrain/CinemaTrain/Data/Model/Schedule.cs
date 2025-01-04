using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTrain.Data.Model
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MovieId { get; set; }

        [ForeignKey(nameof(MovieId))]
        public Movie Film { get; set; } = null!;

        [Required]
        public int HallId { get; set; }

        [ForeignKey(nameof(HallId))]
        public Hall Hall { get; set; } = null!;

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
