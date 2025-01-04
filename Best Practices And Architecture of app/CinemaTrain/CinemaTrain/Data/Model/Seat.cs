using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTrain.Data.Model
{
    public class Seat
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int HallId { get; set; }

        [ForeignKey(nameof(HallId))]
        public Hall Hall { get; set; } = null!;

        [Required]
        public int Row { get; set; }

        [Required]
        public int Number { get; set; }

        public List<Ticket> Tickets { get; set; } = new List<Ticket>(); 
    }
}
