namespace EventMiMVC.Web.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityConstraint;
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EventNameMaxLength)]
        public required string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        [MaxLength(EventPlaceMaxLength)]
        public required string Place { get; set; }

        [MaxLength(EventDescriptionMaxLength)]
        public string? Description { get; set; }

        public required bool? IsActive { get; set; }
    }
}
