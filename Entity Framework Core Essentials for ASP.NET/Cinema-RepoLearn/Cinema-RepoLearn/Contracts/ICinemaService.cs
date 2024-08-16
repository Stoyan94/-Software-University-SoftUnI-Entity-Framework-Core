using Cinema_RepoLearn.Models;

namespace Cinema_RepoLearn.Contracts
{
    public interface ICinemaService
    {
        Task AddCinemaAsync(CinemaModel model);
    }
}
