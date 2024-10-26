using StudentSystem_Training.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentSystem_Training.Data.Models
{
    public class Homework
    {
        [Key]
        public int HomeWorkId { get; set; }

        public Content Content { get; set; }

        public double SubissionTime { get; set; }

        public int StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; } = null!;

        public int CourseId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public Course Course { get; set; } = null!;
    }
}
