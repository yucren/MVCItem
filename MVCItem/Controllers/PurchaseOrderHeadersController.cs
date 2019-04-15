using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCItem.Models;
using Newtonsoft.Json;

namespace MVCItem.Controllers
{
    public class PurchaseOrderHeadersController : Controller
    {
        public ActionResult Header()
        {
            var purchaseOrderHeader = db.PurchaseOrderHeader.Take(100).Include(p => p.ShipMethod).Include(p => p.Vendor).First();
            return View(purchaseOrderHeader);
        }
        private SalesContext db = new SalesContext();
        public JsonResult CheckUserName(string userName)
        {
            var result = true;
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        // GET: PurchaseOrderHeaders
        public async Task<ActionResult> Index()
        {
            var purchaseOrderHeader = db.PurchaseOrderHeader.Take(100).Include(p => p.ShipMethod).Include(p => p.Vendor);
            return View(await purchaseOrderHeader.ToListAsync());
        }
        public ActionResult Purchases()
        {
            return View();

        }
        public string Purchase()
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd"

            };

        
            var purchaseOrderHeader = db.PurchaseOrderHeader.Take(100);

          var json =  Newtonsoft.Json.JsonConvert.SerializeObject(purchaseOrderHeader, jsonSerializerSettings);

            return json;
        }

        // GET: PurchaseOrderHeaders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderHeader purchaseOrderHeader = await db.PurchaseOrderHeader.FindAsync(id);
            if (purchaseOrderHeader == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrderHeader);
        }

        // GET: PurchaseOrderHeaders/Create
        public ActionResult Create()
        {
            ViewBag.ShipMethodID = new SelectList(db.ShipMethod, "ShipMethodID", "Name");
            ViewBag.VendorID = new SelectList(db.Vendor, "BusinessEntityID", "AccountNumber");
            return View();
        }

        // POST: PurchaseOrderHeaders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PurchaseOrderID,RevisionNumber,Status,EmployeeID,VendorID,ShipMethodID,OrderDate,ShipDate,SubTotal,TaxAmt,Freight,TotalDue,ModifiedDate")] PurchaseOrderHeader purchaseOrderHeader)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseOrderHeader.Add(purchaseOrderHeader);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ShipMethodID = new SelectList(db.ShipMethod, "ShipMethodID", "Name", purchaseOrderHeader.ShipMethodID);
            ViewBag.VendorID = new SelectList(db.Vendor, "BusinessEntityID", "AccountNumber", purchaseOrderHeader.VendorID);
            return View(purchaseOrderHeader);
        }

        // GET: PurchaseOrderHeaders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderHeader purchaseOrderHeader = await db.PurchaseOrderHeader.FindAsync(id);
            if (purchaseOrderHeader == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShipMethodID = new SelectList(db.ShipMethod, "ShipMethodID", "Name", purchaseOrderHeader.ShipMethodID);
            ViewBag.VendorID = new SelectList(db.Vendor, "BusinessEntityID", "AccountNumber", purchaseOrderHeader.VendorID);
            return View(purchaseOrderHeader);
        }

        // POST: PurchaseOrderHeaders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PurchaseOrderID,RevisionNumber,Status,EmployeeID,VendorID,ShipMethodID,OrderDate,ShipDate,SubTotal,TaxAmt,Freight,TotalDue,ModifiedDate")] PurchaseOrderHeader purchaseOrderHeader)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseOrderHeader).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ShipMethodID = new SelectList(db.ShipMethod, "ShipMethodID", "Name", purchaseOrderHeader.ShipMethodID);
            ViewBag.VendorID = new SelectList(db.Vendor, "BusinessEntityID", "AccountNumber", purchaseOrderHeader.VendorID);
            return View(purchaseOrderHeader);
        }

        // GET: PurchaseOrderHeaders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderHeader purchaseOrderHeader = await db.PurchaseOrderHeader.FindAsync(id);
            if (purchaseOrderHeader == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrderHeader);
        }

        // POST: PurchaseOrderHeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PurchaseOrderHeader purchaseOrderHeader = await db.PurchaseOrderHeader.FindAsync(id);
            db.PurchaseOrderHeader.Remove(purchaseOrderHeader);
            await db.SaveChangesAsync();
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
