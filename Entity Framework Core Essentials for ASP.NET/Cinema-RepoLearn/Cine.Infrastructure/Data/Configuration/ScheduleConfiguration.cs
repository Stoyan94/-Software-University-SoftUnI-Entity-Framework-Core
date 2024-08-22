namespace Cinema_RepoLearn.Data.Configuration
{
    using Cinema_RepoLearn.Infrastructure.Data.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System.Text.Json;

    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            string path = Path.Combine("bin", "Debug", "net6.0", "Data", "Datasets", "schedules.json"); // Bad practice to use absolute path, but in learning app we don't have any choice,
            string dataRead = File.ReadAllText(path);                                                // because migrations don't start the app and cannot find the current directory


            var schedules = JsonSerializer.Deserialize<List<Schedule>>(dataRead);

            if (schedules != null)
            {
                builder
                     .HasData(schedules);
            }
        }
    }
}
