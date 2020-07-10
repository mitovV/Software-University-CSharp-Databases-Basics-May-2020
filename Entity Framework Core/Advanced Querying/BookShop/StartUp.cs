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

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var dateTime = DateTime.ParseExact(date, "dd-MM-yyyy", null);

            var books = context
                .Books
                .Where(b => b.ReleaseDate < dateTime)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => new
                {
                    b.Title,
                    b.EditionType,
                    b.Price
                })
                .ToArray();

            var sb = new StringBuilder();

            foreach (var b in books)
            {
                sb.AppendLine($"{b.Title} - {b.EditionType} - ${b.Price:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var names = context
                .Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => $"{a.FirstName} {a.LastName}")
                .OrderBy(a => a)
                .ToList();

            return String.Join(Environment.NewLine, names);
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            input = input.ToLower();

            var titles = context
                .Books
                .Where(b => b.Title.ToLower().Contains(input))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToArray();

            return String.Join(Environment.NewLine, titles);
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            input = input.ToLower();

            var books = context
                .Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input))
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    b.Title,
                    FullName = $"{b.Author.FirstName} {b.Author.LastName}"
                })
                .ToArray();

            var sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} ({book.FullName})");
            }

            return sb.ToString().TrimEnd();
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var count = context
                .Books
                .Count(b => b.Title.Length > lengthCheck);

            return count;
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context
                .Authors
                .Select(a => new
                {
                    FullName = $"{a.FirstName} {a.LastName}",
                    Copies = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(a => a.Copies)
                .ToArray();

            var sb = new StringBuilder();

            foreach (var author in authors)
            {
                sb.AppendLine($"{author.FullName} - {author.Copies}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
