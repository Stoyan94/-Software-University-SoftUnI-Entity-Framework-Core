using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Infrastructure.Data.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = null!;

        public List<CinemaHall> CinemaHalls { get; set; } = new List<CinemaHall>();
    }
}
