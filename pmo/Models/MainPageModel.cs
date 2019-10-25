using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using pmo.Models;

namespace pmo.Models
{
    public class MainPageModel
    {
        public SearchModel SearchModel { get; set; }
        public List<PropertyIndex> AllProperty { get; set; }
    }
}

