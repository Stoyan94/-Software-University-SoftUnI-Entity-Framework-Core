namespace Cinema_RepoLearn.Infrastructure.Data.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Hall))]
        public int HallId { get; set; }
        public Hall Hall { get; set; } = null!;

        public DateTime Start { get; set; }

        public TimeSpan Duration { get; set; }

        public List<Ticket> Tickets { get; set; } 
            = new List<Ticket>();
    }
}
