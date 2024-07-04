using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;


namespace SoftUni;

public class StartUp
{
    static async void Main(string[] args)
    {
       using SoftUniContext dbContext = new SoftUniContext();

        var emp = await dbContext.Employees
            .Where(e => e.DepartmentId == 1)
            .Select(e => new EmployeeDTO()
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                JobTitle = e.JobTitle
            }).ToListAsync();

        foreach (var e in emp)
        {
            Console.WriteLine($"{e.FirstName} {e.LastName} {e.JobTitle}");
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
    }

}

