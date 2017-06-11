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
    public class LocosController : Controller
    {
        private TTCEntities db = new TTCEntities();

        // GET: Locos
        public ActionResult Index()
        {
            return View(db.Locos.ToList());
        }

        // GET: Locos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locos locos = db.Locos.Find(id);
            if (locos == null)
            {
                return HttpNotFound();
            }
            return View(locos);
        }

        // GET: Locos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locos/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idLoco,LocoName,State")] Locos locos)
        {
            if (ModelState.IsValid)
            {
                db.Locos.Add(locos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(locos);
        }

        // GET: Locos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locos locos = db.Locos.Find(id);
            if (locos == null)
            {
                return HttpNotFound();
            }
            return View(locos);
        }

        // POST: Locos/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idLoco,LocoName,State")] Locos locos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(locos);
        }

        // GET: Locos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locos locos = db.Locos.Find(id);
            if (locos == null)
            {
                return HttpNotFound();
            }
            return View(locos);
        }

        // POST: Locos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Locos locos = db.Locos.Find(id);
            db.Locos.Remove(locos);
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
