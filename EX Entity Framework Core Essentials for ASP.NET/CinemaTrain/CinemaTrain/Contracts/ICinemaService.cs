using CinemaTrain.Models;

namespace CinemaTrain.Contracts
{
    public interface ICinemaService
    {
        Task AddCinemaAsync(CinemaModel model);
    }
}
