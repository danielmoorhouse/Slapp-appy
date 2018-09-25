using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SlappMain.Models;
using SlappMain.Models.ViewModels;

namespace SlappMain.Controllers
{
    public class DevelopsController : Controller
    {
        private SlappDatabase1Context db = new SlappDatabase1Context();

     

        // GET: Develops
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
         
            //create a list for the viewmodel to link App and Company
            List<DeveloplistViewModel> DevelopList =
               new List<DeveloplistViewModel>();
        //seperate list for the Develop Credit to get the keys
        List<Develop> developCredits;
        //populate the Develop list by selecting all records
        //fromthe db context
        developCredits = db.Dev.ToList();
        
            //loop through each record to get the foreign keys
            //then populate the viewmodel with the relevant app / developer

            foreach (Develop a in developCredits)
            {
                //match the id between Develop and App store the single record in app
                App app = db.App.Where(x => x.AppId == a.AppId).Single();
                //match the id between Develop and App - stroe the single record in develop
                Companies companies = db.Companies.Where(x => x.CompId == a.CompanyId).SingleOrDefault();

                //new DeveloplistViewModel
                DeveloplistViewModel toAdd = new DeveloplistViewModel();

                toAdd.DevelopCredit = a; //get the develop credit record
                toAdd.app = app;         //get the app record
                toAdd.Company = companies; //get the company record


                DevelopList.Add(toAdd);
            }
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(DevelopList.ToPagedList(pageNumber, pageSize)); 
        }







        // GET: Develops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Develop develop = db.Dev.Find(id);
            if (develop == null)
            {
                return HttpNotFound();
            }
            DevDetViewModel devPage = new DevDetViewModel();

            //get the current developer and assign to view model
            devPage.develops = develop;
            //populate the list for the view model by matching
            //all apps where the company id matches

            devPage.apps = new List<App>();
            devPage.companies = new List<Companies>();
            devPage.Reviews = db.Review.Where(x => x.AppId == develop.AppId).ToList();
            devPage.companies = db.Companies.Where(x => x.CompId == develop.CompanyId).ToList();
            devPage.apps = db.App.Where(x => x.AppId == develop.AppId).ToList();

            ViewBag.AverageReview =
                 db.Review.Where(x => x.AppId == develop.AppId)
                                 .Average(x => (double?)x.ReviewRating) ?? 0;


            return View(devPage);
        }

        // GET: Develops/Create
        public ActionResult Create(int AppId = 0, int CompanyId = 0 )
        {
            //APPS---------------------------------------------------------------------------------
            //from the Apps model DbSet
            //select all columns from database
            //order by the app name
            var appQuery = from m in db.App
                           orderby m.AppName
                           select m;
            //if no id set
            if (AppId == 0)
                //construct full apps dropdown list without preselection
                //from the query results and display the AppName
                //store the AppId in the viewbag
                ViewBag.AppId = new SelectList(appQuery, "AppId",
                                                          "AppName", null);
            else
                //construct as above but with AppID preselected
                ViewBag.AppId = new SelectList(appQuery, "AppId",
                                                        "AppName", AppId);

            //Company-----------------------------------------------------------------------------------

            var CompaniesQuery = from c in db.Companies
                                 orderby c.CompName
                                 select c;

            //if no id set
            if (CompanyId == 0)
                //construct full apps dropdown list without preselection
                //from the query results and display the AppName
                //store the AppId in the viewbag
                ViewBag.CompanyId = new SelectList(CompaniesQuery, "CompId",
                                                          "CompName", null);
            else
                //construct as above but with AppID preselected
                ViewBag.CompanyId = new SelectList(CompaniesQuery, "CompId",
                                                        "CompName", CompanyId);


            return View();
        }

        // POST: Develops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DevId,AppId,CompanyId")] Develop develop)
        {
            if (ModelState.IsValid)
            {
                db.Dev.Add(develop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(develop);
        }

        // GET: Develops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Develop develop = db.Dev.Find(id);
            if (develop == null)
            {
                return HttpNotFound();
            }
            //Code to generate dropdowns
            //apps--------------------------------------------------------------------------------
            //from the Apps model DbSet
            //select all columns from database
            //order by the app name
            var appQuery = from m in db.App
                           orderby m.AppName
                           select m;
            //construct full dropdown list
            ViewBag.AppId = new SelectList(appQuery, "AppId",
                                                       "AppName", develop.AppId);

            //from the Companies model DbSet
            //select all columns from database
            //order by the app name
            var companiesQuery = from c in db.Companies
                           orderby c.CompName
                           select c;
            //construct full dropdown list pre selected with foreign key
            ViewBag.CompanyId = new SelectList(companiesQuery, "CompId",
                                                       "CompName", develop.CompanyId);
            return View(develop);
        }
   
    


        // POST: Develops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DevId,AppId,CompanyId")] Develop develop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(develop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(develop);
        }

        // GET: Develops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Develop develop = db.Dev.Find(id);
            if (develop == null)
            {
                return HttpNotFound();
            }
            return View(develop);
        }

        // POST: Develops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Develop develop = db.Dev.Find(id);
            db.Dev.Remove(develop);
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
