using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace BexMediaCom.Models
{
    public class FoodTimeFrame
    {
        public int Id { get; set; }
        public int FoodItemCostId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

    }
}