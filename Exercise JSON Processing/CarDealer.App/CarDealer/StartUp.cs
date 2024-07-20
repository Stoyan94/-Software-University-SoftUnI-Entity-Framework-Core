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
            Console.WriteLine(ImportParts(dbContext, readJsonImportFile));
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
    }
}