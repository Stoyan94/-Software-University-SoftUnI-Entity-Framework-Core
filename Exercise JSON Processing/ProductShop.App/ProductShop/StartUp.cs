namespace ProductShop
{
    using Newtonsoft.Json;
    using ProductShop.Data;
    using ProductShop.DTOs.Import;
    using ProductShop.Models;

    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext dbContext = new ProductShopContext();

            string userText = File.ReadAllText("../../../Datasets/users.json");
            Console.WriteLine(ImportUsers(dbContext, userText));
        }

        public static string ImportUsers(ProductShopContext dbContext, string inputJson)
        {
            var userDtos = JsonConvert.DeserializeObject<ImportUserDTO[]>(inputJson);

            var users = userDtos.Select(dto => new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Age = dto.Age                
            }).ToList();

            dbContext.Users.AddRange(users);
            dbContext.SaveChanges();
            return $"Successfully imported {users.Count}";
        }
    }   
}

