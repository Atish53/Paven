using Paven.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Paven.Controllers
{
    public class CheckoutController : Controller
    {
        ApplicationDbContext dB = new ApplicationDbContext();        

        // GET: Checkout
        public ActionResult Index()
        {
            return View();
        }
                
        [Authorize]
        public async Task<ActionResult> Payment() //Event Booking - id is the Event Id... Booking is stored to the EventBookings Table
        {
            //Variables Required For Tables
            double deliveryFee = 0;
            int numProducts = 0;

            //Get Current Cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            //Create New Sale
            Sale sale = new Sale();
            sale.SaleDate = DateTime.Now.Date.ToString();           

            
            numProducts = cart.GetCount();

            if (numProducts == 1)
            {
                deliveryFee = 60;
            }
            else if (numProducts <= 3)
            {
                deliveryFee = 80;
            }
            else
            {
                deliveryFee = 100;
            }

            //Customer Details
            sale.Email = User.Identity.Name;
            sale.CustomerName = User.Identity.GetName();
            sale.Address = User.Identity.GetAddress();
            sale.City = User.Identity.GetCity();
            sale.State = User.Identity.GetState();
            sale.PostalCode = User.Identity.GetPostalCode();
            sale.Country = User.Identity.GetCountry();
            sale.PhoneNumber = User.Identity.GetPhoneNo();

            //Sale Total
            double SaleFinal = cart.GetTotal();
            sale.SaleTotal = SaleFinal + deliveryFee;            

            //Check product stock
            var x = cart.GetCartItems();
            List<Product> chck = (from q in dB.Products
                                  select q).ToList();

            foreach (var items in x)
            {
                foreach (Product c in chck)
                {
                    if (items.ProductId == c.ProductId)
                    {
                        c.ProductStock -= items.Count;
                        if (c.ProductStock == 0)
                        {
                            c.isActive = false; //Sets item to inactive due to stock being zero.
                        }
                    }
                }
            }

            //Save Order to Delivery and Sale Table    
            Delivery delivery = new Delivery
            {
                SaleId = sale.SaleId,
                DeliveryFee = deliveryFee,
                CurrentLocation = "95 Monty Naicker Rd, Durban Central, Durban, 4001, South Africa",
                isDelivered = false
            };
            dB.Deliveries.Add(delivery);
            dB.Sales.Add(sale);
            await dB.SaveChangesAsync();

            //SaleDetail Saved Using Helper Method
            string orderId = sale.SaleId.ToString();
            sale = cart.CreateOrder(sale);

            //Payment...
            try
            {
                // Retrieve required values for the PayFast Merchant
                string name = "AppDev2 Store Sale Number: #" + sale.SaleTotal;
                string description = "This is a once-off and non-refundable payment. ";

                string site = "https://sandbox.payfast.co.za/eng/process";
                string merchant_id = "";
                string merchant_key = "";

                string paymentMode = System.Configuration.ConfigurationManager.AppSettings["PaymentMode"];

                if (paymentMode == "test")
                {
                    site = "https://sandbox.payfast.co.za/eng/process?";
                    merchant_id = "10000100";
                    merchant_key = "46f0cd694581a";
                }

                // Build the query string for payment site

                StringBuilder str = new StringBuilder();
                str.Append("merchant_id=" + HttpUtility.UrlEncode(merchant_id));
                str.Append("&merchant_key=" + HttpUtility.UrlEncode(merchant_key));
                str.Append("&return_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_ReturnURL"]));
                str.Append("&cancel_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_CancelURL"]));
                str.Append("&notify_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_NotifyURL"]));

                str.Append("&m_payment_id=" + HttpUtility.UrlEncode(sale.SaleId.ToString()));
                str.Append("&amount=" + HttpUtility.UrlEncode(sale.SaleTotal.ToString()));
                str.Append("&item_name=" + HttpUtility.UrlEncode(name));
                str.Append("&item_description=" + HttpUtility.UrlEncode(description));

                // Redirect to PayFast
                return Redirect(site + str.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }        

            // GET: Checkout/Details/5
            public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Checkout/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Checkout/Create
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

        // GET: Checkout/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Checkout/Edit/5
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

        // GET: Checkout/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Checkout/Delete/5
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
