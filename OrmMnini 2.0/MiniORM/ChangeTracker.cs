using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MiniORM
{
    public class ChangeTracker<T>
        where T : class, new()
    {
        private readonly ICollection<T> allEntities;
        private readonly ICollection<T> addedEntities;
        private readonly ICollection<T> removedEntities;

        public ChangeTracker(IEnumerable<T> entities)
        {
            this.allEntities = this.CloneEntities(entities);
            this.addedEntities = new HashSet<T>();
            this.removedEntities = new HashSet<T>();
        }

        public IReadOnlyCollection<T> AllEntities 
            => this.allEntities.ToList().AsReadOnly();

        public IReadOnlyCollection<T> AddedEntities
            => this.addedEntities.ToList().AsReadOnly();

        public IReadOnlyCollection<T> RemovedEntities
            => this.removedEntities.ToList().AsReadOnly();

        public void Add(T entity)
        {
            this.removedEntities.Add(entity);
        }

        public void Remove(T entity)
        {
            this.removedEntities.Add(entity);
        }

        public IEnumerable<T> GetModifiedEntities(DbSet<T> dbSet)
        {
            ICollection<T> modifiedEntities = new HashSet<T>();
            PropertyInfo[] primaryKeys = typeof(T).GetProperties()
                .Where(pi => pi.HasAttribute<KeyAttribute>())
                .ToArray();

            foreach (T proxyEntity in this.allEntities)
            {
                IEnumerable<object> proxyPrimaryKeyValues = 
                    GetPrimaryKeyValues(primaryKeys, proxyEntity);
                T dbSetEntity = dbSet.Entities
                    .Single(e => GetPrimaryKeyValues(primaryKeys, e)
                                           .SequenceEqual(proxyPrimaryKeyValues));

                bool isEntityModified = this.IsModified(proxyEntity, dbSetEntity);
                if (isEntityModified)
                {
                    modifiedEntities.Add(dbSetEntity);
                }
            }

            return modifiedEntities;
        }


        private static IEnumerable<object> GetPrimaryKeyValues(IEnumerable<PropertyInfo> primaryKeys, T entity)
        {
            ICollection<object> primaryKeyValues = new HashSet<object>();

            foreach (PropertyInfo propertyInfo in primaryKeys)
            {
                object? primaryKeyValue = propertyInfo.GetValue(entity);
                if (primaryKeyValue == null)
                {
                    throw new ArgumentNullException
                        (propertyInfo.Name , ErrorMessages.PrimaryKeyNullErrorMessage);
                }

                primaryKeyValues.Add(primaryKeyValue);
            }

            return primaryKeyValues;
        }

        private ICollection<T> CloneEntities(IEnumerable<T> entities)
        {
            ICollection<T> clonedEntities = new HashSet<T>();
            PropertyInfo[] properties = typeof(T).GetProperties()
                .Where(pi => DbContext.AllowedSqlTypes.Contains(pi.PropertyType))
                .ToArray();

            foreach (T entity in entities)
            {
                T entityClone = Activator.CreateInstance<T>();

                foreach (PropertyInfo propertyInfo in properties)
                {
                    object? originalEntityValue = propertyInfo.GetValue(entity);
                    propertyInfo.SetValue(entityClone, originalEntityValue);
                }

                clonedEntities.Add(entityClone);
            }

            return clonedEntities;
        }

        private bool IsModified(T proxyEntity, T dbSetEntity)
        {
            PropertyInfo[] monitoredProperties = typeof(T).GetProperties()
                .Where(pi => DbContext.AllowedSqlTypes.Contains(pi.PropertyType))
                .ToArray();

            foreach (PropertyInfo pi in monitoredProperties)
            {
                object? proxyEntityValue = pi.GetValue(proxyEntity);
                object? dbSetEntityValue = pi.GetValue(dbSetEntity);

                if (proxyEntityValue == null && 
                    dbSetEntityValue == null)
                {
                    continue;
                }

                if (!proxyEntityValue!.Equals(dbSetEntity))
                {
                    return true;
                }
            }

            return false;
        }

    }
}