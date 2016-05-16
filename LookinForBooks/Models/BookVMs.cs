using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LookinForBooks.Models
{
    public class BookVm
    {
        public BookVm()
        {

        }
        public BookVm(Book book)
        {
            BookId = book.Id;
            Title = book.Title;
            ISBN = book.Isbn;
            Author = book.Author;
            NumberofPages = book.NumberofPages;
            OwnedBy = book.Owner?.UserName;
            var loan = book.LoanedOut.FirstOrDefault(x => x.CheckedIn == null);
            if (loan != null)
            {
                IsCheckedOut = true;
                CheckedOutBy = loan.CheckedOutBy?.UserName;
                CheckedOutById = loan.CheckedOutBy?.Id;


            }

        }

        public string OwnedBy { get; set; }

        public int BookId { get; set; }
        public string Title { get; set; }
        public bool IsCheckedOut { get; set; }
        public string CheckedOutBy { get; set; }
        public string CheckedOutById { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int NumberofPages { get; set; }
        public string ISBN { get; set; }
    }
    public class DashBoardVm
    {
        public List<BookVm> MyBooks { get; set; } = new List<BookVm>();
        public List<BookVm> BooksIBorrowed { get; set; } = new List<BookVm>();
        public List<BookVm> AvailableBooks { get; set; } = new List<BookVm>();
    }

    public class BookDetailsVm

    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Summary { get; set; }

    }
}