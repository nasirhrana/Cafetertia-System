using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Gateway
{
    public class DivisionWiseReportGateway : CommonGateway
    {

        public List<DivisionReport> GetDivisionReport(int divisionId, string startdate, string enddate, int breakfast, int lunch, int dinner, int sehuri, string all)
        {
            try
            {
                if (all == "All")
                {
                    Query =
                        @"SELECT Sum(e.ItemQuentity) as ItemQuentity, AVG(e.UnitRate) as UnitRate, e.EmpId, a.Name as EmployeeName, a.Proximity,d.Name as DivisionName, s.Name as DepartmentName, g.Name as DesignationName, c.SectionName
FROM EmployeeCafeteriaTransaction e
INNER JOIN FoodItemCost f on f.Id = e.FoodItemCostId
INNER JOIN Employee a on a.EmpId=e.EmpId
INNER JOIN Division d on d.Id = a.DivisionId
INNER JOIN Department s on s.Id = a.DepartmentId
INNER JOIN Designation g on g.Id = a.DesignationId
INNER JOIN Section c on c.Id = a.SectionId
WHERE CheckDate >= '" + startdate + "' AND CheckDate   <= ' " + enddate + "' AND d.Id = '" + divisionId +
                        "'Group By e.EmpId, a.Name, a.Proximity, d.Name, s.Name, g.Name, c.SectionName";
                }
                else
                {
                    Query =
                        @"SELECT Sum(e.ItemQuentity) as ItemQuentity, AVG(e.UnitRate) as UnitRate, e.EmpId, a.Name as EmployeeName, a.Proximity,d.Name as DivisionName, s.Name as DepartmentName, g.Name as DesignationName, c.SectionName
FROM EmployeeCafeteriaTransaction e
INNER JOIN FoodItemCost f on f.Id = e.FoodItemCostId
INNER JOIN Employee a on a.EmpId=e.EmpId
INNER JOIN Division d on d.Id = a.DivisionId
INNER JOIN Department s on s.Id = a.DepartmentId
INNER JOIN Designation g on g.Id = a.DesignationId
INNER JOIN Section c on c.Id = a.SectionId
WHERE CheckDate >= '" + startdate + "' AND CheckDate   <= ' " + enddate + "' AND d.Id = '" + divisionId +
                        "' AND (e.FoodItemCostId = '" + breakfast + "' OR e.FoodItemCostId = '" + lunch +
                        "' OR e.FoodItemCostId= '" + dinner + "' OR e.FoodItemCostId= '" + sehuri +
                        "') Group By e.EmpId, a.Name, a.Proximity, d.Name, s.Name, g.Name, c.SectionName";
                }

                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<DivisionReport> divisions = new List<DivisionReport>();
                int number = 1;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        DivisionReport division = new DivisionReport();

                        division.SerialNumber = number;
                        division.PIN = Reader["EmpId"].ToString();
                        division.EmployeeName = Reader["EmployeeName"].ToString();
                        division.Designation = Reader["DesignationName"].ToString();
                        division.Department = Reader["DepartmentName"].ToString();
                        division.Division = Reader["DivisionName"].ToString();
                        // division.FoodItem = Reader["FoodItemName"].ToString();
                        division.SectionName = Reader["SectionName"].ToString();
                        // division.CheckDate = Reader["CheckDate"].ToString();

                        //decimal value1 = (decimal)Reader["UnitRate"];
                        //value1 = System.Math.Round(value1, 2);
                        //division.ItemCost =  value1;

                        //  division.ItemQty = (int)Reader["ItemQuentity"];
                        // division.SubTotal = division.ItemCost * division.ItemQty;


                        //decimal value = division.ItemCost * division.ItemQty;
                        //value = System.Math.Round(value, 2);
                        //division.Total = value;

                        decimal value1 = (decimal)Reader["UnitRate"];
                        value1 = System.Math.Round(value1, 2);
                        division.ItemCost = value1;

                        division.ItemQty = (int)Reader["ItemQuentity"];
                        decimal value = division.ItemCost * division.ItemQty;
                        value = System.Math.Round(value, 2);
                        division.Total = value;

                        divisions.Add(division);
                        number++;
                    }
                    Reader.Close();
                }
                Connection.Close();
                return divisions;
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