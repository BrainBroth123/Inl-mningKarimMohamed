using System;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using Slutuppgift_Karim_Mohamed.Data;
using Slutuppgift_Karim_Mohamed.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace Slutuppgift_Karim_Mohamed
{
    public class Program
    {
        public static void Main(String[] args)
        {
            AddSeed();

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
                        AddLoan();
                        break;
                    case 4:
                        RegisterAuthorToBook();
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
        private static void AddSeed()
        {
            using (var context = new AppDbContext())
            {
                var Books = context.Books.ToList();
                var Authors = context.Authors.ToList();
                var Registrations = context.AuthorRegistrations.ToList();
                var Loans = context.LoanRegistrations.ToList();

                if (!Books.Any() && !Authors.Any() && !Registrations.Any() && !Loans.Any())
                {
                    var author = new Author
                    {
                        FirstName = "John",
                        LastName = "Doe"
                    };

                    context.Authors.Add(author);

                    var book = new Book
                    {
                        Title = "C# for dummies 101"
                    };

                    context.Books.Add(book);

                    var loan = new LoanRegistration
                    {
                        Book = book,
                        LoanDate = DateTime.Now,
                        ReturnDate = DateTime.Now.AddDays(7)
                    };

                    context.LoanRegistrations.Add(loan);

                    var registration = new AuthorRegistration
                    {
                        Book = book,
                        Author = author
                    };

                    context.AuthorRegistrations.Add(registration);

                    context.SaveChanges();

                    Console.WriteLine("Seed data added to database.");
                }
            }
        }
        private static void AddAuthor()
        {
            using (var context = new AppDbContext())
            {
                string firstName = "";
                string lastName = "";
                while (firstName == "" || lastName == "")
                {
                    Console.WriteLine("Enter first name of author.");
                    firstName = Console.ReadLine();
                    Console.WriteLine("Enter last name of author.");
                    lastName = Console.ReadLine();
                }

                var author = new Author
                {
                    FirstName = firstName,
                    LastName = lastName
                };

                context.Authors.Add(author);

                context.SaveChanges();

                Console.WriteLine("Author added to database.");
            }
        }
        private static void AddBook()
        {
            using (var context = new AppDbContext())
            {
                string title = "";
                while (title == "")
                {
                    Console.WriteLine("Enter title of book.");
                    title = Console.ReadLine();
                }

                var book = new Book
                {
                    Title = title
                };

                context.Books.Add(book);

                context.SaveChanges();

                Console.WriteLine("Book added to database.");
            }
        }
        private static void AddLoan()
        {
            using (var context = new AppDbContext())
            {
                var Books = context.Books.ToList();
                var Loans = context.LoanRegistrations.ToList();
                if (Books.Any())
                {
                    ShowBooks();

                    tryagain:

                    Console.WriteLine("Enter ID of book you wish to loan.");
                    int bookId;
                    if (!int.TryParse(Console.ReadLine(), out bookId))
                    {
                        goto tryagain;
                    }

                    Book book = null;

                    foreach (Book Book in Books)
                    {
                        if (Book.Id == bookId)
                        {
                            book = Book;
                            bool containsBook = false;
                            foreach (var loan in Loans)
                            {
                                if (loan.Book == book)
                                {
                                    containsBook = true;
                                }
                            }
                            if (!containsBook)
                            {
                                var loan = new LoanRegistration
                                {
                                    Book = book,
                                    LoanDate = DateTime.Now,
                                    ReturnDate = DateTime.Now.AddDays(7)
                                };

                                context.LoanRegistrations.Add(loan);

                                context.SaveChanges();

                                Console.WriteLine("Loan added to database.");
                            } else
                            {
                                Console.WriteLine("Error: book is already loaned.");
                            }
                        }
                    }
                    if (book == null)
                    {
                        Console.WriteLine("Error: book does not exist.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: No books found in the database, could not register loan.");
                }
            }
        }
        private static void RegisterAuthorToBook() 
        {
            using (var context = new AppDbContext())
            {
                var Books = context.Books.ToList();
                var Authors = context.Authors.ToList();

                if (Books.Any())
                {
                    ShowBooks();
                    if (Authors.Any())
                    {
                        ShowAuthors();
                        tryagain:

                        Console.WriteLine("Enter ID of book you wish to register.");
                        int bookId;
                        if (!int.TryParse(Console.ReadLine(), out bookId))
                        {
                            goto tryagain;
                        }

                        Book book = null;

                        foreach (Book Book in Books)
                        {
                            if (Book.Id == bookId)
                            {
                                book = Book;

                                tryagain2:

                                Console.WriteLine("Enter ID of author you wish to register.");
                                int authorId;
                                if (!int.TryParse(Console.ReadLine(), out authorId))
                                {
                                    goto tryagain2;
                                }

                                Author author = null;
                                foreach (Author Author in Authors)
                                {
                                    if (Author.Id == authorId)
                                    {
                                        author = Author;

                                        var registration = new AuthorRegistration
                                        {
                                            Book = book,
                                            Author = author
                                        };

                                        context.AuthorRegistrations.Add(registration);

                                        context.SaveChanges();

                                        Console.WriteLine("Author registration added to database.");
                                    }
                                }
                            }
                        }
                        if (book == null)
                        {
                            Console.WriteLine("Error: book does not exist.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Error: No books found in the database, could not register loan.");
                }
            }
        }
        private static void RemoveAuthor()
        {
            using (var context = new AppDbContext())
            {
                var Authors = context.Authors.ToList();
                var Registrations = context.AuthorRegistrations.ToList();

                ShowAuthors();

                tryagain:

                Console.WriteLine("Enter ID of author you wish to remove.");
                int authorId;
                if (!int.TryParse(Console.ReadLine(), out authorId))
                {
                    goto tryagain;
                }

                foreach (var author in Authors)
                {
                    if (author.Id == authorId)
                    {
                        context.Authors.Remove(author);
                    }
                }
                foreach (var registration in Registrations)
                {
                    if (registration.Author.Id == authorId)
                    {
                        context.AuthorRegistrations.Remove(registration);
                    }
                }

                context.SaveChanges();

                Console.WriteLine("Author and related registrations removed from database.");
            }
        }
        private static void RemoveBook()
        {
            using (var context = new AppDbContext())
            {
                var Books = context.Books.ToList();
                var Registrations = context.AuthorRegistrations.ToList();
                var Loans = context.LoanRegistrations.ToList();
                ShowBooks();

                tryagain:

                Console.WriteLine("Enter ID of book you wish to remove.");
                int bookId;
                if (!int.TryParse(Console.ReadLine(), out bookId))
                {
                    goto tryagain;
                }

                foreach (var book in Books)
                {
                    if (book.Id == bookId)
                    {
                        context.Books.Remove(book);
                    }
                }
                foreach (var registration in Registrations)
                {
                    if (registration.Book.Id == bookId)
                    {
                        context.AuthorRegistrations.Remove(registration);
                    }
                }
                foreach (var loan in Loans)
                {
                    if (loan.Book.Id == bookId)
                    {
                        context.LoanRegistrations.Remove(loan);
                    }
                }

                context.SaveChanges();

                Console.WriteLine("Book and related registrations removed from database.");
            }
        }
        private static void RemoveLoan() 
        {
            using (var context = new AppDbContext())
            {
                var Loans = context.LoanRegistrations.ToList();
                ShowLoans();

                tryagain:

                Console.WriteLine("Enter ID of book you wish to loan.");
                int loanId;
                if (!int.TryParse(Console.ReadLine(), out loanId))
                {
                    goto tryagain;
                }

                foreach (var loan in Loans)
                {
                    if (loan.Id == loanId)
                    {
                        context.LoanRegistrations.Remove(loan);
                    }
                }

                context.SaveChanges();

                Console.WriteLine("Loan was removed from database.");
            }
        }
        #endregion EditData

        #region ReadData
        private static void ShowAuthors()
        {
            using (var context = new AppDbContext())
            {
                var Authors = context.Authors.ToList();

                if (Authors.Any())
                {
                    Console.WriteLine($"------------\n");
                    foreach (var Author in Authors)
                    {
                        Console.WriteLine($"Name: {Author.FirstName} {Author.LastName} ID: {Author.Id}\n");
                    }
                    Console.WriteLine($"------------\n");
                }
                else
                {
                    Console.WriteLine("No authors found in the database.");
                }
            }
        }
        private static void ShowBooks()
        {
            using (var context = new AppDbContext())
            {
                var Books = context.Books.ToList();
                var Authors = context.Authors.ToList();
                var Registrations = context.AuthorRegistrations.ToList();

                if (Books.Any())
                {
                    Console.WriteLine($"------------\n");
                    foreach (var book in Books)
                    {
                        string s = $"Title: {book.Title} ID: {book.Id}";
                        foreach(var author in Authors)
                        {
                            foreach (var registration in Registrations)
                            {
                                if (registration.Book == book && registration.Author == author)
                                {
                                    s += $" Author: {author.FirstName} {author.LastName}";
                                }
                            }
                        }
                        Console.WriteLine(s + "\n");
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
            using (var context = new AppDbContext())
            {
                var Loans = context.LoanRegistrations.ToList();

                if (Loans.Any())
                {
                    Console.WriteLine($"All Books\n");
                    ShowBooks();
                    Console.WriteLine($"Loaned Books\n");
                    Console.WriteLine($"------------\n");
                    foreach (var loan in Loans)
                    {
                        Console.WriteLine($"Loaned Book: ID: {loan.BookId} Return Date: {loan.ReturnDate}\n");
                    }
                    Console.WriteLine($"------------\n");
                }
                else
                {
                    Console.WriteLine("No loans found in the database.");
                }
            }
        }
        #endregion ReadData
    }
}