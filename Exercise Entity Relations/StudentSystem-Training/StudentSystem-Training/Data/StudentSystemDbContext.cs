using Microsoft.EntityFrameworkCore;
using StudentSystem_Training.Data.Configurations;
using StudentSystem_Training.Data.Models;

namespace StudentSystem_Training.Data
{
    public class StudentSystemDbContext : DbContext
    {
        public StudentSystemDbContext()
        {
            
        }

        public StudentSystemDbContext(DbContextOptions<StudentSystemDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {          
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(StrConnConfiguration.ConnectionString);
            }
        }

        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Resource> Resources { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Homework> Homeworks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourses>()
                .HasKey(sc => new {sc.StudentId, sc.CourseId});

            base.OnModelCreating(modelBuilder);
        }
    }
}
