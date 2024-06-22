using System.Reflection;

namespace MiniORM
{
    public abstract class DbContext
    {
        private readonly DatabaseConnection _connection;

        protected DbContext(string connectionStirng)
        {
            this._connection = new DatabaseConnection(connectionStirng);
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

            foreach (var dbSetProperty in dbSetProperties)
            {

            }

            return result;
        }
    }
}
