namespace TravelAgency.DataProcessor.ExportDtos
{
    public class ExportCustomerDTO
    {
        public string FullName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;

        public ExportBookingDTO[] Bookings { get; set; } = default!;
    }
}
