using Microsoft.Data.SqlClient;
using System.Collections;
using System.ComponentModel.DataAnnotations;
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
                    .Where(e => !IsObjectValid(e))
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

        private static bool IsObjectValid(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            ICollection<ValidationResult> validationErros = new List<ValidationResult>();

            return Validator
                .TryValidateObject(obj, validationContext, validationErros, true);
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

        private void Persist<TEntity> (DbSet<TEntity> dbSet)
            where TEntity : class, new()
        {
            string tableName = this.GetTableName(typeof(TEntity));
            IEnumerable<string> columnsNames = this.dbConnection
                .FetchColumnNames(tableName);

            if (dbSet.ChangeTracker.AddedEntities.Any())
            {
                this.dbConnection.InsertEntities(dbSet.ChangeTracker.AddedEntities, tableName, columnsNames.ToArray());
            }

            IEnumerable<TEntity> modifiedEntities = dbSet
                .ChangeTracker.GetModifiedEntities(dbSet);

            if (modifiedEntities.Any())
            {
                this.dbConnection
                    .UpdateEntities(modifiedEntities, tableName, columnsNames.ToArray());
            }

            if (dbSet.ChangeTracker.RemovedEntities.Any())
            {
                this.dbConnection
                    .DeleteEntities(dbSet.ChangeTracker.RemovedEntities, tableName, columnsNames.ToArray());
            }
        }


        private string GetTableName(Type tableType)
        {

        }
    }
}
