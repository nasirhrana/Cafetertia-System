using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BexMediaCom.Models
{
    public class ViewDepartmentTransaction
    {
        public int Id { get; set; }
        public int SerialNumber { get; set; }
        public string EmployeeName { get; set; }
        public string PIN { get; set; }
        public string Department { get; set; }
        public string Division { get; set; }
        public string Designation { get; set; }
        public string FoodItem { get; set; }
        public int ItemQty { get; set; }
        public decimal ItemCost { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string CheckTime { get; set; }
        public string CheckDate { get; set; }   
    }
}