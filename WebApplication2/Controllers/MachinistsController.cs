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
    public class MachinistsController : Controller
    {
        private TTCEntities db = new TTCEntities();

        // GET: Machinists
        public ActionResult Index()
        {
            return View(db.Machinist.ToList());
        }

        // GET: Machinists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machinist machinist = db.Machinist.Find(id);
            if (machinist == null)
            {
                return HttpNotFound();
            }
            return View(machinist);
        }

        // GET: Machinists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Machinists/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idMachinist,FIO,DateBirth,Address,Category,Telephone")] Machinist machinist)
        {
            if (ModelState.IsValid)
            {
                db.Machinist.Add(machinist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(machinist);
        }

        // GET: Machinists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machinist machinist = db.Machinist.Find(id);
            if (machinist == null)
            {
                return HttpNotFound();
            }
            return View(machinist);
        }

        // POST: Machinists/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMachinist,FIO,DateBirth,Address,Category,Telephone")] Machinist machinist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(machinist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(machinist);
        }

        // GET: Machinists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machinist machinist = db.Machinist.Find(id);
            if (machinist == null)
            {
                return HttpNotFound();
            }
            return View(machinist);
        }

        // POST: Machinists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Machinist machinist = db.Machinist.Find(id);
            db.Machinist.Remove(machinist);
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
