using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BexMediaCom.Models
{
    public class SendOrder
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ItemId { get; set; }
        public string EmpId { get; set; }
        public string Status { get; set; }
    }
}