using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Data.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime? BirthDay { get; set; }
    }
}
