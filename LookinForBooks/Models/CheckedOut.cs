using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LookinForBooks.Models
{
    public class CheckedOutVM
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public  string OwnerId { get; set; }
        public string OwnerName { get; set; }
    }
}