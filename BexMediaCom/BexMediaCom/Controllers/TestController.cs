using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using BexMediaCom.Gateway;
using BexMediaCom.Manager;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Controllers
{
    public class TestController : Controller
    {
        readonly EmployeeManager _employeeManager = new EmployeeManager();
        private readonly EmployeeCafeteriaTransactionManager _employee = new EmployeeCafeteriaTransactionManager();
        private readonly MealScheduleManager _mealSchedule = new MealScheduleManager();
        // GET: Test
        public ActionResult Test()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }

            List<EmployeeCafeteriaTransaction> collectionOfTodaysMeal = _mealSchedule.GetTodaysList();

            foreach (var transaction in collectionOfTodaysMeal)
            {
                EmployeeCafeteriaTransaction emptransection = new EmployeeCafeteriaTransaction();

                emptransection.EmpId = transaction.EmpId;
                emptransection.FoodItemCostId = transaction.FoodItemCostId;
                string time589 = DateTime.Now.TimeOfDay.ToString();
                string hh = time589.Substring(0, 2);
                string mm = time589.Substring(3, 2);
                string ss = time589.Substring(6, 2);
                string time = hh + ":" + mm + ":" + ss;
                emptransection.CheckTime = time;

                emptransection.CheckDate = transaction.CheckDate;

                if (emptransection.EmpId != null)
                {
                    var employs = _employee.IsEmployeeExist(emptransection.EmpId, emptransection.FoodItemCostId, emptransection.CheckTime, emptransection.CheckDate);
                    var employs1 = _employee.IsEmployeeExist1(emptransection.EmpId, emptransection.CheckTime, emptransection.CheckDate);
                    if (employs == false)
                    {
                      //  if (employs1 == false)
                      //  {
                            int message = _employee.SaveRecord(emptransection);
                            //var message1 = _employeeManager.GetListofuser(emptransection);

                      //  }

                    }
                }
            }



            return View();
        }
        public ActionResult Test1(int foodItemId)
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            try
            {
                string presentTime = DateTime.Now.TimeOfDay.ToString();

                bool IsFoodItemTimeExist = _employeeManager.IsFoodItemTimeExist(presentTime, foodItemId);
                // List<ShowEmployeeMonitoring> employees1 = new List<ShowEmployeeMonitoring>();
                //Queue employees1 = new Queue();
                Stack employees1 = new Stack();
                List<ShowEmployeeMonitoring> employees420 = new List<ShowEmployeeMonitoring>();
                if (IsFoodItemTimeExist == true)
                {
                    EmployeeMonitoring employ = null;

                    string year = DateTime.Now.Year.ToString();
                    string month = DateTime.Now.Month.ToString();
                    string day = DateTime.Now.Day.ToString();

                    if (month.Length == 1)
                    {
                        month = "0" + month;
                    }
                    if (day.Length == 1)
                    {
                        day = "0" + day;
                    }

                    string textFile = year + month + day + ".txt";
                    //test 1****************
                    //FileStream fileStream = new FileStream(@"E:\\ATT_DATA\\" + textFile + "", FileMode.OpenOrCreate, FileAccess.ReadWrite,
                    //    FileShare.None);
                    // StreamReader sr = new StreamReader(fileStream);

                    //test 2****************
                    using (var fs = new FileStream(@"D:\\ATT_DATA\\" + textFile + "", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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

                            if (foodItemId == 9)
                            {
                                dates1 = dates1.AddDays(-1);
                            }

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
                            bool employs2 = false;

                            bool IsFoodItemTimeExist1 = _employeeManager.IsFoodItemTimeExist(time, foodItemId);

                            if (IsFoodItemTimeExist1 == true)
                            {
                                if (cardNumber != "")
                                {
                                    employ = _employeeManager.GetEmployeeByEmpId(cardNumber);
                                    bool employ12 = _employeeManager.GetEmployeeByEmpId12(cardNumber, foodItemId);

                                    if (employ12 == true)
                                    {
                                        ShowEmployeeMonitoring employ1 = _employeeManager.GetEmployeeByEmpId1(cardNumber, foodItemId);

                                        var status = employees420.Find(m => m.EmpId == employ1.EmpId);
                                        if (status == null)
                                        {
                                            employees420.Add(employ1);
                                            employees1.Push(employ1);
                                        }

                                    }
                                    else
                                    {
                                        ShowEmployeeMonitoring employ1 = _employeeManager.GetEmployeeByEmpId2(cardNumber, foodItemId);

                                        var status = employees420.Find(m => m.EmpId == employ1.EmpId);
                                        if (status == null)
                                        {
                                           // employ1.Statuss = "";
                                            employ1.Statuss = @"<p style=""color: red""> Not Listed </p>";
                                            employees420.Add(employ1);
                                            employees1.Push(employ1);
                                        }
                                    }



                                }
                                EmployeeCafeteriaTransaction emptransection = new EmployeeCafeteriaTransaction();
                                emptransection.EmpId = employ.EmpId;
                                emptransection.FoodItemCostId = foodItemId;
                                emptransection.CheckTime = time;
                                emptransection.CheckDate = dates1.ToString();
                                if (emptransection.EmpId != null)
                                {
                                    employs = _employee.IsEmployeeExist0901(emptransection.EmpId, foodItemId, time, dates);
                                    employs1 = _employee.IsEmployeeExist09011(emptransection.EmpId, time, dates, foodItemId);
                                    employs2 = _employee.IsEmployeeExist090112(emptransection.EmpId, time, dates, foodItemId);
                                    if (employs == false)
                                    {
                                        if (employs1 == false)
                                        {
                                            if (employs2 == false)
                                            {
                                                int message = _employee.SaveRecord1(emptransection);
                                            }
                                         
                                        }

                                    }

                                    employs = _employee.IsEmployeeExist191(emptransection.EmpId, foodItemId, time, dates);
                                    employs1 = _employee.IsEmployeeExist101(emptransection.EmpId, time, dates);
                                    if (employs == true)
                                    {
                                        if (employs1 == true)
                                        {
                                            int message = _employee.ConfirmMeal(emptransection);
                                            var message1 = _employeeManager.GetListofuser(emptransection);

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




                }

                return Json(employees1, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return null;

            }


        }
    }
}