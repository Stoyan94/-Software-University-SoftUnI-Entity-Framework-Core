1.Data Layer
🛠 Role:
The Data Layer handles communication with the database. It is responsible for storing, retrieving, updating, and deleting data.

🔑 What it should contain:
Entity Framework DbContext (or another ORM for database interaction).
Database models – classes that represent tables in the database.
Repositories – classes that manage data and contain CRUD operations (Create, Read, Update, Delete).

📋 Example (in C#):

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("YourConnectionStringHere");
    }
}

📝 Example of a Repository:

public class UserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public User GetUserById(int id)
    {
        return _context.Users.Find(id);
    }
}

🧠 2.Domain Models
🛠 Role:
The Domain Models represent the real-world objects in your application. They define the structure of the data that will be stored and processed.

🔑 What it should contain:
Properties that define the data structure.
Validation attributes (e.g., [Required], [MaxLength]).
Logic inside the models, such as data transformations or calculations.

📋 Example:

public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public DateTime RegisteredOn { get; set; }
}

🎨 3.Presentation Layer(UI)
🛠 Role:
The Presentation Layer is responsible for user interaction. It includes user interfaces, controllers, and request handling logic.

🔑 What it should contain:
UI components – pages, forms, buttons(in web applications).
Controllers – handle user requests and interact with the business logic.
Input validation – ensures that the data entered by the user is valid.

📋 Example of a Controller:

public class UsersController : Controller
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _userService.GetAllUsers();
        return View(users);
    }

    [HttpPost]
    public IActionResult AddUser(User user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }

        _userService.AddUser(user);
        return RedirectToAction("GetAllUsers");
    }
}

⚙️ 4.Business Logic Layer
🛠 Role:
The Business Logic Layer contains the rules and logic of the application. It handles data validation, calculations, and transformations before sending the data to the database or back to the user.

🔑 What it should contain:
Services – classes that process the business logic.
Data validation – checks if the input data is valid before interacting with the database.
Transformations – converting data between different layers.

📋 Example of a Service:

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void AddUser(User user)
    {
        if (string.IsNullOrWhiteSpace(user.Name))
        {
            throw new ArgumentException("User name cannot be empty.");
        }

        _userRepository.AddUser(user);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }
}

✅ How Layers Work Together
👉 Here’s how the layers interact with each other:

The Presentation Layer (UI) sends a request to the Business Logic Layer.
The Business Logic Layer processes the request and performs validations.
If needed, the Business Logic Layer communicates with the Data Layer to retrieve or save data.
The result is sent back to the Presentation Layer to be displayed to the user.

🔄 Example Workflow:
The user fills out a form in the UI and clicks "Submit."
The data is sent to a Controller.
The Controller calls the Business Logic Layer for validation.
The Business Logic Layer validates the data and calls the Data Layer to save it to the database.
The result is returned to the user via the UI.

🧩 Summary of Each Layer:
Layer Role	What it Contains
Data Layer	Manages database communication	DbContext, Repositories
Domain Models	Represents the data structure	Entity classes (e.g., User, Product)
Presentation Layer	Handles user interaction (UI)	Controllers, Views, UI components
Business Logic	Processes business rules	Services, Data Validation

💡 Key Principles:
Do not mix logic between layers.
Each layer should have a clear and specific responsibility.
Layer separation makes the project more modular and easier to maintain.