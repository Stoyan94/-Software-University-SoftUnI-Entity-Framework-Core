using CinemaApp.Infrastructure.Data.Models;
using CinemaApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace CinemaApp.Infrastructure.Data.Configuration
{
    public class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            string path = Path.Combine("bin", "Debug", "net6.0", "Data", "Datasets", "cinemas.json");
            string data = FileValidationService.ValidateAndReadFile(path);

            var cinemas = JsonSerializer.Deserialize<List<Cinema>>(data);

             builder
               .HasData(cinemas);
           
        }
    }
}
