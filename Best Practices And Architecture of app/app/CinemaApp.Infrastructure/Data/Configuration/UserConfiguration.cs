using CinemaApp.Infrastructure.Data.Models;
using CinemaApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace CinemaApp.Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            string path = Path.Combine("bin", "Debug", "net6.0", "Data", "Datasets", "users.json");            
            string data = FileValidationService.ValidateAndReadFile(path);

            var users = JsonSerializer.Deserialize<List<Hall>>(data);

            builder
                .HasData(users);
        }
    }
}
