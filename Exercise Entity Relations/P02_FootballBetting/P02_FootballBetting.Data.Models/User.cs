using P02_FootballBetting.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace P02_FootballBetting.Data.Models;

public class User
{
    public User()
    {
        this.Bets = new HashSet<Bet>();
    }

    [Key]
    public int UserId { get; set; }

    [MaxLength(ValidationConstants.UserUserNameMaxLength)]
    public string Username { get; set; } = null!;

    // Password are saved hashed in the DB
    [MaxLength(ValidationConstants.UserPassWordMaxLength)]
    public string Password { get; set; } = null!;

    [MaxLength(ValidationConstants.UserEmailMaxLenght)]
    public string Email { get; set; } = null!;

    [MaxLength(ValidationConstants.UserNameMaxLength)]
    public string Name { get; set; } = null!;

    public decimal Balance { get; set; }

    public virtual ICollection<Bet> Bets { get; set; }
}
