using Footballers.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey(nameof(Coach))]
        public int CoachId { get; set; }
        public virtual Coach Coach { get; set; } = null!;

        public virtual ICollection<TeamFootballer> TeamsFootballers { get; set; }
            = new HashSet<TeamFootballer>();
    }
}
