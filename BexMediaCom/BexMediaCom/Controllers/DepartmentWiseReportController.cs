using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BexMediaCom.Manager;

namespace BexMediaCom.Controllers
{
    public class DepartmentWiseReportController : Controller
    {
        // GET: DepartmentWiseReport
        private DepartmentWiseReportManager _reportManager = new DepartmentWiseReportManager();

        public ActionResult GetDepartmentWiseReport()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }

            return View();
        }

        public ActionResult DepartmentReport(string name, string startdate, string enddate, int breakfast, int lunch, int dinner, int sehuri, string all)
        {
            var division = _reportManager.DepartmentReport(name, startdate, enddate, breakfast, lunch, dinner, sehuri, all);
            return Json(division, JsonRequestBehavior.AllowGet);
        }
    }
}