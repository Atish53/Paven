using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paven.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        // GET: Payment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Payment/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Payment - Success
        public ActionResult Success()
        {
            return View();
        }

        // GET: Payment - Success
        public ActionResult Cancelled()
        {
            return View();
        }
    }
}