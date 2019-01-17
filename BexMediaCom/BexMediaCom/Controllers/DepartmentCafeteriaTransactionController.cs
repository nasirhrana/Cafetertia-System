using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BexMediaCom.Manager;
using BexMediaCom.Models;

namespace BexMediaCom.Controllers
{
    public class DepartmentCafeteriaTransactionController : Controller
    {
        // GET: DepartmentCafeteriaTransaction
        EmployeeManager employeeManager = new EmployeeManager();
        DepartmentManager _manager = new DepartmentManager();
        private DepartmentCafeteriaTransactionManager _department = new DepartmentCafeteriaTransactionManager();
        public ActionResult Save()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            return View();
        }
        [HttpPost]
        public ActionResult Save(DepartmentCafeteriaTransaction department)
        {
            department.CheckTime = DateTime.Now.TimeOfDay.ToString();
            try
            {
                ViewBag.Message = _department.Save(department) ? "Departmen Cafeteria Transaction Saved Successfully" : "Departmen Cafeteria Transaction Save Failed";
            }
            catch (Exception exception)
            {
                ViewBag.Message = exception.Message;
            }
            return View();
        }

        public ActionResult GetEmployeeId()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            List<Employee> employeesId = employeeManager.GetEmployeeId();
            return Json(employeesId);
        }
        public ActionResult GetDepartmentId()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            List<Department> departmentId = _manager.GetDepartmentId();
            return Json(departmentId);
        }
    }
}