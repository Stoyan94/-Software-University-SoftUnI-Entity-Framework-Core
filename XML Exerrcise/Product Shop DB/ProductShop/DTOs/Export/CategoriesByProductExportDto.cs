using ProductShop.Models;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ProductShop.DTOs.Export
{
    [XmlType(nameof(Category))]
    public class CategoriesByProductExportDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("count")]
        public int Count { get; set; }

        [XmlElement("averagePrice")]
        public decimal AveragePrice { get; set; }

        [XmlElement("totalRevenue")]
        public decimal TotalRevenue { get; set; }
    }
}
 //< name > Garden </ name >
 //   < count > 23 </ count >
 //   < averagePrice > 800.150869 </ averagePrice >
 //   < totalRevenue > 18403.47 </ totalRevenue >
