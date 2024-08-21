namespace Cinema_RepoLearn.Data.Configuration
{
    using Cinema_RepoLearn.Infrastructure.Data.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System.Text.Json;

    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            string path = Path.Combine("bin", "Debug", "net6.0", "Data", "Datasets", "movies.json"); // Bad practice to use absolute path, but in learning app we don't have any choice,
            string dataRead = File.ReadAllText(path);                                                // because migrations don't start the app and cannot find the current directory


            var movies = JsonSerializer.Deserialize<List<Movie>>(dataRead);

            if (movies != null)
            {
                builder
                     .HasData(movies);
            }
        }
    }
}
