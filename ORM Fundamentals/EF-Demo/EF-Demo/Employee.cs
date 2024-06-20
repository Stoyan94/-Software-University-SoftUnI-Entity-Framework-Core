using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Demo
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string LastName { get; set; }

        public string JobTitle { get; set; }

        public int DepartmentID { get; set; }

        public int ManagerID { get; set; }  

        public DateTime HireDate { get; set; }

        public decimal Salary { get; set; }

        public int AddressID { get; set; }

       public bool IsDeleted { get; set; }

        [ForeignKey(nameof(DepartmentID))]
        public Department Department { get; set; }

        [ForeignKey(nameof(ManagerID))]
        public Employee Manager { get; set; }

    }
}
