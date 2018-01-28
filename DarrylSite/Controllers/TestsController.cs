using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;
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
                ViewBag.Message = "Got [" + postedFile.FileName + "][" + postedFile.ContentType + "]";
                ProcessExcelFile(postedFile);
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

        bool ProcessExcelFile(HttpPostedFileBase postedFile)
        {
            if (postedFile.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") // is this an Excel file?
            {
                return false;
            }
            // Use excel library to read file
            //postedFile.InputStream.Read()
            Excel.Application xlApp = new Excel.Application();
            xlApp.Visible = true;
            Excel.Workbook xlWkbk = xlApp.Workbooks.Open(postedFile.FileName);
            String str = "";
            // get sheets
            foreach (Excel.Workbook w in xlApp.Workbooks)
            {
                foreach (Excel.Worksheet s in w.Worksheets)
                {
                    str += "[" + s.Name + "]";
                    if (s.Name.StartsWith("OJ_"))
                    {
                        if (ProcessOJSheet(s))
                        {
                            xlWkbk.Close();
                            xlApp.Quit();
                            return true;
                        } else
                        {
                            return false;
                        }
                    }
                }
            }
            // get cells7
            xlWkbk.Close();
            xlApp.Quit();
            return false;
        }

        bool ProcessOJSheet(Excel.Worksheet worksheet)
        {
            // Load data from sheet into DB
            var v = worksheet.Cells[1, 1];
            v = worksheet.Cells[2, 1];
            v = worksheet.Cells[2, 2];
            v = worksheet.Cells[2, 3];
            v = worksheet.Cells[2, 4];
            v = worksheet.Cells[2, 5];
            v = worksheet.Cells[2, "E"];
            Range r = worksheet.UsedRange;
            int numCols = r.Columns.Count;
            int numRows = r.Rows.Count;
            return true;
        }
    }
}