﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SoftUni;

public class StartUp
{
    static async Task Main(string[] args)
    {
        using SoftUniContext dbContext = new SoftUniContext();



        //await ExplicitAndEagerCombination(dbContext);
        //await ExplicitLoading(dbContext);
        //ExecutingStoredProcedure(dbContext);
        //await Console.Out.WriteLineAsync(await SQLInjectionDefense(dbContext));
        //await ExplicitAndEagerLoadingIssue(dbContext);
        //await EnrtyExample(dbContext);
    }

    private static async void ConcurrencyChecks()
    {
        using SoftUniContext dbContext = new SoftUniContext();
        using SoftUniContext ctx = new SoftUniContext();

        var pr1 = await dbContext.Employees
            .Where(e => e.EmployeeId == 1)
            .Select(e => e.EmployeesProjects.First())
            .FirstOrDefaultAsync();

        pr1.Project.Name = "First";

        var pr2 = await ctx.Employees
            .Where(e => e.EmployeeId == 1)
            .Select(e => e.EmployeesProjects.First())
            .FirstOrDefaultAsync();

        pr2.Project.Name = "Second";

        await dbContext.SaveChangesAsync();
        await ctx.SaveChangesAsync();
    }
    private static async Task EnrtyExample(DbContext dbContext)
    {
        Employee? employee;

        using (SoftUniContext context = new SoftUniContext())
        {
            employee = await context.FindAsync<Employee>(1);
        }

        if (employee != null!)
        {
            employee.MiddleName = "Ralf";

            using SoftUniContext ctx = new SoftUniContext();

            var entry = ctx.Entry(employee);
            entry.State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        //A new SoftUniContext is created for saving changes. (Not reusing the previous context is a good practice in some scenarios.)
        //    ctx.Entry(employee): Attaches the employee object to the new context.
        //    entry.State = EntityState.Modified: Marks the entity as "Modified," indicating EF Core should update it in the database.

    }
    private static async Task ExplicitAndEagerCombination(SoftUniContext dbContext)
    {
        var employees = await dbContext.Employees
            .Where(e => e.DepartmentId == 1)
            .Include(e => e.Department)
            .ToListAsync();

        foreach (var employee in employees)
        {
            if (employee.EmployeeId == 3)
            {
                var entry = dbContext.Entry(employee);
                await entry.Reference(e => e.Address).LoadAsync();

                await Console.Out.WriteLineAsync("The one for who we need address information and Department Name");
                await Console.Out.WriteLineAsync($"{employee.FirstName} {employee.LastName} {employee.Department.Name} - {employee.Address.AddressText}");
                Console.WriteLine();
            }
            else
            {
                await Console.Out.WriteLineAsync("Employees for whose addresses information are not needed");
                await Console.Out.WriteLineAsync($"{employee.FirstName} {employee.LastName}");
            }
        }

        //Eager Loading: The Include(e => e.Department) method is used to eagerly load the related Department entity for each Employee. 
        //               This means that the Department data is retrieved from the database as part of the initial query,
        //               avoiding the need for additional queries to get this related data later.

        //Explicit Loading: Inside the loop, for the specific employee with EmployeeId == 3, explicit loading is used to load the related Address entity.
        //    The dbContext.Entry(employee).Reference(e => e.Address).LoadAsync() method call loads the Address entity for this particular employee on demand, after the initial query has been executed.
    }
    private static async Task ExplicitAndEagerLoadingIssue(SoftUniContext dbContext)
    {
        var employees = await dbContext.Employees
        .Where(e => e.DepartmentId == 1)
                .Include(e => e.Departments)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    DepartmentName = e.Department.Name,
                })
                .ToListAsync();

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

        // Both techniques are universal and can be used to load any related data defined by navigation properties, 
        // not just data linked by foreign keys.
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

