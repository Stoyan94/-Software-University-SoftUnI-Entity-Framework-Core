using CinemaApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace CinemaApp.Infrastructure.Data.Configuration
{
    internal class TariffConfiguration : IEntityTypeConfiguration<Tariff>
    {
        public void Configure(EntityTypeBuilder<Tariff> builder)
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Data", "Datasets", "tariffs.json");
            string data = File.ReadAllText(path);
            var tariffs = JsonSerializer.Deserialize<List<Tariff>>(data);

            if (tariffs != null)
            {
                builder
                .HasData(tariffs);
            }
        }
    }
}
