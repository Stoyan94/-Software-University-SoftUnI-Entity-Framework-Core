🎬 Main Project: CinemaApp(Presentation Layer)
📄 Program.cs

Role: This is the entry point of the application.
Description: This file starts the application and contains logic for:
Configuring the layers (Infrastructure and Core).
Registering dependency injection.
Launching the application (e.g., ASP.NET Core or console application).

🔧 Example Code:

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructure();
builder.Services.AddCore();
var app = builder.Build();
app.Run();

🧠 Project: CinemaApp.Core(Business Logic Layer)
This project contains contracts (interfaces), models (DTOs), and business logic services.

📁 Contracts Folder
This folder contains interfaces for services.
Interfaces define what methods a service should have.

📄 ICinemaService.cs
Role: Interface for a service that handles cinema-related data.
Methods: Defines methods like:
GetAllCinemas()
GetCinemaById(int id)
AddCinema(CinemaModel cinema)

📁 Models Folder(DTO – Data Transfer Objects)
Description: This folder contains data transfer models that are used to send data between layers.

📄 CinemaModel.cs
Role: A DTO class that holds cinema-related data.
Fields:

Id – Unique identifier of the cinema.
Name – Name of the cinema.
Address – Address of the cinema.
NumberOfHalls – Number of halls in the cinema.

🔧 Example Code:

public class CinemaModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int NumberOfHalls { get; set; }
}

📁 Services Folder
This folder contains the business logic services that execute the core functionality of the application.

📄 CinemaService.cs
Role: Implementation of the business logic for managing cinemas.
Description: This class uses the repository from the Infrastructure layer to read and write data to the database.

Methods:
GetAllCinemas()
GetCinemaById(int id)
AddCinema(CinemaModel cinema)


🔧 Example Code:

public class CinemaService : ICinemaService
{
    private readonly IRepository<Cinema> _cinemaRepository;

    public CinemaService(IRepository<Cinema> cinemaRepository)
    {
        _cinemaRepository = cinemaRepository;
    }

    public IEnumerable<CinemaModel> GetAllCinemas()
    {
        return _cinemaRepository.All().Select(c => new CinemaModel
        {
            Id = c.Id,
            Name = c.Name,
            Address = c.Address,
            NumberOfHalls = c.Halls.Count
        });
    }
}

🏗️ Project: CinemaApp.Infrastructure(Data Access Layer)
This project contains files responsible for database interactions, such as repositories and DbContext classes.

📁 Common Folder
This folder contains common interfaces and implementations used for data access.

📄 IRepository.cs
Role: A generic repository interface for basic CRUD operations.
Description: Defines methods like:
All()
Add(T entity)
SaveChanges()

📄 Repository.cs
Role: The implementation of the generic repository.
Description: Contains logic for performing CRUD operations on the database.
📁 Data Folder
This folder is responsible for managing the database connection and configurations.

📄 CinemaDbContext.cs
Role: The main DbContext class that manages the database connection.
Description: Defines all the tables as DbSet<> properties.

🔧 Example Code:

public class CinemaDbContext : DbContext
{
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Movie> Movies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Fluent API configuration
    }
}

📁 Models Folder(Entity Classes)
This folder contains entity classes that represent database tables.

📄 Cinema.cs
Role: An entity class that represents the Cinemas table in the database.
Fields:
Id – Unique identifier.
Name – Cinema name.
Address – Cinema address.

📄 Movie.cs
Role: An entity class that represents the Movies table in the database.
Fields:
Id
Title
Genre
Duration

📁 Extension Folder
This folder contains extension classes that provide additional configurations and helper methods.

📄 ModelBuilderExtension.cs
Role: Contains Fluent API configurations for entity relationships and constraints.
🔧 Example Code:

public static class ModelBuilderExtension
{
    public static void ApplyConfigurations(this ModelBuilder modelBuilder)
    {
        // Custom configurations
    }
}


📁 Migrations Folder
Role: This folder contains database migrations that describe changes in the database schema over time.



📋 Summary of the Structure:
Folder / File               Layer                                 Role                              Description
Program.cs	                Presentation Layer                    Entry point  	                    Starts the application and configures the layers.
                                                 
ICinemaService.cs	        Business Logic Layer(Core)            Interface for the service   	    Defines methods for managing cinema data.
                                                 
CinemaService.cs	        Business Logic Layer(Core)            Business logic service	        Implements business logic for cinemas.
                                                 
CinemaModel.cs	            Business Logic Layer(Core)            DTO model	                        Used for transferring data between layers.
                                                 
IRepository.cs	            Data Access Layer(Infrastructure)     Repository interface              Defines CRUD methods for interacting with the database.
                                                 
Repository.cs	            Data Access Layer(Infrastructure)     Repository	                    Implements CRUD operations.
                                                 
CinemaDbContext.cs	        Data Access Layer(Infrastructure)     DbContext class                   Manages the connection to the database.
                                                 
Cinema.cs	                Data Access Layer(Infrastructure)     Entity class                      Describes the Cinemas table in the database.
                                                 
ModelBuilderExtension.cs    Data Access Layer(Infrastructure)	  Fluent API configurations	        Contains configurations for entity models.



💡 How Do the Layers Work Together?
The Presentation Layer (CinemaApp) starts the application and interacts with users.
The Business Logic Layer (CinemaApp.Core) processes the user's requests and calls the Data Access Layer.
The Data Access Layer (CinemaApp.Infrastructure) manages the database connection, retrieves or saves data, and returns it to the Business Logic Layer.




Layer	                               References To

Presentation Layer	                 - References CinemaService.cs (Business Logic)

Business Logic Layer (Core)          - References IRepository.cs (Data Access)
                                     - References CinemaDbContext.cs (Data Access)

Data Access Layer (Infrastructure)	 - References Cinema.cs (Data Model)
                                     - References CinemaDbContext.cs (Database Context)
                                     - References ModelBuilderExtension.cs (Schema Configuration)

Repository Layer	                 - References CinemaDbContext.cs (Database Context)

Summary:
The Presentation Layer communicates with the Business Logic Layer (CinemaService.cs) to fetch data or trigger actions (e.g., movie bookings).
The Business Logic Layer references the Data Access Layer to interact with the database.
The Data Access Layer manages models (Cinema.cs) and database schema (ModelBuilderExtension.cs).