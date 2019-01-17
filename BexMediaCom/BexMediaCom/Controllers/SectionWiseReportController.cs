using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BexMediaCom.Manager;
using BexMediaCom.Models;

namespace BexMediaCom.Controllers
{
    public class SectionWiseReportController : Controller
    {
        // GET: SectionWiseReport
        SectionReportManager _sectionReport = new SectionReportManager();
        public ActionResult SectioReport()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            return View();
        }

        public ActionResult AutoSearchSectionByDepartmentId(string search, int type, int departmentId)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            List<Employee> employees = _sectionReport.AutoSearchSectionByDepartmentId(search, departmentId);
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetReport(int type, string startdate, string enddate, int departmentId, string name, int breakfast, int lunch, int dinner, int sehuri, string all)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            List<ViewEmployeeTransaction> itemWishEmployeeReport = null;
            if (type == 1)
            {
                itemWishEmployeeReport = _sectionReport.GetItemWishEmployeeReport(type, startdate, enddate, name, breakfast, lunch, dinner, sehuri, all);
            }
            if (type == 2)
            {
                itemWishEmployeeReport = _sectionReport.GetItemWishDepartmentReport(type, startdate, enddate, departmentId, name, breakfast, lunch, dinner, sehuri, all);
            }
            return Json(itemWishEmployeeReport, JsonRequestBehavior.AllowGet);


        }
    }
}