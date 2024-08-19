using Cinema_RepoLearn.Core.Models;

namespace Cinema_RepoLearn.Core.Contracts
{
    public interface ICinemaService
    {
        Task AddCinemaAsync(CinemaModel model);
    }
}
