using Microsoft.VisualBasic;
using SoftUni.Data;
using SoftUni.Models;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SoftUni;

public class StartUp
{
    static void Main(string[] args)
    {
        SoftUniContext dbContext = new SoftUniContext();

        string result = GetEmployeesByFirstNameStartingWithSa(dbContext);
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

    public static string GetEmployeesInPeriod(SoftUniContext context)
    {
        StringBuilder output = new StringBuilder();

        var employeesWithProjects = context.Employees
            .Take(10)
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                ManagerFirstName = e.Manager!.FirstName,
                ManagerLastName = e.Manager!.LastName,
                Projects = e.EmployeesProjects
                    .Where(ep => ep.Project.StartDate.Year >= 2001 &&
                                 ep.Project.StartDate.Year <= 2003)
                    .Select(ep => new
                    {
                        ProjectName = ep.Project.Name,
                        StartDate = ep.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                        EndDate = ep.Project.EndDate.HasValue
                            ? ep.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                            : "not finished"
                    })
                    .ToArray()
            })
            .ToArray();

        foreach (var e in employeesWithProjects)
        {
            output
                .AppendLine($"{e.FirstName} {e.LastName} - Manager: {e.ManagerFirstName} {e.ManagerLastName}");

            foreach (var p in e.Projects)
            {
                output
                    .AppendLine($"--{p.ProjectName} - {p.StartDate} - {p.EndDate}");
            }
        }

        return output.ToString().TrimEnd();
    }

    public static string GetAddressesByTown(SoftUniContext context)
    {
        var emoloyeesAdresses = context.Addresses
           .OrderByDescending(e => e.Employees.Count())
           .ThenBy(e => e.Town!.Name)
           .ThenBy(e => e.AddressText)
           .Take(10)
              .Select(e => $"{e.AddressText}, {e.Town!.Name} - {e.Employees.Count} employees")
                .ToArray();

        return string.Join(Environment.NewLine, emoloyeesAdresses);
    }

    public static string GetEmployee147(SoftUniContext context)
    {
        StringBuilder output = new StringBuilder();

        var findEmployeeByID = context.Employees
            .Where(e => e.EmployeeId == 147)
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                e.JobTitle,
                Projects = e.EmployeesProjects
                          .Select(e => new
                          {
                              e.Project.Name
                          })
            })
            .ToList();

        foreach (var e in findEmployeeByID)
        {
            output.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");

            foreach (var project in e.Projects.OrderBy(e=> e.Name))
            {
                output.AppendLine($"{project.Name}");
            }
        }
                
        return output.ToString().TrimEnd();
    }

    public static string GetLatestProjects(SoftUniContext context)
    {
        StringBuilder output = new StringBuilder();

        var employeeLastProjects = context.Projects
            .OrderByDescending(ep => ep.StartDate)
            .Take(10)
            .Select(ep => new
            {
                Names = ep.Name,
                ep.Description,
                DateTime = ep.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
            })
            .OrderBy(ep => ep.Names)
            .ToArray();
           

        foreach (var employee in employeeLastProjects)
        {
            output.AppendLine($"{employee.Names}")
                  .AppendLine($"{employee.Description}")
                  .AppendLine($"{employee.DateTime}");
        }

        return output.ToString().TrimEnd();
    }

    public static string IncreaseSalaries(SoftUniContext context)
    {
        StringBuilder output = new StringBuilder();

        var selectedDepartments = context.Employees
           .Where(e => e.Department.Name == "Engineering" 
           || e.Department.Name == "Tool Design" 
           || e.Department.Name == "Marketing" 
           || e.Department.Name == "Information Services")
           .Select(e => new
           {
              e.FirstName,
              e.LastName,
              e.Department, 
              IncreaseSalary = e.Salary * 1.12m
           })
           .OrderBy(e=> e.FirstName)
           .ThenBy(e => e.LastName)
           .ToArray();
       
        context.SaveChanges();

        foreach (var employee in selectedDepartments)
        {
            output.AppendLine($"{employee.FirstName} {employee.LastName} (${employee.IncreaseSalary:f2})");
        }
       
        
        return output.ToString().TrimEnd();
    }

    public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
    {
        StringBuilder output = new StringBuilder();

        var employeeStartWith = context.Employees
            .Where(e => e.FirstName.StartsWith("Sa"))
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                e.JobTitle,
                e.Salary,
            })
            .OrderBy(e => e.FirstName)
            .ThenBy(e => e.LastName)
            .ToArray();

        foreach (var emp in employeeStartWith)
        {
            output.AppendLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle} - (${emp.Salary:f2})");
        }
       
        return output.ToString().TrimEnd();
    }
}

