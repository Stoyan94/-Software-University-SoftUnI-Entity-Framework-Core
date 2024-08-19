namespace Cinema_RepoLearn.Infrastructure.Data.Model
{
    using System.ComponentModel.DataAnnotations;
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string UserName { get; set; } = null!;

        [Required]
        [MaxLength(64)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(64)]
        public string LastName { get; set; } = null!;

        public ICollection<Ticket> Tickets { get; set; } 
            = new HashSet<Ticket>();
    }
}
