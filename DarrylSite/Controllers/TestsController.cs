using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;

namespace DarrylSite.Controllers
{
    public class TestsController : Controller
    {
        // GET: Tests
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Text()
        {
            return View();
        }
        public ActionResult Media()
        {
            return View();
        }
        public ActionResult Social()
        {
            return View();
        }
        public ActionResult Images()
        {
            return View();
        }
        public ActionResult Forms()
        {
            return View();
        }
        public ActionResult IO()
        {
            //WebClient wc = new WebClient();
            return View();
        }
        [HttpPost]
        public ActionResult IO(HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                /*string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }*/
                ViewBag.Message = "Got " + postedFile.FileName + "!";
                /*
                postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));
                ViewBag.Message = "File uploaded successfully.";
                */
            } else
            {
                ViewBag.Message = "Didn't get a file :(";
            }
            return View();
        }

        public FileResult IO_Download()
        {
            return File("~/Images/DC Logo Bevel.png", "image/png", "DCLogo.png"); // https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Complete_list_of_MIME_types 
        }

        ActionResult CreateExcelFile() 
        {
            /*
             * https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/interop/how-to-access-office-onterop-objects
             * https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/interop/walkthrough-office-programming
             */
            var xl = new Excel.Application();
            return View();
        }
    }
}