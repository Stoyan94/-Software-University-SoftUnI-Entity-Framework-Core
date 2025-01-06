using CinemaApp.Core.Contracts;
using CinemaApp.Core.Models;
using CinemaApp.Infrastructure.Data.Common;
using CinemaApp.Infrastructure.Data.Models;

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
            throw new NotImplementedException();
        }

        public List<Movie> GetAllMovies()
        {
            throw new NotImplementedException();
        }

        public void InsertAdditionalMovies(List<Movie> movies)
        {
            throw new NotImplementedException();
        }
    }
}
