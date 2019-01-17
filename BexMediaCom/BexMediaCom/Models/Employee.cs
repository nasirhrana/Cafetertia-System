using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace BexMediaCom.Models
{
    public class Employee
    {

        public int  Id { get; set; }
        public string EmpId { get; set; }
        public string Name { get; set; }
        public string JoinDate { get; set; }
        public string Date { get; set; }

        public int DivisionId { get; set; }
        public int DepartmentId  { get; set; }
        public int DesignationId { get; set; }
        public int CardId { get; set; }
        public bool Status { get; set; }
        public string EmailId { get; set; }
        public string UploadFile { get; set; }
        public int Number { get; set; }
        public string UserNumber { get; set; }
        public string Proximity { get; set; }
        public int SectionId { get; set; }
        public string DivisionName { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public string DesignationName { get; set; }
    }
}