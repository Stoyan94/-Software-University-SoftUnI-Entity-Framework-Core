using Footballers.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Footballers.Data.Models
{
    using static DataConstraints;
    public class Footballer
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(FootballerNameMaxLenght)]
        public string Name { get; set; } = null!;
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public PositionType PositionType{ get; set; }
        public BestSkillType BestSkillType { get; set; }

        public int CoachId { get; set; }
        public Coach Coach { get; set; } = null!;

        public ICollection<TeamFootballer> TeamsFootballers { get; set; }
            = new HashSet<TeamFootballer>();
    }
}
