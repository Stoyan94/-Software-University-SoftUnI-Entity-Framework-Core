using CinemaApp.Core.Contracts;
using CinemaApp.Infrastructure.Data;
using CinemaApp.Infrastructure.Data.Common;
using CinemaApp.Core.Models;
using CinemaApp.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true)
    .AddUserSecrets(typeof(Program).Assembly)
    .Build();

var serviceProvider = new ServiceCollection()
    .AddLogging()
    .AddDbContext<CinemaDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("CinemaConnection")))
    .AddScoped<IRepository, Repository>()
    .AddScoped<ICinemaService, CinemaService>()
    .AddScoped<IMovieService, MovieService>()
    .BuildServiceProvider();

using var scope = serviceProvider.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<CinemaDbContext>();

//ResetDatabase(dbContext);

ICinemaService? cinemaService = scope.ServiceProvider.GetService<ICinemaService>();
IMovieService? movieService = scope.ServiceProvider.GetService<IMovieService>();

if (cinemaService is null)
{
    throw new InvalidOperationException("Failed to resolve ICinemaService.");
}

if (movieService is null)
{
    throw new InvalidOperationException("Failed to resolve IMovieService.");
}

ConsoleInterface.Run(cinemaService, movieService);

static void ResetDatabase(CinemaDbContext dbContext)
{
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
}

