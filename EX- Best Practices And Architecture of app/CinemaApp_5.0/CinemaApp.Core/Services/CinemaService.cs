using CinemaApp.Core.Contracts;
using CinemaApp.Core.Models;
using CinemaApp.Core.Models.DTOs;
using CinemaApp.Infrastructure.Data.Common;
using CinemaApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Core.Services
{
    public class CinemaService : ICinemaService
    {
        private readonly IRepository repo;

        public CinemaService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddCinemaAsync(CinemaModel model)
        {
            if (repo.AllReadonly<Cinema>().Any(c => c.Name == model.Name))
            {
                throw new ArgumentException("Cinema already exists");
            }

            Cinema cinema = new Cinema()
            {
                Address = model.Address,
                Name = model.Name
            };

            await repo.AddAsync(cinema);
            await repo.SaveChangesAsync();
        }

        public List<Cinema> GetAllCinemas()
        {
            return repo.AllReadonly<Cinema>()
                       .ToList();
        }

        public List<CinemaHallsExportDto> GetAllCinemasByTown(string cityName)
        {
            return repo.AllReadonly<Cinema>()
                .Where(c => c.Address.StartsWith(cityName.ToLower().ToLower()))
                .Select(c => new CinemaHallsExportDto()
                {
                    Name = c.Name,
                    Address = c.Address,
                    NumberOfHalls = c.CinemaHalls.Count
                })
                .ToList();
                
        }

        public async Task InsertAdditionalMovies(List<Movie> movies)
        {
            await repo.AddAsync(movies);
            await repo.SaveChangesAsync();            
        }
    }
}
