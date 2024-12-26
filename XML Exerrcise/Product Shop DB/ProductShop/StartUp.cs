using ProductShop.Data;
using ProductShop.DTOs.Export;
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

            var inputFiles = File.ReadAllText("../../../Datasets/categories-products.xml");

            Console.WriteLine(GetProductsInRange(dbContext));
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

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            List<CategoriImportDto> categoriesDto = XmlHelper.Deserialize<List<CategoriImportDto>>(inputXml, "Categories");

           var ignoreNull = categoriesDto
                .Where(x => x.Name != null)
                .ToList();

            List<Category> categories = ignoreNull
                .Select(dto => new Category()
                {
                    Name = dto.Name,
                })
                .ToList();

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var categoriesProductIdDto = XmlHelper.Deserialize<List<CategoriesProductImportDto>>
                (inputXml, "CategoryProducts");

            var categoryIds = context.Categories
                .Select(x => x.Id).ToList();
            var productIds = context.Products
                .Select(x => x.Id).ToList();

            List<CategoriesProductImportDto> validIds = categoriesProductIdDto.
                Where(s=> categoryIds.Contains(s.CategoryId) && productIds.Contains(s.ProductId))
                .ToList();

            List<CategoryProduct> categoryProducts = validIds
                .Select(x => new CategoryProduct()
                {
                    CategoryId = x.CategoryId,
                    ProductId = x.ProductId
                })
                .ToList();

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var productsInRange = context.Products
                .Where(p => p.Price >= 500 && p.Price <=1000)
                .OrderBy(p => p.Price)
                .Select(dto => new ProductsInRangeExportDto()
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    Byer = $"{dto.Buyer.FirstName} {dto.Buyer.LastName}"
                })
                .ToList();


            return XmlHelper.SerializeToXml(productsInRange, "Products");
        }

    }
}