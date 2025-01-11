using CinemaApp.Core.Contracts;
using CinemaApp.Infrastructure.Data.Common;
using CinemaApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Core.Services
{
    public class MovieService : IMovieService
    {
        private readonly IRepository repository;

        public MovieService(IRepository _repository)
        {
            repository = _repository;
        }       

        public IList<Movie> GetAllMovies()
        {
            return  repository.AllReadonly<Movie>()                              
                              .ToList();
        }

        public IQueryable<Movie> GetAllAnimationsMovies(Func<Movie, bool> predicate)
        {
            return repository.AllReadonly<Movie>() 
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


           return repository.AllReadonly<Movie>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
