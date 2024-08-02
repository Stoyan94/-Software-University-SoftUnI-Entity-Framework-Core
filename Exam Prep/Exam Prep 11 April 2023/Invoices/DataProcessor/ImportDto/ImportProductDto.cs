using System.ComponentModel.DataAnnotations;

namespace Invoices.DataProcessor.ImportDto
{
    using static Data.DataConstraints;
    public class ImportProductDto
    {
        [Required]
        [MinLength(ProductNameMinLength)]
        [MaxLength(ProductNameMaxLength)]
        public string Name { get; set; } = null!;

        [Range(typeof(decimal), ProductPriceMinValue, ProductPriceMaxValue)]
        public decimal Price { get; set; }

        [Range(ProductCategoryTypeMinValue, ProductCategoryTypeMaxValue)]
        public int CategoryType { get; set; }

        [Required]
        public int[] Clients { get; set; } = null!;
    }
}
