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
    public class DepartmentController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();
        // GET: Department
        public ActionResult Save()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            ViewBag.Division = departmentManager.ShowAllDepartment();
            return View();
        }

        [HttpPost]
        public ActionResult Save(Department department)
        {
            try
            {
                ViewBag.Message = departmentManager.Save(department) ? "Department Saved Successfully" : "Department Save Failed";
            }
            catch (Exception exception)
            {
                ViewBag.Message = exception.Message;
            }
            
            var departments = departmentManager.ShowAllDepartment();
            ViewBag.Division = departments;
            return View();
        }
        public ActionResult JsonViewDepartment()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            var department = departmentManager.ShowAllDepartment();
            return Json(department, JsonRequestBehavior.AllowGet);
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
            Department department = departmentManager.GetDepartmentById(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.Division = departmentManager.ShowAllDepartment();
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                var result = departmentManager.UpdateDepartment(department);
                if (result == 0)
                {
                    ViewBag.Massage = "Departmant Name is Alrady Exist Under This Division! Please Try a different Name.";
                }
                else
                {
                    return RedirectToAction("Save");
                }
               
            }
            ViewBag.Division = departmentManager.ShowAllDepartment();
            return View(department);
        }

        public ActionResult Delete(int? id)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var message = departmentManager.DeleteDepartment(id);
            return RedirectToAction("Save");
        }


    }
}