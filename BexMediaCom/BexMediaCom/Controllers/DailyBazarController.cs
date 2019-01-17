using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BexMediaCom.Manager;
using BexMediaCom.Models;

namespace BexMediaCom.Controllers
{
    public class DailyBazarController : Controller
    {
        //
        // GET: /DailyBazar/
        private DailyBazarManager _bazarManager = new DailyBazarManager();
        public ActionResult SaveBazar()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }

            return View();
        }
        [HttpPost]
        public ActionResult SaveBazar(DailyBazar bazar)
        {
            try
            {
               ViewBag.Message = _bazarManager.SaveBazar(bazar);
            }
            catch (Exception)
            {
                return null;
            }
            return View();
        }

        public ActionResult AddUnitRate()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            return View();
        }
        [HttpPost]
        public ActionResult AddUnitRate(DailyBazar bazar)
        {

            try
            {
                ViewBag.Message = _bazarManager.UpdateRate(bazar);
            }
            catch (Exception)
            {
                return null;
            }
            return View();
        }

        public ActionResult FindUnitRate(string date, int itemId)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            DailyBazar bazar = _bazarManager.FindUnitRate(date, itemId);
            return Json(bazar, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetTotalUser(string date)
        //{
        //    var totaluser = _bazarManager.GetTotalUser(date);
        //    return Json(totaluser, JsonRequestBehavior.AllowGet);
        //}
	}
}