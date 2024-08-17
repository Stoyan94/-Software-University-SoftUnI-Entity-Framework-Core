using Cinema_RepoLearn.Data.Configuration;
using Cinema_RepoLearn.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Cinema_RepoLearn.Data.Extension
{
    public static class ModuleBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CinemaConfiguration());

            modelBuilder.ApplyConfiguration(new HallConfiguration());

            modelBuilder.ApplyConfiguration(new MovieConfiguration());

            modelBuilder.ApplyConfiguration(new ScheduleConfiguration());
        }
    }
}
