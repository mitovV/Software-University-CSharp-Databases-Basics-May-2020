namespace CarDealer
{
    using System.IO;
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