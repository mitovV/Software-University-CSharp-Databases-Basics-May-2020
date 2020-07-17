namespace CarDealer
{
    using System.Linq;

    using Data;
    using Models;

    using Newtonsoft.Json;

    public class StartUp
    {
        public static void Main()
        {
        }

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<Supplier[]>(inputJson);

            context.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}.";
        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var parts = JsonConvert
                .DeserializeObject<Part[]>(inputJson)
                .Where(p => context.Suppliers.Any(s => s.Id == p.SupplierId))
                .ToArray();

            context.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Length}.";
        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var cars = JsonConvert.DeserializeObject<ImportCarDto[]>(inputJson);

            foreach (var car in cars)
            {
                var newCar = new Car()
                {
                    Make = car.Make,
                    Model = car.Model,
                    TravelledDistance = car.TravelledDistance
                };

                context.Add(newCar);

                foreach (var partId in car.PartsId)
                {
                    var partCar = new PartCar()
                    {
                        CarId = newCar.Id,
                        PartId = partId
                    };

                    if (!newCar.PartCars.Any(pc => pc.PartId == partId))
                    {
                        context.PartCars.Add(partCar);
                    }
                }
            }

            context.SaveChanges();

            return $"Successfully imported {cars.Length}.";
        }
    }
}
