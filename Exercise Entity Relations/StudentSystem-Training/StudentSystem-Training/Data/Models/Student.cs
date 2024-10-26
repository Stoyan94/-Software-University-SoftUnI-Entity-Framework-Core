namespace StudentSystem_Training.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(100)]
        [Unicode]
        public string Name { get; set; } = null!;

        [MinLength(10)]
        [MaxLength(10)]
        public string? PhoneNumber { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime? Birthday { get; set; }

        public List<Homework> Homework { get; set; } = new List<Homework>();
        public List<StudentCourses> StudentCourses { get; set; } = new List<StudentCourses>();
    }
}
