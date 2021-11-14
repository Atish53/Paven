using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Paven.Models;

namespace Paven.Controllers
{
    public class SalesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: 
        [Authorize(Roles = "Driver")]
        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var students = from s in db.Sales
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.CustomerName.Contains(searchString)
                                       || s.CustomerName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.CustomerName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.SaleDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.SaleDate);
                    break;
                default:
                    students = students.OrderBy(s => s.SaleDate);
                    break;
            }

            return View(students.ToList());
        }

        public async Task<ActionResult> Delivered(int id)
        {
            int DelId = 0;
            Sale sale = await db.Sales.FindAsync(id);

            var deliverys = from db in db.Deliveries
                             where db.SaleId == id
                             select db;

            foreach (var item in deliverys)
            {
                DelId = item.DeliveryId;
            }

            Delivery delivery = await db.Deliveries.FindAsync(DelId);

            delivery.DeliveryDate = DateTime.Now.ToString();
            delivery.CurrentLocation = sale.Address;
            delivery.isDelivered = true;           
            await db.SaveChangesAsync();
            return RedirectToAction("Details" + "/" + id);
        }

        // GET: Sales/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            int DelId = 0;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = await db.Sales.FindAsync(id);
            if (sale == null)
            {
                return HttpNotFound();
            }

            var deliverys = from db in db.Deliveries
                            where db.SaleId == id
                            select db;

            foreach (var item in deliverys)
            {
                DelId = item.DeliveryId;
            }

            Delivery delivery = await db.Deliveries.FindAsync(DelId);
            ViewBag.Date = delivery.DeliveryDate;
            ViewBag.Delivery = delivery.CurrentLocation;            

            return View(sale);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SaleId,SaleDate,CustomerName,Email,Address,City,State,PostalCode,Country,PhoneNumber,SaleTotal")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Sales.Add(sale);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sale);
        }

        // GET: Sales/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = await db.Sales.FindAsync(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SaleId,SaleDate,CustomerName,Email,Address,City,State,PostalCode,Country,PhoneNumber,SaleTotal")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sale);
        }

        // GET: Sales/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = await db.Sales.FindAsync(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Sale sale = await db.Sales.FindAsync(id);
            db.Sales.Remove(sale);
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
