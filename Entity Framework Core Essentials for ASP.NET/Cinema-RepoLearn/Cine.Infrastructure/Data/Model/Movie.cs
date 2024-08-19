namespace Cinema_RepoLearn.Infrastructure.Data.Model
{
    using Cinema_RepoLearn.Infrastructure.Data.Model.Enums;
    using System.ComponentModel.DataAnnotations;
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = null!;

        public Genre Genre { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public List<Schedule> Schedules { get; set; } 
            = new List<Schedule>();
    }
}
