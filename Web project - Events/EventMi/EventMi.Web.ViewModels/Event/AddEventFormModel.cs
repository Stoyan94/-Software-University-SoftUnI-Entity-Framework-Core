using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using EventMiMVC.Web.Common;

namespace EventMi.Web.ViewModels.Event
{
    using static EntityConstraint;
    public class AddEventFormModel
    {
        [StringLength(EventNameMaxLength, MinimumLength = EventNameMinLength)]
        public required string Name { get; set; } = null!;

        public required string StartDate { get; set; } = null!;

        public required string EndDate { get; set; } = null!;

        [StringLength(EventPlaceMaxLength, MinimumLength = EventPlaceMinLength)]

        public required string Place { get; set; } = null!;
    }
}
