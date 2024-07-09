using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;
using System.Drawing;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace SoftUni;

public class StartUp
{
    static async Task Main(string[] args)
    {
        using SoftUniContext dbContext = new SoftUniContext();

        var employees = await dbContext.Employees
            .Where(e => e.DepartmentId == 1)
            .Include(e => e.Department)
            .ToListAsync();

        //await ExplicitLoading(dbContext);
        //ExecutingStoredProcedure(dbContext);
        //await Console.Out.WriteLineAsync(await SQLInjectionDefense(dbContext));
    }

    private static async Task ExplicitAndEagerLoadingIssue(SoftUniContext dbContext)
    {
        var employees = await dbContext.Employees
            .Where(e => e.DepartmentId == 1)
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                DepartmentName = e.Department.Name,
            })
            .ToListAsync();
        //.Include(e => e.Departments);

        var neesh = dbContext.Entry(employees);

        // Employees table projecting the result into an anonymous object containing FirstName, LastName, and DepartmentName. 
        //     The issue with this code lies in how you're attempting to access e.Department.
        //     Name within the projection. To understand why eager or explicit loading cannot be used directly in this context.
        // 
        // Reasons:
        //         Lack of Eager Loading Setup:
        // 
        //         The Select method immediately creates a projection, which means the query will only include the columns specified in the projection. 
        //         At this point, there's no instruction to include the related Department entity.

        // Projection Limits:  
        //      When you project into an anonymous type, the Entity Framework cannot automatically include related entities because it is not aware of the need to include them before the projection.
        // 
        // Explicit Loading Not Available:        
        //      Explicit loading(.Load()) requires an already existing entity to load related data into.
        //      Since you're directly projecting into an anonymous type, there's no entity on which to call the Load method.
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

