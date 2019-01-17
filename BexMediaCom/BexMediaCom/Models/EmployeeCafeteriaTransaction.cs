using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace BexMediaCom.Models
{
    public class EmployeeCafeteriaTransaction
    {
        public int Id { get; set; }
        public string EmpId { get; set; }
        public int FoodItemCostId { get; set; }
        public int ItemQuantity { get; set; }
        public string CheckTime { get; set; }
        public double UnitRate { get; set; }
        public string CheckDate { get; set; }
        public string Status { get; set; }

    }
}