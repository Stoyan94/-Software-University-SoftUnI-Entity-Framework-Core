using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace NetPay.Data.Models
{
    using static DataConstraints;
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(SupplierNameMaxLenght)]
        public string SupplierName { get; set; } = null!;

        public virtual ICollection<SupplierService> SuppliersServices { get; set; }
            = new HashSet<SupplierService>();

        //        •	SupplierName – text with length[3, 60] (required)
        //•	SuppliersServices - collection of type SupplierService

    }
}
