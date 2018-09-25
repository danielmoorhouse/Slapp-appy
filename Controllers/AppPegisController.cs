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
    public class AppPegisController : Controller
    {
        private SlappDatabase1Context db = new SlappDatabase1Context();

        // GET: AppPegis
    public ActionResult Index()
        {
            List<AppPegViewModel> AppPegList =
                new List<AppPegViewModel>();

            List<AppPegi> appPegis;

            appPegis = db.AppPegis.ToList();

            foreach (AppPegi a in appPegis)
            {
                App app = db.App.Where(x => x.AppId == a.AppId).Single();
                Pegi pegi = db.Pegis.Where(x => x.PegiId == a.PegiId).Single();

                AppPegViewModel toAdd = new AppPegViewModel();

                toAdd.AppPegi = a; //get the AppPegiList record
                toAdd.app = app;         //get the app record
                toAdd.Pegi = pegi; //get the pegi record


                AppPegList.Add(toAdd);
            }
            return View(AppPegList);
        }

        // GET: AppPegis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppPegi appPegi = db.AppPegis.Find(id);
            if (appPegi == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // GET: AppPegis/Create
        public ActionResult Create(int AppId = 0, int PegiId = 0)
        {
            //APPS---------------------------------------------------------------------------------
            //from the Apps model DbSet
            //select all columns from database
            //order by the app name
            var appQuery = from a in db.App
                           orderby a.AppName
                           select a;
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

            //Pegi----------------------------------------------------------------------------------

            var pegQuery = from p in db.Pegis
                           orderby p.PegRate
                           select p;

            //if no id set
            if (PegiId == 0)
                //construct full apps dropdown list without preselection
                //from the query results and display the AppName
                //store the AppId in the viewbag
                ViewBag.PegiId = new SelectList(pegQuery, "PegiId",
                                                          "PegRate", null);
            else
                //construct as above but with AppID preselected
                ViewBag.PegiId = new SelectList(pegQuery, "PegiId",
                                                        "PegRate", PegiId);


            return View();
        }

        // POST: AppPegis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppPegiId,AppId,PegiId")] AppPegi appPegi)
        {
            if (ModelState.IsValid)
            {
                db.AppPegis.Add(appPegi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appPegi);
        }

        // GET: AppPegis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppPegi appPegi = db.AppPegis.Find(id);
            if (appPegi == null)
            {
                return HttpNotFound();
            }
            //APPS---------------------------------------------------------------------------------
            //from the Apps model DbSet
            //select all columns from database
            //order by the app name
            var appQuery = from a in db.App
                           orderby a.AppName
                           select a;
            //if no id set
         
                //construct full apps dropdown list without preselection
                //from the query results and display the AppName
                //store the AppId in the viewbag
          
                //construct as above but with AppID preselected
                ViewBag.AppId = new SelectList(appQuery, "AppId",
                                                        "AppName", appPegi.AppId);

            //Pegi----------------------------------------------------------------------------------

            var pegQuery = from p in db.Pegis
                           orderby p.PegRate
                           select p;

                //construct as above but with AppID preselected
                ViewBag.PegiId = new SelectList(pegQuery, "PegiId",
                                                        "PegRate", appPegi.PegiId);

            return View(appPegi);
        }

        // POST: AppPegis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppPegiId,AppId,PegiId")] AppPegi appPegi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appPegi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appPegi);
        }

        // GET: AppPegis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppPegi appPegi = db.AppPegis.Find(id);
            if (appPegi == null)
            {
                return HttpNotFound();
            }
            //APPS---------------------------------------------------------------------------------
            //from the Apps model DbSet
            //select all columns from database
            //order by the app name
            var appQuery = from a in db.App
                           orderby a.AppName
                           select a;
            //if no id set

            //construct full apps dropdown list without preselection
            //from the query results and display the AppName
            //store the AppId in the viewbag

            //construct as above but with AppID preselected
            ViewBag.AppId = new SelectList(appQuery, "AppId",
                                                    "AppName", appPegi.AppId);

            //Pegi----------------------------------------------------------------------------------

            var pegQuery = from p in db.Pegis
                           orderby p.PegRate
                           select p;

            //construct as above but with AppID preselected
            ViewBag.PegiId = new SelectList(pegQuery, "PegiId",
                                                    "PegRate", appPegi.PegiId);

            return View(appPegi);

           
        }

        // POST: AppPegis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AppPegi appPegi = db.AppPegis.Find(id);
            db.AppPegis.Remove(appPegi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

      
    }
}
