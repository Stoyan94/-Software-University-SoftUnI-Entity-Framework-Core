using SoftUni.Data;
using SoftUni.Models;
using System.Text;

namespace SoftUni;

public class StartUp
{
    static void Main(string[] args)
    {
        SoftUniContext dbContext = new SoftUniContext();

        string result = AddNewAddressToEmployee(dbContext);
        Console.WriteLine(result);

        //var employees = dbContext.Employees.Where(e=> e.EmployeeId == 1);

        //foreach ( var employee in employees ) 
        //{
        //    Console.WriteLine($"{employee.FirstName}");
        //}
    }
    public static string GetEmployeesFullInformation(SoftUniContext context)
    {
        StringBuilder output = new StringBuilder();

        var employees = context.Employees
            .OrderBy(e => e.EmployeeId)
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                e.MiddleName,
                e.JobTitle,
                e.Salary
            })
            .ToArray(); //ToList() - Detach from the change tracker, any changes on the data inside will not be saved into db

        foreach (var employee in employees)
        {
            output.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
        }

        return output.ToString().TrimEnd();
    }

    public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
    {
        StringBuilder output = new StringBuilder();

         var employeesSalary = context.Employees
            .Where(e => e.Salary > 50000)
            .Select(e => new
            {
                e.FirstName,
                e.Salary
            }).OrderBy(e => e.FirstName).ToArray();

        foreach (var employee in employeesSalary)
        {
            output.AppendLine($"{employee.FirstName} - {employee.Salary:f2}");
        }       

        return output.ToString().TrimEnd();
    }

    public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
    {
        StringBuilder output = new StringBuilder();

        var employeesRandDdepartment = context.Employees
            .Where(e => e.Department.Name == "Research and Development")
            .OrderBy(e => e.Salary)
            .ThenByDescending(e => e.FirstName)
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                DepartmentName = e.Department.Name,
                e.Salary
            }).ToArray();

        foreach (var employee in employeesRandDdepartment)
        {
            output.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.DepartmentName} - ${employee.Salary:f2}");
        }

        return output.ToString().TrimEnd();
    }

    public static string AddNewAddressToEmployee(SoftUniContext context)
    {
        Address newAdressEmployee = new Address()
        {
            AddressText = "Vitoshka 15",
            TownId = 4
        };

        var employeeUpdateInfo = context.Employees
            .FirstOrDefault(e => e.LastName == "Nakov");
        employeeUpdateInfo!.Address = newAdressEmployee;

        context.SaveChanges();

        var employee = context.Employees
            .OrderByDescending(e => e.AddressId)
            .Take(10)
            .Select(e => e.Address!.AddressText).ToArray();

        return string.Join(Environment.NewLine, employee);
    }

}

