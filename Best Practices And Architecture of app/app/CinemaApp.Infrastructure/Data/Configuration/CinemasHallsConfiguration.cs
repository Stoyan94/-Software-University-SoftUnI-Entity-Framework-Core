using CinemaApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace CinemaApp.Infrastructure.Data.Configuration
{
    public class CinemasHallsConfiguration : IEntityTypeConfiguration<CinemaHall>
    {
        public void Configure(EntityTypeBuilder<CinemaHall> builder)
        {
            string path = Path.Combine("bin", "Debug", "net6.0", "Data", "Datasets", "cinemasHalls.json");
            string data = File.ReadAllText(path);

            var cinemasHalls = JsonSerializer.Deserialize<List<CinemaHall>>(data);

            if (cinemasHalls != null)
            {
                builder
                    .HasData(cinemasHalls);
            }
            else
            {
                throw new ArgumentException($"The file {nameof(cinemasHalls)} is null or empty.");
            }

        }
    }
}
