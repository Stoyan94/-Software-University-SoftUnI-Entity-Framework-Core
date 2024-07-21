using CarDealer.Data;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Globalization;
using System.IO;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext dbContext = new CarDealerContext();

            string readJsonImportFile = File.ReadAllText(@"../../../Datasets/sales.json");
            Console.WriteLine(GetTotalSalesByCustomer(dbContext));
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
            List<Sale>? sales = JsonConvert.DeserializeObject<List<Sale>>(inputJson);

            dbContext.Sales.AddRange(sales);
            dbContext.SaveChanges();

            return $"Successfully imported {sales.Count}.";
        }

        public static string GetOrderedCustomers(CarDealerContext dbContext)
        {
            var orderedCustomers = dbContext.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .Select(c => new
                {
                    c.Name,
                    BirthDate = c.BirthDate.ToString(@"dd/MM/yyyy", CultureInfo.InvariantCulture),
                    c.IsYoungDriver,
                })
                .ToList();   

            return SerializeObjWithJsonSettings(orderedCustomers);
        }

        public static string GetCarsFromMakeToyota(CarDealerContext dbContext)
        {
            var toyotaCars = dbContext.Cars
                .Where(m => m.Make == "Toyota")
                .OrderBy(m => m.Model)
                .ThenByDescending(td => td.TraveledDistance)
                .Select(c => new
                {
                    c.Id,
                    c.Make,
                    c.Model,
                    c.TraveledDistance
                }).ToList();

            

            return SerializeObjWithJsonSettings(toyotaCars);
        }

        public static string GetLocalSuppliers(CarDealerContext dbContext)
        {
            var localSuppliers = dbContext.Suppliers
                .Where(s => !s.IsImporter)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    PartsCount = s.Parts.Count,
                }).ToList();

            return SerializeObjWithJsonSettings(localSuppliers);
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext dbContext)
        {
            var carsWithTheirParts = dbContext.Cars
                .Select(c => new
                {
                    car = new 
                    {
                        c.Make,
                        c.Model,
                        c.TraveledDistance
                    },
                    parts = c.PartsCars.Select(p => new
                    {
                        p.Part.Name,
                        Price = p.Part.Price.ToString("f2")
                    })
                    
                })
                .ToList();


            return SerializeObjWithJsonSettings(carsWithTheirParts);
        }

        public static string GetTotalSalesByCustomer(CarDealerContext dbContext)
        {
            var soldCartsToCustomers = dbContext.Customers
                .Where(c => c.Sales.Any())
                .Select(c => new
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count(),
                    SpentMoney = c.Sales.Select(p => p.Car.PartsCars.Select(x => x.Part.Price).Sum())
                })                
                .ToArray();


            var totalSalesByCustomer = soldCartsToCustomers.Select(t => new
            {
                t.FullName,
                t.BoughtCars,
                SpentMoney = t.SpentMoney.Sum()
            })
           .OrderByDescending(t => t.SpentMoney)
           .ThenByDescending(t => t.BoughtCars)
           .ToArray();

            return SerializeObjWithJsonSettings(totalSalesByCustomer); 
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

            return JsonConvert.SerializeObject(obj, settingsWithoutNull);
        }
    }
}