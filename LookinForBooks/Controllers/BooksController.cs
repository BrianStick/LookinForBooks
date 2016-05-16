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
            model.MyBooks = CurrentUser.BooksIOwn.Select(b => new BookVm()
            {
                IsCheckedOut = db.BookLoans.Any(x => x.Book.Id == b.Id && x.CheckedIn == null),
                BookId = b.Id,
                Title = b.Title,
            }).ToList();

            model.AvailableBook = db.Books.Where(b => b.Owner.Id != CurrentUser.Id).Select(b => new BookVm()
            {
                BookId = b.Id,
                Title = b.Title,
            }).ToList();

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


        public ActionResult Details(int id)
        {
            //load the book from the database and populate a BookDetailsVM? and pass it to the view
            var modelBd = new BookDetailsVm();
            db.Books.Load();
            return View(modelBd);
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
    }
}