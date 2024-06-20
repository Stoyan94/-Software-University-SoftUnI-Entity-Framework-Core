
using EF_Demo;
using Microsoft.EntityFrameworkCore;

var context = new SoftUniDbContext();

var employees = await context.Employees.
    Where(e => e.DepartmentID == 3).
    Select(e => new 
    { 
        e.FirstName,
        e.LastName,
        e.JobTitle,
        DepartmentName = e.Department.Name,
    }).
    ToListAsync();

foreach (var employee in employees)
{
    Console.WriteLine($"{employee.DepartmentName} - {employee.FirstName} {employee.LastName}: {employee.JobTitle}");
}