using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Core.Models
{
    public class MovieModel
    {
        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Genre { get; set; } = null!;

        public string? Description { get; set; }
    }
}
