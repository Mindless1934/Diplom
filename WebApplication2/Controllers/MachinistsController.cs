using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Calabonga.Xml.Exports;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Text;

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
        public ActionResult Export()
        {
            string result = string.Empty;
            Workbook wb = new Workbook();

            // properties
            wb.Properties.Author = "Calabonga";
            wb.Properties.Created = DateTime.Today;
            wb.Properties.LastAutor = "Calabonga";
            wb.Properties.Version = "14";

            // options sheets
            wb.ExcelWorkbook.ActiveSheet = 1;
            wb.ExcelWorkbook.DisplayInkNotes = false;
            wb.ExcelWorkbook.FirstVisibleSheet = 1;
            wb.ExcelWorkbook.ProtectStructure = false;
            wb.ExcelWorkbook.WindowHeight = 800;
            wb.ExcelWorkbook.WindowTopX = 0;
            wb.ExcelWorkbook.WindowTopY = 0;
            wb.ExcelWorkbook.WindowWidth = 600;
            // get data
            List<Machinist> people = db.Machinist.ToList();
            Worksheet ws3 = new Worksheet("Пользователи");
            ws3.AddCell(0, 0, "Фио");
            ws3.AddCell(0, 1, "Аддрес");
            ws3.AddCell(0, 2, "Телефон");



            // appending rows with data
            for (int i = 0; i < people.Count; i++)
            {
                ws3.AddCell(i + 1, 0, people[i].FIO);
                ws3.AddCell(i + 1, 1, people[i].Address);
                ws3.AddCell(i + 1, 2, people[i].Telephone);

            }

            wb.AddWorksheet(ws3);

            // generate xml 
            string workstring = wb.ExportToXML();

            // Send to user file
            return new ExcelResult("Persons.xls", workstring);
        }
        public class ExcelResult : ActionResult
        {
            /// <summary>
            /// Создает экземпляр класса, которые выдает файл Excel
            /// </summary>
            /// <param name="fileName">наименование файла для экспорта</param>
            /// <param name="report">готовый набор данные для экпорта</param>
            public ExcelResult(string fileName, string report)
            {
                this.Filename = fileName;
                this.Report = report;
            }
            public string Report { get; private set; }
            public string Filename { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var ctx1 = context.HttpContext;
                ctx1.Response.Clear();
                ctx1.Response.ContentType = "application/vnd.ms-excel";
                ctx1.Response.BufferOutput = true;
                ctx1.Response.AddHeader("content-disposition",
                                         string.Format("attachment; filename={0}", Filename));
                ctx1.Response.ContentEncoding = Encoding.UTF8;
                ctx1.Response.Charset = "utf-8";
                ctx1.Response.Write(Report);
                ctx1.Response.Flush();
                ctx1.Response.End();
            }
        }
        [HttpPost]
        public ActionResult Import(HttpPostedFileBase excelfile)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            if (excelfile == null || excelfile.ContentLength == 0)
            {
                ViewBag.Error = "Файл не выбран! <br>";
                return View("Index", db.AspNetUsers.ToList());
            }
            else
            {
                if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
                {
                    db = new  TTCEntities();
                    string path = Server.MapPath("~/Import/" + excelfile.FileName);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    excelfile.SaveAs(path);
                    //Читаем из файла
                    // Excel.Application ap = new Excel.Application();

                    Excel.Application application = new Excel.Application();
                    Excel.Workbook workbook = application.Workbooks.Open(path);
                    Excel.Worksheet worksheet = workbook.ActiveSheet;
                    Excel.Range range = worksheet.UsedRange;
                    List<Machinist> listUsers = new List<Machinist>();
                    for (int row = 2; row <= range.Rows.Count; row++)
                    {
                        Machinist user = new Machinist();
                        user.FIO = ((Excel.Range)range.Cells[row, 1]).Text;
                        user.Address = ((Excel.Range)range.Cells[row, 2]).Text;
                        user.Telephone = ((Excel.Range)range.Cells[row, 3]).Text;
                        db.Machinist.Add(user);
                        db.SaveChanges();


                    }
                    workbook.Close();
                    ViewBag.Error = "Данные загружены <br>";
                    return View("Index", db.Machinist.ToList());
                }
                else
                {
                    ViewBag.Error = "Это не Excel! <br>";
                    return View("Index", db.Machinist.ToList());
                }
            }

        }
    }
}
