
using EF_Demo;
using Microsoft.EntityFrameworkCore;

var context = new SoftUniDbContext();

var employees = await context.Employees.
    Where(e => e.DepartmentID == 3).
    ToListAsync();

foreach (var employee in employees)
{
    Console.WriteLine($"{employee.FirstName} {employee.LastName}: {employee.JobTitle}");
}