using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BexMediaCom.Models
{
    public class DepartmentCafeteriaTransaction
    {
        public int Id { get; set; }
        public int DivisionId { get; set; }
        public int DeptId { get; set; }
        public int FoodItemCostId { get; set; }
        public int ItemQuantity { get; set; }
        public string CheckTime { get; set; }
        public DateTime CheckDate { get; set; }
    }
}