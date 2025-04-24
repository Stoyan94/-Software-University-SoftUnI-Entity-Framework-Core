using Microsoft.EntityFrameworkCore;
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

        Console.WriteLine(GetDepartmentsWithMoreThan5Employees(dbContext));

        //string result = DeleteProjectById(dbContext);
        //Console.WriteLine(result);

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

        var employees = context.Employees
            .Take(10)
            .Select(e => new
            {
                EmployeeFullName = $"{e.FirstName} {e.LastName}",
                ManagerFullName = $"{e.Manager.FirstName} {e.Manager.LastName}",
                Projects = e.EmployeesProjects
                .Where(emp=> emp.Project.StartDate.Year >= 2001 && emp.Project.StartDate.Year <= 2003)
                .Select(p => new
                {
                    p.Project.Name,
                    StartDate = p.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt",CultureInfo.InvariantCulture),
                    EndDate = p.Project.EndDate.HasValue ?
                              p.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture) :
                              "not finished"
                }).ToList()
               
            })
            .ToList();

        foreach (var e in employees)
        {
            output.AppendLine($"{e.EmployeeFullName} {e.ManagerFullName}");

            foreach (var p in e.Projects)
            {
                output.AppendLine($"{p.Name} {p.StartDate} {p.EndDate}");
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

    public static string DeleteProjectById(SoftUniContext context)
    {
        StringBuilder output = new StringBuilder();

        //Delete all rows from mapping entity EmployeesProjects ther refer Project with Id = 2
        IQueryable<EmployeeProject> empToDelete = context.EmployeesProjects
            .Where(e => e.ProjectId == 2);
        context.RemoveRange(empToDelete);

        var projectToDelete = context.Projects.Find(2)!;
        context.Projects.Remove(projectToDelete);
        context.SaveChanges();

        var projectsName = context.Projects
            .Take(10)
            .Select(p => p.Name)
            .ToArray();

        return String.Join(Environment.NewLine, projectsName);
    }

    public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
    {
        StringBuilder output = new StringBuilder();

        var deparments = context.Departments
            .Where(e => e.Employees.Count() > 5)
            .OrderBy(e => e.Employees.Count())
            .ThenBy(d => d.Name)
            .Select(empD => new
            {
                DepartmentInfo = $"{empD.Name} - {empD.Manager.FirstName} {empD.Manager.LastName}",
                Employees = empD.Employees
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .Select(empInfo => new
                {
                   EmployeeFullInfo = $"{empInfo.FirstName} {empInfo.LastName} - {empInfo.Department.Name}",
                    
                })
                .ToList()

            }).ToList();

        foreach ( var emp in deparments )
        {
            output.AppendLine(emp.DepartmentInfo);

            foreach (var dInfo in emp.Employees)
            {
               output.AppendLine(dInfo.EmployeeFullInfo);
            }
        }

        return output.ToString().TrimEnd();
    }


    public static string RemoveTown(SoftUniDbContext context)
    {
        // 1. Намери града
        var townToDelete = context.Towns
            .FirstOrDefault(t => t.Name == "Seattle");

        if (townToDelete == null)
        {
            return "Town 'Seattle' not found.";
        }

        // 2. Извади всички адреси, свързани с града
        var addressesToDelete = context.Addresses
            .Where(a => a.TownId == townToDelete.TownId)
            .ToArray();

        // 3. Намери служителите, които живеят на тези адреси
        var employeesToUpdate = context.Employees
            .Where(e => addressesToDelete.Contains(e.Address))
            .ToList();

        //4.Занули адресите им
        employeesToUpdate.ForEach(e => e.AddressId = null);

        // 5. Изтрий адресите и града
        context.Addresses.RemoveRange(addressesToDelete);
        context.Towns.Remove(townToDelete);

        // 6. Запази промените
        context.SaveChanges();

        // 7. Върни съобщение
        return $"{addressesToDelete.Length} addresses in Seattle were deleted";
    }
}

