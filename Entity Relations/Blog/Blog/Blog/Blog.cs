using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogDemo
{
    [Table("Blogs", Schema = "blg")]
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("BlogName", TypeName = "NVARCHAR")]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        [Column(TypeName = "NVARCHAR")]
        public string? Description { get; set; }


        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
