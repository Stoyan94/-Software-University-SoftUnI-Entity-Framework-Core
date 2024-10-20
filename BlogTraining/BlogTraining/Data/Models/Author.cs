using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogTraining.Data.Models
{
    [Table("Authors", Schema = "blg")]
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required]
        [MaxLength(100)]
        public string AuthorName { get; set; } = null!;

        public int? BlogId { get; set; }

        [ForeignKey(nameof(BlogId))]
        public Blog? Blog { get; set; }
    }
}
