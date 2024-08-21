using Cine.Infrastructure.Data.Model;
using Cinema_RepoLearn.Infrastructure.Data.Extension;
using Cinema_RepoLearn.Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Cinema_RepoLearn.Infrastructure.Data
{
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
            //IConfiguration configuration = new ConfigurationBuilder()
            //     .AddJsonFile("appsettings.json", true)
            //     .AddUserSecrets(Assembly.GetEntryAssembly())
            //     .Build();

            if (optionsBuilder.IsConfigured == false )
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

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            modelBuilder.Entity<CinemaHall>()
                .HasKey(pk => new { pk.CinemaId, pk.HallId });

            //modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Cinema> Cinemas { get; set; } = null!;

        public DbSet<Hall> Halls { get; set; } = null!;

        public DbSet<Movie> Movies { get; set; } = null!;

        public DbSet<Schedule> Schedules { get; set; } = null!;

        public DbSet<Seat> Seats { get; set; } = null!;

        public DbSet<Ticket> Tickets { get; set; } = null!;

        public DbSet<Tariff> Tariffs { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

    }
}
