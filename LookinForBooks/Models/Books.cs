using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LookinForBooks.Models
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int NumberofPages { get; set; }
        public int ISBN { get; set; }
}