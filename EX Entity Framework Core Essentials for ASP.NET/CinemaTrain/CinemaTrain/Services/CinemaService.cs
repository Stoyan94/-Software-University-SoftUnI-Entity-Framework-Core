using CinemaTrain.Contracts;
using CinemaTrain.Data.Common;
using CinemaTrain.Data.Model;
using CinemaTrain.Models;

namespace CinemaTrain.Services
{
    public class CinemaService : ICinemaService
    {
        private readonly IRepository repo;

        public CinemaService(IRepository _repo)
        {
            repo = _repo;
        }
        public async Task AddCinemaAsync(CinemaModel model)
        {
            if (repo.AllReadOnly<Cinema>().Any(c => c.Name == model.Name))
            {
                throw new ArgumentException("Cinema already exist");
            }

            Cinema cinema = new Cinema()
            {
                Address = model.Address,
                Name = model.Name,
            };

            await repo.AddAsync(cinema);
            await repo.SaveChangesAsync();
        }
    }
}
