using System.ComponentModel.DataAnnotations;
using TravelAgency.Data.Models.Enums;

namespace TravelAgency.Data.Models
{
    using static DataConstraints;
    public class Guide
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(GuideFullNameMaxLenght)]
        public string FullName { get; set; } = null!;

        public Language Language { get; set; }

        public virtual ICollection<TourPackageGuide> TourPackagesGuides { get; set; }
            = new HashSet<TourPackageGuide>();
    }
}
