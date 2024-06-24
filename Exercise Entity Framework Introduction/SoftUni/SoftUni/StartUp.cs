using SoftUni.Data;

namespace SoftUni;

public class StartUp
{
    static void Main(string[] args)
    {
        SoftUniContext dbContext = new SoftUniContext();

        var employees = dbContext.Employees.Where(e=> e.EmployeeId == 1);

        foreach ( var employee in employees ) 
        {
            Console.WriteLine($"{employee.FirstName}");
        }
    }
}

