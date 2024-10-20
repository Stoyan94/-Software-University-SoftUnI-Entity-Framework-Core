using BlogTraining.Data.Configurations;
using BlogTraining.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogTraining.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext()
        {

        }

        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                string connectionString = ConStrConfiguration.ConnectionString;

                optionsBuilder.UseSqlServer(connectionString);
            }

        }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BlogConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }
}
