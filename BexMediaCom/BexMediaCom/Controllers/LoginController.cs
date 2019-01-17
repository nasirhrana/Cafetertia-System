using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BexMediaCom.Manager;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        private LoginManager _loginManager = new LoginManager();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login login)
        {
           var result = _loginManager.GetLogin(login);
           if (result.Count > 0)
           {
               Session["Name"] = result[0].Name;
               Session["Id"] = result[0].Id;
               Session["Email"] = result[0].Email;
               Session["EmpId"] = result[0].EmpId;
               Session["DptId"] = result[0].DptId;
               Session["UserRole"] = result[0].RoleName;
               Session["Image"] = result[0].EmployeePhotograph;
               Session["UserRoleId"] = result[0].UserRoleId;
               
                   return RedirectToAction("Index", "Home");
               


               
           }
            return View();
        }

        public ActionResult Logout()
        {
            Session["Name"] = null;
            Session["Email"] = null;
            Session["UserRole"] = null;
            Session["Image"] = null;
            Session["UserRoleId"] = null;

            return RedirectToAction("Login", "Login");
        }

        public ActionResult RoleAssign()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RoleAssign(RoleAssign role)
        {
            ViewBag.Message = _loginManager.RoleAssign(role);
            return View();
        }

        public ActionResult GetAllEmployee()
        {
            var allemployeelist = _loginManager.GetAllEmployee();
            return Json(allemployeelist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllRole()
        {
            var allrolelist = _loginManager.GetAllRole();
            return Json(allrolelist, JsonRequestBehavior.AllowGet);
        }
	}
}