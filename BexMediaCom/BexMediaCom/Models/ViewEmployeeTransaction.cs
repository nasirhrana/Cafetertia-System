using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace BexMediaCom.Models
{
    public class ViewEmployeeTransaction
    {
        public int Id { get; set; }
        public int SerialNo { get; set; }
        public string EmpId { get; set; }
        public string Name { get; set; }
        public string Division { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string SectionName { get; set; }
        public string FoodItem { get; set; }
        public int ItemQty { get; set; }
        public decimal ItemCost { get; set; }
        public decimal Total { get; set; }
        public decimal SubTotal { get; set; }
        public string CheckTime { get; set; }
        public string CheckDate { get; set; }
        public string CardNumber { get; set; }  

    }
}