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
    public class CargoesController : Controller
    {
        private TTCEntities db = new TTCEntities();

        // GET: Cargoes
        public ActionResult Index()
        {
            var cargo = db.Cargo.Include(c => c.RawMaterials).Include(c => c.Shipment);
            return View(cargo.ToList());
        }

        // GET: Cargoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = db.Cargo.Find(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return View(cargo);
        }

        // GET: Cargoes/Create
        public ActionResult Create()
        {
            ViewBag.idRawMaterial = new SelectList(db.RawMaterials, "idMaterial", "NameMaterial");
            ViewBag.idShipment = new SelectList(db.Shipment, "idShipment", "Sender");
            return View();
        }

        // POST: Cargoes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCargo,idShipment,Number,Weight,State,idRawMaterial")] Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                db.Cargo.Add(cargo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idRawMaterial = new SelectList(db.RawMaterials, "idMaterial", "NameMaterial", cargo.idRawMaterial);
            ViewBag.idShipment = new SelectList(db.Shipment, "idShipment", "Sender", cargo.idShipment);
            return View(cargo);
        }

        // GET: Cargoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = db.Cargo.Find(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            ViewBag.idRawMaterial = new SelectList(db.RawMaterials, "idMaterial", "NameMaterial", cargo.idRawMaterial);
            ViewBag.idShipment = new SelectList(db.Shipment, "idShipment", "Sender", cargo.idShipment);
            return View(cargo);
        }

        // POST: Cargoes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCargo,idShipment,Number,Weight,State,idRawMaterial")] Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cargo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idRawMaterial = new SelectList(db.RawMaterials, "idMaterial", "NameMaterial", cargo.idRawMaterial);
            ViewBag.idShipment = new SelectList(db.Shipment, "idShipment", "Sender", cargo.idShipment);
            return View(cargo);
        }

        // GET: Cargoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = db.Cargo.Find(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return View(cargo);
        }

        // POST: Cargoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cargo cargo = db.Cargo.Find(id);
            db.Cargo.Remove(cargo);
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
