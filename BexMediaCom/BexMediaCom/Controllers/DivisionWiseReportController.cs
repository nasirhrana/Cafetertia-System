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
    public class DivisionWiseReportController : Controller
    {
        private DivisionWiseReportManager _division = new DivisionWiseReportManager();
        //
        // GET: /DivisionWiseReport/
        public ActionResult GetDivisionReport()
        {
            return View();
        }

        public ActionResult GetReport(int divisionId, string startdate, string enddate, int breakfast, int lunch, int dinner, int sehuri, string all)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }

            List<DivisionReport> divisionReport = null;
            divisionReport = _division.GetDivisionReport(divisionId, startdate, enddate, breakfast, lunch, dinner, sehuri, all);
            return Json(divisionReport, JsonRequestBehavior.AllowGet);


        }

    }
}