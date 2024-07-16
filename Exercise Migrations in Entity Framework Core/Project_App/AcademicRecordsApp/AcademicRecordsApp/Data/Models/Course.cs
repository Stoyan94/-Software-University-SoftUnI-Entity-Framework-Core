namespace AcademicRecordsApp.Data.Models;

public class Course
{
    public Course()
    {
        this.Students = new HashSet<Student>();
    }
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; }
}
