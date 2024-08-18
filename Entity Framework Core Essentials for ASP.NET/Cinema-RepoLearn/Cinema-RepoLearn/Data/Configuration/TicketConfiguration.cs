using Cinema_RepoLearn.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema_RepoLearn.Data.Configuration
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder
                .HasData
                (
                   new Ticket()
                   {
                       Id = 1,
                       UserId = 1,
                       BasePrice = 20,

                   },
                   new Ticket()
                   {

                   }, 
                   new Ticket()
                   {

                   }, 
                   new Ticket()
                   {

                   },
                   new Ticket()
                   {

                   }
                );
        }
    }
}
