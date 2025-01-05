IRepository Interface
All<T>():

Returns all records of type T from the database (as IQueryable for querying).
AllReadonly<T>():

Similar to All<T>(), but the data is returned as "read-only" (no tracking of changes) to optimize performance for query operations.
GetByIdAsync<T>(object id):
Retrieves a record of type T from the database by its primary key (id) asynchronously.

AddAsync<T>(T entity):
Asynchronously adds a new record of type T to the database.

AddRangeAsync<T>(IEnumerable<T> entities):
Asynchronously adds multiple records of type T to the database in a batch operation.

SaveChangesAsync():
Asynchronously saves changes made in the database context (e.g., insert, update, delete).

Repository Class
DbSet<T>():
Returns a DbSet<T>, which represents a collection of entities of type T (similar to a table in the database). This is used for CRUD operations.

AddAsync<T>(T entity):
Adds a single entity of type T to the DbSet asynchronously (calls AddAsync on the DbSet).

AddRangeAsync<T>(IEnumerable<T> entities):
Adds a range of entities of type T to the DbSet asynchronously (calls AddRangeAsync on the DbSet).

All<T>():
Returns all records of type T from the DbSet<T> as an IQueryable, enabling further querying.

AllReadonly<T>():
Returns all records of type T as read-only (AsNoTracking), meaning no change tracking is performed by the context.

Dispose():
Disposes of the DbContext to release resources when done, ensuring proper memory management.

GetByIdAsync<T>(object id):
Retrieves a record of type T asynchronously by its primary key (id) using FindAsync.

SaveChangesAsync():
Asynchronously saves all changes made to the DbContext (e.g., new entities added, existing entities modified) to the database.


Summary:
The Repository class implements CRUD operations using DbSet<T>, DbContext, and async methods for efficient data handling.
The methods handle adding, retrieving, querying, and saving data, with support for both single and batch operations.