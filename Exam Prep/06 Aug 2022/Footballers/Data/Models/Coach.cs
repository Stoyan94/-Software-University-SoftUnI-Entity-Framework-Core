using System.ComponentModel.DataAnnotations;

namespace Footballers.Data.Models
{
    using static DataConstraints;
    public class Coach
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(CoachNameMaxLenght)]
        public string Name { get; set; } = null!;
        public string Nationality { get; set; } = null!;

        public virtual ICollection<Footballer> Footballers { get; set; }
            = new HashSet<Footballer>();
    }
}
