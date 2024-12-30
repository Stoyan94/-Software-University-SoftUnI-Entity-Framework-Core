1.IConfiguration Setup

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true)
    .AddUserSecrets(typeof(Program).Assembly)
    .Build();

ConfigurationBuilder: Used to build an IConfiguration object, which provides a way to access key-value pairs for application settings.
.AddJsonFile("appsettings.json", true): Adds the configuration settings from the file appsettings.json.

The second parameter (true) means the file is optional, so the app won't crash if it isn't found.
.AddUserSecrets(typeof(Program).Assembly): Adds user secrets to the configuration.

User Secrets are a way to store sensitive information (like API keys or connection strings) during development without hardcoding them in the source code.
The typeof(Program).Assembly indicates that the secrets are associated with the current project.
.Build(): Finalizes and creates the IConfiguration object.

At the end of this step, configuration contains all the merged settings.

2. Service Collection and Dependency Injection

var serviceProvider = new ServiceCollection()
    .AddLogging()
    .AddDbContext<CinemaDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("CinemaConnection")))
    .AddScoped<IRepository, Repository>()
    .BuildServiceProvider();

ServiceCollection: A container that registers services and their lifetimes for dependency injection (DI).

Service Registrations:
.AddLogging()

Adds logging capabilities to the service provider. This could include various loggers (e.g., console, file).
.AddDbContext<CinemaDbContext>()

Registers the CinemaDbContext for dependency injection.
options.UseSqlServer():

Configures the database context to use SQL Server as the database provider.
configuration.GetConnectionString("CinemaConnection"):
Fetches the connection string named CinemaConnection from appsettings.json or user secrets.
.AddScoped<IRepository, Repository>()

Registers the IRepository interface with the Repository implementation.
Scoped Lifetime:
A new instance of Repository is created for each HTTP request or operation.
.BuildServiceProvider()

Finalizes and builds the IServiceProvider, which resolves dependencies and provides instances of the registered services.

Summary
Configuration: Reads settings from appsettings.json and user secrets for use in the application.
Service Registration:
Logging.
Database context(CinemaDbContext) with SQL Server.
A repository pattern(IRepository->Repository).
Dependency Injection: Services are registered with appropriate lifetimes, and the serviceProvider resolves them as needed.