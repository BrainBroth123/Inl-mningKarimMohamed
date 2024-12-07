using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;

namespace Slutuppgift_Karim_Mohamed
{
    public class Program
    {
        public static void Main(String[] args)
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("Add author: 1, Add book: 2, Loan book: 3, Register book to author: 4, Remove author: 5, Remove book: 6, Remove loan: 7, Exit Program: 8");
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

        }
        private static void AddBook()
        {

        }
        private static void AddLoan()
        {

        }
        private static void RegisterAuthorToBook() 
        {
            
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

        }
        private static void ShowLoans()
        {
            
        }
        #endregion ReadData
    }
}