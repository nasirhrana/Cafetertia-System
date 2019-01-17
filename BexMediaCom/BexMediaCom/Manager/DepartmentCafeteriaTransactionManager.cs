using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BexMediaCom.Gateway;
using BexMediaCom.Models;

namespace BexMediaCom.Manager
{
    public class DepartmentCafeteriaTransactionManager
    {
        private DepartmentCafeteriaTransactionGateway _department = new DepartmentCafeteriaTransactionGateway();
        public bool Save(DepartmentCafeteriaTransaction department)
        {
            return _department.Save(department) > 0;
        }

        public List<ViewDepartmentTransaction> GetDepartmentTransaction(string name, string startdate, string enddate, int breakfast, int lunch, int dinner, int sehuri, string all)
        {
            return _department.GetDepartmentTransaction(name, startdate, enddate, breakfast, lunch, dinner, sehuri, all);
        }

        public List<Employee> AutoSearch(string name, int divisionId)
        {
            return _department.AutoSearch(name, divisionId);
        }
    }
}