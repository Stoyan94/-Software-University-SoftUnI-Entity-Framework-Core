using CinemaApp.Infrastructure.Data.Models;
using CinemaApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace CinemaApp.Infrastructure.Data.Configuration
{
    public class HallConfiguration : IEntityTypeConfiguration<Hall>
    {
        public void Configure(EntityTypeBuilder<Hall> builder)
        {
            string path = Path.Combine("bin", "Debug", "net6.0", "Data", "Datasets", "halls.json");
            string data = FileValidationService.ValidateAndReadFile(path);


            var halls = JsonSerializer.Deserialize<List<Hall>>(data);
          
            builder
                .HasData(halls);
            

        }
    }
}
