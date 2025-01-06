using CinemaApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace CinemaApp.Infrastructure.Data.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Data", "Datasets", "users.json");
            string data = File.ReadAllText(path);
            var users = JsonSerializer.Deserialize<List<User>>(data);

            if (users != null)
            {
                builder
                .HasData(users);
            }
        }
    }
}
