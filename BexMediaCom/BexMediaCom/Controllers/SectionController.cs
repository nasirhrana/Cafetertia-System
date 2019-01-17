using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BexMediaCom.Manager;
using BexMediaCom.Models;

namespace BexMediaCom.Controllers
{
    public class SectionController : Controller
    {
        private SectionManager _sectionManager = new SectionManager();
        //
        // GET: /Section/
        public ActionResult AddSection()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            return View();
        }
        [HttpPost]
        public ActionResult AddSection(Section section)
        {
            try
            {
                ViewBag.Message = _sectionManager.SaveSection(section);
            }
            catch (Exception exception)
            {
                ViewBag.Message = exception.Message;
            }
    
            return View();
        }

        public ActionResult GetAllSection()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            var sections = _sectionManager.GetAllSection();
            return Json(sections, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int? id)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            Section section = _sectionManager.GetSectionById(id);
            return View(section);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Section section)
        {
            var message = _sectionManager.UpdateSection(section);
            return RedirectToAction("AddSection");
        }
        public ActionResult GetAllDepartment()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            List<Department> departments = _sectionManager.GetAllDepartments();
            return Json(departments);
        }
	}
}