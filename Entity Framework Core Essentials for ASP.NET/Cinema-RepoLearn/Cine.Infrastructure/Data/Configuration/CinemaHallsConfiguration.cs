using Cine.Infrastructure.Data.Model;
using Cinema_RepoLearn.Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Cine.Infrastructure.Data.Configuration
{
    public class CinemaHallsConfiguration : IEntityTypeConfiguration<CinemaHall>
    {
        public void Configure(EntityTypeBuilder<CinemaHall> builder)
        {
            string path = Path.Combine("bin", "Debug", "net6.0", "Data", "Datasets", "cinemasHalls.json"); // Bad practice to use absolute path, but in learning app we don't have any choice,
            string dataRead = File.ReadAllText(path);                                                // because migrations don't start the app and cannot find the current directory


            var cinemasHalls = JsonSerializer.Deserialize<List<CinemaHall>>(dataRead);

            if (cinemasHalls != null)
            {
                builder
                     .HasData(cinemasHalls);
            }
        }
    }
}
