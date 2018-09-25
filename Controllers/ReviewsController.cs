using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SlappMain.Models;
using SlappMain.Models.ViewModels;

namespace SlappMain.Controllers
{
    public class ReviewsController : Controller
    {
        private SlappDatabase1Context db = new SlappDatabase1Context();

    
        

        // GET: Reviews
        public ActionResult Index()
        {
            //list of app reviews model objects
            //to link reviews with related app
            List<AppReviewViewModel> AppReviewList =
                new List<AppReviewViewModel>();
            //list of review objects to cycle through and map id's
            List<Review> Reviews;
            //populate the list with review records from database
            Reviews = db.Review.ToList();

            //loop through each review in each row of data
            foreach (Review r in Reviews)
            {
                //select the app record where the id's match
                App app = db.App.Where(x => x.AppId == r.AppId).Single();

                //create a new app review view model object to add
                AppReviewViewModel toAdd = new AppReviewViewModel();
                //set the review record and app record from ones
                //matched in the loop
                toAdd.Review = r;
                toAdd.App = app;
                //add to list of app review objects 
                AppReviewList.Add(toAdd);
            }

            return View(AppReviewList);
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Review.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            //find the related film
            App app = db.App.Where(x => x.AppId == review.AppId).Single();
            //create a new view model object and assign the review and app details
            AppReviewViewModel AppReview = new AppReviewViewModel();
            AppReview.Review = review;
            AppReview.App = app;

            //send view model to the view
            return View(AppReview);
        }

        // GET: Reviews/Create
        public ActionResult Create(int id = 0)
        {
            //if no id sent, redirects to reviews index
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            //otherwise, select the app the id matches 
            App app = db.App.Where(x => x.AppId == id).Single();

            //then populate these values in the viewbag
            ViewBag.AppId = id;
            ViewBag.AppName = app.AppName;

            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RevId,AppId,RevUname,ReviewContent,ReviewRating")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Review.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index", "Apps");
            }

            return View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Review.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            //if we have a review, select the app where id matches
            App app = db.App.Where(x => x.AppId == review.AppId).Single();

            //then populate these values in the view bag
            ViewBag.AppId = app.AppId;
            ViewBag.AppName = app.AppName;

            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RevId,AppId,RevUname,ReviewContent,ReviewRating")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Review.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Review.Find(id);
            db.Review.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Index");
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

