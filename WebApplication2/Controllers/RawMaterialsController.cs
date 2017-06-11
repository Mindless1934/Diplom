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
    public class RawMaterialsController : Controller
    {
        private TTCEntities db = new TTCEntities();

        // GET: RawMaterials
        public ActionResult Index()
        {
            return View(db.RawMaterials.ToList());
        }

        // GET: RawMaterials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterials rawMaterials = db.RawMaterials.Find(id);
            if (rawMaterials == null)
            {
                return HttpNotFound();
            }
            return View(rawMaterials);
        }

        // GET: RawMaterials/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RawMaterials/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idMaterial,NameMaterial")] RawMaterials rawMaterials)
        {
            if (ModelState.IsValid)
            {
                db.RawMaterials.Add(rawMaterials);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rawMaterials);
        }

        // GET: RawMaterials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterials rawMaterials = db.RawMaterials.Find(id);
            if (rawMaterials == null)
            {
                return HttpNotFound();
            }
            return View(rawMaterials);
        }

        // POST: RawMaterials/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMaterial,NameMaterial")] RawMaterials rawMaterials)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rawMaterials).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rawMaterials);
        }

        // GET: RawMaterials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterials rawMaterials = db.RawMaterials.Find(id);
            if (rawMaterials == null)
            {
                return HttpNotFound();
            }
            return View(rawMaterials);
        }

        // POST: RawMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RawMaterials rawMaterials = db.RawMaterials.Find(id);
            db.RawMaterials.Remove(rawMaterials);
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
