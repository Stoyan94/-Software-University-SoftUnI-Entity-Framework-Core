using System.ComponentModel.DataAnnotations;

namespace Boardgames.Data.Models
{
    using static DataConstraints;
    public class Creator
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(CreatorFirstNameMaxLenght)]
        public string FirstName { get; set; } = null!;

        [MaxLength(CreatorLastNameMaxLenght)]
        public string LastName { get; set; } = null!;

        // TODO: Add nav prop
    }
}
