namespace AcademicRecordsApp.Data.Models;

public partial class Student
{
    public Student()
    {
        this.Grades = new HashSet<Grade>();
        this.Courses = new HashSet<Course>();
    }

    public int Id { get; set; }
    public string FullName { get; set; } = null!;

    public virtual ICollection<Grade> Grades { get; set; }

    public virtual ICollection<Course> Courses { get; set; }
}
