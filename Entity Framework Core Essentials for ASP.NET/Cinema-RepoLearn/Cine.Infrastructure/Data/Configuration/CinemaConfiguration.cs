namespace Cinema_RepoLearn.Data.Configuration
{
    using Cinema_RepoLearn.Infrastructure.Data.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System.Reflection.Emit;
    public class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder
               .HasData(new Cinema()
               {
                   Id = 1,
                   Name = "Arena Mladost",
                   Address = "Mladost 4, Sofia"
               },
               new Cinema()
               {
                   Id = 2,
                   Name = "Arena Stara Zagora",
                   Address = "Stara Zagora Mall"
               },
               new Cinema()
               {
                   Id = 3,
                   Name = "Ciname City",
                   Address = "Mall of Sofia"
               }
               );
        }
    }
}
