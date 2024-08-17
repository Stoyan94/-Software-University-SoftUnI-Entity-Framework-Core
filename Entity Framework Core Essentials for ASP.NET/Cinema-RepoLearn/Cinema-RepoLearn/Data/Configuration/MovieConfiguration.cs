using Cinema_RepoLearn.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema_RepoLearn.Data.Configuration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder
                .HasData
                (
                    new Movie()
                    {
                        Id = 1,
                        Title = "Snatch"
                    },
                    new Movie()
                    {
                        Id = 2,
                        Title = "Lock, Stock and Two Smoking Barrels"
                    },
                    new Movie()
                    {
                        Id = 3,
                        Title = "Rock n Rolla"
                    }
                );
        }
    }
}
