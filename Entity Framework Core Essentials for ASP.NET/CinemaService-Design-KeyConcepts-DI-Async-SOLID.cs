ICinemaService interface

public interface ICinemaService
{
    Task AddCinemaAsync(CinemaModel model);
}
interface ICinemaService : Defines a contract for the cinema service.
    Interfaces are used to define methods without implementing them, ensuring the concrete class will follow the contract.
Task AddCinemaAsync(CinemaModel model): A method signature that specifies an asynchronous operation for adding a cinema. 
    It takes a CinemaModel as input and returns a Task (indicating the asynchronous nature of the operation).


CinemaService class

public class CinemaService : ICinemaService
{
    private readonly IRepository repo;

    public CinemaService(IRepository _repo)
    {
        repo = _repo;
    }
    public async Task AddCinemaAsync(CinemaModel model)
    {
        if (repo.AllReadOnly<Cinema>().Any(c => c.Name == model.Name))
        {
            throw new ArgumentException("Cinema already exist");
        }

        Cinema cinema = new Cinema()
        {
            Address = model.Address,
            Name = model.Name,
        };

        await repo.AddAsync(cinema);
        await repo.SaveChangesAsync();
    }
}
1. public class CinemaService : ICinemaService
The CinemaService class implements the ICinemaService interface, meaning it provides a concrete implementation of the methods declared in the interface.

2. private readonly IRepository repo
Declares a private, read - only dependency on an IRepository. 
    This is an abstraction for accessing the data layer, enabling operations like fetching, adding, or saving data.

3. public CinemaService(IRepository _repo)
A constructor that uses dependency injection to initialize the repo field. 
    The _repo parameter is provided by the dependency injection container or manually.

4. AddCinemaAsync implementation

if (repo.AllReadOnly<Cinema>().Any(c => c.Name == model.Name)):
Fetches all cinemas in a read-only manner from the repository.
Checks if any existing cinema has the same Name as the input model.Name.

throw new ArgumentException("Cinema already exist");:
If a cinema with the same name exists, it throws an exception to prevent duplicates.

Cinema cinema = new Cinema() { ...}:
Creates a new instance of the Cinema class and maps Address and Name properties from the CinemaModel.

await repo.AddAsync(cinema):
Adds the newly created Cinema object to the repository asynchronously.

await repo.SaveChangesAsync():
Persists the changes to the database asynchronously.

CinemaModel class

public class CinemaModel
{
    public CinemaModel(string name, string address)
    {
        Name = name;
        Address = address;
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
}
1. public class CinemaModel
A data model used to transfer information about a cinema between layers in the application.

2. public CinemaModel(string name, string address)
A constructor that initializes the Name and Address properties when creating a new CinemaModel instance.

3. public int Id { get; set; }
Represents the unique identifier for a cinema. It's commonly used as a primary key in the database.
4. public string Name { get; set; } = null!;
Represents the name of the cinema. The = null! ensures that nullable reference type warnings are suppressed (requires the property to be set).

5. public string Address { get; set; } = null!;
Represents the address of the cinema.


Key Concepts in the Code

Dependency Injection:
The IRepository is injected into the CinemaService, promoting loose coupling and easier testing.
Asynchronous Programming:

Methods like AddCinemaAsync are marked async and use await for database operations to improve application responsiveness.

Validation Logic:
The service checks for duplicate cinemas using a LINQ query before adding a new one.

Mapping Models:
Maps CinemaModel to the domain Cinema entity to maintain separation of concerns.

Exception Handling:
Throws an ArgumentException if a cinema with the same name already exists.
This design follows the SOLID principles, particularly interface segregation and dependency inversion. 
    It promotes scalability and testability.




                              Key Concepts

Interface Design

The use of ICinemaService as a contract that defines the available methods.

Benefits include abstraction and easier testing, as the implementation can be swapped without affecting the code that depends on the interface.


Dependency Injection

The IRepository is injected into CinemaService through its constructor.

This pattern decouples the service from the specific implementation of the repository, improving flexibility and testability.


Asynchronous Programming

The method AddCinemaAsync is asynchronous, utilizing Task and await to handle database operations without blocking the main thread.

This improves performance and scalability, especially in web applications.


Validation Logic

The use of LINQ (repo.AllReadOnly<Cinema>().Any) ensures that no duplicate cinemas are added.

Throwing an exception (ArgumentException) enforces data integrity and communicates errors effectively.


Model Mapping

The CinemaModel is used as a Data Transfer Object (DTO) to encapsulate the input data.

It is mapped to the domain entity Cinema before being saved to the database.

This separation of models ensures clear boundaries between layers in the application.


Exception Handling

If a cinema with the same name already exists, an exception is thrown.

This prevents invalid operations and provides feedback to the user or calling code.


SOLID Principles

Interface Segregation: The ICinemaService provides a minimal, focused contract.

Dependency Inversion: The CinemaService depends on the abstraction IRepository, not a concrete implementation.