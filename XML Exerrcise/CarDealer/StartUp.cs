using CarDealer.Data;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            using CarDealerContext dbContext = new CarDealerContext();

            //string readInputFile = File.ReadAllText("../../../Datasets/sales.xml");
            Console.WriteLine(GetLocalSuppliers(dbContext));
        }

        //9
        public static string ImportSuppliers(CarDealerContext dbContext, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SupplierImportDto[])
                                         ,new XmlRootAttribute("Suppliers"));

            SupplierImportDto[] suppliersDto;

            using (var reader = new StringReader(inputXml))
            {
                suppliersDto = (SupplierImportDto[])xmlSerializer.Deserialize(reader);
            }

            Supplier[] suppliers = suppliersDto
                .Select(dto => new Supplier()
                {
                    Name = dto.Name,
                    IsImporter = dto.isImporter
                })
                .ToArray();

            dbContext.Suppliers.AddRange(suppliers);
            dbContext.SaveChanges();

            return $"Successfully imported {suppliers.Length}";
        }

        public static string ImportParts(CarDealerContext dbContext, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PartImportDto[])
                                         ,new XmlRootAttribute("Parts"));

            PartImportDto[] partsDto;

            using (var reader = new StringReader(inputXml))
            {
                partsDto = (PartImportDto[])xmlSerializer.Deserialize(reader);
            }

            var supplierId = dbContext.Suppliers
                .Select(s => s.Id);

            PartImportDto[] partsWithValidSuppliers = partsDto
                .Where(p => supplierId.Contains(p.SupplierId))
                .ToArray();

            Part[] parts = partsWithValidSuppliers
                .Select(dto => new Part()
                {
                    Name= dto.Name,
                    Price= dto.Price,
                    Quantity= dto.Quantity,
                    SupplierId = dto.SupplierId
                })
                .ToArray();

            dbContext.Parts.AddRange(parts);
            dbContext.SaveChanges();

            return $"Successfully imported {parts.Length}"; ;
        }

        public static string ImportCars(CarDealerContext dbContext, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CarImportDto[]), 
                                       new XmlRootAttribute("Cars"));

            CarImportDto[] carsDto;

            using (var reader = new StringReader(inputXml))
            {
                carsDto = (CarImportDto[])serializer.Deserialize(reader);
            }

            List<Car> cars = new List<Car>();

            foreach (var dto in carsDto)
            {
               Car newCar = new Car()
               {
                   Make = dto.Make,
                   Model = dto.Model,
                   TraveledDistance = dto.TraveledDistance
               };

                int[] carPartsId = dto.PartId
                     .Select(p => p.Id)
                     .Distinct()
                     .ToArray();

                List<PartCar> carParts = new List<PartCar>();

                foreach (var id in carPartsId)
                {
                    carParts.Add(new PartCar()
                    {
                        Car = newCar,
                        PartId = id
                    });
                }

                newCar.PartsCars = carParts;
                cars.Add(newCar);
            }

            dbContext.Cars.AddRange(cars);
            dbContext.SaveChanges();            

            return $"Successfully imported {cars.Count}";
        }

        public static string ImportCustomers(CarDealerContext dbContext, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CustomerImportDto[]),
                                       new XmlRootAttribute("Customers"));

            CustomerImportDto[] customersDto;

            using (var reader = new StringReader(inputXml))
            {
                customersDto = (CustomerImportDto[])serializer.Deserialize(reader);
            }

            List<Customer> customers = customersDto
                .Select(dto => new Customer()
                {
                    Name = dto.Name,
                    BirthDate = dto.BirthDate,
                    IsYoungDriver = dto.IsYoungDriver
                }).
                ToList();

            dbContext.Customers.AddRange(customers);
            dbContext.SaveChanges();

            return $"Successfully imported {customers.Count}"; ;
        }
        public static string ImportSales(CarDealerContext dbContext, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SaleImportDto[]),
                                       new XmlRootAttribute("Sales"));

            SaleImportDto[] saleDto;

            using (var reader = new StringReader(inputXml))
            {
                saleDto = (SaleImportDto[])serializer.Deserialize(reader);
            };

            var carsId = dbContext.Cars
                .Select(c => c.Id)
                .ToList();

            var validCars = saleDto
                .Where(c => carsId.Contains(c.CarId))
                .ToList();

            var sales = validCars
                .Select(dto => new Sale()
                {
                    CarId = dto.CarId,
                    CustomerId = dto.CustomerId,
                    Discount = dto.Discount,
                })
                .ToList();

            dbContext.Sales.AddRange(sales);
            dbContext.SaveChanges();

            return $"Successfully imported {sales.Count}";
        }

        public static string GetCarsWithDistance(CarDealerContext dbContext)
        {
            List<CarWithDistanceExportDto> carsWitchDistance = dbContext.Cars
                .Where(c => c.TraveledDistance > 2000000)
                .Select(c => new CarWithDistanceExportDto()
                {
                    Make = c.Make,
                    Model = c.Model,
                    TraveledDistance = c.TraveledDistance
                })
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .ToList();

            return SerializeToXml(carsWitchDistance, "cars");
        }

        public static string GetCarsFromMakeBmw(CarDealerContext dbContext)
        {
            var getBwmCars = dbContext.Cars
                .Where(c => c.Make == "BMW")
                .Select(c => new BmwExportDto()
                {
                    Id = c.Id,
                    Model = c.Model,
                    TraveledDistance = c.TraveledDistance
                })
                .OrderBy(c=> c.Model)
                .ThenByDescending(c => c.TraveledDistance)
                .ToList();

            return SerializeToXml(getBwmCars, "cars", true);
        }

        public static string GetLocalSuppliers(CarDealerContext dbContext)
        {
            var localSuppliers = dbContext.Suppliers
                .Where(s => !s.IsImporter)
                .Select(s => new LocalSupplierExportDto()
                {
                    Id= s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToList();

            return SerializeToXml(localSuppliers, "suppliers");
        }

        private static string SerializeToXml<T>(T dto, string xmlRootAttribute, bool omitDeclaration = false)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootAttribute));
            StringBuilder stringBuilder = new StringBuilder();

            XmlWriterSettings settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = omitDeclaration,
                Encoding = new UTF8Encoding(false),
                Indent = true
            };

            using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
            using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
            {
                XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
                xmlSerializerNamespaces.Add(string.Empty, string.Empty);

                try
                {
                    xmlSerializer.Serialize(xmlWriter, dto, xmlSerializerNamespaces);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return stringBuilder.ToString();
        }
    }
}