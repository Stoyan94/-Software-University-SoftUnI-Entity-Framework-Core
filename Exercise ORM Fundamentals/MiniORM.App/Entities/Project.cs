using System.ComponentModel.DataAnnotations;


namespace MiniORM.App.Entities
{
    public class Project
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }

        public ICollection<EmployeeProject> EmployeesProjects { get; set; } 
    }
}
