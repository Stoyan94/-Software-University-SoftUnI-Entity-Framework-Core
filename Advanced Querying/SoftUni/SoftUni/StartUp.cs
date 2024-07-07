using Microsoft.EntityFrameworkCore;
using SoftUni.Data;

namespace SoftUni;

public class StartUp
{
    static async Task Main(string[] args)
    {
        SoftUniContext dbContext = new SoftUniContext();

        SQLInjectionDefense(dbContext);

        
    }

    private static async Task<Models.Employee[]> SQLInjectionDefense(SoftUniContext dbContext)
    {
        string realJobTitle = "Marketing Assistant";
        string jobTitleSQLinjection = "' OR 1=1--";

        // Weak query that is vulnerable to injection
        string weakQuery = "SELECT * FROM Employees WHERE JobTitle = '" + jobTitleSQLinjection + "'";

        var employeesWeakQuery = await dbContext.Employees
            .FromSqlRaw(weakQuery)
            .ToArrayAsync();

        Console.WriteLine(employeesWeakQuery.Count());

        // Strong query that will detect the injection
        string strongQuery = "SELECT * FROM Employees WHERE JobTitle = {0}";

        var employeesStrongQuery = await dbContext.Employees
           .FromSqlRaw(strongQuery, jobTitleSQLinjection)
           .ToArrayAsync();
        Console.WriteLine(employeesStrongQuery.Count());

        // To avoid SQL injection, you must use a placeholder in the Native SQL query syntax
        // Always the query must be parameterized.
        // Never use concatenation

        return employeesStrongQuery;
    }
}

