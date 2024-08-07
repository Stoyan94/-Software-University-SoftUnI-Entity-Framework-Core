﻿namespace BookShop
{
    using BookShop.Models;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using var dbContext = new BookShopContext();
            //DbInitializer.ResetDatabase(db);

            
            //int input = int.Parse(Console.ReadLine());
            //int result = CountBooks(dbContext, input);           
            Console.WriteLine(GetTotalProfitByCategory(dbContext));
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

        public static string GetAuthorNamesEndingIn(BookShopContext dbContext, string input)
        {
            StringBuilder output = new StringBuilder();

            var authorNamesEndingWith = dbContext.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new 
                {
                    FullName = a.FirstName + " " + a.LastName
                })
                .OrderBy(a=> a.FullName)
                .ToArray();

            // string[] authorNamesEndingIn = dbContext
            //.Authors
            //.Where(a => a.FirstName.EndsWith(input))
            //.Select(a => a.FirstName + " " + a.LastName)
            //.OrderBy(name => name)
            //.ToArray();

            foreach (var a in authorNamesEndingWith)
            {
                output.AppendLine($"{a.FullName}");
            }

            return output.ToString().TrimEnd();
        }

        public static string GetBookTitlesContaining(BookShopContext dbContext, string input)
        {
            string[] getBookTitles = dbContext.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .OrderBy(b => b.Title)
                .Select(b => $"{b.Title}")
                .ToArray();

            return string.Join(Environment.NewLine,getBookTitles);
        }

        public static string GetBooksByAuthor(BookShopContext dbContext, string input)
        {
            StringBuilder output = new StringBuilder();

            var getBooksByAuthor = dbContext.Books
                .Where(a => a.Author.LastName.ToLower()
                            .StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    b.Title,
                    FullName = b.Author.FirstName + " " + b.Author.LastName
                })
                .ToArray();

            foreach (var a in getBooksByAuthor)
            {
                output.AppendLine($"{a.Title} ({a.FullName})");
            }

            return output.ToString().TrimEnd();
        }

        public static int CountBooks(BookShopContext dbContext, int lengthCheck)
        {
            var booksCount = dbContext.Books
                .Where(b => b.Title.Length > lengthCheck)
                .Count();                

            return booksCount;
        }

        public static string CountCopiesByAuthor(BookShopContext dbContext)
        {
            var copiesCount = dbContext.Authors
                .OrderByDescending(a => a.Books.Sum(b => b.Copies))
                .Select(a => $"{a.FirstName} {a.LastName} - {a.Books.Sum(b => b.Copies)}")
                .ToArray();

            return string.Join(Environment.NewLine, copiesCount);
        }

        public static string GetTotalProfitByCategory(BookShopContext dbContext)
        {
            string[] totalProfitByCategory = dbContext
                .Categories
                .OrderByDescending(c => c.CategoryBooks
                    .Sum(cb => cb.Book.Copies * cb.Book.Price))
                .ThenBy(c => c.Name)
                .Select(c => $"{c.Name} ${c.CategoryBooks.Sum(cb => cb.Book.Copies * cb.Book.Price):f2}")
                .ToArray();

            return string.Join(Environment.NewLine, totalProfitByCategory);
        }

        public static string GetMostRecentBooks(BookShopContext dbContext)
        {
            var mostRecentBooks = dbContext
                .Categories
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    CategoryName = c.Name,
                    Books = c.CategoryBooks
                        .OrderByDescending(cb => cb.Book.ReleaseDate)
                        .Select(cb => new
                        {
                            BookTitle = cb.Book.Title,
                            BookYear = cb.Book.ReleaseDate.Value.Year
                        })
                        .Take(3)
                })
                .ToArray();

            StringBuilder output = new StringBuilder();

            foreach (var c in mostRecentBooks)
            {
                output.AppendLine($"--{c.CategoryName}");
                foreach (var b in c.Books)
                {
                    output.AppendLine($"{b.BookTitle} ({b.BookYear})");
                }
            }

            return output.ToString().TrimEnd();
        }
    }
}


