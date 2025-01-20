using Microsoft.AspNetCore.Identity;

namespace Blog.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
