using Footballers.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Footballers.Data.Models
{
    public class TeamFootballer
    {
        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; }
        public virtual Team Team { get; set; } = null!;

        [ForeignKey(nameof(Footballer))]
        public int FootballerId { get; set; }
        public virtual Footballer Footballer { get; set; } = null!;
    }
}

//•	TeamId – integer, Primary Key, foreign key (required)
//•	Team – Team
//•	FootballerId – integer, Primary Key, foreign key (required)
//•	Footballer – Footballer
