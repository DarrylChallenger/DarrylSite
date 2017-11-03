using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DarrylSite.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.displayItems = "N";
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayItems = "Y";
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        public ActionResult Resources()
        {
            ViewBag.Message = "Your Resource page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult ReturnFromPayPal()
        {
            ViewBag.Message = "Your Resource page.";

            return View();
        }
    }
}