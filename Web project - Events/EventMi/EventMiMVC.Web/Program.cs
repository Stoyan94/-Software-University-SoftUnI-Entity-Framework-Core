using EventMiMVC.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace EventMiMVC.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Create the host builder
            // This will create a new instance of the WebApplicationBuilder class, which is used to configure the application.
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Add services to the container.
            // This will add the services to the application's service container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<EventMiDbContext>(optionsBuilder =>
                optionsBuilder.UseSqlServer(connectionString))
            builder.Services.AddScoped<ieventser>();

            // Build the application
            // This class is used to configure the HTTP request pipeline and start the application.
            // The Build method will return an instance of the WebApplication class, which can be used to configure the application.
            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //This will ensure that the application uses HTTPS for all requests.
            app.UseHttpsRedirection();

            //This will serve static files from the wwwroot folder.
            app.UseStaticFiles();

            //This will enable the use of the MVC pattern in the application.
            //It will also enable the use of routing in the application.
            app.UseRouting();

            //This will enable the use of authentication in the application.
            app.UseAuthorization();

            //This will enable the use of the MVC pattern in the application.
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            //Every time the application starts, it will check if the database is up to date with the latest migrations
            //and apply them if necessary.
            //This is useful when deploying the application to a new environment, where the database may not exist yet.
            //It will create the database and apply the migrations automatically.
            //This is done by calling the MigrateAsync method on the database context.
            //This method will apply any pending migrations to the database.
            IServiceScope serviceScope = app.Services.CreateScope();
            EventMiDbContext context = serviceScope.ServiceProvider.GetRequiredService<EventMiDbContext>();
            await context.Database.MigrateAsync();
            

           await app.RunAsync(); 
        }
    }
}
