using CinemaApp.Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Infrastructure.Data.Extension
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CinemaConfiguration());
            modelBuilder.ApplyConfiguration(new HallConfiguration());
            modelBuilder.ApplyConfiguration(new CinemasHallsConfiguration());

            //modelBuilder.ApplyConfiguration(new UserConfiguration());
            
        }
    }
}
