﻿namespace CarDealer
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    using Data;
    using Dtos.Export;
    using Dtos.Import;
    using Models;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

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

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(ImportCarDto[]), new XmlRootAttribute("Cars"));

            ImportCarDto[] carDtos;

            using (var reader = new StringReader(inputXml))
            {
                carDtos = (ImportCarDto[])serializer.Deserialize(reader);
            }

            var cars = new List<Car>();
            var partCars = new List<PartCar>();

            foreach (var carDto in carDtos)
            {
                var car = new Car()
                {
                    Make = carDto.Make,
                    Model = carDto.Model,
                    TravelledDistance = carDto.TravelledDistance
                };

                var partsId = carDto
                    .Parts
                    .Where(pDto => context.Parts.Any(p => p.Id == pDto.Id))
                    .Select(p => p.Id)
                    .Distinct();

                foreach (var partId in partsId)
                {
                    var partCar = new PartCar()
                    {
                        CarId = car.Id,
                        PartId = partId
                    };

                    partCars.Add(partCar);
                }

                cars.Add(car);
            }

            context.Cars.AddRange(cars);
            context.PartCars.AddRange(partCars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}";
        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            ConfigureMapper();

            var serializer = new XmlSerializer(typeof(ImportCustomerDto[]), new XmlRootAttribute("Customers"));

            ImportCustomerDto[] customerDtos;

            using (var reader = new StringReader(inputXml))
            {
                customerDtos = (ImportCustomerDto[])serializer.Deserialize(reader);
            }

            var customers = mapper.Map<Customer[]>(customerDtos);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Length}";
        }

        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            ConfigureMapper();

            var serializer = new XmlSerializer(typeof(ImportSaleDto[]), new XmlRootAttribute("Sales"));

            ImportSaleDto[] saleDtos;

            using (var reader = new StringReader(inputXml))
            {
                saleDtos = ((ImportSaleDto[])serializer.Deserialize(reader))
                    .Where(sDto => context.Cars.Any(c => c.Id == sDto.CarId))
                    .ToArray();
            }

            var sales = mapper.Map<Sale[]>(saleDtos);

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Length}";
        }

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            ConfigureMapper();

            var cars = context
                .Cars
                .Where(c => c.TravelledDistance > 2000000)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .ProjectTo<ExportCarDto>(mapper.ConfigurationProvider)
                .ToArray();

            var serializer = new XmlSerializer(typeof(ExportCarDto[]), new XmlRootAttribute("cars"));

            var sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            using (var writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, cars, namespaces);
            }

            return sb.ToString().Trim();
        }

        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            ConfigureMapper();

            var cars = context
                .Cars
                .Where(c => c.Make == "BMW")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ProjectTo<ExportCarFromMakeBmwDto>(mapper.ConfigurationProvider)
                .ToArray();

            var serializer = new XmlSerializer(typeof(ExportCarFromMakeBmwDto[]), new XmlRootAttribute("cars"));

            var sb = new StringBuilder();

            using (var writer = new StringWriter(sb))
            {
                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");

                serializer.Serialize(writer, cars, namespaces);
            }

            return sb.ToString().Trim();
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            ConfigureMapper();

            var suppliers = context
                .Suppliers
                .Where(s => !s.IsImporter)
                .ProjectTo<ExportLocalSupplierDto>(mapper.ConfigurationProvider)
                .ToArray();


            var sb = new StringBuilder();

            using (var writer = new StringWriter(sb))
            {
                var serializer = new XmlSerializer(typeof(ExportLocalSupplierDto[]), new XmlRootAttribute("suppliers"));

                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");

                serializer.Serialize(writer, suppliers, namespaces);
            }

            return sb.ToString().Trim();
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context
                .Cars
                .Select(c => new ExportCarWithPartDto()
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.PartCars.Select(pc => new ExportPartDto()
                    {
                        Name = pc.Part.Name,
                        Price = pc.Part.Price
                    })
                    .OrderByDescending(p => p.Price)
                    .ToArray()
                })
                .OrderByDescending(c => c.TravelledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .ToArray();

            var sb = new StringBuilder();

            using (var writer = new StringWriter(sb))
            {
                var serializer = new XmlSerializer(typeof(ExportCarWithPartDto[]), new XmlRootAttribute("cars"));

                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");

                serializer.Serialize(writer, cars, namespaces);
            }

            return sb.ToString().Trim();
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context
                .Customers
                .Where(c => c.Sales.Any())
                .ToArray()
                .Select(c => new ExportSalesByCustomerDto()
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count(),
                    SpendMoney = c.Sales.Sum(s => s.Car.PartCars.Sum(pc => pc.Part.Price))
                })
                .OrderByDescending(c => c.SpendMoney)
                .ToArray();

            var sb = new StringBuilder();

            using (var writer = new StringWriter(sb))
            {
                var serializer = new XmlSerializer(typeof(ExportSalesByCustomerDto[]), new XmlRootAttribute("customers"));

                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");

                serializer.Serialize(writer, customers, namespaces);
            }

            return sb.ToString().Trim();
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