using ProductShop.Data;
using ProductShop.DTOs.Import;
using ProductShop.Models;
using System.Xml.Serialization;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            using ProductShopContext dbContext = new ProductShopContext();

            var inputFiles = File.ReadAllText("../../../Datasets/products.xml");

            Console.WriteLine(ImportProducts(dbContext, inputFiles));
        }

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {      
            var userDtos = XmlHelper.Deserialize<List<UsersImportDto>>(inputXml, "Users");            

            List<User> users = userDtos
                .Select(dto => new User()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Age = dto.Age,
                })
                .ToList();

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count}";
        }

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var productsDto = XmlHelper.Deserialize<List<ProductImportDto>>(inputXml, "Products");

            List<Product> products = productsDto
                .Select(dto => new Product()
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    SellerId = dto.SellerId,
                    BuyerId = dto.SellerId
                })
                .ToList();

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count}";
        }


    }
}