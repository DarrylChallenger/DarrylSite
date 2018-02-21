using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DarrylSite.Models
{
    public class ArrayModel
    {
        public ArrayModel() { }
        public int intCol { get; set; }
        public String stringCol { get; set; }
        public int intCol2 { get; set; }
    }
    public class ArrayListModel
    {
        public List<ArrayModel> Array { get; set; } 
    }
}