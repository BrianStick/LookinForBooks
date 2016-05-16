using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            var model = new DashBoardVm();
            model.MyBooks = CurrentUser.BooksIOwn.Select(b => new BookVm(b)).ToList();

            model.AvailableBooks =
                db.Books.Include(x => x.Owner)
                    .Where(b => b.Owner.Id != CurrentUser.Id  && b.LoanedOut.All(x => x.CheckedIn != null))
                    .ToList()
                    .Select(b => new BookVm(b))
                    .ToList();

            model.BooksIBorrowed =
                db.BookLoans.Include(x => x.Book.Owner)
                    .Where(bl => bl.CheckedOutBy.Id == CurrentUser.Id && bl.CheckedIn == null)
                    .ToList()
                    .Select(bl => new BookVm(bl.Book))
                    .ToList();

            return View(model);
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.AllUsers = new SelectList(db.Users.Select(u => new {u.Id, u.UserName}).ToList(), "Id", "UserName");

            return View();
        }

        // POST: BooksTemp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Author,Publisher,NumberofPages,Isbn")] Book book,
            string selectedOwnerId)
        {
            if (ModelState.IsValid)
            {
                var owner = db.Users.Find(selectedOwnerId);

                book.Owner = owner;
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("DashBoard");
            }

            return View(book);
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);

        }

        public ActionResult Delete(int? id)
        {
            // throw new NotImplementedException();????

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult Checkedout(int bookid)
        {
            var book = db.Books.Find(bookid);

            var model = new CheckedOutVM()
            {
                BookId = book.Id,
                Title  =   book.Title,
                OwnerId =  book.Owner?.Id,
                OwnerName = book.Owner == null ? "" : book.Owner.FirstName + " " + book.Owner.LastName,

            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Checkedout(CheckedOutVM checkOut)
        {
            var book = db.Books.Find(checkOut.BookId);


            var bl = new BookLoan() { Book = book, CheckedOut = DateTime.Now, CheckedOutBy = CurrentUser};

            db.BookLoans.Add(bl);
            db.SaveChanges();

            return RedirectToAction("DashBoard");
        }

    }
}
