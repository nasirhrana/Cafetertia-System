using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BexMediaCom.Manager;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeManager employeeManager = new EmployeeManager();
        DivisionManager divisionManager = new DivisionManager();
        DepartmentManager departmentManager = new DepartmentManager();
        DesignationManager designationManager = new DesignationManager();
        CardRegistrationManager cardRegistrationManager = new CardRegistrationManager();
        // GET: Employee
        public ActionResult Save()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            return View();
        }

        [HttpPost]
        public ActionResult Save(Employee employee, HttpPostedFileBase UploadFile)
        {

            if (UploadFile != null)
            {
                if (UploadFile.ContentType == "image/jpeg" || UploadFile.ContentType == "image/png" || UploadFile.ContentType == "image/gif")
                {
                    var empId = employee.EmpId;
                    string imageUrl = "";
                    var imageType = UploadFile.ContentType;
                    if (imageType == "image/jpeg")
                    {
                        imageUrl = empId + ".Jpg";
                    }
                    if (imageType == "image/png")
                    {
                        imageUrl = empId + ".png";
                    }
                    if (imageType == "image/gif")
                    {
                        imageUrl = empId + ".gif";
                    }

                    UploadFile.SaveAs(Server.MapPath("/") + "/UploadFile/" + imageUrl);
                    employee.UploadFile = imageUrl;
                }
                else
                {
                    return View();
                }
            }
            try
            {
                if (UploadFile == null)
                {
                    employee.UploadFile = "DEMO.Jpg";
                }
                employee.UserNumber = employeeManager.GetUserNumber();

                ViewBag.Message = employeeManager.Save(employee) ? "Employee Saved Successfully" : "Employee Saved Failed";

            }
            catch (Exception exception)
            {
                ViewBag.Message = exception.Message;
            }
            return View();
        }


        public JsonResult CheckEmployeeId(string employeeId)
        {
            var employee = employeeManager.IsEmployeeIdExist(employeeId);

            if (employee == true)
                return Json(false);
            return Json(true);
        }

        public ActionResult GetAllDivision()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            List<Division> divisions = divisionManager.GetAllDivisions();
            return Json(divisions);
        }

        public ActionResult GetAllDepartmentByDivisionId(int divisionId)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            List<Department> departments = departmentManager.GetAllDepartments(divisionId);
            return Json(departments);
        }

        public ActionResult GetAllDesignation()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            List<Designation> designations = designationManager.GetAllDesignations();
            return Json(designations);
        }

        public ActionResult GetAllCardNo()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            List<CardRegistration> cardRegistrations = employeeManager.GetAllCardRegistrations();
            return Json(cardRegistrations);

        }

        public ActionResult GetAllEmployee()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            var employees = employeeManager.GetAllEmployee();
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = employeeManager.GetEmployeeById(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee, HttpPostedFileBase UploadFile)
        {
            if (ModelState.IsValid)
            {
                if (UploadFile != null)
                {
                    if (UploadFile.ContentType == "image/jpeg" || UploadFile.ContentType == "image/png" ||
                        UploadFile.ContentType == "image/gif")
                    {
                        var empId = employee.EmpId;
                        string imageUrl = "";
                        var imageType = UploadFile.ContentType;
                        if (imageType == "image/jpeg")
                        {
                            imageUrl = empId + ".Jpg";
                        }
                        if (imageType == "image/png")
                        {
                            imageUrl = empId + ".png";
                        }
                        if (imageType == "image/gif")
                        {
                            imageUrl = empId + ".gif";
                        }
                        // var folderPath = Server.MapPath("/") + "/UploadFile/" + imageUrl;
                        // var folderPath = Server.MapPath("/") + "/UploadFile/" + imageUrl;
                        string fullPath = Request.MapPath("~/UploadFile/" + imageUrl);

                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                            //Session["DeleteSuccess"] = "Yes";
                        }
                        UploadFile.SaveAs(Server.MapPath("/") + "/UploadFile/" + imageUrl);
                        employee.UploadFile = imageUrl;
                        ViewBag.Message = employeeManager.UpdateEmployee(employee) ? "Employee Update Successfully" : "Employee Update Failed";
                    }

                }
                else
                {
                    ViewBag.Message = employeeManager.UpdateEmployeeWithoutImage(employee) ? "Employee Update Successfully" : "Employee Update Failed";

                }
                // var message = designationManager.UpdateDesignation(employee);
                return RedirectToAction("Save");
            }

            return View();
        }

        public JsonResult GetAllSection(int departmentId)
        {
            var sections = employeeManager.GetAllSection(departmentId);
            return Json(sections, JsonRequestBehavior.AllowGet);
        }
    }
}