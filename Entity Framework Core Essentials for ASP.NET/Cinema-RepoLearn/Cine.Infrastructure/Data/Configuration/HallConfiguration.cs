using Cinema_RepoLearn.Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Cinema_RepoLearn.Data.Configuration
{
    public class HallConfiguration : IEntityTypeConfiguration<Hall>
    {
        public void Configure(EntityTypeBuilder<Hall> builder)
        {
            //builder
            //   .HasData
            //   (
            //       new Hall()
            //       {
            //           Id = 1,
            //           Name = "IMAX Hall 1",
            //           CinemaId = 1
            //       },
            //       new Hall()
            //       {
            //           Id = 2,
            //           Name = "IMAX-5D Hall 1",
            //           CinemaId = 1
            //       },
            //       new Hall()
            //       {
            //           Id = 3,
            //           Name = "3D Hall 1",
            //           CinemaId = 1
            //       },
            //       new Hall()
            //       {
            //           Id = 4,
            //           Name = "VIP Hall",
            //           CinemaId = 2
            //       },
            //       new Hall()
            //       {
            //           Id = 5,
            //           Name = "IMAX Hall 1",
            //           CinemaId = 3
            //       }
            //   );
        }
    }
}
