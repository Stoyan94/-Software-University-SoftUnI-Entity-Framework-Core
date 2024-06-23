using DBFirst_Demo.infrastructure.Data;
using DBFirst_Demo.infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

using SoftUniDbContext context = new SoftUniDbContext();

var employees = await context.Employees
    .Select(e => new
    {
        e.FirstName,
        e.LastName,
        e.Salary
    }).ToListAsync();



foreach (var employee in employees)
{
    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.Salary}");
}

var employeesSqlQury = context.Employees
    .Select(e => new
                 {
                     e.FirstName,
                     e.LastName,
                     e.Salary
                 }).ToQueryString();

Console.WriteLine(employeesSqlQury); // SELECT [e].[FirstName], [e].[LastName], [e].[Salary]
                                     // FROM[Employees] AS[e]