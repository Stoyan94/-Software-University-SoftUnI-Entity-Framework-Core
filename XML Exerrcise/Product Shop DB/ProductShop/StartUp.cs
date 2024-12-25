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

            var inputFiles = File.ReadAllText("../../../Datasets/users.xml");

            Console.WriteLine(ImportUsers(dbContext, inputFiles));
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
       
    }
}