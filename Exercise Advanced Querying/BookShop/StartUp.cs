﻿namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using var dbContext = new BookShopContext();
            //DbInitializer.ResetDatabase(db);

            string goldenBooks = GetGoldenBooks(dbContext);

            //string input = Console.ReadLine();
            //string result = GetBooksByAgeRestriction(dbContext, input);
            Console.WriteLine(goldenBooks);
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
    }
}

