using CinemaApp.Core.Models;
using CinemaApp.Infrastructure.Data.Models;

namespace CinemaApp.Core.Contracts
{
    public interface ICinemaService
    {
        Task AddCinemaAsync(CinemaModel model);
        List<Cinema> GetAllCinemas();
        List<Movie> GetAllMovies();
        Task InsertAdditionalMovies(List<Movie> movies);
    }
}
