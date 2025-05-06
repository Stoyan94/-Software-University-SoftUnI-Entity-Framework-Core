In C#, IQueryable<T> is an interface that is used for querying data from a data source. 
It is part of the System.Linq namespace and extends the IEnumerable<T> interface. 
The main advantage of IQueryable<T> is its ability to build expressions that can be translated into queries that run on the data source (e.g., SQL queries for a database). 
This is particularly useful in scenarios involving LINQ to SQL, LINQ to Entities (Entity Framework), and other LINQ providers that can translate expressions into native queries for the underlying data source.

Key Features of IQueryable<T>

Deferred Execution: Queries are not executed when they are defined. 
    Instead, they are executed when the query is enumerated, such as with a foreach loop or a call to methods like ToList() or FirstOrDefault().

Expression Trees: IQueryable<T> works with expression trees, allowing the provider to translate the LINQ query into a specific query language (e.g., SQL for databases).

Provider: IQueryable<T> has a provider (IQueryProvider) that is responsible for executing the query against the data source.

Example of Using IQueryable<T>
Suppose you have a database context with a DbSet for Products. Here is how you can use IQueryable<T> to query the products.

First, let's assume we have the following context and entity:

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
}

Now, let's use IQueryable<T> to query this data.

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main()
    {
        using (var context = new AppDbContext())
        {
            // Define an IQueryable query
            IQueryable<Product> query = context.Products
                                               .Where(p => p.Price > 50)
                                               .OrderBy(p => p.Name);

            // The query is not executed until we enumerate it
            foreach (var product in query)
            {
                Console.WriteLine($"{product.Name} - {product.Price}");
            }

            // Or we can force execution with a method like ToList()
            var productList = query.ToList();
            foreach (var product in productList)
            {
                Console.WriteLine($"{product.Name} - {product.Price}");
            }
        }
    }
}

Explanation of the Example
Defining the Query:

IQueryable<Product> query = context.Products
                                   .Where(p => p.Price > 50)
                                   .OrderBy(p => p.Name);
This defines an IQueryable<Product> that filters products with a price greater than 50 and orders them by name. The query is not executed at this point.

Executing the Query:


foreach (var product in query)
{
    Console.WriteLine($"{product.Name} - {product.Price}");
}
When we enumerate the query with a foreach loop, the query is sent to the database, and the results are fetched.

Forcing Execution:

var productList = query.ToList();
Calling ToList() forces the execution of the query immediately, and the results are stored in productList.

Benefits of IQueryable<T>

Optimized Queries: By translating LINQ queries to native queries (like SQL), IQueryable<T> ensures that only the necessary data is retrieved from the data source.

Composability: Queries can be composed and modified before execution, allowing for dynamic query building.

Deferred Execution: Queries are not executed until needed, which can improve performance by delaying execution until the results are actually required.

Conclusion
IQueryable<T> is a powerful interface in C# for querying data sources efficiently. 
    It provides flexibility and optimization by allowing LINQ queries to be translated into native queries for the data source, 
    and its use of deferred execution can lead to performance benefits in data access scenarios.