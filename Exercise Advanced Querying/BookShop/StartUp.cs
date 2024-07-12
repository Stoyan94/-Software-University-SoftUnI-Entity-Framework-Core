namespace BookShop
{
    using BookShop.Models;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using System.Globalization;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using var dbContext = new BookShopContext();
            //DbInitializer.ResetDatabase(db);

            
            string input = Console.ReadLine();
            string result = GetBooksReleasedBefore(dbContext, input);           
            Console.WriteLine(result);
        }

        public static string GetBooksByAgeRestriction(BookShopContext dbContext, string command)
        {
            //bool hasparsed = enum.tryparse(typeof(agerestriction), command, true, out object? agerestrictionobj);
            //agerestriction agerestriction;

            //if (hasparsed)
            //{
            //    agerestriction = (agerestriction)agerestrictionobj!;

            //    string[] booktitles = dbcontext.books
            //    .where(b => b.agerestriction == agerestriction)
            //    .orderby(b => b.title)
            //    .select(b => b.title)
            //    .toarray();

            //    return string.join(environment.newline, booktitles);
            //}

            //return null;


            try
            {
                AgeRestriction ageRestriction = Enum.Parse<AgeRestriction>(command, true);

                string[] bookTitles = dbContext.Books
                .Where(b => b.AgeRestriction == ageRestriction)
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToArray();

                return string.Join(Environment.NewLine, bookTitles);

            }
            catch (Exception)
            {

                return "Not Found";
            }

        }

        public static string GetGoldenBooks(BookShopContext dbContext)
        {
            string[] goldenBooks = dbContext.Books
                .Where(b => b.EditionType == EditionType.Gold && 
                            b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select (b => b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, goldenBooks);
        }

        public static string GetBooksByPrice(BookShopContext dbContext)
        {
            StringBuilder output = new StringBuilder();

            var booksByPrice = dbContext.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => new
                {
                    b.Title,
                    b.Price
                })
                .ToArray();

            foreach (var book in booksByPrice)
            {
                output.AppendLine($"{book.Title} - ${book.Price:f2}");
            }

            return output.ToString().TrimEnd();
        }

        public static string GetBooksNotReleasedIn(BookShopContext dbContext, int year)
        {
            string[] books = dbContext.Books
                .Where(b => b.ReleaseDate!.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetBooksByCategory(BookShopContext dbContext, string input)
        {
            string[] categories = input.ToLower().Split().ToArray();

            string[] bookByCategory = dbContext.BooksCategories
                .Where(b => categories.Contains(b.Category.Name.ToLower()))
                .Select(b => b.Book.Title)
                .OrderBy(b => b)
                .ToArray();

            return string.Join(Environment.NewLine, bookByCategory);
        }

        public static string GetBooksReleasedBefore(BookShopContext dbContext, string date)
        {
            DateTime parsedDate = DateTime.ParseExact(date, "dd-MM-yyyy", null);

            var booksBefore = dbContext.Books
                .Where(b => b.ReleaseDate < parsedDate)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:f2}")
                .ToArray();

            return string.Join(Environment.NewLine, booksBefore);
        }
    }
}


