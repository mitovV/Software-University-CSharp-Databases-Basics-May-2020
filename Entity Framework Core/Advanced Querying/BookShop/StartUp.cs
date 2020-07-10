namespace BookShop
{
    using System;
    using System.Linq;

    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            command = command.ToLower();

            var titles = context
                .Books
                .Where(b => b.AgeRestriction.ToString().ToLower() == command)
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToArray();

            return String.Join(Environment.NewLine, titles);
        }
    }
}
