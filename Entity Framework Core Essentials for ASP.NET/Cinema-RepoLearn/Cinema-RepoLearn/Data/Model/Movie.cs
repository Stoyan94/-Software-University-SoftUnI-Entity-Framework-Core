﻿using System.ComponentModel.DataAnnotations;

namespace Cinema_RepoLearn.Data.Model
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = null!;

        [StringLength(500)]
        public string? Description { get; set; }

        public List<Schedule> Schedules { get; set; } 
            = new List<Schedule>();
    }
}