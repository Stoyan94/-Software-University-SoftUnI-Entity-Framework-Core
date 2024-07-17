using Microsoft.EntityFrameworkCore;
using StudentSystem.Data.Models;

namespace StudentSystem.Data
{
    public class StudentSystemDbContext : DbContext
    {
        public StudentSystemDbContext()
        {
                
        }

        public StudentSystemDbContext(DbContextOptions options) 
            :base(options)
        {
            
        }

        public virtual DbSet<Resource> Resources { get; set; } = null!;

        public virtual DbSet<Course> Courses { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=STOYAN;Database=StudentSystemDB;User Id=sa;Password=558955;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
        }
    }
}
