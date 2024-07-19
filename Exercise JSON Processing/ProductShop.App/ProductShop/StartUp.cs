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

            string userText = File.ReadAllText("../../../Datasets/categories.json");
            Console.WriteLine(ImportCategories(dbContext, userText));
        }

        //Define your DTO and entity classes:
        //    Ensure you have your ImportUserDTO and User entity classes properly defined.

        //Map DTOs to entities:
        //    Convert each DTO to the corresponding entity object before adding them to the context.
        public static string ImportUsers(ProductShopContext dbContext, string inputJson)
        {     
            ImportUserDTO[]? userDtos = JsonConvert.DeserializeObject<ImportUserDTO[]>(inputJson);

            List<User> users = userDtos.Select(dto => new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Age = dto.Age                
            }).ToList();

            dbContext.Users.AddRange(users);
            dbContext.SaveChanges();
            return $"Successfully imported {users.Count}";
        }

        public static string ImportProducts(ProductShopContext dbContext, string inputJson)
        {
            var productsDto = JsonConvert.DeserializeObject<ImportProductsDTO[]>(inputJson);
            //"Name": "Care One Hemorrhoidal",
            //    "Price": 932.18,
            //    "SellerId": 25,
            //    "BuyerId"
            List<Product> products = productsDto.Select(dto => new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                SellerId = dto.SellerId,
                BuyerId = dto.BuyerId
            }).ToList();

            dbContext.Products.AddRange(products);
            dbContext.SaveChanges();

            return $"Successfully imported {products.Count()}";
        }

        public static string ImportCategories(ProductShopContext dbContext, string inputJson)
        {
            List<Category>? categories = JsonConvert.DeserializeObject<List<Category>>(inputJson);
            categories.RemoveAll(n => n.Name == null);

            dbContext.Categories.AddRange(categories);
            dbContext.SaveChanges();

            return $"Successfully imported {categories.Count()}";
        }
    }   
}

