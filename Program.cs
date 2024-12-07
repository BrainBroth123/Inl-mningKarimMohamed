using System;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using Slutuppgift_Karim_Mohamed.Data;
using Slutuppgift_Karim_Mohamed.Models;

namespace Slutuppgift_Karim_Mohamed
{
    public class Program
    {
        public static void Main(String[] args)
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("Add author: 1, Add book: 2, Loan book: 3, Register book to author: 4, Remove author: 5, Remove book: 6, Remove loan: 7, Show books: 8, Show loans: 9, Exit Program: 0");
                int menu = (int)char.GetNumericValue(Console.ReadKey().KeyChar);
                Console.WriteLine("\n");
                switch (menu)
                {
                    case 1:
                        AddAuthor();
                        break;
                    case 2:
                        AddBook();
                        break;
                    case 3:
                        //AddLoan();
                        break;
                    case 4:
                        //RegisterAuthorToBook();
                        break;
                    case 5:
                        RemoveAuthor();
                        break;
                    case 6:
                        RemoveBook();
                        break;
                    case 7:
                        RemoveLoan();
                        break;
                    case 8:
                        ShowBooks();
                        break;
                    case 9:
                        ShowLoans();
                        break;
                    case 0:
                        loop = false;
                        Console.WriteLine("Closing Program...");
                        break;
                    default:
                        Console.WriteLine("Error: Invalid Input, Try Again.");
                        break;
                }
            }
        }

        #region EditData
        private static void AddAuthor()
        {
            using (var context = new AppDbContext())
            {
                var author = new Author
                {
                    FirstName = "John",
                    LastName = "Doe"
                };

                context.Authors.Add(author);

                context.SaveChanges();

                Console.WriteLine("Test data added to database.");
            }
        }
        private static void AddBook()
        {
            using (var context = new AppDbContext())
            {
                var book = new Book
                {
                    Title = "C# for dummies 101"
                };

                context.Books.Add(book);

                context.SaveChanges();

                Console.WriteLine("Test data added to database.");
            }
        }
        private static void AddLoan(Book book)
        {
            using (var context = new AppDbContext())
            {
                var loan = new LoanRegistration
                {
                    Book = book,
                    LoanDate = DateTime.Now,
                    ReturnDate = DateTime.Now.AddDays(7)
                };

                context.LoanRegistrations.Add(loan);

                context.SaveChanges();

                Console.WriteLine("Test data added to database.");
            }
        }
        private static void RegisterAuthorToBook(Author author, Book book) 
        {
            using (var context = new AppDbContext())
            {
                var registration = new AuthorRegistration
                {
                    Book = book,
                    Author = author
                };

                context.AuthorRegistrations.Add(registration);

                context.SaveChanges();

                Console.WriteLine("Test data added to database.");
            }
        }
        private static void RemoveAuthor()
        {

        }
        private static void RemoveBook()
        {

        }
        private static void RemoveLoan() 
        { 
        
        }
        #endregion EditData

        #region ReadData
        private static void ShowBooks()
        {
            using (var context = new AppDbContext())
            {
                var Books = context.Books.ToList();
                
                if (Books.Any())
                {
                    Console.WriteLine($"------------\n");
                    foreach (var book in Books)
                    {
                        Console.WriteLine($"Title: {book.Title}\n");
                    }
                    Console.WriteLine($"------------\n");
                }
                else
                {
                    Console.WriteLine("No books found in the database.");
                }
            }
        }
        private static void ShowLoans()
        {
            
        }
        #endregion ReadData
    }
}