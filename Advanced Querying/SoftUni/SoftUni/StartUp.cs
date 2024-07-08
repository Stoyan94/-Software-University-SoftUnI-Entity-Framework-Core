using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using System.Text;

namespace SoftUni;

public class StartUp
{
    static async Task Main(string[] args)
    {
        using SoftUniContext dbContext = new SoftUniContext();
        await ExplicitLoading(dbContext);

        

        ExecutingStoredProcedure(dbContext);
        //await Console.Out.WriteLineAsync(await SQLInjectionDefense(dbContext));
    }

    private static async Task ExplicitLoading(SoftUniContext dbContext)
    {
        var employee = await dbContext.Employees.FindAsync(1);
        var enrty = dbContext.Entry(employee);

        await enrty.Reference(e => e.Address).LoadAsync(); // represent a single related entity
        await enrty.Collection(e => e.EmployeesProjects).LoadAsync(); //represent a collection of related entities

        //Explicit loading in Entity Framework Core allows you to load related data that is connected through foreign keys in your entities.
        //Loads data immediately at the moment we wanted.
    }

    private static void ExecutingStoredProcedure(SoftUniContext dbContext)
    {
        SqlParameter salaryParameter = new SqlParameter("@salary", 5);
        string query = "EXEC UpdateSalary @salary";
        dbContext.Database.ExecuteSqlRaw(query, salaryParameter);
    }

    private static async Task<string> SQLInjectionDefense(SoftUniContext dbContext)
    {
        StringBuilder output = new StringBuilder();

        string realJobTitle = "Marketing Assistant";
        string jobTitleSQLinjection = "' OR 1=1--";

        // Weak query that is vulnerable to injection
        string weakQuery = "SELECT * FROM Employees WHERE JobTitle = '" + jobTitleSQLinjection + "'";

        var employeesWeakQuery = await dbContext.Employees
            .FromSqlRaw(weakQuery)
            .ToArrayAsync();

        output.AppendLine($"All data stolen {employeesWeakQuery.Count()}");


        // Strong query that will detect the injection
        string strongQuery = "SELECT * FROM Employees WHERE JobTitle = {0}";

        var employeesStrongQuery = await dbContext.Employees
           .FromSqlRaw(strongQuery, jobTitleSQLinjection)
           .ToArrayAsync();
        output.AppendLine($"Unsuccessful attack {employeesStrongQuery.Count()}"); // 0

        // To avoid SQL injection, you must use a placeholder in the Native SQL query syntax
        // Always the query must be parameterized.
        // Never use concatenation

        FormattableString query = $"SELECT * FROM Employees WHERE JobTitle = {jobTitleSQLinjection}";

        var employeesInterpolatedQue = await dbContext.Employees
            .FromSqlInterpolated(query)
            .ToArrayAsync();

        // If we want to use an interpolated string ($"Some text {string}") we must use
        // FormattableString to set the query type, because if you use "var" C# will convert it to a normal "string"
        // making the query vulnerable to SQL injection

        output.AppendLine($"Unsuccessful attack {employeesInterpolatedQue.Count()}"); // 0

        return output.ToString().TrimEnd();
    }
}

