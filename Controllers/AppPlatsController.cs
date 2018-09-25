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
    public class AppPlatsController : Controller
    {
        private SlappDatabase1Context db = new SlappDatabase1Context();

        // GET: AppPlats
        public ActionResult Index()
        {
            List<AppPlatViewModel> AppPlatList =
                new List<AppPlatViewModel>();

            List<AppPlats> appPlats;

            appPlats = db.AppPlats.ToList();

            foreach (AppPlats b in appPlats)
            {
                App app = db.App.Where(x => x.AppId == b.AppId).Single();
                Platform platform = db.Platforms.Where(x => x.PlatId == b.PlatId).Single();

                AppPlatViewModel toAdd = new AppPlatViewModel();

                toAdd.AppPlats = b; //get the AppPegiList record
                toAdd.Apps = app;         //get the app record
                toAdd.Platform = platform; //get the pegi record


                AppPlatList.Add(toAdd);
            }
            return View(AppPlatList);
        }

        // GET: AppPlats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppPlats appPlats = db.AppPlats.Find(id);
            if (appPlats == null)
            {
                return HttpNotFound();
            }
            return View(appPlats);
        }

        // GET: AppPlats/Create
        public ActionResult Create(int AppId = 0, int PlatId = 0)
        {
            //APPS---------------------------------------------------------------------------------
            var appQuery = from b in db.App
                           orderby b.AppName
                           select b;

            //if no id set
            if (AppId == 0)
                //construct full apps dropdown (no preselection)
                //do so from the query results, display AppName
                //Store AppId in viewbag
                ViewBag.AppId = new SelectList(appQuery, "AppId",
                                                "AppName", null);
            else
                //construct as above but with AppId preselected
                ViewBag.AppId = new SelectList(appQuery, "AppId",
                                    "AppName", AppId);

            //PLATFORMS-----------------------------------------------------------------------------
            //From the Platforms DbSet
            var platformQuery = from p in db.Platforms
                                orderby p.PlatName
                                select p;
            //if no id set
            if (PlatId == 0)
                //construct full platform dropdown (no preselection)
                //do so from the query results, display PlatName
                //Store PlatId in viewbag
                ViewBag.PlatId = new SelectList(platformQuery, "PlatId",
                                                "PlatName", null);

            else
                //construct as above (PlatId preselecetd)
                ViewBag.PlatId = new SelectList(platformQuery, "PlatId",
                                                "PlatName", PlatId);


                return View();
        }

        // POST: AppPlats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppPlatId,AppId,PlatId")] AppPlats appPlats)
        {
            if (ModelState.IsValid)
            {
                db.AppPlats.Add(appPlats);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appPlats);
        }

        // GET: AppPlats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppPlats appPlats = db.AppPlats.Find(id);
            if (appPlats == null)
            {
                return HttpNotFound();
            }
            //APPS-----------------------------------------------------------------
            var appQuery = from b in db.App
                           orderby b.AppName
                           select b;


            ViewBag.AppId = new SelectList(appQuery, "AppId",
                                "AppName", appPlats.AppId);

            //PLATFORMS-----------------------------------------------------------------------------
            //From the Platforms DbSet
            var platformQuery = from p in db.Platforms
                                orderby p.PlatName
                                select p;
       
                //construct full platform dropdown (no preselection)

                //construct as above (PlatId preselecetd)
                ViewBag.PlatId = new SelectList(platformQuery, "PlatId",
                                                "PlatName", appPlats.PlatId);
        

            return View(appPlats);
        }

        // POST: AppPlats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppPlatId,AppId,PlatId")] AppPlats appPlats)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appPlats).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appPlats);
        }

        // GET: AppPlats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppPlats appPlats = db.AppPlats.Find(id);
            if (appPlats == null)
            {
                return HttpNotFound();
            }
            return View(appPlats);
        }

        // POST: AppPlats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AppPlats appPlats = db.AppPlats.Find(id);
            db.AppPlats.Remove(appPlats);
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
