using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace MiniORM
{
    public abstract class DbContext
    {
        private readonly DatabaseConnection _connection;
        private readonly IDictionary<Type, PropertyInfo> _dbSetProperties;
        private readonly IDictionary<Type, DbSetDescriptor> _dbSetDescriptors;
        protected DbContext(string connectionStirng)
        {
            this._connection = new DatabaseConnection(connectionStirng);

            this._dbSetProperties = this.DiscoverDbSets();

            using (new ConnectionManager (this._connection))
            {
                this.InitializeDbSets();
            }

            this._dbSetDescriptors = this.DescribeDbSets();

            MapRelations();
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

        private IDictionary<Type, DbSetDescriptor> DescribeDbSets()
        {
            Dictionary<Type, DbSetDescriptor> result = new Dictionary<Type, DbSetDescriptor>();

            foreach (var (entityType, dbSetProperty) in this._dbSetProperties)
            {
                var name = dbSetProperty.Name;

                object? dbSet = dbSetProperty.GetValue(this);

                if (dbSet is not (IEnumerable<object> typedDbset))
                {
                    throw new InvalidOperationException($"DbSet {name} was not initialized correctly");
                }

                DbSetDescriptor descriptor = new DbSetDescriptor(entityType, name, typedDbset);

                result[entityType] = descriptor;
            }

            return result;
        }

        private void InitializeDbSets() 
        {
            foreach (var (entityType, dbSetProperty) in this._dbSetProperties)
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

            PropertyInfo dbSetProperty = this._dbSetProperties[entityType];
            return dbSetProperty.Name;
        }

        private IEnumerable<string> GetColumnNames(string tableName)
        {
            return this._connection.FetchColumnNames(tableName);
        }

        private class DbSetDescriptor
        {
            public DbSetDescriptor(Type entityType, string name, IEnumerable<object> dbSet)
            {
                this.EntityType = entityType;
                this.Name = name;
                this.DbSet = dbSet;
            }

            public Type EntityType { get; }
            public string Name { get; }
            public IEnumerable<object> DbSet {  get; }
        }
    }    
}
