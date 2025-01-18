using System.ComponentModel.DataAnnotations;
using EventMiMVC.Web.Common;

namespace EventMi.Web.ViewModels.Event
{
    using static EntityConstraint;
    public class EditEventFormModel
    {
        [StringLength(EventNameMaxLength, MinimumLength = EventNameMinLength)]
        public required string Name { get; set; } = null!;

        public required string StartDate { get; set; } = null!;

        public required string EndDate { get; set; } = null!;

        [StringLength(EventPlaceMaxLength, MinimumLength = EventPlaceMinLength)]

        [MaxLength(EventDescriptionMaxLength)]
        public string? Description { get; set; }
        public required string Place { get; set; } = null!;

    }
}
