using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BexMediaCom.Manager;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Controllers
{
    public class TextFileController : Controller
    {
        EmployeeManager _employeeManager = new EmployeeManager();
        private EmployeeCafeteriaTransactionManager _employee = new EmployeeCafeteriaTransactionManager();
        //
        // GET: /TextFile/
        public ActionResult ReadFile()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            return View();
        }
        [HttpPost]
        public JsonResult Export(HttpPostedFileBase Textfile)
        {
            HttpPostedFileBase file = Request.Files[0]; // your file
            EmployeeMonitoring employ = null;
            string imageUrl = "";
            if (file != null)
            {
                if (file.ContentType == "text/plain")
                {
                   
                    var imageType = file.ContentType;
                    if (imageType == "text/plain")
                    {
                        imageUrl = "123" + ".txt";
                    }
                    file.SaveAs(Server.MapPath("/") + "/UploadFile/" + imageUrl);
                }
            }
            StreamReader sr = new StreamReader(Server.MapPath("/") + "/UploadFile/" + imageUrl);

            var line = sr.ReadLine();
            List<EmployeeMonitoring> employees = new List<EmployeeMonitoring>();
            employees.Clear();

            while (line != null)
            {
                int index = line.IndexOf(',');
                string date = line.Substring(0, index);

                string year = date.Substring(0, 4);
                string month = date.Substring(4, 2);
                string day = date.Substring(6, 2);
                string dates = year + "-" + month + "-" + day;

                int index1 = index + 1;
                string time = line.Substring(index1, 6);

                int index2 = index + 8;
                string cardNumber = line.Substring(index2, 8);

                bool employs = false;
                bool employs1 = false;
                if (cardNumber != "")
                {
                    employ = _employeeManager.GetEmployeeByEmpId(cardNumber);

                    if (employ.EmpId != null)
                    {
                        employees.Add(employ);
                    }
                   

                }

                line = sr.ReadLine();
            }
            sr.Close();
            string fullPath = Request.MapPath("~/UploadFile/" + imageUrl);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);

            }
            return Json(employees, JsonRequestBehavior.AllowGet);


        }

        public JsonResult Import(HttpPostedFileBase Textfile)
        {
            HttpPostedFileBase file = Request.Files[0]; // your file
            EmployeeMonitoring employ = null;
            Stack employees1 = new Stack();
            string imageUrl = "";
            if (file != null)
            {
                if (file.ContentType == "text/plain")
                {

                    var imageType = file.ContentType;
                    if (imageType == "text/plain")
                    {
                        imageUrl = "123" + ".txt";
                    }
                    file.SaveAs(Server.MapPath("/") + "/UploadFile/" + imageUrl);
                }
            }

          //  StreamReader sr = new StreamReader(Server.MapPath("/") + "/UploadFile/" + imageUrl);

            using (var fs = new FileStream(Server.MapPath(" / ") + "/UploadFile/" + imageUrl, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs, Encoding.Default))
            {
                //Read the first line of text
                var line = sr.ReadLine();
                List<EmployeeMonitoring> employees = new List<EmployeeMonitoring>();

             

                employees.Clear();
                //int number = 1;
                //Continue to read until you reach end of file
                while (line != null)
                {
                    int index = line.IndexOf(',');
                    string date = line.Substring(0, index);

                    string year1 = date.Substring(0, 4);
                    string month1 = date.Substring(4, 2);
                    string day1 = date.Substring(6, 2);
                    string dates = year1 + "-" + month1 + "-" + day1;

                    DateTime dates1 = Convert.ToDateTime(dates);

                  

                    int index1 = index + 1;
                    string time1 = line.Substring(index1, 6);

                    int index2 = index + 8;
                    string cardNumber = line.Substring(index2, 8);

                    string hh = time1.Substring(0, 2);
                    string mm = time1.Substring(2, 2);
                    string ss = time1.Substring(4, 2);
                    string time = hh + ":" + mm + ":" + ss;
                    bool employs = false;
                    bool employs1 = false;
                    int foodItemId = _employeeManager.GetFoodItemCostIdByTime(time);

                    if (foodItemId == 9)
                    {
                        dates1 = dates1.AddDays(-1);
                    }

                    bool IsFoodItemTimeExist1 = _employeeManager.IsFoodItemTimeExist(time, foodItemId);

                    if (IsFoodItemTimeExist1 == true)
                    {
                        if (cardNumber != "")
                        {
                            employ = _employeeManager.GetEmployeeByEmpId(cardNumber);
                            //bool employ12 = _employeeManager.GetEmployeeByEmpId123(cardNumber, foodItemId, dates);

                            //if (employ12 == true)
                            //{
                            //    ShowEmployeeMonitoring employ1 = _employeeManager.GetEmployeeByEmpId1(cardNumber, foodItemId);
                            //    // employees1.Add(employ1);
                            //    //  employees1.Enqueue(employ1);
                            //    employees1.Push(employ1);
                            //}



                        }
                        EmployeeCafeteriaTransaction emptransection = new EmployeeCafeteriaTransaction();
                        emptransection.EmpId = employ.EmpId;
                        emptransection.FoodItemCostId = foodItemId;
                        emptransection.CheckTime = time;
                        emptransection.CheckDate = dates1.ToString();
                        if (emptransection.EmpId != null)
                        {
                            employs = _employee.IsEmployeeExist(emptransection.EmpId, foodItemId, time, dates);
                            employs1 = _employee.IsEmployeeExist1(emptransection.EmpId, time, dates);
                            if (employs == false)
                            {
                                if (employs1 == false)
                                {
                                    int message = _employee.SaveRecord(emptransection);
                                   // var message1 = _employeeManager.GetListofuser(emptransection);

                                    bool employ12 = _employeeManager.GetEmployeeByEmpId123(cardNumber, foodItemId, dates);

                                    if (employ12 == true)
                                    {
                                        ShowEmployeeMonitoring employ1 = _employeeManager.GetEmployeeByEmpId1235(cardNumber, foodItemId, dates);
                                        // employees1.Add(employ1);
                                        //  employees1.Enqueue(employ1);
                                        employees1.Push(employ1);
                                    }

                                }

                            }
                        }

                    }


                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
            }

            //Read the first line of text
            //var line = sr.ReadLine();
            //List<EmployeeMonitoring> employees = new List<EmployeeMonitoring>();
            //List<ShowEmployeeMonitoring> employees1 = new List<ShowEmployeeMonitoring>();
            //employees.Clear();
            ////int number = 1;
            ////Continue to read until you reach end of file
            //while (line != null)
            //{
            //    int index = line.IndexOf(',');
            //    string date = line.Substring(0, index);

            //    string year = date.Substring(0, 4);
            //    string month = date.Substring(4, 2);
            //    string day = date.Substring(6, 2);
            //    string dates = year + "-" + month + "-" + day;

            //    int index1 = index + 1;
            //    string time1 = line.Substring(index1, 6);

            //    int index2 = index + 8;
            //    string cardNumber = line.Substring(index2, 8);

            //    string hh = time1.Substring(0, 2);
            //    string mm = time1.Substring(2, 2);
            //    string ss = time1.Substring(4,2);
            //    string time = hh + ":" + mm + ":" + ss;
            //    int foodItemId = 0;

            //    bool employs = false;
            //    bool employs1 = false;
            //    if (cardNumber != "")
            //    {
            //        foodItemId = _employeeManager.GetFoodItemCostIdByTime(time);
            //        employ = _employeeManager.GetEmployeeByEmpId(cardNumber);
            //        ShowEmployeeMonitoring employ1 = _employeeManager.GetEmployeeByEmpId1(cardNumber, foodItemId);
            //        employees1.Add(employ1);
            //        //employees1.Add(number);
            //        //   number++;

            //    }
            //    EmployeeCafeteriaTransaction emptransection = new EmployeeCafeteriaTransaction();
            //    emptransection.EmpId = employ.EmpId;
            //    emptransection.FoodItemCostId = foodItemId;
            //    emptransection.CheckTime = time;
            //    emptransection.CheckDate = dates;
            //    employs = _employee.IsEmployeeExist(emptransection.EmpId, foodItemId, time, dates);
            //    employs1 = _employee.IsEmployeeExist1(emptransection.EmpId, time, dates);
            //    if (employs == false)
            //    {
            //        if (employs1 == false)
            //        {
            //            int message = _employee.SaveRecord(emptransection);
            //            var message1 = _employeeManager.GetListofuser(emptransection);
            //            //employees.Add(message1);
            //            //employ.Number = number;
            //            //number++;
            //        }

            //    }
            //    //Read the next line
            //    line = sr.ReadLine();
            //}
            ////close the file
            //sr.Close();

            string fullPath = Request.MapPath("~/UploadFile/" + imageUrl);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);

            }
            return Json(employees1, JsonRequestBehavior.AllowGet);


        }
	}
}