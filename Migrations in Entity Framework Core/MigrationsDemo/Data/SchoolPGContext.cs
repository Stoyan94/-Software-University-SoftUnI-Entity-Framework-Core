using Microsoft.EntityFrameworkCore;
using MigrationsDemo.Models;

namespace MigrationsDemo.Data
{
    public class SchoolPGContext : DbContext
    {
        public DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder
                .UseNpgsql("Host=localhost;Database=school_db;User Id=postgres;Password=159159");
        
        }
    }
}
