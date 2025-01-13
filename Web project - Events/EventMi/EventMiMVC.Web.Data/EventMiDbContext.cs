using EventMiMVC.Web.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EventMiMVC.Web.Data
{
    public class EventMiDbContext : DbContext 
    {
        public EventMiDbContext()
        {
            
        }

        public EventMiDbContext(DbContextOptions<EventMiDbContext> options)
               : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    // optionsBuilder.UseSqlServer("Server=.;Database=EventMiDb;Trusted_Connection=True;");
            //}

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
