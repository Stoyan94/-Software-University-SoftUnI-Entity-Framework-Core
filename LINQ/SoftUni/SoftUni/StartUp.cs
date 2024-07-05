using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SoftUni.Data;
using SoftUni.Models;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Xml.Linq;
using System;
using Microsoft.EntityFrameworkCore.Storage;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Runtime.Intrinsics.X86;

namespace SoftUni;

public class StartUp
{
    static async Task Main(string[] args)
    {
        using SoftUniContext dbContext = new SoftUniContext();

        Console.WriteLine(await GroupByMethod(dbContext));
        Console.WriteLine(await AsyncAndDTO(dbContext));      
        Console.WriteLine(await JoinAgg(dbContext));  
    }

    private static async Task<string> GroupByMethod(SoftUniContext dbContext)
    {
        StringBuilder sb = new StringBuilder();

        var employees = await dbContext.Employees.ToListAsync();

        var emp = employees
        .GroupBy(e => e.JobTitle)
                 .ToList();
        // Snippet 1:
            // Executes the initial query on the database to load all employees.
            // Performs the grouping operation in memory on the client side.


        var emp1 = await dbContext.Employees
            .GroupBy(e => e.JobTitle)
            .Select(grp => new
            {
                JobTitle = grp.Key,
                Salary = grp.Sum(e => e.Salary)
            })
            .ToListAsync();
        // Snippet 2:
        // Returns the results directly from the database without loading all employee records into memory.
        // Executes the entire query on the database, including the grouping and selection of keys.

        foreach (var item in emp)
        {
            Console.WriteLine(item);
        }

        foreach (var e in emp1)
        {
            sb.AppendLine($"{e.JobTitle} - {e.Salary:f2}");
        }

        // Summary
        // Snippet 1 is less efficient because it loads all employee records into memory before grouping them.
        //     It results in a collection of groups of employees by job title.
        //
        // Snippet 2 is more efficient because it performs the grouping directly in the database and only retrieves the distinct job titles.
        //     It results in a list of job titles.
        //
        // When to Use Each Approach
        // Use Snippet 1 if you need to perform further processing on the grouped employees in memory and are dealing with a manageable amount of data.
        //
        // Use Snippet 2 if you only need the distinct job titles or if you are dealing with a large dataset and want to optimize performance by offloading the grouping to the database.

        return sb.ToString().TrimEnd();

        //When we use in EF with LINQ GroupBy, we must comply with the rules based on the use of GroupBy 
        //    => All Columns Must Be Aggregated or Grouped: 
        //        Every column in the SELECT list must either be part of the GROUP BY clause or be used with an aggregate function(e.g., COUNT, SUM, AVG, MAX, MIN).
    }
    private static async Task<string> AsyncAndDTO(SoftUniContext dbContext)
    {
        StringBuilder sb = new StringBuilder();
        var em = await dbContext.Employees
        .Where(e => e.DepartmentId == 1)
        .Select(e => new EmployeeDTO()
        {
            FirstName = e.FirstName,
            LastName = e.LastName,
            JobTitle = e.JobTitle
        }).ToListAsync();
        foreach (var e in em)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} {e.JobTitle}");
        }
        return sb.ToString().TrimEnd();
    }
    private static async Task<string> JoinAgg(SoftUniContext dbContext)
    {
        StringBuilder sb = new StringBuilder();
        var emp1 = await dbContext.Employees
        .Where(e => e.DepartmentId == 1)
        .Join(
        dbContext.Departments,
        e => e.DepartmentId,
        d => d.DepartmentId,
        (e, d) => new
        {
            e.FirstName,
            e.LastName,
            e.JobTitle,
            d.Name
        })
        .ToListAsync();
        var emp = await dbContext.Employees
        .Where(e => e.DepartmentId == 1)
        .Select(e => new
        {
            e.FirstName,
            e.LastName,
            e.JobTitle,
            e.Department.Name
                    })
        .ToListAsync();

        var emp2 = await dbContext.Employees
                   .Where(e => e.DepartmentId == 1) //Include: However, it does not support specifying individual properties of the related entity.
                                                    //Instead, it is used to include the entire related entity.
                   .Include(e => e.Department).
                   ToListAsync();

        // While both queries aim to achieve the same result(retrieving employee details along with their department name), 
        //     Query 2 is preferred due to its simplicity and readability, provided that the Employee entity has a proper navigation property to the Department entity. 
        //     Both should produce equivalent SQL and perform similarly in most scenarios.
        // To know that EF should do Join, we must use Select

        foreach (var e in emp)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} {e.JobTitle}");
        }

        return sb.ToString().TrimEnd();
    }
}


//// SELECT * FROM Employees WHERE DepartmentId = 1
//IQueryable<Employee> employees = from employee in dbContext.Employees
//                where employee.DepartmentId == 1
//                select employee; // this is classic (old) LINQ

//List<Employee> emp = await employees.ToListAsync();


//List<Employee> emp = await dbContext.Employees
//    .Where(e => e.DepartmentId == 1)
//    .ToListAsync();

//foreach (var e in emp)
//{
//    Console.WriteLine(e.FirstName);
//}

