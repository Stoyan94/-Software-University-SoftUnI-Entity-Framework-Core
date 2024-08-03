using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.DataProcessor.ImportDtos
{
    public class JsonImportBooking
    {
        [Required]
        [JsonProperty("BookingDate")]
        public string BookingDate { get; set; } = null!;

        [Required]
        [JsonProperty("CustomerName")]
        public string CustomerName { get; set; } = null!;

        [Required]
        [JsonProperty("TourPackageName")]
        public string TourPackageName { get; set; } = null!;
    }
}
