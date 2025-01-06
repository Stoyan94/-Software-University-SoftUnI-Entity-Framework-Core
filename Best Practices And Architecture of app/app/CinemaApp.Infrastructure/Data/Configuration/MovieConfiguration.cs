using CinemaApp.Infrastructure.Data.Models;
using CinemaApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace CinemaApp.Infrastructure.Data.Configuration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            string path = Path.Combine("bin", "Debug", "net6.0", "Data", "Datasets", "movies.json");
            string data = FileValidationService.ValidateAndReadFile(path);

            var movies = JsonSerializer.Deserialize<List<Movie>>(data);

            builder
                .HasData(movies);
        }
    }
}
