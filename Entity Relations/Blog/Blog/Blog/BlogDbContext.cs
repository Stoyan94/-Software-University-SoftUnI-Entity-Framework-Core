using BlogDemo.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BlogDemo;

public class BlogDbContext : DbContext
{
    public BlogDbContext()
    {
            
    }

    public BlogDbContext(DbContextOptions<BlogDbContext> options) 
        : base(options) 
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BlogConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       if (optionsBuilder.IsConfigured == false)
       {
            string connectionString = "Server=STOYAN;Database=BlogDb;User Id=sa;Password=558955;Trusted_Connection=True;";

            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    public DbSet<Blog> Blogs { get; set; }
}

