namespace ProductShop
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using ProductShop.Data;
    using ProductShop.DTOs.Export;
    using ProductShop.DTOs.Import;
    using ProductShop.Models;
    using System.Diagnostics.Metrics;
    using System.Drawing;
    using System.Reflection;

    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext dbContext = new ProductShopContext();

            //string userText = File.ReadAllText("../../../Datasets/categories-products.json");
            Console.WriteLine(GetUsersWithProducts(dbContext));
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

        public static string ImportCategoryProducts(ProductShopContext dbContext, string inputJson)
        {
            List<CategoryProduct>? categoryProducts = JsonConvert.DeserializeObject<List<CategoryProduct>>(inputJson);
            
            dbContext.CategoriesProducts.AddRange(categoryProducts);
            dbContext.SaveChanges();

            return $"Successfully imported {categoryProducts.Count()}";
        }

        public static string GetProductsInRange(ProductShopContext dbContext)
        {
            List<ExportProducInRangeDTO> productsInRange = dbContext.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new ExportProducInRangeDTO
                {
                    Name = p.Name,
                    Price = p.Price,
                    Seller = $"{p.Seller.FirstName} {p.Seller.LastName}"
                })
                .OrderBy(p => p.Price)
                .ToList();

            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore, // Ignore null values in objects 
                Formatting = Formatting.Indented, // formatting the text for easy human readable
                //ContractResolver = new CamelCasePropertyNamesContractResolver(), when we use DTO wee dont need this setting
            };

            return JsonConvert.SerializeObject(productsInRange, settings);
        }

        public static string GetSoldProducts(ProductShopContext dbContext)
        {
                var soldProducts = dbContext.Users
                    .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                    .OrderBy(u => u.LastName)
                    .ThenBy(u => u.FirstName)
                    .Select(u => new
                    {
                        u.FirstName,
                        u.LastName,
                        SoldProducts = u.ProductsSold.Select(p => new
                        {
                            p.Name,
                            p.Price,
                            BuyerFirstName = p.Buyer.FirstName,
                            BuyerLastName = p.Buyer.LastName,

                        }).ToArray()
                    })
                    .ToList();
            

            return SerializeObjWithJsonSettings(soldProducts);
        }

        public static string GetCategoriesByProductsCount(ProductShopContext dbContext)
        {
            var categoriesByProductsCount = dbContext.Categories
                .OrderByDescending(c => c.CategoriesProducts.Count)
                .Select(c => new
                {
                    Category = c.Name,
                    ProductsCount = c.CategoriesProducts.Count,
                    AveragePrice = c.CategoriesProducts.Average(p => p.Product.Price).ToString("f2"),
                    TotalRevenue = c.CategoriesProducts.Sum(p => p.Product.Price).ToString("f2")
                })
                .ToArray();

            return SerializeObjWithJsonSettings(categoriesByProductsCount);
        }

        public static string GetUsersWithProducts(ProductShopContext dbContext)
        {
            var usersWithProduct = dbContext.Users
                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))                
                .Select(u => new
                {   
                    u.FirstName,
                    u.LastName,
                    u.Age,
                    SoldProducts = new
                    {
                        Count = u.ProductsSold.Count(p => p.Buyer != null),
                        Products = u.ProductsSold.Where(p => p.Buyer != null)
                        .Select(p => new
                        {
                            p.Name,
                            p.Price,
                        })
                        .ToArray()
                    }                    
                })
                .OrderByDescending(u => u.SoldProducts.Count)
                .ToList();

            var userWrapper = new
            {
                UsersCount = usersWithProduct.Count,
                Users = usersWithProduct 
            };

            return SerializeObjWithJsonSettings(userWrapper);
        }

        private static string SerializeObjWithJsonSettings(object obj)
        {
            var settingsWithNull = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };

            var settingsWithoutNull = new JsonSerializerSettings()
            {               
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };

            return JsonConvert.SerializeObject(obj, settingsWithNull);
        }
    }   
}

