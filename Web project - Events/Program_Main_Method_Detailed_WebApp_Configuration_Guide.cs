public static void Main(string[] args)
{
This is the entry point of the application. The Main method is the first method that gets called when the application starts. 
It takes an array of strings(args) as parameters, which can be used to pass command - line arguments to the application.


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
This line creates a new WebApplicationBuilder instance using the provided command - line arguments.
The builder is used to configure and build the web application's services and middleware.



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
HSTS: "HTTP Strict Transport Security"




    app.UseHttpsRedirection();
This line forces all HTTP requests to be redirected to HTTPS, ensuring secure communication between the client and the server.
HTTP: "Hypertext Transfer Protocol"
HTTPS: "Hypertext Transfer Protocol Secure"



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





 БГ ПРЕВОД


    public static void Main(string[] args)
{
    Това е входната точка на приложението. Методът Main е първият метод, който се извиква при стартирането на приложението.
    Той приема масив от низове (args) като параметри, които могат да се използват за предаване на аргументи от командния ред на приложението.

    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    Този ред създава нова инстанция на WebApplicationBuilder, използвайки предоставените аргументи от командния ред.
    Builder-ът се използва за конфигуриране и изграждане на услугите и междинния софтуер (middleware) на уеб приложението.


    string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

     Този ред извлича връзковия низ (connection string) с име "DefaultConnection" от конфигурационните настройки на приложението 
     (обикновено от файла appsettings.json или променливи на средата). 
     Връзковият низ се използва за връзка с базата данни.


     Добавяне на услуги към контейнера.
    builder.Services.AddControllersWithViews();

     Този ред добавя необходимите услуги към контейнера за dependency injection, за да поддържа контролери с изгледи (MVC модел).
     Това позволява на приложението да използва контролери и да обслужва изгледи.



    builder.Services.AddDbContext<EventMiDbContext>(optionsBuilder =>
        optionsBuilder.UseSqlServer(connectionString));

     Този ред регистрира EventMiDbContext в контейнера за dependency injection, 
     конфигурирайки го да използва SQL Server с предоставения връзков низ.
     EventMiDbContext е класът за контекста на Entity Framework, който взаимодейства с базата данни.



    WebApplication app = builder.Build();

     Този ред изгражда WebApplication, използвайки конфигурирания builder.
     Полученият обект app представлява конфигурираното уеб приложение.


     Конфигуриране на HTTP заявките.

    if (!app.Environment.IsDevelopment())
    {
         Този ред проверява дали приложението се изпълнява в среда за разработка.
         Ако не е, следващият блок код ще бъде изпълнен за конфигуриране на настройки, специфични за продукция.

        app.UseExceptionHandler("/Home/Error");

         Този ред конфигурира глобален обработчик на изключения, който пренасочва потребителите към маршрута "/Home/Error" 
         при възникване на необработено изключение. Това помага за показване на потребителски ориентирана страница за грешки.


         Стойността по подразбиране на HSTS е 30 дни. Препоръчва се промяна за продукционни сценарии.
        app.UseHsts();

         Този ред активира HTTP Strict Transport Security (HSTS) за приложението.
         HSTS е защитна функция, която налага използването на HTTPS и помага за защита от определени видове атаки.
         HSTS: "Строга транспортна защита на HTTP"
         или "Строга политика за транспортна сигурност на HTTP".
    }

    app.UseHttpsRedirection();
     Този ред пренасочва всички HTTP заявки към HTTPS, като гарантира сигурна комуникация между клиента и сървъра.
     HTTP: "Протокол за пренос на хипертекст"
     HTTPS: "Протокол за пренос на хипертекст със защита"


    app.UseStaticFiles();
     Този ред позволява обслужването на статични файлове (напр. HTML, CSS, JavaScript, изображения) от папката wwwroot.


    app.UseRouting();
     Този ред активира маршрутизацията в приложението, като позволява свързването на входящите заявки с 
     съответните крайни точки (контролери, действия).


    app.UseAuthorization();
     Този ред активира middleware за оторизация, което гарантира, че само автентифицирани и оторизирани потребители 
     имат достъп до определени части на приложението.


    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
     Този ред дефинира маршрут по подразбиране за приложението. Той свързва шаблона на URL с 
     съответния контролер и действие. Ако не е предоставен конкретен маршрут, приложението ще използва 
     контролера "Home" и действието "Index" по подразбиране. Параметърът id е опционален.

     URL (Uniform Resource Locator) се превежда като "Унифициран локатор на ресурси".


    IServiceScope serviceScope = app.Services.CreateScope();
    EventMiDbContext context = serviceScope.ServiceProvider.GetRequiredService<EventMiDbContext>();
    context.Database.Migrate();

     Тези редове създават нов обхват на услугата и извличат инстанция на EventMiDbContext от контейнера за dependency injection.
     Извикването на context.Database.Migrate() прилага всички чакащи миграции към базата данни, като гарантира, че схемата на базата данни е актуална.


    app.Run();
     Този ред стартира уеб приложението, позволявайки му да обработва входящи HTTP заявки.
     Методът app.Run() блокира изпълнението на основния поток, докато приложението не бъде спряно.
}



public static void Main(string[] args) { ... }: Входна точка на приложението.

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);: Инициализира builder с аргументи от командния ред.

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");: Извлича връзковия низ.

builder.Services.AddControllersWithViews();: Добавя услуги за контролери с изгледи (MVC).

builder.Services.AddDbContext<EventMiDbContext>(...);: Регистрира DbContext с конфигурация за SQL Server.

WebApplication app = builder.Build();: Изгражда уеб приложението.

if (!app.Environment.IsDevelopment()) { ... }: Конфигурира настройки, специфични за продукция.

app.UseExceptionHandler("/Home/Error");: Настройва глобален обработчик на изключения.

app.UseHsts();: Активира HTTP Strict Transport Security (HSTS).

app.UseHttpsRedirection();: Пренасочва HTTP заявки към HTTPS.

app.UseStaticFiles();: Обслужва статични файлове. HTML, CSS, JavaScript, изображения

app.UseRouting();: Активира маршрутизация.(контролери, действия)

app.UseAuthorization();: Активира middleware за оторизация.

app.MapControllerRoute(...);: Дефинира маршрут по подразбиране.

IServiceScope serviceScope = app.Services.CreateScope();: Създава обхват на услугите (Service Scope).

EventMiDbContext context = serviceScope.ServiceProvider.GetRequiredService<EventMiDbContext>();: Извлича инстанция на EventMiDbContext от контейнера за зависимостите (Dependency Injection).

context.Database.Migrate();: Прилага всички чакащи миграции към базата данни, за да гарантира, че нейната схема е актуална.

app.Run();: Стартира приложението и му позволява да обработва входящи HTTP заявки.

