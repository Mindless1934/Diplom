using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class RailwaysController : Controller
    {
        private TTCEntities db = new TTCEntities();

        // GET: Railways
        public ActionResult Index()
        {
            return View(db.Railway.ToList());
        }

        // GET: Railways/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Railway railway = db.Railway.Find(id);
            if (railway == null)
            {
                return HttpNotFound();
            }
            return View(railway);
        }

        // GET: Railways/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Railways/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idRailway,RailwayType,RailwayName")] Railway railway)
        {
            if (ModelState.IsValid)
            {
                db.Railway.Add(railway);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(railway);
        }

        // GET: Railways/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Railway railway = db.Railway.Find(id);
            if (railway == null)
            {
                return HttpNotFound();
            }
            return View(railway);
        }

        // POST: Railways/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idRailway,RailwayType,RailwayName")] Railway railway)
        {
            if (ModelState.IsValid)
            {
                db.Entry(railway).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(railway);
        }

        // GET: Railways/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Railway railway = db.Railway.Find(id);
            if (railway == null)
            {
                return HttpNotFound();
            }
            return View(railway);
        }

        // POST: Railways/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Railway railway = db.Railway.Find(id);
            db.Railway.Remove(railway);
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
