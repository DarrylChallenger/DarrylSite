using DarrylSite.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Stripe;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            ViewBag.StripePublishableKey = ConfigurationManager.AppSettings["StripePublishableKey"];

            return View();
        }

        public ActionResult Private()
        {
            ViewBag.Message = "Test pages.";
            ViewBag.wndvw = ConfigurationManager.AppSettings["WEBSITE_NODE_DEFAULT_VERSION"];
            ViewBag.StripeSecretKey = ConfigurationManager.AppSettings["StripeSecretKey"]; 
            ViewBag.StripePublishableKey = ConfigurationManager.AppSettings["StripePublishableKey"]; 
            ViewBag.ClientValidationEnabled = ConfigurationManager.AppSettings["ClientValidationEnabled"];

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
            ViewBag.Message = "Return From PayPal page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult ReturnFromStripe()
        {
            ViewBag.Message = "Return From Stripe page.";

            return View();
        }

        public ActionResult Charge(string stripeEmail, string stripeToken) // Called by form on Seller page
        {
            var customers = new StripeCustomerService();
            var charges = new StripeChargeService();

            var customer = customers.Create(new StripeCustomerCreateOptions
            {
                Email = stripeEmail,
                SourceToken = stripeToken
            });

            var charge = charges.Create(new StripeChargeCreateOptions
            {
                Amount = 500,//charge in cents
                Description = "Sample Charge",
                Currency = "usd",
                CustomerId = customer.Id
            });

            // further application specific code goes here

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