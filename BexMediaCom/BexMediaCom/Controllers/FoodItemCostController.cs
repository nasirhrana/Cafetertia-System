using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BexMediaCom.Manager;
using BexMediaCom.Models;

namespace BexMediaCom.Controllers
{
    public class FoodItemCostController : Controller
    {
        FoodItemCostManager foodItemCostManager = new FoodItemCostManager();

        // GET: FoodItemCost
        public ActionResult Save()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            return View();
        }
        [HttpPost]
        public ActionResult Save(FoodItemCost foodItemCost)
        {
            try
            {
                ViewBag.Message = foodItemCostManager.Save(foodItemCost) ? "Food Item Saved Successfully" : "Food Item Saved Failed";
            }
            catch (Exception exception)
            {
                ViewBag.Message = exception.Message;
            }
            ViewBag.FoodItemCost = foodItemCostManager.ShowAllFoodItemCost();
            return View();
        }

        public ActionResult JsonViewFoodItemCost()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            var foodItemCost = foodItemCostManager.ShowAllFoodItemCost();
            return Json(foodItemCost, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonViewFoodItemTimeSchedule()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            var foodItemTimeSchedule = foodItemCostManager.ViewFoodItemTimeSchedule();
            return Json(foodItemTimeSchedule, JsonRequestBehavior.AllowGet);
        }
    }
}