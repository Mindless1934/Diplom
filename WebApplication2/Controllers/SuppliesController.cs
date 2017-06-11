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
    public class SuppliesController : Controller
    {
        private TTCEntities db = new TTCEntities();

        // GET: Supplies
        public ActionResult Index()
        {
            var supply = db.Supply.Include(s => s.Branches).Include(s => s.Locos).Include(s => s.Machinist).Include(s => s.Railway).Include(s => s.Shipment);
            return View(supply.ToList());
        }

        // GET: Supplies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supply supply = db.Supply.Find(id);
            if (supply == null)
            {
                return HttpNotFound();
            }
            return View(supply);
        }

        // GET: Supplies/Create
        public ActionResult Create()
        {
            ViewBag.idBranch = new SelectList(db.Branches, "idBranch", "idBranch");
            ViewBag.idLoco = new SelectList(db.Locos, "idLoco", "LocoName");
            ViewBag.idMachinist = new SelectList(db.Machinist, "idMachinist", "FIO");
            ViewBag.idRailway = new SelectList(db.Railway, "idRailway", "RailwayType");
            ViewBag.idShipment = new SelectList(db.Shipment, "idShipment", "Sender");
            return View();
        }

        // POST: Supplies/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idSupply,idRailway,idShipment,idMachinist,DateofDelivery,idLoco,DateofDeparture,idBranch")] Supply supply)
        {
            if (ModelState.IsValid)
            {
                db.Supply.Add(supply);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idBranch = new SelectList(db.Branches, "idBranch", "idBranch", supply.idBranch);
            ViewBag.idLoco = new SelectList(db.Locos, "idLoco", "LocoName", supply.idLoco);
            ViewBag.idMachinist = new SelectList(db.Machinist, "idMachinist", "FIO", supply.idMachinist);
            ViewBag.idRailway = new SelectList(db.Railway, "idRailway", "RailwayType", supply.idRailway);
            ViewBag.idShipment = new SelectList(db.Shipment, "idShipment", "Sender", supply.idShipment);
            return View(supply);
        }

        // GET: Supplies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supply supply = db.Supply.Find(id);
            if (supply == null)
            {
                return HttpNotFound();
            }
            ViewBag.idBranch = new SelectList(db.Branches, "idBranch", "idBranch", supply.idBranch);
            ViewBag.idLoco = new SelectList(db.Locos, "idLoco", "LocoName", supply.idLoco);
            ViewBag.idMachinist = new SelectList(db.Machinist, "idMachinist", "FIO", supply.idMachinist);
            ViewBag.idRailway = new SelectList(db.Railway, "idRailway", "RailwayType", supply.idRailway);
            ViewBag.idShipment = new SelectList(db.Shipment, "idShipment", "Sender", supply.idShipment);
            return View(supply);
        }

        // POST: Supplies/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idSupply,idRailway,idShipment,idMachinist,DateofDelivery,idLoco,DateofDeparture,idBranch")] Supply supply)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supply).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idBranch = new SelectList(db.Branches, "idBranch", "idBranch", supply.idBranch);
            ViewBag.idLoco = new SelectList(db.Locos, "idLoco", "LocoName", supply.idLoco);
            ViewBag.idMachinist = new SelectList(db.Machinist, "idMachinist", "FIO", supply.idMachinist);
            ViewBag.idRailway = new SelectList(db.Railway, "idRailway", "RailwayType", supply.idRailway);
            ViewBag.idShipment = new SelectList(db.Shipment, "idShipment", "Sender", supply.idShipment);
            return View(supply);
        }

        // GET: Supplies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supply supply = db.Supply.Find(id);
            if (supply == null)
            {
                return HttpNotFound();
            }
            return View(supply);
        }

        // POST: Supplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supply supply = db.Supply.Find(id);
            db.Supply.Remove(supply);
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
