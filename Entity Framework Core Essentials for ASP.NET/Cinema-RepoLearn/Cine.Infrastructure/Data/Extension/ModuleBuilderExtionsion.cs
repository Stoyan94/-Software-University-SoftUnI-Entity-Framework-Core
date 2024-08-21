namespace Cinema_RepoLearn.Infrastructure.Data.Extension
{
    using Cine.Infrastructure.Data.Configuration;
    using Cinema_RepoLearn.Data.Configuration;
    using Cinema_RepoLearn.Infrastructure.Data.Model;
    using Microsoft.EntityFrameworkCore;
    public static class ModuleBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CinemaConfiguration());

            modelBuilder.ApplyConfiguration(new HallConfiguration());

            modelBuilder.ApplyConfiguration(new CinemaHallsConfiguration());

            //modelBuilder.ApplyConfiguration(new MovieConfiguration());

            //modelBuilder.ApplyConfiguration(new ScheduleConfiguration());

            //modelBuilder.ApplyConfiguration(new UserConfiguration());

            //modelBuilder.ApplyConfiguration(new TariffConfiguration());

            //modelBuilder.ApplyConfiguration(new SeatConfiguration());

            //modelBuilder.ApplyConfiguration(new TicketConfiguration());
        }
    }
}
