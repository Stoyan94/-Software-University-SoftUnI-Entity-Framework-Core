﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema_RepoLearn.Data.Model
{
    public class Tariff
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Film))]
        public int FilmId { get; set; }
        public Movie Film { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        public List<Ticket> Tickets { get; set; } 
            = new List<Ticket>();
    }
}