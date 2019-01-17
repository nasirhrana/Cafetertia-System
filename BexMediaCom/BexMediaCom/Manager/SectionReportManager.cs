using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BexMediaCom.Gateway;
using BexMediaCom.Models;

namespace BexMediaCom.Manager
{
    public class SectionReportManager
    {
        private SectionReportGateway _sectionReport = new SectionReportGateway();

        public List<Employee> AutoSearchSectionByDepartmentId(string name, int departmentId)
        {
            return _sectionReport.AutoSearchSectionByDepartmentId(name, departmentId);
        }

        public List<ViewEmployeeTransaction> GetItemWishEmployeeReport(int type, string startdate, string enddate, string name, int breakfast, int lunch, int dinner, int sehuri, string all)
        {
            return _sectionReport.GetItemWishEmployeeReport(type, startdate, enddate, name, breakfast, lunch, dinner, sehuri, all);
        }

        public List<ViewEmployeeTransaction> GetItemWishDepartmentReport(int type, string startdate, string enddate, int departmentId, string name, int breakfast, int lunch, int dinner, int sehuri, string all)
        {
            return _sectionReport.GetItemWishDepartmentReport(type, startdate, enddate, departmentId, name, breakfast, lunch, dinner, sehuri, all);
        }
    }
}