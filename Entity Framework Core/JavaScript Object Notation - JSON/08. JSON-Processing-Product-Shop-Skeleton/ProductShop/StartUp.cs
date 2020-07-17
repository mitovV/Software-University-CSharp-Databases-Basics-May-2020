namespace ProductShop
{
    using System;
    using System.IO;
    using System.Linq;

    using Data;
    using Models;

    using Newtonsoft.Json;

    public class StartUp
    {
        public static void Main()
        {
        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<User[]>(inputJson);

            context.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<Product[]>(inputJson);

            context.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Length}";
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<Category[]>(inputJson)
                .Where(p => p.Name != null)
                .ToArray();

            context.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Length}";
        }
    }
}