using ProductShop.Models;
using System.Diagnostics;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ProductShop.DTOs.Export
{
    [XmlType(nameof(Product))]
    public class ProductsInRangeExportDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("buyer")]
        public string Byer { get; set; }
    }
}
