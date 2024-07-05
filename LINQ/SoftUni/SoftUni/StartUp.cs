using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SoftUni.Data;
using SoftUni.Models;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Xml.Linq;


namespace SoftUni;

public class StartUp
{
    static async Task Main(string[] args)
    {
        using SoftUniContext dbContext = new SoftUniContext();

        List<EmployeeDTO> emp = await AsyncAndDTO(dbContext);

        foreach (var e in emp)
        {
            Console.WriteLine($"{e.FirstName} {e.LastName} {e.JobTitle}");
        }
        //----------------------------------------------------

        Console.WriteLine(await JoinAgg(dbContext));      
    }

    private static async Task<List<EmployeeDTO>> AsyncAndDTO(SoftUniContext dbContext)
    {
        return await dbContext.Employees
                             .Where(e => e.DepartmentId == 1)
                             .Select(e => new EmployeeDTO()
                             {
                                 FirstName = e.FirstName,
                                 LastName = e.LastName,
                                 JobTitle = e.JobTitle
                             }).ToListAsync();
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
                   .Include(e => e.Department.Name).
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

