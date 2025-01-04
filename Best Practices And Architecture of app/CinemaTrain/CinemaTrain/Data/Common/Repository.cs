using Microsoft.EntityFrameworkCore;

namespace CinemaTrain.Data.Common
{
    public class Repository : IRepository
    {
        protected DbContext Context;

        public Repository(CinemaDbContext dbContext)
        {
            Context = dbContext;
        }

        protected DbSet<T> Dbset<T>() where T : class
        {
            return Context.Set<T>();
        }
        public async Task AddAsync<T>(T entity) where T : class
        {
            await Dbset<T>()
                .AddAsync(entity);
        }

        public async Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            await Dbset<T>()
                .AddRangeAsync(entities);
        }

        public IQueryable<T> All<T>() where T : class
        {
            return Dbset<T>();
        }

        public IQueryable<T> AllReadOnly<T>() where T : class
        {
            return Dbset<T>()
                .AsNoTracking();
        }

        public void Dispose()
        {
           Context.Dispose();
        }

        public async Task<T?> GetByIdAsync<T>(object id) where T : class
        {
           return await Dbset<T>()
                .FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }
    }
}
