using Boardgames.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boardgames.Data.Models
{
    using static DataConstraints;
    public class Boardgame
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(BoardGameNameMaxLenght)]
        public string Name { get; set; } = null!;

        public double Rating { get; set; }

        public int YearPublished { get; set; }

        public CategoryType CategoryType  { get; set; }

        public string Mechanics { get; set; } = null!;

        [ForeignKey(nameof(Creator))]
        public int CreatorId { get; set; }
        public virtual Creator Creator { get; set; } = null!;

        public ICollection<BoardgameSeller> BoardgamesSellers { get; set; } 
            = new HashSet<BoardgameSeller>();
    }
}
