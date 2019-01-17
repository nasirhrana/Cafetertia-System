using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BexMediaCom.Manager;

namespace BexMediaCom.Controllers
{
    public class CheckStatusController : Controller
    {
        // GET: CheckStatus
        private readonly CheckStatusManager _check = new CheckStatusManager();
        public ActionResult GetStatus()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }

            return View();
        }

        public JsonResult GetAllResuest()
        {
            string empId = Session["EmpId"].ToString();
            var allRequest = _check.GetAllResuest(empId);
            return Json(allRequest, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Cancel(int id)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            var allRequest = _check.GetOneResuest(id);

           // DateTime? findSubmitedTime = _check.GetApplicationTime(allRequest[0].ItemId);
            string findSubmitedTime1 = _check.GetApplicationTime(allRequest[0].ItemId);
            string applicationDaye = _check.GetApplicationDate(id);

           // string time2 = DateTime.Now.ToString("yyyy-MM-dd");
            string time3 = applicationDaye + " " + findSubmitedTime1;
            DateTime? findSubmitedTime = Convert.ToDateTime(time3);


            DateTime presentTime = DateTime.Now;
            TimeSpan duration = (TimeSpan) (presentTime - findSubmitedTime);
            string time = duration.ToString(@"hh\:mm\:ss");

            DateTime startTime = Convert.ToDateTime(presentTime);
            DateTime endTime = Convert.ToDateTime(findSubmitedTime);

            //We subtract the time.
            string span = endTime.Subtract(startTime).ToString();
           // TimeSpan upTime = DateTime.Parse(presentTime).Subtract(DateTime.Parse(findSubmitedTime));
            int date = 0;
            int index = span.IndexOf('.');
            int index1 = time.IndexOf(':');
            if (index <= 2)
            {
               date = Convert.ToInt32(span.Substring(0, index));
            }

            if (date > 0)
            {
                int hour = Convert.ToInt32(time.Substring(0, index1));

                var totalhour = (24 * date) + hour;
                if (totalhour >= 24)
                {
                    _check.CancelOrder(id);
                }
            }
            if (index >= 3)
            {
                int hour = Convert.ToInt32(time.Substring(0, index1));
                var totalhour = hour;
                if (totalhour >= 24)
                {
                    _check.CancelOrder(id);
                }
                else
                {
                    TempData["TimeLeft"] = totalhour;
                }
            }


          
            return RedirectToAction("GetStatus");
        }

        public ActionResult GetMealSchedul()
        
        
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }

            return View();
        }

        public JsonResult GetAllMealSchedul()
        {

            string empId = Session["EmpId"].ToString();
            var allRequest = _check.GetAllMealSchedul(empId);
            return Json(allRequest, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CancelMeal(int id)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            var allRequest = _check.GetOneResuest1(id);

            // DateTime? findSubmitedTime = _check.GetApplicationTime(allRequest[0].ItemId);
            string findSubmitedTime1 = _check.GetApplicationTime(allRequest[0].ItemId);
            //string applicationDaye = _check.GetApplicationDate(id);
            string applicationDaye = allRequest[0].Date;

            // string time2 = DateTime.Now.ToString("yyyy-MM-dd");
            string time3 = applicationDaye + " " + findSubmitedTime1;
            DateTime? findSubmitedTime = Convert.ToDateTime(time3);


            DateTime presentTime = DateTime.Now;
            TimeSpan duration = (TimeSpan)(presentTime - findSubmitedTime);
            string time = duration.ToString(@"hh\:mm\:ss");

            DateTime startTime = Convert.ToDateTime(presentTime);
            DateTime endTime = Convert.ToDateTime(findSubmitedTime);

            //We subtract the time.
            string span = endTime.Subtract(startTime).ToString();
            // TimeSpan upTime = DateTime.Parse(presentTime).Subtract(DateTime.Parse(findSubmitedTime));
            int date = 0;
            int index = span.IndexOf('.');
            int index1 = time.IndexOf(':');
            if (index <= 2)
            {
                date = Convert.ToInt32(span.Substring(0, index));
            }

            if (date > 0)
            {
                int hour = Convert.ToInt32(time.Substring(0, index1));

                var totalhour = (24 * date) + hour;
                if (totalhour >= 24)
                {
                    _check.CancelOrder1(id);
                }
            }
            if (index >= 3)
            {
                int hour = Convert.ToInt32(time.Substring(0, index1));
                var totalhour = hour;
                if (totalhour >= 24)
                {
                    _check.CancelOrder(id);
                }
                else
                {
                    TempData["TimeLeft"] = totalhour;
                }
            }



            return RedirectToAction("GetMealSchedul");
        }

    }
}