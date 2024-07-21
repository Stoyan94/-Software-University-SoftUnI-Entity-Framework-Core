using CarDealer.Data;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using Castle.Core.Resource;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext dbContext = new CarDealerContext();

            string readJsonImportFile = File.ReadAllText(@"../../../Datasets/sales.json");
            Console.WriteLine(ImportSales(dbContext, readJsonImportFile));
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

        public static string ImportCars(CarDealerContext dbContext, string inputJson)
        {
            var carsDtos = JsonConvert.DeserializeObject<List<ImportCarsAndCarPartsDto>>(inputJson);

            var cars = new HashSet<Car>();
            var partsCars = new HashSet<PartCar>();

            foreach (var carDto in carsDtos)
            {
                var newCar = new Car()
                {
                    Make = carDto.Make,
                    Model = carDto.Model,
                    TraveledDistance = carDto.TraveledDistance
                };
                cars.Add(newCar);

                foreach (var partId in carDto.PartsId.Distinct())
                {
                    partsCars.Add(new PartCar()
                    {
                        Car = newCar,
                        PartId = partId
                    });
                }
            }

            dbContext.Cars.AddRange(cars);
            dbContext.PartsCars.AddRange(partsCars);
            dbContext.SaveChanges();

            return $"Successfully imported {cars.Count}.";
        }
        public static string ImportCustomers(CarDealerContext dbContext, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<List<Customer>>(inputJson);

            dbContext.Customers.AddRange(customers);
            dbContext.SaveChanges();

            return $"Successfully imported {customers.Count}.";
        }

        public static string ImportSales(CarDealerContext dbContext, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<List<Sale>>(inputJson);

            dbContext.Sales.AddRange(sales);
            dbContext.SaveChanges();

            return $"Successfully imported {sales.Count}.";
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