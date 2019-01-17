using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BexMediaCom.ViewModels
{
    public class SubmitedApplicationInfo
    {
        public int Id { get; set; }
        public string EmpId { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string Date { get; set; }
        public string ItemName { get; set; }
        public string Quantity { get; set; }
    }
}