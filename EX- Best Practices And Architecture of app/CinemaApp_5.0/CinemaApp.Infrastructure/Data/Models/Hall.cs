using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaApp.Infrastructure.Data.Models
{
    public class Hall
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        public List<CinemaHall> CinemaHalls { get; set; } = new List<CinemaHall>();

        public List<Seat> Seats { get; set; } = new List<Seat>();

        public List<Schedule> Schedules { get; set; } = new List<Schedule>();
    }
}
