using CarDealer.Data;
using CarDealer.Models;
using Castle.Core.Resource;
using Newtonsoft.Json;
using System.IO;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            using CarDealerContext dbContext = new CarDealerContext();

            string readJsonImportFile = File.ReadAllText("../../../Datasets/customers.json");
            Console.WriteLine(ImportCustomers(dbContext, readJsonImportFile));
        }

        public static string ImportSuppliers(CarDealerContext dbContext, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(inputJson);

            dbContext.Suppliers.AddRange(suppliers);
            dbContext.SaveChanges();

            return $"Successfully imported {suppliers.Count}.";
        }

        public static string ImportParts(CarDealerContext dbContext, string inputJson) 
        { 
            var parts = JsonConvert.DeserializeObject<List<Part>>(inputJson);

            var validSuppliersId = dbContext.Suppliers
                .Select(s => s.Id)
                .ToArray();

            var partsWithValidSuppId = parts.
                Where(p => validSuppliersId.Contains(p.SupplierId))
                .ToArray();

            dbContext.Parts.AddRange(partsWithValidSuppId);
            dbContext.SaveChanges();

            return $"Successfully imported {partsWithValidSuppId.Length}."; 
        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            return null;
        }
        public static string ImportCustomers(CarDealerContext dbContext, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<List<Customer>>(inputJson);

            dbContext.Customers.AddRange(customers);
            dbContext.SaveChanges();

            return $"Successfully imported {customers.Count}.";
        }
    }
}