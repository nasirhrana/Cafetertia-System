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
    public class FoodTimeFrameController : Controller
    {
        FoodItemCostManager foodItemCostManager = new FoodItemCostManager();
        FoodTimeFrameManager foodTimeFrameManager = new FoodTimeFrameManager();
        // GET: FoodTimeFrame
        public ActionResult Save()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            return View();
        }

        [HttpPost]
        public ActionResult Save(FoodTimeFrame foodTimeFrame)
        {
            string status = null;
            try
            {
                status = foodTimeFrameManager.Save(foodTimeFrame) ? "Time Frame Assigned Successfully" : "Time Frame Assigned Faild";
            }
            catch (Exception exception)
            {
                status = exception.Message;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFoodItemCost()
        {
            if (Session["UserRoleId"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            List<FoodItemCost> foodItemCostManagers = foodItemCostManager.GetFoodItemCost();
            return Json(foodItemCostManagers);
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
            FoodTimeFrame timeFrame = foodTimeFrameManager.GetFoodTimeFrameById(id);
            if (timeFrame == null)
            {
                return HttpNotFound();
            }

            return View(timeFrame);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FoodTimeFrame foodTimeFrame)
        {
            if (ModelState.IsValid)
            {
                int result = foodTimeFrameManager.UpdatefoodTimeFrame(foodTimeFrame);
                if (result == 0)
                {
                    ViewBag.Massage = "Item Name is Alrady Exist! Please Try a different Name.";
                }
                else
                {
                    return RedirectToAction("Save");
                }

            }

            return View();
        }

    }
}