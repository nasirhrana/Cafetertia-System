using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BexMediaCom.Models;

namespace BexMediaCom.Gateway
{
    public class DepartmentWiseReportGateway: CommonGateway
    {
        public List<ViewDepartmentTransaction> DepartmentReport(string name, string startdate, string enddate, int breakfast, int lunch, int dinner, int sehuri, string all)
        {
            try
            {
                if (all == "All")
                {
                    Query = @"SELECT AVG(e.UnitRate) AS UnitRate, Sum(e.ItemQuentity) AS ItemQuentity, s.Name as DivisionName, a.Name AS DepartmentName
FROM EmployeeCafeteriaTransaction e
INNER JOIN Employee d on d.EmpId = e.EmpId 
INNER JOIN Division s on s.Id = d.DivisionId
INNER JOIN Department a on a.Id = d.DepartmentId
WHERE  s.Id = '" + name + "' AND CheckDate >= '" + startdate + "' AND CheckDate   <= '" + enddate +
                    "'GROUP BY s.Name, a.Name";
                }
                else
                {
                    Query = @"SELECT AVG(e.UnitRate) AS UnitRate, Sum(e.ItemQuentity) AS ItemQuentity, s.Name as DivisionName, a.Name AS DepartmentName
FROM EmployeeCafeteriaTransaction e
INNER JOIN Employee d on d.EmpId = e.EmpId 
INNER JOIN Division s on s.Id = d.DivisionId
INNER JOIN Department a on a.Id = d.DepartmentId
WHERE  s.Id = '" + name + "' AND CheckDate >= '" + startdate + "' AND CheckDate   <= '" + enddate + "' AND ( FoodItemCostId = '" +
                        breakfast + "' OR FoodItemCostId= '" + lunch + "' OR FoodItemCostId= '" + dinner + "' OR FoodItemCostId = '" + sehuri +
                        "') GROUP BY s.Name, a.Name";
                }


                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<ViewDepartmentTransaction> viewDepartmentTransactions = new List<ViewDepartmentTransaction>();
                int number = 1;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        ViewDepartmentTransaction viewDepartmentTransaction = new ViewDepartmentTransaction();

                        viewDepartmentTransaction.SerialNumber = number;

                        viewDepartmentTransaction.Division = Reader["DivisionName"].ToString();
                        viewDepartmentTransaction.Department = Reader["DepartmentName"].ToString();
                        viewDepartmentTransaction.ItemCost = (decimal)Reader["UnitRate"];
                        viewDepartmentTransaction.ItemQty = (int)Reader["ItemQuentity"];

                        viewDepartmentTransaction.Total = viewDepartmentTransaction.ItemCost * viewDepartmentTransaction.ItemQty;

                        viewDepartmentTransactions.Add(viewDepartmentTransaction);
                        number++;
                    }
                    Reader.Close();
                }
                return viewDepartmentTransactions;
            }
            catch (Exception exception)
            {
                return null;
            }
            finally
            {
                Connection.Close();
            }

        }

    }
}