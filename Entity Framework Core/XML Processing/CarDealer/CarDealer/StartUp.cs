namespace CarDealer
{
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    using Data;
    using Models;

    using AutoMapper;

    public class StartUp
    {
        private static IMapper mapper;

        public static void Main()
        {
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            ConfigureMapper();

            var serializer = new XmlSerializer(typeof(ImportSupplierDto[]), new XmlRootAttribute("Suppliers"));

            ImportSupplierDto[] supplierDtos;

            using (var reader = new StringReader(inputXml))
            {
                supplierDtos = (ImportSupplierDto[])serializer.Deserialize(reader);
            }

            var suppliers = mapper.Map<Supplier[]>(supplierDtos);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}";
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            ConfigureMapper();

            var serializer = new XmlSerializer(typeof(ImportPartDto[]), new XmlRootAttribute("Parts"));

            ImportPartDto[] partDtos;

            using (var reader = new StringReader(inputXml))
            {
                partDtos = ((ImportPartDto[])serializer.Deserialize(reader))
                    .Where(p => context.Suppliers.Any(s => s.Id == p.SupplierId))
                    .ToArray();
            }

            var parts = mapper.Map<Part[]>(partDtos);

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Length}";
        }

        private static void ConfigureMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });

            mapper = new Mapper(config);
        }
    }
}