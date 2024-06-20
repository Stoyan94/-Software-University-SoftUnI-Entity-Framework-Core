using Microsoft.EntityFrameworkCore;

namespace EF_Demo
{
    public class SoftUniDbContext : DbContext
    {
        public SoftUniDbContext()
        {
            
        }

        public SoftUniDbContext(DbContextOptions<SoftUniDbContext> options)
            : base(options) 
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           if (optionsBuilder.IsConfigured == false)
           {
                optionsBuilder.UseSqlServer("Server=STOYAN;Database=SoftUni;User Id=sa;Password=558955;Trusted_Connection=True;");
           }
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }
    }
}
