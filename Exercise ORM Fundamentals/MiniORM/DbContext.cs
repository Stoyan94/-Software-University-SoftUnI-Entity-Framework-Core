using System.Reflection;

namespace MiniORM
{
    public abstract class DbContext
    {
        private readonly DatabaseConnection _connection;

        protected DbContext(string connectionStirng)
        {
            this._connection = new DatabaseConnection(connectionStirng);

            IDictionary<Type, PropertyInfo> dbSetProperties = DiscoverDbSets();

            using (new ConnectionManager (this._connection))
            {
                InitializeDbSets(dbSetProperties);
            }
        }
        internal static HashSet<Type> AllowedSqlTypes { get; set; } = new HashSet<Type>
        {
            typeof(string),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(decimal),
            typeof(bool),
            typeof(DateTime),
        };

        private IDictionary<Type, PropertyInfo> DiscoverDbSets()
        {
            IEnumerable<PropertyInfo> dbSetProperties = this.GetType().GetProperties().Where(pi =>
            pi.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

            var result = new Dictionary<Type, PropertyInfo>();

            foreach (var property in dbSetProperties)
            {
                Type entityType = property.PropertyType.GenericTypeArguments[0];
                result[entityType] = property;
            }

            return result;
        }


        private void InitializeDbSets(IDictionary<Type, PropertyInfo> dbSetProperties)
        {
            foreach (var (entityType, dbSetProperty) in dbSetProperties)
            {
                MethodInfo populateMethod = typeof(DbContext).GetMethod(nameof(PopulateDbSet),
                     BindingFlags.Instance | BindingFlags.NonPublic)!
                    .MakeGenericMethod(entityType);

                populateMethod.Invoke(this, new object?[] { dbSetProperty });
            }
        }


    
        private void PopulateDbSet<T>(PropertyInfo dbSetProperty)
            where T : class, new ()
        {
            var entities = Enumerable.Empty<T>();
            var dbSet = new DbSet<T>(entities);
            ReflectionHelper.ReplaceBackingField(this, dbSetProperty.Name, dbSet);
        }
    }    
}
