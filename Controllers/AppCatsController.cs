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
    public class AppCatsController : Controller
    {
        private SlappDatabase1Context db = new SlappDatabase1Context();

        //to be accessed via AJAX - autocomplete jQuery UI plugin
  

        // GET: AppCats
        public ActionResult Index(string sortOrder, string searchString,
            string currentFilter)
        {
          //create a list for the viewmodel to link App and Category
            IList<CategoryListViewModel> catList =
            new List<CategoryListViewModel>();
            //seperate list for the Category Credit to get the keys
            List<AppCat> categoryCredits;
            //populate the Develop list by selecting all records
            //fromthe db context
            categoryCredits = db.AppCats.ToList();

            //loop through each record to get the foreign keys
            //then populate the viewmodel with the relevant app / developer

            foreach (AppCat a in categoryCredits)
            {
                //match the id between App and Cat, store the single record in app
                App app = db.App.Where(x => x.AppId == a.AppId).SingleOrDefault();
                Cat cat = db.Cat.Where(x => x.CatId == a.CatsId).SingleOrDefault();

                //new DeveloplistViewModel
                CategoryListViewModel toAdd = new CategoryListViewModel();

                toAdd.AppCatLink = a; //get the develop credit record
                toAdd.App = app;      //get the app record
                toAdd.Cat = cat;     //get the company record


                catList.Add(toAdd);
            }

            return View(catList);
        }

   


        public ActionResult Social()
        {
            List<CategoryListViewModel> socialList = new List<CategoryListViewModel>();
            List<AppCat> categoryCredits = db.AppCats.Where(a => a.CatsId == (4)).ToList();
            foreach (AppCat a in categoryCredits)
            {
                App app = db.App.Where(x => x.AppId == a.AppId).SingleOrDefault();
                Cat cat = db.Cat.Where(x => x.CatId == a.CatsId).SingleOrDefault();

                //new DeveloplistViewModel
                CategoryListViewModel toAdd = new CategoryListViewModel();

                toAdd.AppCatLink = a; //get the develop credit record
                toAdd.App = app;      //get the app record
                toAdd.Cat = cat;     //get the company record


                socialList.Add(toAdd);
            }
            var apps = from a in db.AppCats

                       where a.CatsId == (4)
                       select a;


            return View(socialList);

        }
        public ActionResult Music()
        {
            List<CategoryListViewModel> socialList = new List<CategoryListViewModel>();
            List<AppCat> categoryCredits = db.AppCats.Where(a => a.CatsId == (2)).ToList();
            foreach (AppCat a in categoryCredits)
            {
                App app = db.App.Where(x => x.AppId == a.AppId).SingleOrDefault();
                Cat cat = db.Cat.Where(x => x.CatId == a.CatsId).SingleOrDefault();

                //new DeveloplistViewModel
                CategoryListViewModel toAdd = new CategoryListViewModel();

                toAdd.AppCatLink = a; //get the develop credit record
                toAdd.App = app;      //get the app record
                toAdd.Cat = cat;     //get the company record


                socialList.Add(toAdd);
            }
            var apps = from a in db.AppCats

                       where a.CatsId == (2)
                       select a;


            return View(socialList);

        }
        public ActionResult Organise()
        {
            List<CategoryListViewModel> socialList = new List<CategoryListViewModel>();
            List<AppCat> categoryCredits = db.AppCats.Where(a => a.CatsId == (3)).ToList();
            foreach (AppCat a in categoryCredits)
            {
                App app = db.App.Where(x => x.AppId == a.AppId).SingleOrDefault();
                Cat cat = db.Cat.Where(x => x.CatId == a.CatsId).SingleOrDefault();

                //new DeveloplistViewModel
                CategoryListViewModel toAdd = new CategoryListViewModel();

                toAdd.AppCatLink = a; //get the develop credit record
                toAdd.App = app;      //get the app record
                toAdd.Cat = cat;     //get the company record


                socialList.Add(toAdd);
            }
            var apps = from a in db.AppCats

                       where a.CatsId == (3)
                       select a;


            return View(socialList);

        }
        public ActionResult Creative()
        {
            List<CategoryListViewModel> socialList = new List<CategoryListViewModel>();
            List<AppCat> categoryCredits = db.AppCats.Where(a => a.CatsId == (1)).ToList();
            foreach (AppCat a in categoryCredits)
            {
                App app = db.App.Where(x => x.AppId == a.AppId).SingleOrDefault();
                Cat cat = db.Cat.Where(x => x.CatId == a.CatsId).SingleOrDefault();

                //new DeveloplistViewModel
                CategoryListViewModel toAdd = new CategoryListViewModel();

                toAdd.AppCatLink = a; //get the develop credit record
                toAdd.App = app;      //get the app record
                toAdd.Cat = cat;     //get the company record


                socialList.Add(toAdd);
            }
            var apps = from a in db.AppCats

                       where a.CatsId == (1)
                       select a;


            return View(socialList);

        }


        // GET: AppCats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppCat appCat = db.AppCats.Find(id);
            if (appCat == null)
            {
                return HttpNotFound();
            }
        
            return View(appCat);
        }
        

        // GET: AppCats/Create
        public ActionResult Create(int AppId = 0, int CatId = 0)
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

            //Category-----------------------------------------------------------------------------------

            var CatQuery = from c in db.Cat
                           orderby c.CatName
                           select c;

            //if no id set
            if (CatId == 0)
                //construct full apps dropdown list without preselection
                //from the query results and display the AppName
                //store the AppId in the viewbag
                ViewBag.CatId = new SelectList(CatQuery, "CatId",
                                                          "CatName", null);
            else
                //construct as above but with AppID preselected
                ViewBag.CatId = new SelectList(CatQuery, "CatId",
                                                        "CatName", CatId);


            return View();
        }

        // POST: AppCats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppCatId,AppId,CatId")] AppCat appCat)
        {
            if (ModelState.IsValid)
            {
                db.AppCats.Add(appCat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appCat);
        }

        // GET: AppCats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppCat appCat = db.AppCats.Find(id);

            if (appCat == null)
            {
                return HttpNotFound();
            }
            //APPS---------------------------------------------------------------------------------
            //from the Apps model DbSet
            //select all columns from database
            //order by the app name
            var appQuery = from m in db.App
                           orderby m.AppName
                           select m;



            //construct as above but with AppID preselected
            ViewBag.AppId = new SelectList(appQuery, "AppId",
                                                    "AppName", appCat.AppId);

            //Category-----------------------------------------------------------------------------------

            var CatQuery = from m in db.Cat
                           orderby m.CatName
                           select m;


            //construct full cats dropdown list without preselection
            //from the query results and display the CatName
            //store the Cat id
            //construct as above but with AppID preselected
            ViewBag.CatList = new SelectList(CatQuery, "CatId",
                                                    "CatName", appCat.CatsId);


            return View();
        }

        // POST: AppCats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppCatId,AppId,CatId")] AppCat appCat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appCat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appCat);
        }

        // GET: AppCats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppCat appCat = db.AppCats.Find(id);
            if (appCat == null)
            {
                return HttpNotFound();
            }
        
        //APPS---------------------------------------------------------------------------------
        //from the Apps model DbSet
        //select all columns from database
        //order by the app name
        var appQuery = from m in db.App
                       orderby m.AppName
                       select m;



        //construct as above but with AppID preselected
        ViewBag.AppId = new SelectList(appQuery, "AppId",
                                                    "AppName", appCat.AppId);

        //Category-----------------------------------------------------------------------------------

        var CatQuery = from m in db.Cat
                       orderby m.CatName
                       select m;


        //construct full cats dropdown list without preselection
        //from the query results and display the CatName
        //store the Cat id
        //construct as above but with AppID preselected
        ViewBag.CatList = new SelectList(CatQuery, "CatId",
                                                    "CatName", appCat.CatsId);
          
            return View();
        }

        // POST: AppCats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AppCat appCat = db.AppCats.Find(id);
            db.AppCats.Remove(appCat);
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
