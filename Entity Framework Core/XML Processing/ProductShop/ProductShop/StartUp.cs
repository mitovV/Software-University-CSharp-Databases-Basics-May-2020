namespace ProductShop
{
    using System.IO;
    using System.Xml.Serialization;

    using Data;
    using Dtos.Import;
    using Models;

    using AutoMapper;

    public class StartUp
    {
        private static IMapper mapper;

        public static void Main()
        {
            var config = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            mapper = new Mapper(config);
        }

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(ImportUserDto[]), new XmlRootAttribute("Users"));

            ImportUserDto[] userDtos;

            using (var reader = new StringReader(inputXml))
            {
                userDtos = (ImportUserDto[])serializer.Deserialize(reader);
            }

            var users = mapper.Map<User[]>(userDtos);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }
    }
}