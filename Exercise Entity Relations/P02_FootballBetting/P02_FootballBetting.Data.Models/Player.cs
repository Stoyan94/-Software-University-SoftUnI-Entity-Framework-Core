using P02_FootballBetting.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace P02_FootballBetting.Data.Models;

public class Player
{
    [Key]
    public int PlayerId { get; set; }

    [Required]
    [MaxLength(ValidationConstants.PlayerNameMaxLength)]
    public string Name { get; set; }

    public int SquadNumber { get; set; }

    public bool IsInjured { get; set; }

    public int? TeamId { get; set; } // This FK should not be NOT NULL

    public int PositionId { get; set; }
}
