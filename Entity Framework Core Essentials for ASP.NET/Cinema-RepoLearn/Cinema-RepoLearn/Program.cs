using Cinema_RepoLearn.Contracts;
using Cinema_RepoLearn.Data;
using Cinema_RepoLearn.Data.Common;
using Cinema_RepoLearn.Models;
using Cinema_RepoLearn.Services;
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
        options.UseSqlServer(configuration.GetConnectionString("Server=STOYAN;Database=Cinema24;User Id=sa;Password=558955;Trusted_Connection=True;")))
    .AddScoped<IRepository, Repository>()
    .AddScoped<ICinemaService, CinemaService>()
    .BuildServiceProvider();

using var scope = serviceProvider.CreateScope();
ICinemaService? service = scope.ServiceProvider.GetService<ICinemaService>();

if (service != null)
{
    var cinema = new CinemaModel("Capitol", "Sofia, Car Boris III");
    await service.AddCinemaAsync(cinema);
}