using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema_RepoLearn.Data.Model
{
    public class Seat
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Hall))]
        public int HallId { get; set; }
        public Hall Hall { get; set; } = null!;

        public int Row { get; set; }

        public int Number { get; set; }

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
