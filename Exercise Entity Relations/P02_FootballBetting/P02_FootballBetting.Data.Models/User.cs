using P02_FootballBetting.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace P02_FootballBetting.Data.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [MaxLength(ValidationConstants.UserUserNameMaxLength)]
    public string UserName { get; set; }

    [Required]
    [MaxLength(ValidationConstants.UserPassWordMaxLength)]
    public string Password { get; set; }

    [Required]
    [MaxLength(ValidationConstants.UserEmailMaxLenght)]
    public string Email { get; set; }

    [Required]
    [MaxLength(ValidationConstants.UserNameMaxLength)]
    public string Name { get; set; }

    public decimal Balance { get; set; }
}
