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
    public class DesignationController : Controller
    {
        private DesignationManager designationManager = new DesignationManager();
        // GET: Designation
        public ActionResult Save()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            return View();
        }

        [HttpPost]
        public ActionResult Save(Designation designation)
        {
            try
            {
                ViewBag.Message = designationManager.Save(designation) ? "Designation Saved Successfully" : "Designation Save Failed";
            }
            catch (Exception exception)
            {
                ViewBag.Message = exception.Message;
            }
            ViewBag.Designation = designationManager.ShowAllDesignation();
            return View();
        }
        public ActionResult JsonViewDesignation()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            var designation = designationManager.ShowAllDesignation();
            return Json(designation, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int? id)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designation designation = designationManager.GetDesignationById(id);
            if (designation == null)
            {
                return HttpNotFound();
            }

            return View(designation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Designation designation)
        {
            if (ModelState.IsValid)
            {
                var result = designationManager.UpdateDesignation(designation);
                if (result == 0 )
                {
                    ViewBag.Massage = "Designation Name is Alrady Exist! Please Try a different Name.";
                }
                else
                {
                    return RedirectToAction("Save");
                }
            }

            return View();
        }


    }
}