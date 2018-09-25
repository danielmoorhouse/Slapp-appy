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
    public class CatsController : Controller
    {
        private SlappDatabase1Context db = new SlappDatabase1Context();

        // GET: Cats
        public ActionResult Index()
        {
            var catQuery = from m in db.Cat
                           orderby m.CatId
                           select m;

            return View(catQuery);
        }

        // GET: Cats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cat cat = db.Cat.Find(id);
            if (cat == null)
            {
                return HttpNotFound();
            }
         
             return View(cat);
        }
       

        // GET: Cats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CatId,CatName,CatDesc")] Cat cat)
        {
            if (ModelState.IsValid)
            {
                db.Cat.Add(cat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cat);
        }
    

        // GET: Cats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cat cat = db.Cat.Find(id);
            if (cat == null)
            {
                return HttpNotFound();
            }
            return View(cat);
        }

        // POST: Cats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CatId,CatName,CatDesc")] Cat cat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
          
      
            }
            return View(cat);
        }
        public ActionResult Social()
        {

            var social = from a in db.AppCats
                           where a.CatsId == 4
                           select a;
            return View(social);


          
        }

        // GET: Cats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cat cat = db.Cat.Find(id);
            if (cat == null)
            {
                return HttpNotFound();
            }
            return View(cat);
        }

        // POST: Cats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cat cat = db.Cat.Find(id);
            db.Cat.Remove(cat);
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
