using Microsoft.Data.SqlClient;
using System.Collections;
using System.Reflection;
using static MiniORM.ErrorMessages;

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

        public void SaveChanges()
        {
            IEnumerable<object> dbSetsObjects = this.dbSetProperties
                .Select(edb => edb.Value.GetValue(this)!)
                .ToArray();

            foreach (IEnumerable<object> dbSet in dbSetsObjects)
            {
                IEnumerable<object> invalidEntities = dbSet
                    .Where(e => !this.IsObjectValid(e))
                    .ToArray();

                if (invalidEntities.Any())
                {
                    throw new InvalidOperationException(string.Format(InvalidEntitiesInDbSetMessage,
                        invalidEntities.Count(),dbSet.GetType().Name));
                }
            }

            using (new ConnectionManager(this.dbConnection))
            {
                using SqlTransaction transaction = this.dbConnection
                    .StartTransaction();

                foreach (IEnumerable dbSet in dbSetsObjects)
                {
                    MethodInfo persistMethod = typeof(DbContext)
                        .GetMethod("Persist", BindingFlags.NonPublic | BindingFlags.Instance)!
                        .MakeGenericMethod(dbSet.GetType());

                    try
                    {
                        try
                        {
                            persistMethod.Invoke(this, new object[] { dbSet });
                        }
                        catch (TargetInvocationException tie)
                            when (tie.InnerException != null)
                        {

                            throw tie.InnerException;
                        }
                    }
                    catch 
                    {
                        Console.WriteLine("Performing Rollback due to Exception!!!");
                        transaction.Rollback();
                        throw;
                    }
                    
                    transaction.Commit();
                }
            }
        }

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
