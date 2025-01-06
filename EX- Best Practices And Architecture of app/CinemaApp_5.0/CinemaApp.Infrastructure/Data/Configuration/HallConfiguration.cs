using CinemaApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace CinemaApp.Infrastructure.Data.Configuration
{
    public class HallConfiguration : IEntityTypeConfiguration<Hall>
    {
        public void Configure(EntityTypeBuilder<Hall> builder)
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Data", "Datasets", "halls.json");
            string data = File.ReadAllText(path);
            var halls = JsonSerializer.Deserialize<List<Hall>>(data);

            if (halls != null)
            {
                builder
                .HasData(halls);
            }
        }
    }
}
