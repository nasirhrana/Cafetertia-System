using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BexMediaCom.ViewModels
{
    public class EmployeeTransaction
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string EmpId { get; set; }
        public string Division { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
        public string ItemName { get; set; }
        public string CheckDate { get; set; }
        public string CheckTime { get; set; }
    }
}