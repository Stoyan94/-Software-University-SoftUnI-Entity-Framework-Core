namespace Boardgames.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstraints;
    public class Seller
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(SellerNameMaxLenght)]
        public string Name { get; set; } = null!;

        [MaxLength(SellerAddressMaxLenght)]
        public string Address { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string Website { get; set; } = null!;

        // TOD: Add nav prop
    }
}
