using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BexMediaCom.Models
{
    public class ViewFoodItemTimeSchedule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}