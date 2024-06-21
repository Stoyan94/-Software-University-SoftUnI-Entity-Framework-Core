namespace MiniORM
{
    public class ChangeTracker<T>
        where T : class, new ()
    {
        private readonly List<T> _allEntities;
        private readonly List<T> _added;
        private readonly List<T> _removed;

        public ChangeTracker(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            this._added = new List<T>();
            this._removed = new List<T>();
            this._allEntities = CloneEntities(entities).ToList();
        }


        public IReadOnlyCollection<T> Added => this._added.AsReadOnly();
        public IReadOnlyCollection<T> Removed => this._removed.AsReadOnly();

        private static IEnumerable<T> CloneEntities(IEnumerable<T> entities)
        {
            var properties = typeof(T).GetAllowedSqlProperties();

            List<T> result = new List<T>();

            foreach ( var originalEntity in entities)
            {
                var copy = new T();

                foreach ( var property in properties )
                {
                    var value = property.GetValue(originalEntity);
                    property.SetValue(copy, value);
                }

                result.Add(copy);
            }

            return result;
        }
    }
}