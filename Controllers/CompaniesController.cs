using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SlappMain.Models;

namespace SlappMain.Controllers
{
    public class CompaniesController : Controller
    {
        private SlappDatabase1Context db = new SlappDatabase1Context();

        public ActionResult Search(string term)
        {
            var devs = from d in db.Companies
                       select new
                       {
                           id = d.CompId,
                           label = d.CompName
                       };
            devs = devs.Where(d => d.label.Contains(term));
            return Json(devs, JsonRequestBehavior.AllowGet);
        }
        // GET: Companies
        public ActionResult Index(string sortOrder, string searchString,
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
            var comps = from b in db.Companies
                       select b;
            //check if the search string is not empty 
            if (!String.IsNullOrEmpty(searchString))
            {
                //if we have a searchterm, select where the app name contains it
                //analogous to LIKE %term% in SQL
                comps = comps.Where(a => a.CompName.Contains(searchString));
            }
            //check the sort order param
            switch (sortOrder)
            {
                case "name_desc":
                    //order by name descending
                    comps = comps.OrderByDescending(a => a.CompName);
                    break;
                default:
                    //order by name ascending
                    comps = comps.OrderBy(a => a.CompName);
                    break;
            }
            int pageSize = 12;

            int pageNumber = (page ?? 1);


            //send updated apps list to the view 
            return View(comps.ToPagedList(pageNumber, pageSize));
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Companies companies = db.Companies.Find(id);
            if (companies == null)
            {
                return HttpNotFound();
            }
            return View(companies);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompId,CompName,CompLogo,CompCont")] Companies companies,
        HttpPostedFileBase uploads)
        {
            if (ModelState.IsValid)
            {
                if (uploads != null && uploads.ContentLength > 0)

                {
                    //check if valid MIME type
                    if (uploads.ContentType == "image/jpeg" ||
                        uploads.ContentType == "image/jpg" ||
                        uploads.ContentType == "image/gif" ||
                        uploads.ContentType == "image/png")

                    {
                        //construct path to put the file in Content/images
                        string path1 = Path.Combine(Server.MapPath("~/Content/ManLogo/"),
                                      Path.GetFileName(uploads.FileName));
                        //store the relative path to the image in the database
                        companies.CompLogo = "~/Content/ManLogo/" +
                                 Path.GetFileName(uploads.FileName);
                        //save file to path location
                        uploads.SaveAs(path1);
                    }
                    else
                    {
                        //message to be displayed in view
                        ViewBag.Message = "Not valid image format";
                    }
                }
                db.Companies.Add(companies);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(companies);
            }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Companies companies = db.Companies.Find(id);
            if (companies == null)
            {
                return HttpNotFound();
            }
            return View(companies);
        }


        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompId,CompName,CompLogo,CompCont")] Companies companies,
             HttpPostedFileBase uploads)
        {
            if (ModelState.IsValid)
            {

                if (uploads != null && uploads.ContentLength > 0)

                {
                    //check if valid MIME type
                    if (uploads.ContentType == "image/jpeg" ||
                        uploads.ContentType == "image/jpg" ||
                        uploads.ContentType == "image/gif" ||
                        uploads.ContentType == "image/png")

                    {
                        //construct path to put the file in Content/images
                        string path = Path.Combine(Server.MapPath("~/Content/ManLogo/"),
                                      Path.GetFileName(uploads.FileName));
                        //store the relative path to the image in the database
                        companies.CompLogo = "~/Content/ManLogo/" +
                                 Path.GetFileName(uploads.FileName);
                        //save file to path location
                        uploads.SaveAs(path);
                    }

                    else
                    {
                        //message to be displayed in view
                        ViewBag.Message = "Not valid image format";
                    }
                }
                db.Entry(companies).State = EntityState.Modified;
                db.SaveChanges()  ;

                return RedirectToAction("Index");
            }
            return View(companies);
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Companies companies = db.Companies.Find(id);
            if (companies == null)
            {
                return HttpNotFound();
            }
            return View(companies);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Companies companies = db.Companies.Find(id);
            db.Companies.Remove(companies);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
