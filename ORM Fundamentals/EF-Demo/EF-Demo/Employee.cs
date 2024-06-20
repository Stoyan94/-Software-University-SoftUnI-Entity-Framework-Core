using System.ComponentModel.DataAnnotations;

namespace EF_Demo
{
    public class Employee
    {
        [Key]
        public int EmplyeeID { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string JobTitle { get; set; }

        public string DepartmentId { get; set; }

        public int MnagerId { get; set; }

        public DateTime HireDate { get; set; }

        public decimal Salary { get; set; }

        public int AddresId { get; set; }

        public bool IsDeleted { get; set; }

        public Department Department { get; set; }

    }
}
