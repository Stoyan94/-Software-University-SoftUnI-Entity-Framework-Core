using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Demo
{
    public class Department
    {
        public int DepartmentID { get; set; }

        public string Name { get; set; }

        public int ManagerID { get; set; }

        [ForeignKey(nameof(ManagerID))]
        public Employee Manager { get; set; }

        //public ICollection<Employee> Employees { get; set; }
    }
}
