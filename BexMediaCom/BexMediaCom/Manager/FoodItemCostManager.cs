using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BexMediaCom.Gateway;
using BexMediaCom.Models;

namespace BexMediaCom.Manager
{
    public class FoodItemCostManager
    {
        FoodItemCostGateway foodItemCostGateway = new FoodItemCostGateway();

        public bool Save(FoodItemCost foodItemCost)
        {

            if (IsFoodExists(foodItemCost.Name))
            {
                throw new Exception("Sorry!!!Food Item already Exists");
            }

            return foodItemCostGateway.Save(foodItemCost) > 0;
        }

        public bool IsFoodExists(string name)
        {
            return foodItemCostGateway.IsFoodExists(name);
        }

        public List<FoodItemCost> ShowAllFoodItemCost()
        {
            return foodItemCostGateway.ShowAllFoodItemCost();
        }

        public List<FoodItemCost> GetAllFoodItemCosts()
        {
            return foodItemCostGateway.GetAllFoodItemCosts();
        }

        public List<FoodItemCost> GetFoodItemCost()
        {
            return foodItemCostGateway.GetFoodItemCost();
        }

        public List<ViewFoodItemTimeSchedule> ViewFoodItemTimeSchedule()
        {
            return foodItemCostGateway.ViewFoodItemTimeSchedule();
        }

    }
}