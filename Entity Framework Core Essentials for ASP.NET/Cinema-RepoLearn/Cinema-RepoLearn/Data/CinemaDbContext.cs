using Cinema_RepoLearn.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Cinema_RepoLearn.Data
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext()
        {
        }
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
            : base(options) 
        {
            
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=STOYAN;Database=Cinema24;User Id=sa;Password=558955;Trusted_Connection=True;");
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

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Cinema> Cinemas { get; set; } = null!;

        public DbSet<Hall> Halls { get; set; } = null!;

        public DbSet<Film> Films { get; set; } = null!;

        public DbSet<Schedule> Schedules { get; set; } = null!;

        public DbSet<Seat> Seats { get; set; } = null!;

        public DbSet<Ticket> Tickets { get; set; } = null!;

        public DbSet<Tariff> Tariffs { get; set; } = null!;

    }
}
