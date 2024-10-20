using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogTraining.Data.Models
{
    [Table("Blogs", Schema = "blg")]
    [Index(nameof(Name), IsUnique = true, Name = "IX_Blogs_Name_Unique")]
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("BlogName", TypeName = "NVARCHAR")]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        [Column(TypeName ="NVARCHAR")]
        public string? Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public int AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; } = null!;

        public List<Post> Posts { get; set; } = new List<Post>();
    }
}