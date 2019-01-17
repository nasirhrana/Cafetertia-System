using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BexMediaCom.Manager;
using BexMediaCom.Models;

namespace BexMediaCom.Controllers
{
    public class DivisionController : Controller
    {
        DivisionManager divisionManager = new DivisionManager();
        // GET: Division

        public ActionResult Save()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");
                
            }

            return View();
        }

        [HttpPost]
        public ActionResult Save(Division division)
        {
            try
            {
                ViewBag.Message = divisionManager.Save(division) ? "Division Saved Successfully" : "Division Save Failed";
            }
            catch (Exception exception)
            {
                ViewBag.Message = exception.Message;
            }
            ViewBag.Division = divisionManager.ShowAllDivision();
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");
                ;
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Division division = divisionManager.GetDivisionById(id);
            if (division == null)
            {
                return HttpNotFound();
            }

            return View(division);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Division division)
        {
            if (ModelState.IsValid)
            {
                int result = divisionManager.UpdateDivision(division);
                if (result == 0)
                {
                    ViewBag.Massage = "Division Name is Alrady Exist! Please Try a different Name.";
                }
                else
                {
                    return RedirectToAction("Save");
                }
               
            }

            return View();
        }
        public JsonResult GetAllDivision()
        {
            var division = divisionManager.ShowAllDivision();
            return Json(division, JsonRequestBehavior.AllowGet);
        }
    }
}