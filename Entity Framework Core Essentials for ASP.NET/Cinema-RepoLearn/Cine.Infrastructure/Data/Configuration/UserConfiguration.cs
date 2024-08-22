namespace Cinema_RepoLearn.Data.Configuration
{
    using Cinema_RepoLearn.Infrastructure.Data.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System.Text.Json;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            string path = Path.Combine("bin", "Debug", "net6.0", "Data", "Datasets", "users.json"); // Bad practice to use absolute path, but in learning app we don't have any choice,
            string dataRead = File.ReadAllText(path);                                                // because migrations don't start the app and cannot find the current directory


            var users = JsonSerializer.Deserialize<List<User>>(dataRead);

            if (users != null)
            {
                builder
                     .HasData(users);
            }
        }
    }
}
