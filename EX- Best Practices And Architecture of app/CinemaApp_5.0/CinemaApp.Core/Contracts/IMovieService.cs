using CinemaApp.Infrastructure.Data.Models;

namespace CinemaApp.Core.Contracts
{
    public interface IMovieService
    {
        IList<Movie> GetAllMovies();

        IQueryable<Movie> GetAllMovies(Func<Movie, bool> predicate);

        IQueryable<Movie> GetAllMoviesPage(int pageNumber, int pageSize);
    }
}
