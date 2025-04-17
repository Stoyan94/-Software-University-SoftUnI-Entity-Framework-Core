using System.Reflection;

namespace MiniORM
{
    public class DbContext
    {
        private readonly DatabaseConnection dbConnection;
        private readonly IDictionary<Type, PropertyInfo> dbSetProperties;

        protected DbContext(string connectionString)
        {
            this.dbConnection = new DatabaseConnection(connectionString);
            this.dbSetProperties = this.DiscoverDbSet();
            using (new ConnectionManager(this.dbConnection))
            {
                this.InitializeDbSets();
            }
            this.MapAllRelations(); // This is done after connection close because it is in-memory operation
        }

        internal static readonly Type[] AllowedSqlTypes =
        {
            typeof(string),
            typeof(byte),
            typeof(sbyte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(bool),
            typeof(DateTime),
            typeof(Guid)
        };

        private IDictionary<Type, PropertyInfo> DiscoverDbSet()
        {
            throw new NotImplementedException();
        }

        private void InitializeDbSets()
        {
            throw new NotImplementedException();
        }

        private void MapAllRelations()
        {
            throw new NotImplementedException();
        }
    }
}
