using CinemaApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace CinemaApp.Infrastructure.Data.Configuration
{
    internal class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Data", "Datasets", "movies.json");
            string data = File.ReadAllText(path);
            var movies = JsonSerializer.Deserialize<List<Movie>>(data);

            if (movies != null)
            {
                builder
                .HasData(movies);
            }
        }
    }
}
