using DarrylSite.Models;
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
            ViewBag.isAdmin = "N";
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.displayItems = "Y";
            }

            if (isAdminUser())
            {
                ViewBag.isAdmin = "Y";
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
            ViewBag.Message = "Test pages.";

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

        private Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}