using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DarrylSite.Models
{
    public class FormsModel
    {
        public List<SelectListItem> Teams { get; set; }

        [Display(Name ="Team")]
        [Required(ErrorMessage ="{0} is required.")]
        public string Index { get; set; }
    }
}