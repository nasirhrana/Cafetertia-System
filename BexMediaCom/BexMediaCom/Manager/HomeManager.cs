using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BexMediaCom.Gateway;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Manager
{
    public class HomeManager
    {
        private HomeGateway _home = new HomeGateway();

        public List<UserInfo> UserInfo(string empId)
        {
            return _home.UserInfo(empId);
        }

        public List<UserInfo> Notification(string empId)
        {
            return _home.Notification(empId);
        }       
        public List<UserInfo> Notification1(string empId)
        {
            return _home.Notification1(empId);
        }

        public List<UserInfo> Notification5()
        {
            return _home.Notification5();
        }
        public List<UserInfo> Notification6()
        {
            return _home.Notification6();
        }
    }
}