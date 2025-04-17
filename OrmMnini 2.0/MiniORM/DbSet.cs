using System.Collections;

namespace MiniORM
{
    public class DbSet<TEntity> : ICollection<TEntity>
        where TEntity : class, new()
    {
        internal DbSet(IEnumerable<TEntity> entities)
        {
            this.ChangeTracker = new ChangeTracker<TEntity>(entities);
            this.Entities = entities.ToArray();
        }
        internal ChangeTracker<TEntity> ChangeTracker { get; set; }

        internal ICollection<TEntity> Entities { get; set; }

        public int Count
            => this.Entities.Count();

        public bool IsReadOnly 
            => this.Entities.IsReadOnly;

        public void Add(TEntity item)
        {
            this.Entities.Add(item);
            this.ChangeTracker.Add(item); // Notify the change tracker about the new entity added
        }

        public void Clear()
        {
            while (this.Entities.Any())
            {
                TEntity entity = this.Entities.First();
                this.Entities.Remove(entity);
            }
        }

        public bool Contains(TEntity item)
        {
            return this.Entities.Contains(item);
        }

        public void CopyTo(TEntity[] array, int arrayIndex)
        {
            this.Entities.CopyTo(array, arrayIndex);
        }


        public bool Remove(TEntity item)
        {
            bool isRemoved = this.Entities.Remove(item);
            if (isRemoved)
            {
                this.ChangeTracker.Remove(item); // Notify the change tracker about the entity removed
            }
            return isRemoved;
        }

        public bool RemoveRange(IEnumerable<TEntity> range)
        {
            foreach (TEntity entityToRemove in range)
            {
                bool result = this.Entities.Remove(entityToRemove);
                if (!result)
                {
                    return false; // Stop the removing since we have invalid parameter
                }
            }

            return true;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
           return this.Entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
