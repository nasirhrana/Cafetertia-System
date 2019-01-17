using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using BexMediaCom.Manager;

namespace BexMediaCom.Controllers
{
    public class ViewDepartmentTransactionController : Controller
    {
        // GET: ViewDepartmentTransaction
       // ViewEmployeeTransactionManager viewEmployeeTransactionManager = new ViewEmployeeTransactionManager();
        DepartmentCafeteriaTransactionManager departmentManager = new DepartmentCafeteriaTransactionManager();
        // GET: ViewEmployeeTransaction
        public ActionResult DeptTransactionView()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            return View();
        }

        public ActionResult GetDepartmentTransaction(string name, string startdate, string enddate, int breakfast, int lunch, int dinner, int sehuri, string all)
        {
            var division = departmentManager.GetDepartmentTransaction(name, startdate, enddate, breakfast, lunch, dinner, sehuri, all);
            return Json(division, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutoSearch(string search, int divisionId)
        {
            var division = departmentManager.AutoSearch(search, divisionId);
            return Json(division, JsonRequestBehavior.AllowGet);
        }
    }
}