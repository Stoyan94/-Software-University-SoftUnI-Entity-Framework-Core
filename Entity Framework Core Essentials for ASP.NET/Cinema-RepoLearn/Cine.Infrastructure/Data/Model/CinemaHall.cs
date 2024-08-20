using Cinema_RepoLearn.Infrastructure.Data.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cine.Infrastructure.Data.Model
{
    public class CinemaHall
    {
        [ForeignKey(nameof(Cinema))]
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; } = null!;

        [ForeignKey(nameof(Hall))]
        public int HallId { get; set; }
        public Hall Hall { get; set; } = null!;
    }
}
