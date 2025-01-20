using Blog.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class ArticleController : Controller
    {
        private List<Article> articles;

        public ArticleController()
        {
            articles = new List<Article> 
            {
                new Article
                {
                    Title = "What is a Full Stack Developer?",
                    CreatedOn = new DateTime(2024, 10 ,5),
                    Content = "A Full Stack Developer works on both the front-end and back-end of applications. " +
                              "They handle UI design with HTML, CSS, and JavaScript and server-side logic with languages like C# or Python. " +
                              "This versatility makes them valuable for bridging communication between teams. Their skills are essential for end-to-end development. " +
                              "Full Stack roles are in high demand due to their flexibility.",
                    UserId = "64fc2377-b1b7-4b84-b540-78fe85585741"
                },
                new Article
                {
                    Title = "Why Learn C# in 2025?",
                    CreatedOn = new DateTime(2024, 10 ,5),
                    Content = "C# remains a top choice for developers due to its versatility and .NET framework support. " +
                              "It’s widely used in web development, game development with Unity, and enterprise applications. " +
                              "The language offers strong performance and a large community. Learning C# can boost career opportunities, especially in backend and Full Stack roles. " +
                              "It’s beginner-friendly yet powerful for advanced needs.",
                    UserId = "64fc2377-b1b7-4b84-b540-78fe85585741"
                },
                new Article
                {
                    Title = "The Importance of Clean Code",
                    CreatedOn = new DateTime(2024, 10 ,6),
                    Content = "Clean code is essential for maintainable and scalable software. " +
                              "It improves readability, making it easier for teams to collaborate and debug. " +
                              "Practices like using descriptive names and avoiding code duplication are key. " +
                              "Clean code reduces technical debt and saves time in the long run. It’s not just about writing code; it’s about writing code that lasts.",
                    UserId = "d9cd77bf-c99b-4a51-9c28-e48003a63bcb"
                },
                new Article
                {
                    Title = "SQL for Developers: A Must-Have Skill",
                    CreatedOn = new DateTime(2024, 10 ,9),
                    Content = "SQL is crucial for interacting with databases in almost any application. " +
                              "It allows developers to store, retrieve, and manipulate data efficiently. " +
                              "Mastering SQL enhances productivity and enables dynamic web applications. " +
                              "Knowledge of SQL is foundational for backend and Full Stack developers. " +
                              "It’s a timeless skill with endless applications in the tech world.",
                    UserId = "d9cd77bf-c99b-4a51-9c28-e48003a63bcb"
                }
            };
        }
        public IActionResult Index()
        {
            return View(articles);
        }
    }
}
