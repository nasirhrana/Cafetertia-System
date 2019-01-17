using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BexMediaCom.Manager;
using BexMediaCom.Models;
using Microsoft.Ajax.Utilities;

namespace BexMediaCom.Controllers
{
    public class ViewItemWiseReportController : Controller
    {
        ViewItemWiseReportManager eManager = new ViewItemWiseReportManager();
        // GET: ViewItemWiseReport
        public ActionResult ItemWiseReport()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");
                
            }

            return View();
        }


        public ActionResult GetItem()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");
                
            }

            var itemList = eManager.GetItem();
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetReport(int type, string startdate, string enddate, string name, int breakfast, int lunch, int dinner, int sehuri, string all)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");
                ;
            }

            List<ViewDepartmentTransaction> itemWishEmployeeReport = null;
            if (type == 1)
            {
                itemWishEmployeeReport = eManager.GetItemWishEmployeeReport(type, startdate, enddate, name, breakfast, lunch, dinner, sehuri, all);
            }
            if (type == 2)
            {
                 itemWishEmployeeReport = eManager.GetItemWishDepartmentReport(type, startdate, enddate, name, breakfast, lunch, dinner, sehuri, all);
            }
             return Json(itemWishEmployeeReport, JsonRequestBehavior.AllowGet);


        }
        public ActionResult GetAllType()
        {
            List<FoodItemCost> type = new List<FoodItemCost>()
            {
                new FoodItemCost {Id = 1, Name = "Employee"},
                new FoodItemCost {Id = 2, Name = "Department"}

            };

            return Json(type, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AutoSearch(string search, int type)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            List<Employee> employees = eManager.AutoSearch(search);
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutoSearchDepartment(string search, int type, int divisionId)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            List<Employee> employees = eManager.AutoSearchDepartment(search, divisionId);
            return Json(employees, JsonRequestBehavior.AllowGet);
        }
    }
}