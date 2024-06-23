using DBFirst_Demo.Data;

using SoftUniDbContext context = new SoftUniDbContext();

var employees = context.Employees
    .Select(e=> new
    {
        e.FirstName,
        e.LastName,
        e.Salary
    }).ToList();

foreach (var employee in employees)
{
    Console.WriteLine($"{employee.FirstName} { employee.LastName} - {employee.Salary}");
}