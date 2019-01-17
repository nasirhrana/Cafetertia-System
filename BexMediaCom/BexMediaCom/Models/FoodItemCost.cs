using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BexMediaCom.Models
{
    public class FoodItemCost
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string  ItemDescription { get; set; }
    }
}