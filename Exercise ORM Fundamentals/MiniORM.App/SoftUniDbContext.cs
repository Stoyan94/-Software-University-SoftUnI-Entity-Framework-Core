
using MiniORM.App.Entities;

namespace MiniORM.App
{
    public class SoftUniDbContext : DbContext
    {
        public SoftUniDbContext(string connectionStirng) 
            : base(connectionStirng)
        {
        }

        public DbSet<Department> Departments { get; } = null!;
        public DbSet<Employee> Employees { get; } = null!;
    }
}
