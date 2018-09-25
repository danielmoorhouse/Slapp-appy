using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SlappMain.Models;
using SlappMain.Models.ViewModels;

namespace SlappMain.Controllers
{
    public class PegisController : Controller
    {
        private SlappDatabase1Context db = new SlappDatabase1Context();

        // GET: Pegis
        public ActionResult Index()
           
        {
            var pegs = from a in db.Pegis
                       select a;

            return View(pegs);
        }

        // GET: Pegis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pegi pegi = db.Pegis.Find(id);
            if (pegi == null)
            {
                return HttpNotFound();
            }
            return View(pegi);
        }

        // GET: Pegis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pegis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PegiId,PegRate,PegDesc,PegImg")] Pegi pegi,
            HttpPostedFileBase upload)
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
                        //save file to path location
                        upload.SaveAs(path);
                        //store the relative path to the image in the database
                        pegi.PegImg = "~/Content/images/" +
                                 Path.GetFileName(upload.FileName);
                    }
                    else
                    {
                        //message to be displayed in view
                        ViewBag.Message = "Not valid image format";
                    }

                }
                        db.Pegis.Add(pegi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pegi);
        }

        // GET: Pegis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pegi pegi = db.Pegis.Find(id);
            if (pegi == null)
            {
                return HttpNotFound();
            }
            return View(pegi);
        }

        // POST: Pegis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PegiId,PegRate,PegDesc,PegImg")] Pegi pegi,
            HttpPostedFileBase upload)
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
                        //save file to path location
                        upload.SaveAs(path);
                        //store the relative path to the image in the database
                        pegi.PegImg = "~/Content/images/" +
                                 Path.GetFileName(upload.FileName);
                    }
                    else
                    {
                        //message to be displayed in view
                        ViewBag.Message = "Not valid image format";
                    }

                }
                db.Entry(pegi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pegi);
        }

        // GET: Pegis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pegi pegi = db.Pegis.Find(id);
            if (pegi == null)
            {
                return HttpNotFound();
            }
            return View(pegi);
        }

        // POST: Pegis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pegi pegi = db.Pegis.Find(id);
            db.Pegis.Remove(pegi);
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
