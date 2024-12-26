using ProductShop.Models;
using System.Xml.Serialization;

namespace ProductShop.DTOs.Export
{
    [XmlType(nameof(User))]
    public class SoldProductsUserExportDto
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlArray("soldProducts")]
        public SoldProductsExportDto[] SoldProducts { get; set; }

    }
}

[XmlType("ProductDetail")]
public class SoldProductsExportDto
{
    [XmlElement("name")]
    public string Name { get; set; }

    [XmlElement("price")]
    public decimal Price { get; set; }
}

//< Users >
//  < User >
//    < firstName > Almire </ firstName >
//    < lastName > Ainslee </ lastName >
//    < soldProducts >
//      < Product >
//        < name > Ampicillin </ name >
//        < price > 674.63 </ price >
//      </ Product >

