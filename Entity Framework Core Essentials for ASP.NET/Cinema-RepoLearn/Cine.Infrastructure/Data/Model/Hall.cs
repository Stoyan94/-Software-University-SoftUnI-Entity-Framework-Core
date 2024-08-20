namespace Cinema_RepoLearn.Infrastructure.Data.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Hall
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        public List<Cinema> Cinemas { get; set; }
            = new List<Cinema>();

        public List<Seat> Seats { get; set; } 
            = new List<Seat>();

        public List<Schedule> Schedules { get; set; } 
            = new List<Schedule>();
    }
}
