using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BexMediaCom.ViewModels
{
    public class ShowNeed
    {
        public string TotalUser { get; set; }
        public string LunchRequest { get; set; }
        public string DinnerRequest { get; set; }
        public string BreakfastRequest { get; set; }
        public string LateNightDinner { get; set; }
        public string Cancel { get; set; }
    }
}