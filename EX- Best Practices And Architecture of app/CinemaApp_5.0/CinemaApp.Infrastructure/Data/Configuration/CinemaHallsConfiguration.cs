using CinemaApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace CinemaApp.Infrastructure.Data.Configuration
{
    public class CinemaHallsConfiguration : IEntityTypeConfiguration<CinemaHall>
    {
        public void Configure(EntityTypeBuilder<CinemaHall> builder)
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Data", "Datasets", "cinemasHalls.json");
            string data = File.ReadAllText(path);
            var cinemasHalls = JsonSerializer.Deserialize<List<CinemaHall>>(data);

            if (cinemasHalls != null)
            {
                builder
                .HasData(cinemasHalls);
            }
        }
    }
}
