using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace StudentSystem_Training.Data.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        [Unicode]
        [MaxLength(80)]
        public string Name { get; set; } = null!;

        [Unicode]
        [MaxLength(500)]
        public string? Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }

        public List<Resource> Resources { get; set; } = new List<Resource>();
        public List<Homework> Homework { get; set; } = new List<Homework>();
        public List<StudentCourses> StudentCourses { get; set; } = new List<StudentCourses>();
    }
}
