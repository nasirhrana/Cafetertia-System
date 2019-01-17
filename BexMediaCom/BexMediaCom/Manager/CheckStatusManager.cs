using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BexMediaCom.Gateway;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Manager
{
    public class CheckStatusManager
    {
        private CheckStatusGateway _check = new CheckStatusGateway();

        public List<CheckStatus> GetAllResuest(string empId)
        {
            return _check.GetAllResuest(empId);
        }
        public List<CheckStatus> GetOneResuest(int id)
        {
            return _check.GetOneResuest(id);
        }

        public string CancelOrder(int id)
        {
            int result = _check.CancelOrder(id);
            string message = " ";
            if (result > 0)
            {
                message = "Cancel";
            }
            message = "Failed!";
            return message;
        }
        public string GetApplicationTime(int id)
        {
            return _check.GetApplicationTime(id);
        }

        public string GetApplicationDate(int id)
        {
            return _check.GetApplicationDate(id);
        }
        public List<CheckStatus> GetAllMealSchedul(string empId)
        {
            return _check.GetAllMealSchedul(empId);
        }

        public List<CheckStatus> GetOneResuest1(int id)
        {
            return _check.GetOneResuest1(id);
        }

        public string CancelOrder1(int id)
        {
            int result = _check.CancelOrder1(id);
            string message = " ";
            if (result > 0)
            {
                message = "Cancel";
            }
            message = "Failed!";
            return message;
        }

    }
}