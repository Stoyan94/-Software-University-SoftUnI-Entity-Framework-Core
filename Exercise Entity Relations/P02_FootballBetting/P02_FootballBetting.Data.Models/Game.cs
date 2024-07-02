using P02_FootballBetting.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace P02_FootballBetting.Data.Models;

public class Game
{
    [Key]
    public int GameId { get; set; }

    public int HomeTeamId { get; set; }

    public int AwayTeamId { get; set; }

    public byte HomeTeamGoals { get; set; }

    public byte AwayTeamGoals { get; set; }

    public DateTime DateTime { get; set; }

    public double HomeTeamBetRate { get; set; }

    public double AwayTeamBetRate { get; set; }

    public double DrawBetRait { get; set; }

    [MaxLength(ValidationConstants.GameResultMaxLength)]
    public string? Result { get; set; }
}
