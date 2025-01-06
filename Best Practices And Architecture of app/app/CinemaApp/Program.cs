using CinemaApp.Core.Contracts;
using CinemaApp.Infrastructure.Data;
using CinemaApp.Infrastructure.Data.Common;
using CinemaApp.Core.Models;
using CinemaApp.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true)
    .AddUserSecrets(typeof(Program).Assembly)
    .Build();

var serviceProvider = new ServiceCollection()
    .AddLogging()
    .AddDbContext<CinemaDbContext>(options =>
        options.UseSqlServer("Server=STOYAN;Database=Cinema;User Id=sa;Password=558955;Trusted_Connection=True;"))
    .AddScoped<IRepository, Repository>()
    .AddScoped<ICinemaService, CinemaService>()
    .BuildServiceProvider();

using var scope = serviceProvider.CreateScope();
ICinemaService? service = scope.ServiceProvider.GetService<ICinemaService>();

