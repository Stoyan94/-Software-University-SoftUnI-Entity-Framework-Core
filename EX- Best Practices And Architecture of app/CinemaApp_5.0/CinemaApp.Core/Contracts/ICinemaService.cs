using CinemaApp.Core.Models;
using CinemaApp.Core.Models.DTOs;
using CinemaApp.Infrastructure.Data.Models;

namespace CinemaApp.Core.Contracts
{
    public interface ICinemaService
    {
        Task AddCinemaAsync(CinemaModel model);
        List<Cinema> GetAllCinemas();        
        Task InsertAdditionalMovies(List<Movie> movies);

        List<CinemaHallsExportDto> GetAllCinemasByTown(string cityName);
    }
}
