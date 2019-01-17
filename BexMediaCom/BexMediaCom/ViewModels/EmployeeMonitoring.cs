using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BexMediaCom.ViewModels
{
    public class EmployeeMonitoring
    {
        public int Id { get; set; }
        public string EmpId { get; set; }
        public string Name { get; set; }
        public DateTime JoinDate { get; set; }

        public string Division { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string CardNumber { get; set; }
        public bool Status { get; set; }
        public string EmailId { get; set; }
        public string UploadFile { get; set; }
        public int Number { get; set; }
    }
}