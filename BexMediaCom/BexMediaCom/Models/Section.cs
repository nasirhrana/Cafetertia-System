using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BexMediaCom.Models
{
    public class Section
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string SectionName { get; set; }
        public string DepartmentName { get; set; }
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
    }
}