using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DarrylSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Seller()
        {
            ViewBag.Message = "Your seller page.";

            return View();
        }

        public ActionResult Private()
        {
            ViewBag.Message = "Private, secured page.";

            return View();
        }

        public ActionResult Resources()
        {
            ViewBag.Message = "Your Resource page.";

            return View();
        }
    }
}