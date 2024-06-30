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
        modelBuilder.Entity<Blog>()
            .HasKey(b => b.BlogId);

        modelBuilder.Entity<Blog>()
            .ToTable("Blogs", "blg");

        modelBuilder.Entity<Blog>()
            .Property(b => b.Name)
            .HasColumnName("BlogName")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<Blog>()
            .Property(b => b.Description)
            .HasColumnName("Description")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(500);

        modelBuilder.Entity<Blog>()
            .Property(b => b.Created)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Blog>()
            .Property(b => b.LastUpdated)
            .ValueGeneratedOnAddOrUpdate();


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

