using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema_RepoLearn.Data.Model
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Film))]
        public int FilmId { get; set; }
        public Movie Film { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Hall))]
        public int HallId { get; set; }
        public Hall Hall { get; set; } = null!;

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public List<Ticket> Tickets { get; set; } 
            = new List<Ticket>();
    }
}
