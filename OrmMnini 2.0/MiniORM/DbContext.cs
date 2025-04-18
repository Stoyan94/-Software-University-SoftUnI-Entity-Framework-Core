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
                        Console.WriteLine(TransactionRollbackMessage);
                        transaction.Rollback();
                        throw;
                    }

                    try
                    {
                        transaction.Commit();
                    }
                    catch
                    {
                        Console.WriteLine(TransactionExceptionMessage);
                        throw;
                    }                    
                   
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
            return this.GetType()
                .GetProperties()
                .Where(pi => pi.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .ToDictionary(pi => pi.PropertyType.GetGenericArguments().First(), pi => pi);
        }

        private void InitializeDbSets()
        {
            foreach (KeyValuePair<Type, PropertyInfo> dbSetKvp in dbSetProperties)
            {
                Type dbSetType = dbSetKvp.Key;
                PropertyInfo dbSetPoperty = dbSetKvp.Value;

                MethodInfo populateDbSetMethodInfo = typeof(DbContext)
                    .GetMethod("PopulateDbSet", BindingFlags.NonPublic | BindingFlags.Instance)!
                    .MakeGenericMethod(dbSetType);

                populateDbSetMethodInfo.Invoke(this, new object[] { dbSetPoperty });
            }
        }

        private void MapAllRelations()
        {
            foreach (KeyValuePair<Type, PropertyInfo> dbSetKvp in dbSetProperties)
            {
                Type dbSetType = dbSetKvp.Key;
                PropertyInfo dbSetPropertyInfo = dbSetKvp.Value;

                MethodInfo mapRelationsGenericMethodInfo = typeof(DbContext)
                    .GetMethod("MapRelations", BindingFlags.Instance | BindingFlags.NonPublic)!
                    .MakeGenericMethod(dbSetType);

                object? dbSetInstance = dbSetPropertyInfo.GetValue(this);

                if (dbSetInstance == null)
                {
                    throw new ArgumentNullException(dbSetPropertyInfo.Name,
                        String.Format(NullDbSetMessage, dbSetPropertyInfo.Name));
                }

                mapRelationsGenericMethodInfo.Invoke(this, new object[] { dbSetInstance });
            }
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

        private void PopulateDbSet<TEntity>(PropertyInfo dbSetPropertyInfo)
            where TEntity : class, new()
        {
            IEnumerable<TEntity> dbSetEntities = this.LoadTableEntities<TEntity>();
            DbSet<TEntity> dbSetInstance = new DbSet<TEntity>(dbSetEntities);
            ReflectionHelper.ReplaceBackingField(this, dbSetPropertyInfo.Name, dbSetInstance);
        }


        private IEnumerable<TEntity> LoadTableEntities<TEntity>()
          where TEntity : class
        {
            Type tableType = typeof(TEntity);
            IEnumerable<string> columnNames = this.GetEntityColumnNames(tableType);
            string tableName = this.GetTableName(tableType);

            return this.dbConnection
                .FetchResultSet<TEntity>(tableName, columnNames.ToArray());
        }

    }
}
