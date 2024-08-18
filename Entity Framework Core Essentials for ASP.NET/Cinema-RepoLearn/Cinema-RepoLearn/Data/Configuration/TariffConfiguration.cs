using Cinema_RepoLearn.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema_RepoLearn.Data.Configuration
{
    public class TariffConfiguration : IEntityTypeConfiguration<Tariff>
    {
        public void Configure(EntityTypeBuilder<Tariff> builder)
        {
            //builder
            //    .HasData
            //    (
            //        new Tariff()
            //        {
            //            Id = 1,
            //            Name = "Adult",

            //        },
            //        new Tariff()
            //        {

            //        },
            //        new Tariff()
            //        {

            //        }
            //    );
        }
    }
}
