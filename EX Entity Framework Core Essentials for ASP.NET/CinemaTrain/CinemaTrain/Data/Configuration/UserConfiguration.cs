using CinemaTrain.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaTrain.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasData(new User()
                {
                    Id = 1,
                    UserName = "Pesho123",
                    FirstName = "Pesho",
                    LastName = "Petrov"
                }, new User()
                {
                    Id = 2,
                    UserName = "Pesho987",
                    FirstName = "Pesho",
                    LastName = "Ivanov"
                }, new User()
                {
                    Id = 3,
                    UserName = "vankata89",
                    FirstName = "Ivan",
                    LastName = "Ivanov"
                }, new User()
                {
                    Id = 4,
                    UserName = "maria12312",
                    FirstName = "Maria",
                    LastName = "Petrova"
                });
        }
    }
}
