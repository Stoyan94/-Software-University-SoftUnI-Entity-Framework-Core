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

        public IList<Movie> GetAllMoviesAsync()
        {
            return  _repository.AllReadonly<Movie>()                              
                              .ToList();
        }
    }
}
