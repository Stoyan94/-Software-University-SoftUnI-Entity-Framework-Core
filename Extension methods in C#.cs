In the context of Entity Framework Core (EF Core), extension methods are widely used to extend the capabilities 
of EF Core's DbContext, IQueryable, and other types. They enable developers to add custom functionalities, 
improve code readability, and promote code reuse.

Common Usage in EF Core:

Custom Query Extensions: Add custom query methods to IQueryable or DbSet.
Model Configuration: Add extension methods to configure models in the DbContext.
Seeding Data: Create extension methods for seeding data into the database.

Example 1: Custom Query Extension
create a custom extension method to filter active entities from a DbSet.

Define the Extension Method:

using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MyApp.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<T> WhereActive<T>(this IQueryable<T> query) where T : class, IActiveEntity
        {
            return query.Where(entity => entity.IsActive);
        }
    }

    public interface IActiveEntity
    {
        bool IsActive { get; set; }
    }
}
Using the Extension Method:
Assume you have an Entity class that implements IActiveEntity.


public class Entity : IActiveEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}

And your DbContext class:

public class MyDbContext : DbContext
{
    public DbSet<Entity> Entities { get; set; }
}
Now,i can use the WhereActive extension method:


using MyApp.Extensions;
using System.Linq;

class Program
{
    static void Main()
    {
        using (var context = new MyDbContext())
        {
            var activeEntities = context.Entities.WhereActive().ToList();

            foreach (var entity in activeEntities)
            {
                Console.WriteLine(entity.Name);
            }
        }
    }
}


Example 2: Model Configuration Extension
Create an extension method to configure a model in the OnModelCreating method of DbContext.

Define the Extension Method:

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyApp.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ConfigureEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
            });
        }
    }
}


Using the Extension Method in DbContext:

public class MyDbContext : DbContext
{
    public DbSet<Entity> Entities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureEntity();
    }
}


Example 3: Seeding Data Extension
Create an extension method to seed initial data into the database.

Define the Extension Method:

using Microsoft.EntityFrameworkCore;

namespace MyApp.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entity>().HasData(
                new Entity { Id = 1, Name = "Entity1", IsActive = true },
                new Entity { Id = 2, Name = "Entity2", IsActive = false }
            );
        }
    }
}


Using the Extension Method in DbContext:

public class MyDbContext : DbContext
{
    public DbSet<Entity> Entities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
    }
}
Summary
Extension methods in EF Core are useful for:

Custom Queries: Creating reusable query methods.
Model Configuration: Simplifying and organizing model configuration.
Seeding Data: Organizing and maintaining seed data logic.
These examples illustrate how to leverage extension methods to keep your EF Core code clean, maintainable, and reusable.