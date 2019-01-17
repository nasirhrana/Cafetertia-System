using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BexMediaCom.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DivisionId { get; set; }
    }
}