using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temp.Models
{
    public class MenuItem
    {
        public int MenuId { set; get; }
        public string MenuName { get; set; }
        public string PageUrl { set; get; }
        public int ParentId { set; get; }
    }
}