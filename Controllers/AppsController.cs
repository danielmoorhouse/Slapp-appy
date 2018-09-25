using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using PagedList;
using SlappMain.Models;
using SlappMain.Models.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace SlappMain.Controllers
{
    public class AppsController : BaseController
    {
        private SlappDatabase1Context db = new SlappDatabase1Context();

        //to be accessed via AJAX - autocomplete jQuery UI plugin

            public ActionResult Search(string term)
        {
            //select all apps in db
            //get the id and name only
            //id and label used for autocomplete functionality
            var apps = from a in db.App
                       select new
                       {
                           id = a.AppId,
                           label = a.AppName
                       };
            //check search string given for matches in name
            apps = apps.Where(a => a.label.Contains(term));

            //convert to and return the json for the search ui
            return Json(apps, JsonRequestBehavior.AllowGet);
        }

       

        // GET: Applications
        public ActionResult Index(string sortOrder, string searchString, string searchString1,
            string currentFilter, int? page)
        {
            //for the viewbag to keep a note of current sort order
            ViewBag.CurrentSort = sortOrder;
            //add a new value to the Viewbag to retain current sort order
            //check if the sortOrder param is empty - if so we'll set the next choice
            //to name_desc (order by name descending) otherwise empty string 
            //lets us construct a toggle link for the alternative
            ViewBag.NameSortParam =
                String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            //if there is a search string
            if (searchString != null)
            {
                //set page as 1
                page = 1;
            }
            else
            {
                //if no search string set to current filter
                searchString = currentFilter;
            }

            //the current filter is now the searchstring - note kept in view
            ViewBag.CurrentFilter = searchString;

            //select all apps in the database
            var apps = from b in db.App
                       select b;
            //check if the search string is not empty 
            if (!String.IsNullOrEmpty(searchString))
            {
                //if we have a searchterm, select where the app name contains it
                //analogous to LIKE %term% in SQL
                apps = apps.Where(a => a.AppName.Contains(searchString));
            }
            //check the sort order param
            switch (sortOrder)
            {
                case "name_desc":
                    //order by name descending
                    apps = apps.OrderByDescending(a => a.AppName);
                    break;
                default:
                    //order by name ascending
                    apps = apps.OrderBy(a => a.AppName);
                    break;
            }
            int pageSize = 12;

            int pageNumber = (page ?? 1);


            //send updated apps list to the view 
            return View(apps.ToPagedList(pageNumber, pageSize));
        }

        // GET: Applications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App application = db.App.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            //new view model object
            AppViewModel appPage = new AppViewModel();

            //get the current app and assign to view model
            appPage.App = application;
            //populate the reviews list for the view model by matching
            //all reviews where the app id matches
            appPage.Reviews = db.Review.Where(x => x.AppId == application.AppId).ToList();

            //for companies, first we need to get all related records from join table
            IList<Develop> developLinks = db.Dev.Where(x => x.AppId == application.AppId).ToList();
            //construct a list of company records to match
            IList<Companies> companies = new List<Companies>();
            //here we loop through the develop records in join table
            foreach (Develop d in developLinks)
            {
                //add to the list of developers the matching company record 
                companies.Add(db.Companies.Where(x => x.CompId == d.CompanyId).Single());
            }
            //once populated, we can assign the list of companies as developers
            //to the view model
            appPage.Companies = companies;
            //for platforms we need to get all related records from join table
            IList<AppPlats> platformLinks = db.AppPlats.Where(x => x.AppId == application.AppId).ToList();
            //construct a list of platform records to match
            IList<Platform> platforms = new List<Platform>();
            //loop through app/platform records in join table
            foreach (AppPlats p in platformLinks)
            {
                //add to list of app plats the matching platform record
                platforms.Add(db.Platforms.Where(x => x.PlatId == p.PlatId).Single());
            }
            //once populated, assign the list of Platforms as app plats
            //to the viewmodel
            appPage.Platforms = platforms;

            //////Pegi
            //for platforms we need to get all related records from join table
            IList<AppPegi> pegiLinks = db.AppPegis.Where(x => x.AppId == application.AppId).ToList();
            //construct a list of platform records to match
            IList<Pegi> pegis = new List<Pegi>();
            //loop through app/platform records in join table
            foreach (AppPegi p in pegiLinks)
            {
                //add to list of app plats the matching platform record
                pegis.Add(db.Pegis.Where(x => x.PegiId == p.PegiId).Single());
            }
            //once populated, assign the list of Platforms as app plats
            //to the viewmodel
            appPage.Pegi = pegis;

            //here we will use a LINQ query to get the average review score for reviews 
            //related to this app - the additional ? symbols are if there is a null result
            //if so we set to 0
            ViewBag.AverageReview =
                db.Review.Where(x => x.AppId == application.AppId)
                                .Average(x => (double?)x.ReviewRating) ?? 0;
            return View(appPage);
        }
        public ActionResult mainIndex(string sortOrder, string searchString,
            string currentFilter, int? page)
        {
            //for the viewbag to keep a note of current sort order
            ViewBag.CurrentSort = sortOrder;
            //add a new value to the Viewbag to retain current sort order
            //check if the sortOrder param is empty - if so we'll set the next choice
            //to name_desc (order by name descending) otherwise empty string 
            //lets us construct a toggle link for the alternative
            ViewBag.NameSortParam =
                String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            //if there is a search string
            if (searchString != null)
            {
                //set page as 1
                page = 1;
            }
            else
            {
                //if no search string set to current filter
                searchString = currentFilter;
            }

            //the current filter is now the searchstring - note kept in view
            ViewBag.CurrentFilter = searchString;

            //select all apps in the database
            var apps = from b in db.App
                       select b;
            //check if the search string is not empty 
            if (!String.IsNullOrEmpty(searchString))
            {
                //if we have a searchterm, select where the app name contains it
                //analogous to LIKE %term% in SQL
                apps = apps.Where(a => a.AppName.Contains(searchString));
            }
            //check the sort order param
            switch (sortOrder)
            {
                case "name_desc":
                    //order by name descending
                    apps = apps.OrderByDescending(a => a.AppName);
                    break;
                default:
                    //order by name ascending
                    apps = apps.OrderBy(a => a.AppName);
                    break;
            }


            //send updated apps list to the view 
            return View(apps);
        }

        public ActionResult Free()
        {
            var freeApps = from a in db.App
                           where a.AppPrice == 0
                           select a;
            return View(freeApps);
        }
        public ActionResult Paid()
        {
            var paidApps = from a in db.App
                           where a.AppPrice > 0
                           select a;
            return View(paidApps);
        }

        public ActionResult App(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App application = db.App.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Applications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "AppId,AppName,AppDesc,AppDate,AppImg1,AppImg2,AppPrice,AppTile")] App Apps,
            HttpPostedFileBase upload, HttpPostedFileBase upload2, HttpPostedFileBase upload3)
           
        {


            if (ModelState.IsValid)
            {

                //check if a file has been uploaded
                if (upload != null && upload.ContentLength > 0)

                {
                    //check if valid MIME type
                    if (upload.ContentType == "image/jpeg" ||
                        upload.ContentType == "image/jpg" ||
                        upload.ContentType == "image/gif" ||
                        upload.ContentType == "image/png")

                    {
                        //construct path to put the file in Content/images
                        string path = Path.Combine(Server.MapPath("~/Content/images/"),
                                      Path.GetFileName(upload.FileName));
                        //store the relative path to the image in the database
                        Apps.AppImg1 = "~/Content/images/" +
                                 Path.GetFileName(upload.FileName);
                        //save file to path location
                        upload.SaveAs(path);
                    }
                    if (upload2 != null && upload2.ContentLength > 0)

                    {
                        //check if valid MIME type
                        if (upload2.ContentType == "image/jpeg" ||
                            upload2.ContentType == "image/jpg" ||
                            upload2.ContentType == "image/gif" ||
                            upload2.ContentType == "image/png")

                        {
                            //construct path to put the file in Content/images
                            string path = Path.Combine(Server.MapPath("~/Content/images2/"),
                                          Path.GetFileName(upload2.FileName));
                            //store the relative path to the image in the database
                            Apps.AppImg2 = "~/Content/images2/" +
                                     Path.GetFileName(upload2.FileName);
                            //save file to path location
                            upload2.SaveAs(path);
                        }
                    }
                    if (upload3 != null && upload3.ContentLength > 0)

                    {
                        //check if valid MIME type
                        if (upload3.ContentType == "image/jpeg" ||
                            upload3.ContentType == "image/jpg" ||
                            upload3.ContentType == "image/gif" ||
                            upload3.ContentType == "image/png")

                        {
                            //construct path to put the file in Content/images
                            string path = Path.Combine(Server.MapPath("~/Content/appTiles"),
                                          Path.GetFileName(upload3.FileName));
                            //store the relative path to the image in the database
                            Apps.AppTile = "~/Content/appTiles/" +
                                     Path.GetFileName(upload3.FileName);
                            //save file to path location
                            upload3.SaveAs(path);
                        }
                    }
                    else
                    {
                        //message to be displayed in view
                        ViewBag.Message = "Not valid image format";
                    }
                        }
                        //add app to database
                        db.App.Add(Apps);
                        db.SaveChanges();
                        //back to index
                        return RedirectToAction("Index");
                    }

                    return View(Apps);
                }
            
        // GET: Applications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App application = db.App.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (
            [Bind(Include = "AppId,AppName,AppDesc,AppDate,AppImg1,AppImg2,AppPrice,AppTile")] App Apps,
            HttpPostedFileBase upload, HttpPostedFileBase upload2, HttpPostedFileBase upload3)

        {


            if (ModelState.IsValid)
            {

                //check if a file has been uploaded
                if (upload != null && upload.ContentLength > 0)

                {
                    //check if valid MIME type
                    if (upload.ContentType == "image/jpeg" ||
                        upload.ContentType == "image/jpg" ||
                        upload.ContentType == "image/gif" ||
                        upload.ContentType == "image/png")

                    {
                        //construct path to put the file in Content/images
                        string path = Path.Combine(Server.MapPath("~/Content/images/"),
                                      Path.GetFileName(upload.FileName));
                        //store the relative path to the image in the database
                        Apps.AppImg1 = "~/Content/images/" +
                                 Path.GetFileName(upload.FileName);
                        //save file to path location
                        upload.SaveAs(path);
                    }
                    if (upload2 != null && upload2.ContentLength > 0)

                    {
                        //check if valid MIME type
                        if (upload2.ContentType == "image/jpeg" ||
                            upload2.ContentType == "image/jpg" ||
                            upload2.ContentType == "image/gif" ||
                            upload2.ContentType == "image/png")

                        {
                            //construct path to put the file in Content/images
                            string path = Path.Combine(Server.MapPath("~/Content/images2/"),
                                          Path.GetFileName(upload2.FileName));
                            //store the relative path to the image in the database
                            Apps.AppImg2 = "~/Content/images2/" +
                                     Path.GetFileName(upload2.FileName);
                            //save file to path location
                            upload2.SaveAs(path);
                        }
                    }
                    if (upload3 != null && upload3.ContentLength > 0)

                    {
                        //check if valid MIME type
                        if (upload3.ContentType == "image/jpeg" ||
                            upload3.ContentType == "image/jpg" ||
                            upload3.ContentType == "image/gif" ||
                            upload3.ContentType == "image/png")

                        {
                            //construct path to put the file in Content/images
                            string path = Path.Combine(Server.MapPath("~/Content/appTiles"),
                                          Path.GetFileName(upload3.FileName));
                            //store the relative path to the image in the database
                            Apps.AppTile = "~/Content/appTiles/" +
                                     Path.GetFileName(upload3.FileName);
                            //save file to path location
                            upload3.SaveAs(path);
                        }
                    }
                    else
                    {
                        //message to be displayed in view
                        ViewBag.Message = "Not valid image format";
                    }
                }
                //add app to database
                db.Entry(Apps).State = EntityState.Modified;
                db.SaveChanges();
            
                //back to index
                return RedirectToAction("Index");
            }

            return View(Apps);
        }

        // GET: Applications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App application = db.App.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            App Application = db.App.Find(id);
            db.App.Remove(Application);
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

