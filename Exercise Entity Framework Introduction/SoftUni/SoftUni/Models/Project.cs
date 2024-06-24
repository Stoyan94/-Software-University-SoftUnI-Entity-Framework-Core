namespace SoftUni.Models;

public class Project
{
    public Project()
    {
        this.EmployeeProjects = new HashSet<EmployeeProject>();
    }

    public int ProjectId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }
}

