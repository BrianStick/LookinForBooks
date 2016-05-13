using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LookinForBooks.Models;
using Microsoft.AspNet.Identity;

namespace LookinForBooks.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userid = User.Identity.GetUserId();
            CurrentUser = db.Users.Find(userid);
            base.OnActionExecuting(filterContext);
        }
        private User CurrentUser;
        ApplicationDbContext db = ApplicationDbContext.Create();

        [HttpGet]
        public ActionResult DashBoard()
        {
            var model = new DashBoardVM();
            model.MyBooks = CurrentUser.BooksIOwn.Select(b => new BookVM()
            {
                IsCheckedOut = db.BookLoans.Any(x=>x.Book.Id == b.Id && x.CheckedIn == null),
                BookID = b.Id, 
                Title = b.Title,
            }).ToList();

            model.AvailableBook = db.Books.Where(b => b.Owner.Id != CurrentUser.Id).Select(b => new BookVM()
            {
                BookID = b.Id,
                Title = b.Title,
            }).ToList();

            return View(model);
        }

        public ActionResult Details(int id)
        {
            //load the book from the database and populate a BookDetailsVM? and pass it to the view
            return View();
        }
    }
}