using Boardgames.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Boardgames.Data.Models
{
    using static DataConstraints;
    public class Boardgame
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(BoardGameNameMinLenght)]
        public string Name { get; set; } = null!;

        public double Rating { get; set; }

        public int YearPublished { get; set; }

        public CategoryType CategoryType  { get; set; }

        public string Mechanics { get; set; } = null!;

        // TODO: Add nav prop
    }
}
