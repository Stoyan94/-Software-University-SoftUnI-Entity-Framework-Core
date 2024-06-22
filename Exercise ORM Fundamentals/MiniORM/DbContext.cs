using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace MiniORM
{
    public abstract class DbContext
    {
        private readonly DatabaseConnection _connection;
        private readonly IDictionary<Type, PropertyInfo> dbSetProperties;
        protected DbContext(string connectionStirng)
        {
            this._connection = new DatabaseConnection(connectionStirng);

            this.dbSetProperties = DiscoverDbSets();

            using (new ConnectionManager (this._connection))
            {
                InitializeDbSets();
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


        private void InitializeDbSets() 
        {
            foreach (var (entityType, dbSetProperty) in this.dbSetProperties)
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
            var entities = LoadEntities<T>();
            var dbSet = new DbSet<T>(entities);
            ReflectionHelper.ReplaceBackingField(this, dbSetProperty.Name, dbSet);
        }

        private IEnumerable<T> LoadEntities<T>()
            where T : class, new ()
        {
            Type entityType = typeof(T);

            string tableName = this.GetTableName(entityType);
            string[] columnNames = this.GetColumnNames(tableName).ToArray();

            return this._connection.FetchResultSet<T>(tableName, columnNames);
        }

        private string GetTableName(Type entityType)
        {
            TableAttribute? tableAttribute = entityType.GetCustomAttribute<TableAttribute>();

            if (tableAttribute is not null) 
            {
                return tableAttribute.Name;                
            }

            PropertyInfo dbSetProperty = this.dbSetProperties[entityType];
            return dbSetProperty.Name;
        }

        private IEnumerable<string> GetColumnNames(string tableName)
        {
            return this._connection.FetchColumnNames(tableName);
        }
    }    
}
