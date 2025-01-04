using CinemaTrain.Data.Configuration;
using CinemaTrain.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace CinemaTrain.Extension
{
    public static class ModulBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CinemaConfiguration());

            modelBuilder.ApplyConfiguration(new HallConfiguration());

            modelBuilder.ApplyConfiguration(new MovieConfiguration());

            modelBuilder.ApplyConfiguration(new ScheduleConfiguration());

            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.ApplyConfiguration(new TariffConfiguration());

            modelBuilder.ApplyConfiguration(new SeatConfiguration());

            modelBuilder.ApplyConfiguration(new TicketConfiguration());
        }
    }
}
