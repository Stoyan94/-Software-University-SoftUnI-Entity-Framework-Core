using P02_FootballBetting.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace P02_FootballBetting.Data.Models;

public class Team
{
    [Key]
    public int TeamId { get; set; }

    [Required]
    [MaxLength(ValidationConstants.TeamMaxLength)]
    public string Name { get; set; }

    [MaxLength(ValidationConstants.TeamLogoUrlMaxLength)]
    public string LogoUrl { get; set; }

    [Required]
    [MaxLength(ValidationConstants.TeamInitialsMaxLength)]
    public string Initials { get; set; }
        
    public decimal Budget { get; set; }

    // TODO: Set Relations
    public int PrimaryKitColorId { get; set; }

    public int SecondaryKitColorId { get; set; }

    public int TownId { get; set; }

}

