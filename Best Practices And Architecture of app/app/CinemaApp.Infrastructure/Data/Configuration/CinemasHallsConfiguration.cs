using CinemaApp.Infrastructure.Data.Models;
using CinemaApp.Infrastructure.Services;
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
            string data = FileValidationService.ValidateAndReadFile(path);


            var cinemasHalls = JsonSerializer.Deserialize<List<CinemaHall>>(data);
          
                builder
                    .HasData(cinemasHalls);            

        }
    }
}
