namespace BookShop
{
    using System;
    using System.Linq;
    using System.Text;

    using Data;
    using Initializer;
    using Models.Enums;

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

        public static string GetGoldenBooks(BookShopContext context)
        {
            var titles = context
                .Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            return String.Join(Environment.NewLine, titles);
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var sb = new StringBuilder();

            var books = context
                .Books
                .Where(b => b.Price > 40)
                .Select(b => new
                {
                    b.Title,
                    b.Price
                })
                .OrderByDescending(b => b.Price)
                .ToArray();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var titles = context
                .Books
                .OrderBy(b => b.BookId)
                .Where(b => b.ReleaseDate.Value.Year != year)
                .Select(b => b.Title)
                .ToArray();

            return String.Join(Environment.NewLine, titles);
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categories = input
                .ToLower()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var titles = context
                .Books
                .Where(c => c.BookCategories.Any(bc => categories.Contains(bc.Category.Name.ToLower())))
                .Select(b => b.Title)
                .OrderBy(b => b)
            .ToArray();

            return String.Join(Environment.NewLine, titles);
        }
    }
}
