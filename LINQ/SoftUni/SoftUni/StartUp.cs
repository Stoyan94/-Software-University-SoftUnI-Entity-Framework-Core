using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;


namespace SoftUni;

public class StartUp
{
    static async void Main(string[] args)
    {
       using SoftUniContext dbContext = new SoftUniContext();

        // SELECT * FROM Employees WHERE DepartmentId = 1
        IQueryable<Employee> employees = from employee in dbContext.Employees
                        where employee.DepartmentId == 1
                        select employee; // this is classic (old) LINQ

        foreach (var e in employees)
        {
            Console.WriteLine(e.FirstName);
        }


        var emp = await dbContext.Employees
            .Where(e => e.DepartmentId == 1)
            .ToListAsync();

        foreach (var e in emp)
        {
            Console.WriteLine(e.FirstName);
        }
    }
  
}

