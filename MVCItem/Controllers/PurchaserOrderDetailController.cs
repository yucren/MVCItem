using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCItem.Models;

namespace MVCItem.Controllers
{
    public class PurchaserOrderDetailController : Controller
    {
        private SalesContext db = new SalesContext();
        public string GetDetail()
        {
            var pucharseid = Convert.ToInt32( Request["PurchaseOrderID"]);
            var detail = db.PurchaseOrderDetail.Where(p => p.PurchaseOrderHeader.PurchaseOrderID == pucharseid);
       
       string json   =   Newtonsoft.Json.JsonConvert.SerializeObject(detail);

            return json;

        }
        // GET: PurchaserOrderDetail
        public ActionResult Index()
        {
            return View();
        }

        // GET: PurchaserOrderDetail/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PurchaserOrderDetail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PurchaserOrderDetail/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PurchaserOrderDetail/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PurchaserOrderDetail/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PurchaserOrderDetail/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PurchaserOrderDetail/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
              
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
