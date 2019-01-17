using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BexMediaCom.Gateway;
using BexMediaCom.Models;

namespace BexMediaCom.Manager
{
    public class ViewItemWiseReportManager
    {
        private ViewItemWiseReportGateway eGateway = new ViewItemWiseReportGateway();

        public List<FoodItemCost> GetItem()
        {
            return eGateway.GetItem();
        }

        public List<ViewDepartmentTransaction> GetItemWishEmployeeReport(int type, string startdate, string enddate, string name, int breakfast, int lunch, int dinner, int sehuri, string all)
        {
            return eGateway.GetItemWishEmployeeReport(type, startdate, enddate, name, breakfast, lunch, dinner, sehuri, all);
        }

        public List<ViewDepartmentTransaction> GetItemWishDepartmentReport(int type, string startdate, string enddate, string name, int breakfast, int lunch, int dinner, int sehuri, string all)
        {
            return eGateway.GetItemWishDepartmentReport(type, startdate, enddate, name, breakfast, lunch, dinner, sehuri, all);
        }
        public List<Employee> AutoSearch(string name)
        {
            return eGateway.AutoSearch(name);
        }

        public List<Employee> AutoSearchDepartment(string name, int divisionId)
        {
            return eGateway.AutoSearchDepartment(name, divisionId);
        }
    }
}