using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI.WebControls;

namespace LookinForBooks.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int NumberofPages { get; set; }
        public string Isbn { get; set; }
        public User Owner { get; set; }

        public virtual ICollection<BookLoan> LoanedOut { get; set; } = new List<BookLoan>();

    }

    public class BookLoan   
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public User CheckedOutBy { get; set; }
        public DateTime CheckedOut { get; set; }
        public DateTime? CheckedIn { get; set; }

    }
} 