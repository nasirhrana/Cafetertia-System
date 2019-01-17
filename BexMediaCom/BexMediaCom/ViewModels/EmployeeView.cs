using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BexMediaCom.ViewModels
{
    public class EmployeeView
    {
        public int Id { get; set; }
        public string EmpId { get; set; }
        public string Name { get; set; }
        public string Division { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string CardNumber { get; set; }
        public string SectionName { get; set; }

        public string UploadFile { get; set; }
        public string Proximity { get; set; }
        public string Status { get; set; }
    }
}