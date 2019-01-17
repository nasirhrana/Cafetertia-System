using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BexMediaCom.Gateway;
using BexMediaCom.Models;

namespace BexMediaCom.Manager
{
    public class FoodTimeFrameManager
    {
        FoodTimeFrameGateway foodTimeFrameGateway = new FoodTimeFrameGateway();
        public bool Save(FoodTimeFrame foodTimeFrame)
        {
            bool message = false;
            if (IsTimeExists(foodTimeFrame))
            {
                message = false;
            }
            else
            {
                message = foodTimeFrameGateway.Save(foodTimeFrame) > 0;
            }
            return message;
        }

        public FoodTimeFrame GetFoodTimeFrameById(int? id)
        {
            return foodTimeFrameGateway.GetFoodTimeFrameById(id);
        }

        public int UpdatefoodTimeFrame(FoodTimeFrame foodTimeFrame)
        {
            int message = 0;
            if (IsNameExists(foodTimeFrame))
            {
                message = 0;
            }
            else
            {
                message = foodTimeFrameGateway.UpdatefoodTimeFrame(foodTimeFrame);

            }
            return message;
        }
        public bool IsNameExists(FoodTimeFrame foodTimeFrame)
        {
            return foodTimeFrameGateway.IsNameExists(foodTimeFrame);
        }
        public bool IsTimeExists(FoodTimeFrame foodTimeFrame)
        {
            return foodTimeFrameGateway.IsTimeExists(foodTimeFrame);
        }

    }
}