using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using BexMediaCom.Manager;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Controllers
{
    public class SendOrderAndCheckNeedController : Controller
    {
        private SendOrderAndCheckNeedManager _sendOrder = new SendOrderAndCheckNeedManager();
        //
        // GET: /SendOrderAndCheckNeed/
        public ActionResult SendOrder()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            return View();
        }
        [HttpPost]
        public ActionResult SendOrder(SendOrder sendOrder)
        {
            sendOrder.EmpId = Session["EmpId"].ToString();

            try
            {
                ViewBag.Message = _sendOrder.SendOrder(sendOrder) ? "Request send Successfully" : "Request send Failed";
            }
            catch (Exception exception)
            {
                ViewBag.Message = exception.Message;
            }
            return View();
        }
        public ActionResult CheckNeed()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            return View();
        }
        public ActionResult GetCheckNeedData()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            List<CheckOrder> checkOrders = _sendOrder.CheckNeed();
            return Json(checkOrders, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDepartmentRequest()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            List<CheckOrder> checkOrders = _sendOrder.GetDepartmentRequest();
            return Json(checkOrders, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Confirm(int id)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }


            _sendOrder.ConfirmCheckNeed(id);
            //List<SubmitedApplicationInfo> userEmail = _sendOrder.GetUserEmail(id);
            //if (userEmail.Count != 0)
            //{
            //    bool result = _sendOrder.SendEmail(userEmail[0].Email, "About your Meal Requisition",
            //        "<p>Hello " + userEmail[0].EmployeeName + " <br/> <br/> Your Meal Requisition date -" +
            //        userEmail[0].Date + ", Item -" + userEmail[0].ItemName + ", Quantity - " +
            //        userEmail[0].Quantity + " are Approved by Cafe Admin<br/> <br/> Regards <br/>Cafe Admin</p>");

            //    if (result == true)
            //    {
            //        TempData["Confirm"] = "Send email successfully";
            //    }
            //}


                return RedirectToAction("CheckNeed");


        }
        public ActionResult ConfirmDepartment(int id)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            _sendOrder.ConfirmCheckNeed1(id);
            List<SubmitedApplicationInfo> userEmail = _sendOrder.GetUserEmail1(id);
            if (userEmail.Count != 0)
            {
                bool result = _sendOrder.SendEmail(userEmail[0].Email, "About your meal requisition",
                    "<p>Hello " + userEmail[0].EmployeeName + " <br/> <br/> Your official Meal Requisition date -" +
                    userEmail[0].Date + ", Item -" + userEmail[0].ItemName + ", Quantity - " +
                    userEmail[0].Quantity + " are Approved by Cafe Admin<br/> <br/> Regards <br/>Cafe Admin</p>");

                if (result == true)
                {
                    TempData["Confirm"] = "Send email successfully";
                }
            }

            return RedirectToAction("CheckNeed");
        }


        public ActionResult Reject(int id)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }


            _sendOrder.RejectCheckNeed(id);
            List<SubmitedApplicationInfo> userEmail = _sendOrder.GetUserEmail(id);
            if (userEmail.Count != 0)
            {
                bool result = _sendOrder.SendEmail(userEmail[0].Email, "About your Meal Requisition",
                    "<p>Hello " + userEmail[0].EmployeeName + " <br/> <br/> Your Meal Requisition date -" +
                    userEmail[0].Date + ", Item -" + userEmail[0].ItemName + ", Quantity - " +
                    userEmail[0].Quantity + " are Reject by Cafe Admin<br/> <br/> Regards <br/>Cafe Admin</p>");

                if (result == true)
                {
                    TempData["Confirm"] = "Send email successfully";
                }
            }
            return RedirectToAction("CheckNeed");

        }

        public ActionResult RejectDepartment(int id)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            _sendOrder.RejectCheckNeed1(id);
            List<SubmitedApplicationInfo> userEmail = _sendOrder.GetUserEmail1(id);
            if (userEmail.Count != 0)
            {
                bool result = _sendOrder.SendEmail(userEmail[0].Email, "About your meal requisition",
                    "<p>Hello " + userEmail[0].EmployeeName + " <br/> <br/> Your official Meal Requisition date -" +
                    userEmail[0].Date + ", Item -" + userEmail[0].ItemName + ", Quantity - " +
                    userEmail[0].Quantity + " are Reject by Cafe Admin<br/> <br/> Regards <br/>Cafe Admin</p>");

                if (result == true)
                {
                    TempData["Confirm"] = "Send email successfully";
                }
            }

            return RedirectToAction("CheckNeed");
        }

        public ActionResult TodaysNeed()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            return View();
        }

        public ActionResult GetTodayNeed()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            List<ShowNeed> totalneed = _sendOrder.GetTodayNeed();
            return Json(totalneed.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutoSearchDepartment(string search, int type)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            List<Employee> employees = _sendOrder.AutoSearchDepartment(search);
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SendOrderSave(int type, string date, int breakfast, int lunch, int dinner, int sehuri, int snacks, int tea, int quantity, string menu, int menu1)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            DateTime datea = DateTime.Now;
            DateTime dateb = Convert.ToDateTime(date);
            int result = DateTime.Compare(datea, dateb);
            string message = "";

            if (result < 0)
            {
                if (type == 1)
                {
                    string name = Session["EmpId"].ToString();
                    _sendOrder.SendOrderSave(type, date, name, breakfast, lunch, dinner, sehuri, snacks, tea, quantity, menu, menu1);
                }
                else
                {
                    string Id = Session["DptId"].ToString();
                    string name = Session["EmpId"].ToString();
                    _sendOrder.DepartmentSendOrderSave(type, date, breakfast, lunch, dinner, sehuri, snacks, tea, Id, name, quantity, menu, menu1);
                }

                 message = "Your request submited";
            }
            else
            {
                message = @"Please select a valid date";
            }

            return Json(message, JsonRequestBehavior.AllowGet);


        }
	}
}