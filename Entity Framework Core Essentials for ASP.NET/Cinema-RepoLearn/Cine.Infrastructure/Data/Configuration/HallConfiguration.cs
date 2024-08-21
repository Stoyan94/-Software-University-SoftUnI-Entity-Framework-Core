using Cinema_RepoLearn.Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.IO;
using System.Reflection.Emit;
using System.Text.Json;

namespace Cinema_RepoLearn.Data.Configuration
{
    public class HallConfiguration : IEntityTypeConfiguration<Hall>
    {
        public void Configure(EntityTypeBuilder<Hall> builder)
        {
            string path = Path.Combine("bin", "Debug", "net6.0", "Data", "Datasets", "halls.json"); // Bad practice to use absolute path, but in learning app we don't have any choice,
            string dataRead = File.ReadAllText(path);                                                // because migrations don't start the app and cannot find the current directory


            var halls = JsonSerializer.Deserialize<List<Hall>>(dataRead);

            if (halls != null)
            {
                builder
                     .HasData(halls);
            }
        }
    }
}
