using CinemaApp.Infrastructure.Data.Models;
using CinemaApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace CinemaApp.Infrastructure.Data.Configuration
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            string path = Path.Combine("bin", "Debug", "net6.0", "Data", "Datasets", "schedules.json");
            string data = FileValidationService.ValidateAndReadFile(path);

            var schedules = JsonSerializer.Deserialize<List<Schedule>>(data);

            builder
                .HasData(schedules);
        }
    }
}
