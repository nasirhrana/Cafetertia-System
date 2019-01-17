using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BexMediaCom.Gateway;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Manager
{
    public class LoginManager
    {
        private LoginGateway _loginGateway = new LoginGateway();
        public List<Login> GetLogin(Login login)
        {
            return _loginGateway.GetLogin(login);

        }

        public List<Employee> GetAllEmployee()
        {
            return _loginGateway.GetAllEmployee();
        }

        public List<UserRole> GetAllRole()
        {
            return _loginGateway.GetAllRole();
        }

        public string RoleAssign(RoleAssign role)
        {
            int result =_loginGateway.RoleAssign(role);
            if (result > 0)
            {
                return "Employee Role Assign Successfull";
            }
            return "Employee Role Assign Failed";
        }
    }
}