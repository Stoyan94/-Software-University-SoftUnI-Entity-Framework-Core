namespace CinemaTrain.Data
{
    using CinemaTrain.Data.Model;
    using CinemaTrain.Extension;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class CinemaDbContext : DbContext
    {

        //public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
        //    : base(options)
        //{

        //}

        //public CinemaDbContext()
        //{

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true)
                .AddUserSecrets(typeof(Program).Assembly)
                .Build();

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.
                    UseSqlServer(configuration.GetConnectionString("CinemaConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Seat)
                .WithMany(s => s.Tickets)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Tariff)
                .WithMany(t => t.Tickets)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasIndex(t => t.UserName)
                .IsUnique();

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Cinema> Cinemas { get; set; } = null!; 

        public DbSet<Hall> Halls { get; set; } = null!;

        public DbSet<Movie> Movies { get; set; } = null!;

        public DbSet<Schedule> Schedules { get; set; } = null!;

        public DbSet<Seat> Seats { get; set; } = null!;

        public DbSet<Ticket> Tickets { get; set; } = null!;

        public DbSet<Tariff> Tariffs { get; set; } = null!;
    }
}
