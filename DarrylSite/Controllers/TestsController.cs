using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using DarrylSite.Models;
using System.Configuration;
using Stripe;
using PayPal.Api;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using DarrylSite.Classes;
using PayPal;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Portal;

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
        private void SetFormModel(FormsModel fm)
        {
            fm.Teams = new List<SelectListItem>();
            fm.Teams.Add(new SelectListItem() { Text = "Yankees", Value = "1" });
            fm.Teams.Add(new SelectListItem() { Text = "Mets", Value = "2" });
            fm.Teams.Add(new SelectListItem() { Text = "Giants", Value = "3" });
            fm.Teams.Add(new SelectListItem() { Text = "Jets", Value = "4" });
            fm.Teams.Add(new SelectListItem() { Text = "Bills", Value = "5" });
            fm.Teams.Add(new SelectListItem() { Text = "Knicks", Value = "6" });
            fm.Teams.Add(new SelectListItem() { Text = "Nets", Value = "7" });
            fm.Index = "";
        }
        public ActionResult Forms()
        {
            FormsModel fm = new FormsModel();
            SetFormModel(fm);
            return View(fm);
        }

        [HttpPost]
        public ActionResult Forms(FormsModel fm)
        {
            string index = fm.Index;
            SetFormModel(fm);
            fm.Index = index;
            return View(fm);
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

        public FileResult OpenXML_Download()
        {
            // save the doc on local storage (blob)
            string fileName = "D:/Documents/tmp/tst.xlsx";
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet(new SheetData());

                DocumentFormat.OpenXml.Spreadsheet.Sheets sheets = workbookPart.Workbook.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Sheets());

                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Test Sheet" };

                sheets.Append(sheet);

                workbookPart.Workbook.Save();
            }
            return File(fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public FileResult OpenXML_DownloadNoSave()
        {
            byte[] b;
            // save the doc on local storage (blob)
            using (MemoryStream memStream = new MemoryStream())
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(memStream, SpreadsheetDocumentType.Workbook))
                {
                    WorkbookPart workbookPart = document.AddWorkbookPart();
                    workbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();

                    WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                    worksheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet(new SheetData());

                    DocumentFormat.OpenXml.Spreadsheet.Sheets sheets = workbookPart.Workbook.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Sheets());

                    Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "DownloadNoSave" };

                    sheets.Append(sheet);

                    workbookPart.Workbook.Save();
                }
                b = memStream.ToArray();
                return File(b, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }

        public ActionResult CreateExcelFile()
        {
            /*
             * https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/interop/how-to-access-office-onterop-objects
             * https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/interop/walkthrough-office-programming
             */
            var xl = new Excel.Application();
            Excel.Workbook bk = xl.Workbooks.Add();
            Excel.Worksheet worksheet = xl.ActiveSheet;
            worksheet.Name = "OJ_DarrylsSheet";
            xl.Visible = true;
            worksheet.Cells[1, "A"] = "Header 1";
            worksheet.Cells[1, 2] = "Header # 2";
            worksheet.Cells[1, 3] = "Header Wide 3";
            worksheet.Cells[1, 4] = "Header $ 4";
            worksheet.Cells[1, 5] = "Date Header 5";

            // Add content?
            worksheet.Cells[2, 1] = "Row 1";
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

        [HttpGet]
        public ActionResult Array()
        {
            ArrayListModel array = new ArrayListModel();
            array.Array = new List<ArrayModel>();
            ArrayModel am;
            for (int i = 1; i < 5; i++)
            {
                am = new ArrayModel() { intCol = i, stringCol = "Hello" + i, intCol2 = i * 2 };
                array.Array.Add(am);
            }
            return View(array);
        }

        [HttpPost]
        public ActionResult Array(ArrayListModel array)
        {
            int c = array.Array.Count();
            return View(array);

        }

        public ActionResult Payments()
        {
            //StripeConstants.publicKey = ConfigurationManager.AppSettings["publicKey"];
            PaymentModel pm = new PaymentModel();
            // Get list of cards
            return View(pm);
        }

        [HttpPost]
        public ActionResult Charge(PaymentModel pm)
        {
            StripeConfiguration.SetApiKey(StripeConfig.secretKey = ConfigurationManager.AppSettings["secretKey"]);
            //if (pm.StripeData.livemode == true)
            /*
            if (false)
            {
                pm.appError = "A live key was provided; this app only processes test mode.";
            }
            else
            */
            {
                //Handle Simple Checkout form data
                string custEmail = "Contact@dctecnologysolutions.com";

                if (pm.stripeToken != null)
                {
                    pm.StripeData.tokenId = pm.stripeToken;
                }
                if (pm.stripeEmail != null)
                {
                    custEmail = pm.stripeEmail;
                }
                StripeChargeCreateOptions options = new StripeChargeCreateOptions
                {
                    Amount = pm.StripeData.checkoutAmount,
                    Currency = "usd",
                    Description = "Example charge " + DateTime.Now.ToString(),
                    SourceTokenOrExistingSourceId = pm.StripeData.tokenId,
                    StatementDescriptor = "Descriptor",
                    ReceiptEmail = custEmail
                };
                var service = new StripeChargeService();
                try
                {
                    pm.StripeData.charge = service.Create(options);
                }
                catch (StripeException e)
                {
                    pm.StripeData.stripeError = e.StripeError;
                }
            }
            return View(pm);
        }

        [HttpPost]
        public ActionResult ProcessAddCardForCustomer(PaymentModel pm)
        {
            return View(pm);
        }

        public string PayPalCreatePayment()
        {
            try
            {
                Payment payment = new Payment()
                {
                    intent = "sale",
                    redirect_urls = new RedirectUrls()
                    {
                        cancel_url = Url.Action("PayPalCreatePaymentCancel"),
                        return_url = Url.Action("PayPalCreatePaymentReturn")
                    },
                    payer = new Payer()
                    {
                        payment_method = "paypal"
                    },
                    transactions = new List<Transaction>()
                    {
                        {
                           new Transaction()
                           {
                               amount = new Amount()
                               {
                                   total = "2.01",
                                   currency = "USD",
                               }
                           }
                        },
                    },
                    note_to_payer = "Test Payment"
                };
                // make the call!
                string accessToken;
                OAuthTokenCredential oAuth = new OAuthTokenCredential(PayPalConfig.clientId, PayPalConfig.secretKey);
                accessToken = oAuth.GetAccessToken();
                APIContext aPIContext = new APIContext(accessToken);
                Payment result = payment.Create(aPIContext);
                string resultString = result.ConvertToJson();

                return resultString;
            }
            catch (PayPalException e)
            {
                return null;
            }
        }

        public string PayPalExecutePayment(string PaymentID, string PayerID, string intent, string orderId, string token, string returnURL, string param)
        {
            try
            {
                Payment payment = new Payment()
                {
                    id = PaymentID,
                    intent = intent,
                    redirect_urls = new RedirectUrls()
                    {
                        cancel_url = Url.Action("PayPalCreatePaymentCancel"),
                        return_url = Url.Action("PayPalCreatePaymentReturn")
                    },
                    payer = new Payer()
                    {
                        payment_method = "paypal"
                    },
                    transactions = new List<Transaction>()
                    {
                        {
                           new Transaction()
                           {
                               amount = new Amount()
                               {
                                   total = "2.02",
                                   currency = "USD",
                               }
                           }
                        },
                    },
                    note_to_payer = "Test Payment"
                };

                string accessToken;
                OAuthTokenCredential oAuth = new OAuthTokenCredential(PayPalConfig.clientId, PayPalConfig.secretKey);
                accessToken = oAuth.GetAccessToken();
                APIContext aPIContext = new APIContext(accessToken);
                PaymentExecution paymentExecution = new PaymentExecution()
                {
                    payer_id = PayerID
                };
                Payment result = payment.Execute(aPIContext, paymentExecution);
                string resultString = result.ConvertToJson();

                return resultString;
            }
            catch (PayPalException e)
            {
                return null;
            }
        }

        public ActionResult PayPalCreatePaymentReturn(string intent, string orderId, string paymentID, string payerID, string returnUrl, string param)
        {
            // Success
            return View();
        }

        public ActionResult PayPalCreatePaymentCancel(string intent, string paymentID, string payerID, string billingId, string cancelURL, string token, string param)
        {
            // Cancel
            return View();
        }

        public ActionResult PayPalError(string param)
        {
            return View();
            // Error
        }

        public ActionResult ActiveDirectory()
        {
            return View();
        }
        /*
        public ActionResult ArcGISMap()
        {

            Esri.ArcGISRuntime.Mapping.Map myMap = new Esri.ArcGISRuntime.Mapping.Map(Basemap.CreateImagery());
            //Esri.ArcGISRuntime.Portal.Item item = new Esri.ArcGISRuntime.Portal.Item();
            Basemap b = new Basemap(item);
            myMap = new Esri.ArcGISRuntime.Mapping.Map(b);

            // Assign the map to the MapView
            //myMap.

            return View();
        }*/

        public ActionResult Ethereum()
        {
            return View();
        }
    }
}