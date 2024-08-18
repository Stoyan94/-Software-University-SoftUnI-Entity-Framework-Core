using Cinema_RepoLearn.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema_RepoLearn.Data.Configuration
{
    public class TariffConfiguration : IEntityTypeConfiguration<Tariff>
    {
        public void Configure(EntityTypeBuilder<Tariff> builder)
        {
            builder
                .HasData
                (
                    new Tariff()
                    {
                        Id = 1,
                        Name = "Adult",
                        Factor = 1
                    },
                    new Tariff()
                    {
                        Id= 2,
                        Name = "Student",
                        Factor = 0.8m
                    },
                    new Tariff()
                    {
                        Id= 3,
                        Name = "Senior",
                        Factor = 0.7m
                    },
                    new Tariff()
                    {
                        Id= 4,
                        Name = "SoftUni Corporate Discount",
                        Factor = 0.5m
                    }
                );
        }
    }
}
