using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BexMediaCom.Gateway;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Manager
{
    public class DivisionWiseReportManager
    {
        private DivisionWiseReportGateway _division = new DivisionWiseReportGateway();
        public List<DivisionReport> GetDivisionReport(int divisionId, string startdate, string enddate, int breakfast, int lunch, int dinner, int sehuri, string all)
        {
            return _division.GetDivisionReport(divisionId, startdate, enddate, breakfast, lunch, dinner, sehuri, all);
        }
    }
}