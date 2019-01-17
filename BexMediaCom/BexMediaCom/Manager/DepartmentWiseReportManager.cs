using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BexMediaCom.Gateway;
using BexMediaCom.Models;

namespace BexMediaCom.Manager
{
    public class DepartmentWiseReportManager
    {
        private DepartmentWiseReportGateway _reportGateway = new DepartmentWiseReportGateway();

        public List<ViewDepartmentTransaction> DepartmentReport(string name, string startdate, string enddate, int breakfast, int lunch, int dinner, int sehuri, string all)
        {
            return _reportGateway.DepartmentReport(name, startdate, enddate, breakfast, lunch, dinner, sehuri, all);
        }
    }
}