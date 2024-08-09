using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace NetPay.Data.Models
{
    using static DataConstraints;
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(ServiceNameMaxLenght)]
        public string ServiceName { get; set; } = null!;

        public virtual ICollection<Expense> Expenses { get; set; }
            = new HashSet<Expense>();

        public virtual ICollection<SupplierService> SuppliersServices { get; set; } 
            = new HashSet<SupplierService>();

    }
}
