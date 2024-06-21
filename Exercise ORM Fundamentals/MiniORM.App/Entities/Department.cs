using System.ComponentModel.DataAnnotations;

namespace MiniORM.App.Entities
{
    public class Department
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
