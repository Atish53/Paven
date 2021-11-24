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
using PagedList;

namespace Paven.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var students = from s in db.Products
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.ProductName.Contains(searchString)
                                       || s.ProductPrice.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.ProductName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.ProductPrice);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.ProductPrice);
                    break;
                default:
                    students = students.OrderBy(s => s.ProductName);
                    break;
            }
            return View(students.ToList());
        }

        // GET: Products 
        public ActionResult OurProducts(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var items = from i in db.Products
                        select i;
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.ProductName.ToUpper().Contains(searchString.ToUpper())
                                       || s.ProductCategory.CategoryName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(s => s.ProductName);
                    break;
                case "Price":
                    items = items.OrderBy(s => s.ProductPrice);
                    break;
                case "price_desc":
                    items = items.OrderByDescending(s => s.ProductPrice);
                    break;
                default:  // Sort By Name ASC
                    items = items.OrderBy(s => s.ProductName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        // GET: Products 
        public ActionResult Food(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var items = from i in db.Products.Where(x => x.ProductCategoryId == 1)
                        select i;
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.ProductName.ToUpper().Contains(searchString.ToUpper())
                                       || s.ProductCategory.CategoryName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(s => s.ProductName);
                    break;
                case "Price":
                    items = items.OrderBy(s => s.ProductPrice);
                    break;
                case "price_desc":
                    items = items.OrderByDescending(s => s.ProductPrice);
                    break;
                default:  // Sort By Name ASC
                    items = items.OrderBy(s => s.ProductName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        // GET: Products 
        public ActionResult Drinks(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var items = from i in db.Products.Where(x => x.ProductCategoryId == 2)
                        select i;
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.ProductName.ToUpper().Contains(searchString.ToUpper())
                                       || s.ProductCategory.CategoryName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(s => s.ProductName);
                    break;
                case "Price":
                    items = items.OrderBy(s => s.ProductPrice);
                    break;
                case "price_desc":
                    items = items.OrderByDescending(s => s.ProductPrice);
                    break;
                default:  // Sort By Name ASC
                    items = items.OrderBy(s => s.ProductName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        // GET: Products 
        public ActionResult Household(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var items = from i in db.Products.Where(x => x.ProductCategoryId == 4)
                        select i;
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.ProductName.ToUpper().Contains(searchString.ToUpper())
                                       || s.ProductCategory.CategoryName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(s => s.ProductName);
                    break;
                case "Price":
                    items = items.OrderBy(s => s.ProductPrice);
                    break;
                case "price_desc":
                    items = items.OrderByDescending(s => s.ProductPrice);
                    break;
                default:  // Sort By Name ASC
                    items = items.OrderBy(s => s.ProductName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        // GET: Products 
        public ActionResult Electronics(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var items = from i in db.Products.Where(x => x.ProductCategoryId == 3)
                        select i;
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.ProductName.ToUpper().Contains(searchString.ToUpper())
                                       || s.ProductCategory.CategoryName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(s => s.ProductName);
                    break;
                case "Price":
                    items = items.OrderBy(s => s.ProductPrice);
                    break;
                case "price_desc":
                    items = items.OrderByDescending(s => s.ProductPrice);
                    break;
                default:  // Sort By Name ASC
                    items = items.OrderBy(s => s.ProductName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Product product = await db.Products.FindAsync(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }

            
            // GET: Products/Create
            [Authorize(Roles = "Admin")]
            public ActionResult Create()
            {
                return View();
            }

            // POST: Products/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> Create([Bind(Include = "ProductId,ProductCategoryId,ProductName,ProductStock,ProductPrice,ProductImage,isActive")] Product product, HttpPostedFileBase img_upload)
            {
                byte[] data;
                data = new byte[img_upload.ContentLength];
                img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
                product.ProductImage = data;

                {
                    if (ModelState.IsValid)
                    {
                        db.Products.Add(product);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }

                    return View(product);
                }
            }

            // GET: Products/Edit/5
            public async Task<ActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Product product = await db.Products.FindAsync(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }

            // POST: Products/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> Edit([Bind(Include = "ProductId,ProductCategoryId,ProductName,ProductStock,ProductPrice,ProductImage,isActive")] Product product)
            {
                if (ModelState.IsValid)
                {
                product.ProductImage = product.ProductImage;
                    db.Entry(product).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(product);
            }

            // GET: Products/Delete/5
            public async Task<ActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Product product = await db.Products.FindAsync(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }

            // POST: Products/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> DeleteConfirmed(int id)
            {
                Product product = await db.Products.FindAsync(id);
                db.Products.Remove(product);
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
