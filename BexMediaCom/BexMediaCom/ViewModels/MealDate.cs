using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BexMediaCom.ViewModels
{
    public class MealDate
    {
        public int Id { get; set; }
        public string EmpId { get; set; }
        public string Date { get; set; }
        public int ItemId { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
    }
}