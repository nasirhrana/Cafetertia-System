using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BexMediaCom.Manager;

namespace BexMediaCom.Controllers
{
    public class ViewEmployeeTransactionController : Controller
    {
        ViewEmployeeTransactionManager viewEmployeeTransactionManager = new ViewEmployeeTransactionManager();
        // GET: ViewEmployeeTransaction
        public ActionResult TransactionView()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            return View();
        }
        public ActionResult JsonViewEmployeeTransaction(string name, string startdate, string enddate, int breakfast, int lunch, int dinner, int sehuri, string all)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            var division = viewEmployeeTransactionManager.ViewAllEmployeeTransaction(name, startdate, enddate, breakfast, lunch, dinner, sehuri, all);
            return Json(division, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewOneEmployeeTransaction(string startdate, string enddate, int breakfast, int lunch, int dinner, int sehuri, string all)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            string name = Session["EmpId"].ToString();
            var division = viewEmployeeTransactionManager.ViewAllEmployeeTransaction(name, startdate, enddate, breakfast, lunch, dinner, sehuri, all);
            return Json(division, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutoSearch(string search)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            var division = viewEmployeeTransactionManager.AutoSearch(search);
            return Json(division, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OneEmployeeTransactionView()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            return View();
        }
    }
}