using CinemaApp.Infrastructure.Data.Extension;
using CinemaApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace CinemaApp.Infrastructure.Data
{
    public class CinemaDbContext : DbContext
    {
        //public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
        //    : base(options)
        //{
        //}

        public CinemaDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true)
                .AddUserSecrets("69c38dfb-32b9-44a6-82ac-cd4f0b38e8fc")
                .Build();

            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("CinemaConnection"));
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
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<CinemaHall>()
                .HasKey(c => new { c.CinemaId, c.HallId });

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<Hall> Halls { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Tariff> Tariffs { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
