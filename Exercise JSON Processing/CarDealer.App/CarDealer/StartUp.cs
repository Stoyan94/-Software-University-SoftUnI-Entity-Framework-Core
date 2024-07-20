using CarDealer.Data;
using CarDealer.Models;
using Newtonsoft.Json;
using System.IO;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            using CarDealerContext dbContext = new CarDealerContext();

            string readJsonImportFile = File.ReadAllText("../../../Datasets/parts.json");
            Console.WriteLine(ImportSuppliers(dbContext, readJsonImportFile));
        }

        public static string ImportSuppliers(CarDealerContext dbContext, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(inputJson);

            dbContext.Suppliers.AddRange(suppliers);
            dbContext.SaveChanges();

            return $"Successfully imported {suppliers.Count}.";
        }

       
    }
}