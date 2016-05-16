using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LookinForBooks.Models
{
    public class BookVm
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public bool IsCheckedOut { get; set; }
    }
    public class DashBoardVm
    {
        public List<BookVm> MyBooks { get; set; } = new List<BookVm>();
        public List<BookVm> LoandedOutBooks { get; set; } = new List<BookVm>();
        public List<BookVm> AvailableBook { get; set; } = new List<BookVm>();
        public int NumberofPages { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Isbn { get; set; }
    }

    public class BookDetailsVm

    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Summary { get; set; }

    }
}