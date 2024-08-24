namespace Cinema_RepoLearn.Data.Configuration
{
    using Cinema_RepoLearn.Infrastructure.Data.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            List<Seat> seats = new List<Seat>();
            int id = 0;
            Random rand = new Random();

            for (int i = 1; i < 8; i++)
            {
                int rows = rand.Next(10, 15);
                int seatCount = rand.Next(10, 20);

                for (int j = 1; j < rows + 1; j++)
                {
                    for (int k = 1; k < rows + 1; k++)
                    {
                        id++;
                        seats.Add(new Seat()
                        {
                            Id = id,
                            HallId = i,
                            Row = j,
                            Number = k,
                        });
                    }
                }
                //089 7950978
            }

            builder.HasData(seats);
        }
    }
}
