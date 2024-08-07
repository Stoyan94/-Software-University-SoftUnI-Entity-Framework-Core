using System.ComponentModel.DataAnnotations;

namespace Footballers.Data.Models
{
    using static DataConstraints;
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(TeamNameMaxLenght)]
        public string Name { get; set; } = null!;

        [MaxLength(TeamNationalityMaxLenght)]
        public string Nationality { get; set; } = null!;
        public int Trophies { get; set; }

        public virtual ICollection<TeamFootballer> TeamsFootballers { get; set; }
            = new HashSet<TeamFootballer>();
    }
}
