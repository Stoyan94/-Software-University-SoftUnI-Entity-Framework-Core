using CarDealer.Data;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            using CarDealerContext dbContext = new CarDealerContext();

            string readInputFile = File.ReadAllText("../../../Datasets/parts.xml");
            Console.WriteLine(ImportParts(dbContext, readInputFile));
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
    }
}