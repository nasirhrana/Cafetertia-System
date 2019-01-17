using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BexMediaCom.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/
        public ActionResult Cafetoria()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }

            return View();
        }

        public ActionResult Inventory()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }

            return View();
        }

        public ActionResult Meeting()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }

            return View();
        }
	}
}