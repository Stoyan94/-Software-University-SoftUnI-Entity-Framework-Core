using CinemaApp.Core.Contracts;
using CinemaApp.Infrastructure.Data.Common;
using CinemaApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Core.Services
{
    public class MovieService : IMovieService
    {
        private readonly IRepository _repository;

        public MovieService(IRepository repository)
        {
            _repository = repository;
        }       

        public IList<Movie> GetAllMovies()
        {
            return  _repository.AllReadonly<Movie>()                              
                              .ToList();
        }

        public IQueryable<Movie> GetAllMovies(Func<Movie, bool> predicate)
        {
            return _repository.AllReadonly<Movie>() 
                .Where(predicate).AsQueryable();
        }

        public IQueryable<Movie> GetAllMoviesPage(int pageNumber, int pageSize)
        {
            if (pageSize < 1)
            {
                throw new ArgumentException(nameof(pageNumber));
            }

            if (pageNumber < 1)
            {
                throw new ArgumentException(nameof(pageSize));
            }


           return _repository.AllReadonly<Movie>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
