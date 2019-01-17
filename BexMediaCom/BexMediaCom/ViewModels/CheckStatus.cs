using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BexMediaCom.ViewModels
{
    public class CheckStatus
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Date { get; set; }
        public string Item { get; set; }
        public string Status { get; set; }
        public string Quantity { get; set; }
        public int Number { get; set; }

    }
}