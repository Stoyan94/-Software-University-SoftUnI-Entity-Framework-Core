namespace MiniORM
{
    public class DbSet<T>
        where T : class, new()
    {
        internal ChangeTracker<T> ChangeTracker { get; }
        internal IList<T> Entities { get; }

        public DbSet(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            this.Entities = entities.ToList();
            this.ChangeTracker = new ChangeTracker<T>(this.Entities);
        }
    }
}
