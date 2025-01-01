
this is in folder with name Extension, and why the class and the methods need to be static
public static class ModulBuilderExtension
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CinemaConfiguration());

        modelBuilder.ApplyConfiguration(new HallConfiguration());
    }
}


this is in folder with name Configuration 

public class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
{
    public void Configure(EntityTypeBuilder<Cinema> builder)
    {
        builder
            .HasData(new Cinema()
            {
                Id = 1,
                Name = "Arena Mladost",
                Address = "Mladost 4, Sofia"
            },
            new Cinema()
            {
                Id = 2,
                Name = "Arena Stara Zagora",
                Address = "Stara Zagora Mall"
            },
            new Cinema()
            {

                Id = 3,
                Name = "Cinema City",
                Address = "Mall of Sofia"
            });
    }
}

and call the method seed here 

 protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Ticket>()
        .HasOne(t => t.Seat)
        .WithMany(s => s.Tickets)
        .OnDelete(DeleteBehavior.NoAction);

    modelBuilder.Entity<Ticket>()
        .HasOne(t => t.Tariff)
        .WithMany(t => t.Tickets)
        .OnDelete(DeleteBehavior.NoAction);

    modelBuilder.Seed();

    base.OnModelCreating(modelBuilder);
}


Purpose and Logic

This setup is part of an Entity Framework Core (EF Core) project. The goal is to configure the database model 
    and seed some initial data into the database during the OnModelCreating phase of the DbContext.

1. Why is the ModulBuilderExtension class static, and why are its methods static?

Static Class
A static class is used when you don't need to instantiate the class to use its members.
In this case, ModulBuilderExtension is designed to hold an extension method for the ModelBuilder class. 
    It does not require maintaining state or creating objects, so it is appropriate to make it static.

Static Method
The Seed method is static because it is an extension method. 
    Extension methods allow you to add new functionality to an existing class (ModelBuilder) without modifying its source code.
The this keyword before the first parameter (this ModelBuilder modelBuilder) specifies that this method is an extension of the ModelBuilder class. 
    As a result, you can call it directly on any ModelBuilder instance, as shown in the OnModelCreating method.

2. ModulBuilderExtension.Seed
This method applies configurations defined elsewhere. It uses the ApplyConfiguration method of the ModelBuilder class to apply the database configuration for specific entity types (e.g., Cinema and Hall).

Key Points:

Separation of Concerns:
The actual configuration logic for entities (CinemaConfiguration, HallConfiguration) is kept in separate classes. 
    This improves maintainability and readability.

Extensibility:
Adding new entity configurations requires only adding a new ApplyConfiguration call in the Seed method,
    without modifying the core OnModelCreating method.


3. CinemaConfiguration
This class implements the IEntityTypeConfiguration<T> interface, which is used to define configuration for the Cinema entity. The Configure method specifies how the Cinema table should be seeded with initial data.

Key Points:
HasData Method: Used to seed predefined records into the database. T
    his ensures that when the database is created or updated, the specified data is inserted if it does not already exist.
Example Data:
Three Cinema records are seeded with Id, Name, and Address.

4. OnModelCreating
This method is called by the EF Core framework when the DbContext is being created. 
    It defines the overall model configuration.

Key Steps in OnModelCreating:

Entity Relationships:
Defines relationships between the Ticket, Seat, and Tariff entities, specifying the delete behavior as NoAction.

Seed Data:
The Seed extension method is called to apply configurations and seed data for all configured entities.

Call base.OnModelCreating:
Invokes the base implementation to ensure any other internal EF Core logic is executed.
Advantages of This Approach

Modularity:
Configuration and seeding logic for each entity are encapsulated in their own classes, making the codebase easier to manage.

Reusability:
The Seed method can be reused for other similar configurations.

Extension Methods:
Using extension methods enhances readability and makes the OnModelCreating method cleaner.

Scalability:
New entities can be added to the Seed method with minimal impact on the existing code.

Execution Flow

The DbContext is initialized.
OnModelCreating is invoked.
Entity relationships are configured.
The Seed method is called:
Applies CinemaConfiguration and HallConfiguration.
Seeds data for the Cinema table.
The database is created/updated with the configurations and seeded data.