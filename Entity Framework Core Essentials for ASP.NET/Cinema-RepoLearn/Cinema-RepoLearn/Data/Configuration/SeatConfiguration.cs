using Cinema_RepoLearn.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema_RepoLearn.Data.Configuration
{
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder
                .HasData
                (
                    new Seat
                    {
                        Id = 1,
                        HallId = 1,
                        Row = 1,
                        Number = 1
                    },
                    new Seat
                    {
                        Id = 2,
                        HallId = 1,
                        Row = 1,
                        Number = 2
                    },
                    new Seat
                    {
                        Id = 3,
                        HallId = 1,
                        Row = 1,
                        Number = 3
                    },
                    new Seat
                    {
                        Id = 4,
                        HallId = 1,
                        Row = 2,
                        Number = 1
                    },
                    new Seat
                    {
                        Id = 5,
                        HallId = 1,
                        Row = 2,
                        Number = 2
                    },
                    new Seat
                    {
                        Id = 6,
                        HallId = 1,
                        Row = 2,
                        Number = 3
                    },
                    new Seat
                    {
                        Id = 7,
                        HallId = 1,
                        Row = 3,
                        Number = 1
                    },
                    new Seat
                    {
                        Id = 8,
                        HallId = 1,
                        Row = 3,
                        Number = 2
                    },
                    new Seat
                    {
                        Id = 9,
                        HallId = 1,
                        Row = 3,
                        Number = 3
                    }
                );
        }
    }
}
