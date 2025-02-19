﻿using ProductShop.Models;
using System.Xml.Serialization;

namespace ProductShop.DTOs.Import
{
    [XmlType(nameof(CategoryProduct))]
    public class CategoriesProductImportDto
    {
        [XmlElement("CategoryId")]
        public int CategoryId { get; set; }

        [XmlElement("ProductId")]
        public int ProductId { get; set; }
    }

    //<CategoryProducts>
    //<CategoryProduct>
    //    <CategoryId>4</CategoryId>
    //    <ProductId>1</ProductId>
}
