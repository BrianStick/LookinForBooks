using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LookinForBooks.Models
{
    public class BookVM
    {
        public int BookID { get; set; }
        public string Title { get; set; }
    }
    public class DashBoardVM
    {
        public List<BookVM> MyBooks { get; set; } = new List<BookVM>();
        public List<BookVM> LoandedOutBooks { get; set; } = new List<BookVM>();
        public List<BookVM> AvailableBook { get; set; } = new List<BookVM>();
         
    }
}