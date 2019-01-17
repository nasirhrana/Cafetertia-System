using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BexMediaCom.Gateway;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Manager
{
    public class MealScheduleManager
    {
        private readonly MealScheduleGateway _mealSchedule = new MealScheduleGateway();

        public int SaveMealSchedule(MealDate meal)
        {
            if (_mealSchedule.SaveMealScheduleIsExist(meal))
            {
                return 0;
            }
            return _mealSchedule.SaveMealSchedule(meal);



        }

        public List<EmployeeCafeteriaTransaction> GetTodaysList()
        {
            return _mealSchedule.GetTodaysList();
        } 

    }
}