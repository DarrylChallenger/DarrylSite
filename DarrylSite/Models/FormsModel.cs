using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DarrylSite.Models
{
    public class FormsModel
    {
        public List<SelectListItem> Teams { get; set; }
        public int Index { get; set; }
    }
}