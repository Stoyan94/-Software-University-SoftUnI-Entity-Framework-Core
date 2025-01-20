using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntitiesConstraints;
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(ArticleTitleMaxLength)]
        public required string Title { get; set; }

        [MaxLength(ArticleContentMaxLength)]
        public required string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        [MaxLength(ArticleAuthorMaxLength)]
        public string? Author { get; set; }

        [ForeignKey(nameof(UserId))]
        public required string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
