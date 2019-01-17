using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BexMediaCom.Gateway;
using BexMediaCom.Models;

namespace BexMediaCom.Manager
{
    public class ViewEmployeeTransactionManager
    {
        ViewEmployeeTransactionGateway viewEmployeeTransactionGateway = new ViewEmployeeTransactionGateway();
        public List<ViewEmployeeTransaction> ViewAllEmployeeTransaction(string name, string startdate, string enddate, int breakfast, int lunch, int dinner, int sehuri, string all)
        {
            return viewEmployeeTransactionGateway.ViewAllEmployeeTransaction(name, startdate, enddate, breakfast, lunch, dinner, sehuri, all);
        }

        public List<Employee> AutoSearch(string name)
        {
            return viewEmployeeTransactionGateway.AutoSearch(name);
        }

    }
}