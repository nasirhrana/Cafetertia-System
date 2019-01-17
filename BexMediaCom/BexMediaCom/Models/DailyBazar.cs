using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BexMediaCom.Models
{
    public class DailyBazar
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public double UnitRate { get; set; }
        public int TotalUser { get; set; }
        public int TotalDepartmentUser { get; set; }
        public DateTime Date { get; set; }
        public int ItemId { get; set; }
    }
}