using Microsoft.EntityFrameworkCore;

namespace StudentSystem.Data
{
    public class StudentSystemDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=STOYAN;Database=StudentSystemDB;User Id=sa;Password=558955;Trusted_Connection=True;");
            }
        }
    }
}
