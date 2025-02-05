﻿using System.ComponentModel.DataAnnotations;

namespace MigrationsDemo.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Age { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; } = null!;

        public string City { get; set; } = null!;
    }
}
