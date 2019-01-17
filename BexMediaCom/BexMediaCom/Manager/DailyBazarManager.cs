using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using BexMediaCom.Gateway;
using BexMediaCom.Models;

namespace BexMediaCom.Manager
{
    public class DailyBazarManager
    {
        private DailyBazarGateway _bazarGateway = new DailyBazarGateway();

        public string SaveBazar(DailyBazar bazar)
        {
            if (IsBazarExist(bazar))
            {
                int result = _bazarGateway.UpdateBazar(bazar);
                if (result > 0)
                {
                    return "Bazar Update Successfully!";
                }
                return "Connection Error.....!";
            }
            else
            {
                int result = _bazarGateway.SaveBazar(bazar);
                if (result > 0)
                {
                    return "Bazar Saved Successfully!";
                }
                return "Connection Error.....!";

            }
        }

        public bool IsBazarExist(DailyBazar bazar)
        {
            return _bazarGateway.IsBazarExist(bazar);
        }

        //public DailyBazar GetTotalAmount(string date)
        //{
        //    return _bazarGateway.GetTotalAmount(date);
        //}

        //public DailyBazar GetTotalUser(string date)
        //{
        //    return _bazarGateway.GetTotalUser(date);
        //}

        public DailyBazar FindUnitRate(string date, int itemId)
        {
            DailyBazar dailyBazar = _bazarGateway.GetTotalAmount(date, itemId);
            DailyBazar daily = _bazarGateway.GetTotalUser(date, itemId);
            DailyBazar dailt1 = _bazarGateway.GetTotalUser1(date, itemId);
            if (dailyBazar != null && daily != null || dailt1 != null) 
            {
                double totalAmount = dailyBazar.Amount;
                int employee = 0;
                int department = 0;
                int totalUser = 0;
                if (daily == null)
                {
                     employee = 0;
                     department = dailt1.TotalDepartmentUser;
                     totalUser = employee + department;
                }
                if (dailt1 == null)
                {
                    employee = daily.TotalUser;
                    department = 0;
                    totalUser = employee + department;
                }
                else
                {
                    employee = daily.TotalUser;
                    department = dailt1.TotalDepartmentUser;
                    totalUser = employee + department;
                }

               
                double unitrate = totalAmount / totalUser;
                DailyBazar bazar = new DailyBazar()
                {
                    TotalUser = totalUser,
                    Amount = totalAmount,
                    UnitRate = unitrate
                };
                return bazar;
            }
            else
            {
                DailyBazar bazar = new DailyBazar()
                {
                    TotalUser = 0,
                    Amount = 0,
                    UnitRate = 0.00,
                };
                return bazar;
            }
            
        }

        public string UpdateRate(DailyBazar bazar)
        {
          int result = _bazarGateway.UpdateRate(bazar);
            if (result > 0)
            {
                return "Rate Update Successfully!";
            }
            return "Connection Error.....!";
        }
    }
}