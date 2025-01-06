using CinemaApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaApp.Infrastructure.Data.Configuration
{
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            List<Seat> seats = new List<Seat>();
            int id = 0;

            Random random = new Random();

            int hallsCount = 7;

            for (int i = 1; i <= hallsCount; i++)
            {
                int rows = random.Next(10, 15);
                int seatCount = random.Next(10, 20);

                for (int j = 0; j <= rows + 1; j++)
                {
                    for (int k = 0; k <= seatCount + 1; k++)
                    {
                        id++;
                        seats.Add(new Seat()
                        {
                            Id = id,
                            HallId = i,
                            Row = j,
                            Number = k
                        });
                    }
                }
            }


            builder
                .HasData(seats);
        }
    }
}
