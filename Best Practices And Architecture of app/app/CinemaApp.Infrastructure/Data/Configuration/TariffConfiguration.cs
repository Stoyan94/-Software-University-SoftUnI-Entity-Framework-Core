using CinemaApp.Infrastructure.Data.Models;
using CinemaApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace CinemaApp.Infrastructure.Data.Configuration
{
    public class TariffConfiguration : IEntityTypeConfiguration<Tariff>
    {
        public void Configure(EntityTypeBuilder<Tariff> builder)
        {
            string path = Path.Combine("bin", "Debug", "net6.0", "Data", "Datasets", "tariffs.json");
            string data = FileValidationService.ValidateAndReadFile(path);

            var tariffs = JsonSerializer.Deserialize<List<Tariff>>(data);

            builder
                .HasData(tariffs);
        }
    }
}
