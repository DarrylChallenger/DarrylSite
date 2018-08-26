using DarrylSite.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
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

        public ActionResult Resources()
        {
            ViewBag.Message = "Your Resource page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Samples()
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
        public async Task<ActionResult> ReturnFromStripe()
        {
            Dictionary<string, string> pairs = new Dictionary<string, string>();

            ViewBag.Message = "Return From Stripe page.";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://connect.stripe.com/");
            client.DefaultRequestHeaders.Accept.Clear();

            string stripeCode = Request.QueryString.Get("code");
            var payload = new
            {
                client_secret = "sk_test_LRlhXB1wikIe5Se6tuSXCjD4",
                //code = "ac_CsNJb6tRNZBQevCKv0kXWDO5AipBvpOg",
                code = stripeCode,
                grant_type = "authorization_code",
                redirect_uri= "https://localhost:44340/Home/ReturnFromStripe"
            };
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://connect.stripe.com/oauth/token");
            //AuthManager.AddAuthTokenToHeader(request, authManager.result);

            request.Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);

            stripeErrorPayload errorPayload = null;
            stripeReturnCredsModel stripeReturnCreds = null;
            string str = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                //Stream st = await response.Content.ReadAsStreamAsync();
                //stripeErrorPayload errorPayload = (stripeErrorPayload)JsonConvert.DeserializeObject<stripeErrorPayload>(str);
                 errorPayload = JsonConvert.DeserializeObject<stripeErrorPayload>(str);
            } else {
                 stripeReturnCreds = JsonConvert.DeserializeObject<stripeReturnCredsModel>(str);
            }
            ViewBag.statusCode = response.StatusCode.ToString();
            ViewBag.stripeCode = stripeCode;
            return View();
        }

        private Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                IList<string> s;
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                try
                {
                    s = UserManager.GetRoles(user.GetUserId());
                }
                catch (Exception e)
                {
                    return false;
                }

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