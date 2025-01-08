using CinemaApp.Infrastructure.Data.Models;

namespace CinemaApp.Core.Contracts
{
    public interface IMovieService
    {
        IList<Movie> GetAllMovies();
    }
}
