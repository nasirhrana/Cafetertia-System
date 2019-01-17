using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BexMediaCom.ViewModels
{
    public class CheckOrder
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public string ItemName { get; set; }
        public int TypeId { get; set; }
        public string EmpId { get; set; }
        public int DptId { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string Division { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string CustomMenu { get; set; }
    }
}