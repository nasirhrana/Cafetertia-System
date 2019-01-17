using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BexMediaCom.ViewModels
{
    public class DivisionReport
    {
        public int SerialNumber { get; set; }
        public string PIN { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Division { get; set; }
        public string FoodItem { get; set; }
        public decimal ItemCost { get; set; }
        public int ItemQty { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string CheckDate { get; set; }
        public string SectionName { get; set; }

    }
}