using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BexMediaCom.Manager;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeManager _home = new HomeManager();
        private readonly MealScheduleManager _mealSchedule = new MealScheduleManager();
        public ActionResult Index()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            string empId = Session["EmpId"].ToString();
            var userInfo = _home.UserInfo(empId);
            var notification = _home.Notification(empId);
            var notification1 = _home.Notification1(empId);
            int number1 = Convert.ToInt32(notification[0].EmaiNotification);
            int number2 = Convert.ToInt32(notification1[0].EmaiNotification);
            int total = number1 + number2;
            if (userInfo != null)
            {
                Session["TotalCost"] = userInfo[0].TotalCost;
                Session["TotalMeil"] = userInfo[0].TotalMeil;
                Session["AvgUnitRate"] = userInfo[0].AvgUnitRate;
            }
            else
            {
                Session["TotalCost"] = 0;
                Session["TotalMeil"] = 0;
                Session["AvgUnitRate"] = 0;
            }
            if (notification != null)
            {
                Session["Notification"] = total;

            }

            var notification5 = _home.Notification5();
            var notification6 = _home.Notification6();
            int number5 = Convert.ToInt32(notification5[0].EmaiNotification);
            int number6 = Convert.ToInt32(notification6[0].EmaiNotification);
            int total7 = number5 + number6;

            if (notification5 != null && notification6 != null)
            {
                Session["Notification1"] = total7;

            }             

            return View();
        }

        [HttpPost]
        public ActionResult GetBreakfastSchedule(SendMale sendMale)
        {

            List<DateTime> dates = new List<DateTime>();
            var datesDispo = sendMale.DateCollection.Split(',');

            foreach (var date1 in datesDispo)
            {
                dates.Add(DateTime.ParseExact(date1, "dd/MM/yyyy", null));
            }

            MealDate meal = new MealDate();
            meal.EmpId = Session["EmpId"].ToString();
            meal.ItemId = 8;
            meal.Quantity = 1;
            meal.Status = "Pending";
            int number = 0;

            foreach (var date2 in dates)
            {
                DateTime datea = DateTime.Now;
                DateTime dateb = date2;
                int result = DateTime.Compare(datea, dateb);
                string relationship;

                if (result < 0)
                {
                    meal.Date = date2.ToString();
                    number += _mealSchedule.SaveMealSchedule(meal);
                }


            }
            string msg = number + " " + "Breakfast Schedule Save";
            if (number != 0)
            {
                TempData["msg"] = msg;
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult GetLunchSchedule(SendMale sendMale)
        {
            //string checkTime = DateTime.Now.TimeOfDay.ToString();

            List<DateTime> dates = new List<DateTime>();
            var datesDispo = sendMale.DateCollection.Split(',');

            foreach (var date1 in datesDispo)
            {
                dates.Add(DateTime.ParseExact(date1, "dd/MM/yyyy", null));
            }

            MealDate meal = new MealDate();
            meal.EmpId = Session["EmpId"].ToString();
            meal.ItemId = 6;
            meal.Quantity = 1;
            meal.Status = "Pending";
            int number = 0;

            foreach (var date2 in dates)
            {
                DateTime datea = DateTime.Now;
                DateTime dateb = date2;
                int result = DateTime.Compare(datea, dateb);
                string relationship;

                if (result < 0)
                {
                    meal.Date = date2.ToString();
                    number += _mealSchedule.SaveMealSchedule(meal);
                }


            }
            string msg = number + " " + "Lunch Schedule Save";
            if (number != 0)
            {
                TempData["msg"] = msg;
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult GetDinnerSchedule(SendMale sendMale)
        {

            List<DateTime> dates = new List<DateTime>();
            var datesDispo = sendMale.DateCollection.Split(',');

            foreach (var date1 in datesDispo)
            {
                dates.Add(DateTime.ParseExact(date1, "dd/MM/yyyy", null));
            }

            MealDate meal = new MealDate();
            meal.EmpId = Session["EmpId"].ToString();
            meal.ItemId = 7;
            meal.Quantity = 1;
            meal.Status = "Pending";
            int number = 0;

            foreach (var date2 in dates)
            {
                DateTime datea = DateTime.Now;
                DateTime dateb = date2;
                int result = DateTime.Compare(datea, dateb);
                string relationship;

                if (result < 0)
                {
                    meal.Date = date2.ToString();
                    number += _mealSchedule.SaveMealSchedule(meal);
                }


            }
            string msg = number + " " + "Dinner Schedule Save";
            if (number != 0)
            {
                TempData["msg"] = msg;
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult GetSehriSchedule(SendMale sendMale)
        {

            List<DateTime> dates = new List<DateTime>();
            var datesDispo = sendMale.DateCollection.Split(',');

            foreach (var date1 in datesDispo)
            {
                dates.Add(DateTime.ParseExact(date1, "dd/MM/yyyy", null));
            }

            MealDate meal = new MealDate();
            meal.EmpId = Session["EmpId"].ToString();
            meal.ItemId = 9;
            meal.Quantity = 1;
            meal.Status = "Pending";
            int number = 0;

            foreach (var date2 in dates)
            {
                DateTime datea = DateTime.Now;
                DateTime dateb = date2;
                int result = DateTime.Compare(datea, dateb);
                string relationship;

                if (result < 0)
                {
                    meal.Date = date2.ToString();
                    number += _mealSchedule.SaveMealSchedule(meal);
                }

            }
            string msg = number + " " + "Sehri Schedule Save";
            if (number != 0)
            {
                TempData["msg"] = msg;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}