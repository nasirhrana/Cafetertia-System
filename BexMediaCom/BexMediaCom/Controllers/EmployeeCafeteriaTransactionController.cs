using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BexMediaCom.Manager;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Controllers
{
    public class EmployeeCafeteriaTransactionController : Controller
    {
        private EmployeeManager _employeeManager = new EmployeeManager();
        private EmployeeCafeteriaTransactionManager _employee = new EmployeeCafeteriaTransactionManager();
        // GET: EmployeeCafeteriaTransaction
        public ActionResult Save()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            return View();
        }
        [HttpPost]
        public ActionResult Save(EmployeeCafeteriaTransaction cafeteriaTransaction)
        {
            cafeteriaTransaction.CheckTime = DateTime.Now.TimeOfDay.ToString();
            int UPPER = cafeteriaTransaction.ItemQuantity;
            for (int i = 1; i <= UPPER; i++)
            {
                try
                {
                    ViewBag.Message = _employee.Save(cafeteriaTransaction) ? "Employee Cafeteria Transaction Saved Successfully" : "Employee Cafeteria Transaction Save Failed";
                }
                catch (Exception exception)
                {
                    ViewBag.Message = exception.Message;
                }
            }

            return View();
        }

        public ActionResult GetEmployeeId()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            List<Employee> employeesId = _employeeManager.GetEmployeeId();
            return Json(employeesId);
        }

        public JsonResult GetDayTransaction(string date)
        {

            var employeeTransaction = _employee.GetDayTransaction(date);
            return Json(employeeTransaction, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TransactionDelete(int? id)
        {
            
            int number = _employee.TransactionDelete(id);
            if (number > 0) 
            {
                return RedirectToAction("Save");
            }
            return null;

        }
    }
}