using System.ComponentModel.DataAnnotations;

namespace CinemaTrain.Data.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(94)]
        public string UserName { get; set; } = null!;

        [Required]
        [StringLength(64)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(64)]
        public string LastName { get; set; } = null!;

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();


    }
}
