﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DarrylSite.Controllers
{
    public class SiteController : Controller
    {
        // GET: Site
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GreyPprMetal()
        {
            ViewBag.myStylesheet = "GreyPprMetal.css";
            return View();
        }

    }
}