using System.ComponentModel.DataAnnotations;

namespace TravelAgency.DataProcessor.ImportDtos
{
    using static Data.DataConstraints;
    public class JsonImportBooking
    {
        [Required]
        public string BookingDate { get; set; } = null!;

        [Required]
        [MinLength(CustomerEmailMinLenght)]
        [MaxLength(CustomerEmailMaxLenght)]
        public string CustomerName { get; set; } = null!;

        [Required]
        [MinLength(TourPackageNameMinLenght)]
        [MaxLength(TourPackageNameMaxLenght)]
        public string TourPackageName { get; set; } = null!;
    }
}
