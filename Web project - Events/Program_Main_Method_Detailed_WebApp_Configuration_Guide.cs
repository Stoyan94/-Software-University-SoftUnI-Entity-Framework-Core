public static void Main(string[] args)
{
This is the entry point of the application. The Main method is the first method that gets called when the application starts. 
It takes an array of strings(args) as parameters, which can be used to pass command - line arguments to the application.


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
This line creates a new WebApplicationBuilder instance using the provided command - line arguments.The builder is used to configure and build the web application's services and middleware.



string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
This line retrieves the connection string named "DefaultConnection" from the application's configuration settings (usually from appsettings.json or environment variables). 
The connection string is used to connect to the database.



// Add services to the container.
builder.Services.AddControllersWithViews();
This line adds the necessary services to the dependency injection container to support controllers with views(MVC pattern). 
It allows the application to use controllers and serve views.



builder.Services.AddDbContext<EventMiDbContext>(optionsBuilder =>
        optionsBuilder.UseSqlServer(connectionString));
This line registers the EventMiDbContext with the dependency injection container, 
configuring it to use SQL Server with the provided connection string.EventMiDbContext is the Entity Framework context class that interacts with the database.




WebApplication app = builder.Build();
This line builds the WebApplication using the configured builder.
The resulting app object represents the configured web application.



// Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
This line checks if the application is running in a development environment. 
If it is not, the following block of code will be executed to configure production-specific settings.



app.UseExceptionHandler("/Home/Error");
This line configures a global exception handler that redirects users to the "/Home/Error" route in case of an unhandled exception. 
This helps to display a user-friendly error page instead of the default error page.



// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
This line enables HTTP Strict Transport Security (HSTS) for the application. 
HSTS is a security feature that enforces the use of HTTPS and helps protect against certain types of attacks. 
The default duration is 30 days.




    app.UseHttpsRedirection();
This line forces all HTTP requests to be redirected to HTTPS, ensuring secure communication between the client and the server.




app.UseStaticFiles();
This line enables serving static files (e.g., HTML, CSS, JavaScript, images) from the wwwroot folder.



app.UseRouting();
This line enables routing in the application, which allows mapping of incoming requests to the corresponding endpoints (controllers, actions).



app.UseAuthorization();
This line enables authorization middleware, which ensures that only authenticated and authorized users can access certain parts of the application.

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
This line defines a default route for the application. It maps the URL pattern to the corresponding controller and action. 
If no specific route is provided, the application will use the "Home" controller and "Index" action by default. 
The id parameter is optional.



IServiceScope serviceScope = app.Services.CreateScope();
EventMiDbContext context = serviceScope.ServiceProvider.GetRequiredService<EventMiDbContext>();
context.Database.Migrate();
These lines create a new service scope and retrieve an instance of EventMiDbContext from the dependency injection container. 
The context.Database.Migrate() call applies any pending migrations to the database, ensuring that the database schema is up-to-date.



app.Run();

This line starts the web application, allowing it to process incoming HTTP requests. 
The app.Run() method blocks the calling thread until the application is shut down.


    Summary: 

This code sets up an ASP.NET Core web application, configures services, sets up the request pipeline, and starts the application. 
    It includes configuration for database connectivity, error handling, secure communication, static file serving, routing, and more.

- public static void Main(string[] args) { ...}: Entry point of the application.

- WebApplicationBuilder builder = WebApplication.CreateBuilder(args);: Initializes the builder with command-line arguments.

- string? connectionString = builder.Configuration.GetConnectionString(""DefaultConnection"");: Retrieves the connection string.

- builder.Services.AddControllersWithViews();: Adds services for controllers with views.

- builder.Services.AddDbContext<EventMiDbContext>(...);: Registers the DbContext with SQL Server configuration.

- WebApplication app = builder.Build();: Builds the web application.

- if (!app.Environment.IsDevelopment()) {...}: Configures production-specific settings.

- app.UseExceptionHandler(""/Home/Error"");: Sets up a global exception handler.

- app.UseHsts();: Enables HTTP Strict Transport Security.

- app.UseHttpsRedirection();: Redirects HTTP requests to HTTPS.

- app.UseStaticFiles();: Serves static files.

- app.UseRouting();: Enables routing.

-  app.UseAuthorization();: Enables authorization middleware.

- app.MapControllerRoute(...): Defines the default route.

- IServiceScope serviceScope = app.Services.CreateScope();: Creates a service scope.

- EventMiDbContext context = serviceScope.ServiceProvider.GetRequiredService<EventMiDbContext>();: Gets the DbContext instance.

- context.Database.Migrate();: Applies any pending migrations.

- app.Run();: Starts the application.