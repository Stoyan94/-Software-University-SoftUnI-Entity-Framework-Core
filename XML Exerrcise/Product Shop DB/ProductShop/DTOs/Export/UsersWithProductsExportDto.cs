using ProductShop.Models;
using System.Xml.Serialization;

namespace ProductShop.DTOs.Export
{
    [XmlType("User")]
    public class UsersWithProductsExportDto
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlElement("users")]        
        public UserInfoExportDto Users { get; set; } 

        [XmlElement("SoldProducts")]
        public SoldProductsInfoUserExportDto SoldProcuts { get; set; }
    }
}

[XmlType("users")]
public class UserInfoExportDto
{
    [XmlElement("firstName")]
    public string FirstName { get; set; }

    [XmlElement("lastName")]
    public string LastName { get; set; }

    [XmlElement("age")]
    public int? Age { get; set; }
}

[XmlType("ProductInfo")]
public class SoldProductsInfoUserExportDto
{
    [XmlElement("count")]
    public int Count { get; set; }

    [XmlArray("products")]
    public SoldProductsExportDto[] Products { get; set; }
}


//< Users >
//  < count > 54 </ count >
//  < users >
//    < User >
//      < firstName > Dale </ firstName >
//      < lastName > Galbreath </ lastName >
//      < age > 31 </ age >
//      < SoldProducts >
//        < count > 9 </ count >
//        < products >
//          < Product >
//            < name > Fair Foundation SPF 15</name>
//            <price>1394.24</price>
//          </Product>
//          <Product> 
//            <name>Finasteride</name>
//            <price>1374.01</price>
//          </Product>
//          <Product>
//            <name>EMEND</name>
//            <price>1365.51</price>
//          </Product>

