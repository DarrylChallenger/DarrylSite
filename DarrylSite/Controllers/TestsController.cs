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

        public ActionResult CreateExcelFile() 
        {
            /*
             * https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/interop/how-to-access-office-onterop-objects
             * https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/interop/walkthrough-office-programming
             */
            var xl = new Excel.Application();
            Workbook bk = xl.Workbooks.Add();
            Worksheet worksheet = xl.ActiveSheet;
            worksheet.Name = "OJ_DarrylsSheet";
            xl.Visible = true;
            worksheet.Cells[1, "A"] = "Header 1";
            worksheet.Cells[1, 2] = "Header # 2";
            worksheet.Cells[1, 3] = "Header Wide 3";
            worksheet.Cells[1, 4] = "Header $ 4";
            worksheet.Cells[1, 5] = "Date Header 5";

            // Add content?
            worksheet.Cells[2,1] = "Row 1";
            worksheet.Cells[2, 2] = 111;
            worksheet.Cells[2, 3] = "Lots of text that goes on and on and on.";
            worksheet.Cells[2, 4] = 178.99;
            worksheet.Cells[2, 5] = "1/2/2018";

            worksheet.Cells[3, 1] = "Row 2";
            worksheet.Cells[3, 2] = 222;
            worksheet.Cells[3, 3] = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
            worksheet.Cells[3, 4] = "$178.00";
            worksheet.Cells[3, 5] = 55555;

            // Make Headers Bold 
            Range range = worksheet.Rows[1];
            range.EntireRow.Font.Bold = true;
            // Make col 3 wide
            range = worksheet.Columns[3];
            range.AutoFit();
            // Make col 4 currency
            range = worksheet.Columns[4];
            range.EntireColumn.NumberFormat = "$ #,###,###.00";
            // Fomat col 5 as Dates 
            range = worksheet.Columns[5];
            range.NumberFormat = "mm/dd/yyyy";

            //Save file
            // http://csharp.net-informations.com/excel/csharp-create-excel.htm
            object misValue = System.Reflection.Missing.Value;
            bk.SaveAs("MyTestXLFile2", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive);

            bk.Close();
            xl.Quit();
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