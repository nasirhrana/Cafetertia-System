using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using BexMediaCom.Models;
using Microsoft.Ajax.Utilities;

namespace BexMediaCom.Gateway
{
    public class SectionReportGateway: CommonGateway
    {
        public List<Employee> AutoSearchSectionByDepartmentId(string name, int departmentId)
        {
            try
            {
                Query =
                    @"SELECT Id, SectionName FROM Section
WHERE DepartmentId = '" + departmentId + "'AND SectionName LIKE '%" + name + "%'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<Employee> employees = new List<Employee>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.Id = (int)Reader["Id"];
                        employee.Name = Reader["SectionName"].ToString();
                        employees.Add(employee);
                    }
                    Reader.Close();
                }
                return employees;
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

        public List<ViewEmployeeTransaction> GetItemWishEmployeeReport(int type, string startdate, string enddate, string name, int breakfast, int lunch, int dinner, int sehuri, string all)
        {
            try
            {
                if (all == "All")
                {
                    Query = @"SELECT e.Id, e.EmpId,convert(varchar, e.CheckDate, 23) as CheckDate, Sum(e.ItemQuentity) as ItemQuentity, AVG(e.UnitRate) as UnitRate, a.Name as EmployeeName, a.Proximity, d.Name as DivisionName,s.Name as DepartmentName,o.SectionName,g.Name as DesignationName, f.Name as FoodItemName
FROM EmployeeCafeteriaTransaction e
INNER JOIN Employee a on a.EmpId=e.EmpId
INNER JOIN Division d on d.Id = a.DivisionId
INNER JOIN Department s on s.Id = a.DepartmentId
INNER JOIN Section o on o.Id = a.SectionId
INNER JOIN Designation g on g.Id = a.DesignationId
INNER JOIN FoodItemCost f on f.Id = e.FoodItemCostId
WHERE  a.EmpId = '" + name + "' AND CheckDate >= '" + startdate + "' AND CheckDate   <= '" + enddate + "'" +
                    "Group By e.Id,e.EmpId,e.CheckDate,a.Name, a.Proximity,d.Name,s.Name,o.SectionName,g.Name, f.Name";
                }
                else
                {
                    Query = @"SELECT e.Id, e.EmpId,convert(varchar, e.CheckDate, 23) as CheckDate, Sum(e.ItemQuentity) as ItemQuentity, AVG(e.UnitRate) as UnitRate, a.Name as EmployeeName, a.Proximity, d.Name as DivisionName,s.Name as DepartmentName,o.SectionName,g.Name as DesignationName, f.Name as FoodItemName
FROM EmployeeCafeteriaTransaction e
INNER JOIN Employee a on a.EmpId=e.EmpId
INNER JOIN Division d on d.Id = a.DivisionId
INNER JOIN Department s on s.Id = a.DepartmentId
INNER JOIN Section o on o.Id = a.SectionId
INNER JOIN Designation g on g.Id = a.DesignationId
INNER JOIN FoodItemCost f on f.Id = e.FoodItemCostId
WHERE  a.EmpId = '" + name + "' AND CheckDate >= '" + startdate + "' AND CheckDate   <= '" + enddate + "' and ( FoodItemCostId = '" + breakfast + "' OR FoodItemCostId = '" + lunch + "' OR FoodItemCostId = '" + dinner + "' OR FoodItemCostId = '" + sehuri + "')" +
                   "Group By e.Id,e.EmpId,e.CheckDate,a.Name, a.Proximity,d.Name,s.Name,o.SectionName,g.Name, f.Name";
                }
//                if (all == "All")
//                {
//                    Query = @"SELECT convert(varchar, e.CheckDate, 23) as CheckDate, f.Name as FoodItemName, Sum(e.ItemQuentity) as Quentity, a.Name, f.Cost
//FROM EmployeeCafeteriaTransaction e
//INNER JOIN FoodItemCost f on f.Id = e.FoodItemCostId
//INNER JOIN Employee a on a.EmpId=e.EmpId
//WHERE  CheckDate >= '" + startdate + "' AND CheckDate   <= ' " + enddate + "' AND a.Name = '" + name +
//                            "' GROUP BY e.CheckDate, f.Name, a.Name, f.Cost";
//                }
//                else
//                {
//                    Query = @"SELECT convert(varchar, e.CheckDate, 23) as CheckDate, f.Name as FoodItemName, Sum(e.ItemQuentity) as Quentity, a.Name, f.Cost
//FROM EmployeeCafeteriaTransaction e
//INNER JOIN FoodItemCost f on f.Id = e.FoodItemCostId
//INNER JOIN Employee a on a.EmpId=e.EmpId
//WHERE  CheckDate >= '" + startdate + "' AND CheckDate   <= ' " + enddate + "' AND a.Name = '" + name +
//                            "' AND (e.FoodItemCostId = '" + breakfast + "' OR e.FoodItemCostId= '" + lunch +
//                            "' OR e.FoodItemCostId= '" + dinner + "' OR e.FoodItemCostId= '" + sehuri +
//                            "') GROUP BY e.CheckDate, f.Name, a.Name, f.Cost";
//                }

                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<ViewEmployeeTransaction> viewEmployeeTransactions = new List<ViewEmployeeTransaction>();
                int number = 1;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        ViewEmployeeTransaction viewEmployeeTransaction = new ViewEmployeeTransaction();
                        viewEmployeeTransaction.EmpId = Reader["EmpId"].ToString();
                        viewEmployeeTransaction.Name = Reader["EmployeeName"].ToString();
                        viewEmployeeTransaction.Division = Reader["DivisionName"].ToString();
                        viewEmployeeTransaction.Department = Reader["DepartmentName"].ToString();
                        viewEmployeeTransaction.SectionName = Reader["SectionName"].ToString();
                        viewEmployeeTransaction.Designation = Reader["DesignationName"].ToString();
                        viewEmployeeTransaction.FoodItem = Reader["FoodItemName"].ToString();
                        viewEmployeeTransaction.ItemQty = Convert.ToInt32(Reader["ItemQuentity"]);
                        decimal value1 = Convert.ToDecimal(Reader["UnitRate"]);
                        value1 = System.Math.Round(value1, 2);
                        viewEmployeeTransaction.ItemCost = value1;

                        decimal value = viewEmployeeTransaction.ItemCost * viewEmployeeTransaction.ItemQty;
                        value = System.Math.Round(value, 2);
                        viewEmployeeTransaction.Total = value;

                        viewEmployeeTransaction.CardNumber = Reader["Proximity"].ToString();
                        viewEmployeeTransaction.CheckDate = Reader["CheckDate"].ToString();
                        viewEmployeeTransaction.SerialNo = number;
                        viewEmployeeTransactions.Add(viewEmployeeTransaction);
                        number++;
                    }
                    Reader.Close();
                }
                Connection.Close();
                return viewEmployeeTransactions;
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

        public List<ViewEmployeeTransaction> GetItemWishDepartmentReport(int type, string startdate, string enddate, int departmentId, string name, int breakfast, int lunch, int dinner, int sehuri, string all)
        {
            try
            {
                if (all == "All")
                {
                    if (name == "")
                    {
                        Query = @"SELECT e.Id, e.EmpId, Sum(e.ItemQuentity) as ItemQuentity, AVG(e.UnitRate) as UnitRate, a.Name as EmployeeName, a.Proximity, d.Name as DivisionName,s.Name as DepartmentName,o.SectionName,g.Name as DesignationName, f.Name as FoodItemName
FROM EmployeeCafeteriaTransaction e
INNER JOIN Employee a on a.EmpId=e.EmpId
INNER JOIN Division d on d.Id = a.DivisionId
INNER JOIN Department s on s.Id = a.DepartmentId
INNER JOIN Section o on o.Id = a.SectionId
INNER JOIN Designation g on g.Id = a.DesignationId
INNER JOIN FoodItemCost f on f.Id = e.FoodItemCostId
WHERE CheckDate >= '" + startdate + "' AND CheckDate   <= '" + enddate + "' AND s.Id = '" + departmentId + "'" +
                                "Group By e.Id,e.EmpId,a.Name, a.Proximity,d.Name,s.Name,o.SectionName,g.Name, f.Name";
                    }
                    else
                    {
                        Query = @"SELECT e.Id, e.EmpId, Sum(e.ItemQuentity) as ItemQuentity, AVG(e.UnitRate) as UnitRate, a.Name as EmployeeName, a.Proximity, d.Name as DivisionName,s.Name as DepartmentName,o.SectionName,g.Name as DesignationName, f.Name as FoodItemName
FROM EmployeeCafeteriaTransaction e
INNER JOIN Employee a on a.EmpId=e.EmpId
INNER JOIN Division d on d.Id = a.DivisionId
INNER JOIN Department s on s.Id = a.DepartmentId
INNER JOIN Section o on o.Id = a.SectionId
INNER JOIN Designation g on g.Id = a.DesignationId
INNER JOIN FoodItemCost f on f.Id = e.FoodItemCostId
WHERE CheckDate >= '" + startdate + "' AND CheckDate   <= '" + enddate + "' AND s.Id = '" + departmentId + "' AND o.Id = '" + name + "'" +
                                "Group By e.Id,e.EmpId,a.Name, a.Proximity,d.Name,s.Name,o.SectionName,g.Name, f.Name";
                    }
                    

                }
                else
                {
                    if (name == "")
                    {
                        Query =
                       @"SELECT e.Id, e.EmpId, Sum(e.ItemQuentity) as ItemQuentity, AVG(e.UnitRate) as UnitRate, a.Name as EmployeeName, a.Proximity, d.Name as DivisionName,s.Name as DepartmentName,o.SectionName,g.Name as DesignationName, f.Name as FoodItemName
FROM EmployeeCafeteriaTransaction e
INNER JOIN Employee a on a.EmpId=e.EmpId
INNER JOIN Division d on d.Id = a.DivisionId
INNER JOIN Department s on s.Id = a.DepartmentId
INNER JOIN Section o on o.Id = a.SectionId
INNER JOIN Designation g on g.Id = a.DesignationId
INNER JOIN FoodItemCost f on f.Id = e.FoodItemCostId
WHERE CheckDate >= '" + startdate + "' AND CheckDate   <= ' " + enddate + "' AND s.Id = '" + departmentId + "' AND (e.FoodItemCostId = '" + breakfast + "' OR e.FoodItemCostId = '" + lunch +
                       "' OR e.FoodItemCostId= '" + dinner + "' OR e.FoodItemCostId= '" + sehuri +
                       "') Group By e.Id,e.EmpId,a.Name, a.Proximity,d.Name,s.Name,o.SectionName,g.Name, f.Name";
                    }
                    else
                    {
                        Query =
                       @"SELECT e.Id, e.EmpId, Sum(e.ItemQuentity) as ItemQuentity, AVG(e.UnitRate) as UnitRate, a.Name as EmployeeName, a.Proximity, d.Name as DivisionName,s.Name as DepartmentName,o.SectionName,g.Name as DesignationName, f.Name as FoodItemName
FROM EmployeeCafeteriaTransaction e
INNER JOIN Employee a on a.EmpId=e.EmpId
INNER JOIN Division d on d.Id = a.DivisionId
INNER JOIN Department s on s.Id = a.DepartmentId
INNER JOIN Section o on o.Id = a.SectionId
INNER JOIN Designation g on g.Id = a.DesignationId
INNER JOIN FoodItemCost f on f.Id = e.FoodItemCostId
WHERE CheckDate >= '" + startdate + "' AND CheckDate   <= ' " + enddate + "' AND s.Id = '" + departmentId + "' AND o.Id = '" + name + "' AND (e.FoodItemCostId = '" + breakfast + "' OR e.FoodItemCostId = '" + lunch +
                       "' OR e.FoodItemCostId= '" + dinner + "' OR e.FoodItemCostId= '" + sehuri +
                       "') Group By e.Id,e.EmpId,a.Name, a.Proximity,d.Name,s.Name,o.SectionName,g.Name, f.Name";
                    }
                   
                }

                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<ViewEmployeeTransaction> viewEmployeeTransactions = new List<ViewEmployeeTransaction>();
                int number = 1;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        ViewEmployeeTransaction viewEmployeeTransaction = new ViewEmployeeTransaction();
                        viewEmployeeTransaction.EmpId = Reader["EmpId"].ToString();
                        viewEmployeeTransaction.Name = Reader["EmployeeName"].ToString();
                        viewEmployeeTransaction.Division = Reader["DivisionName"].ToString();
                        viewEmployeeTransaction.Department = Reader["DepartmentName"].ToString();
                        viewEmployeeTransaction.SectionName = Reader["SectionName"].ToString();
                        viewEmployeeTransaction.Designation = Reader["DesignationName"].ToString();
                        viewEmployeeTransaction.FoodItem = Reader["FoodItemName"].ToString();
                        viewEmployeeTransaction.ItemQty = Convert.ToInt32(Reader["ItemQuentity"]);
                        decimal value1 = Convert.ToDecimal(Reader["UnitRate"]);
                        value1 = System.Math.Round(value1, 2);
                        viewEmployeeTransaction.ItemCost = value1;

                        decimal value = viewEmployeeTransaction.ItemCost * viewEmployeeTransaction.ItemQty;
                        value = System.Math.Round(value, 2);
                        viewEmployeeTransaction.Total = value;

                        viewEmployeeTransaction.CardNumber = Reader["Proximity"].ToString();
                        //viewEmployeeTransaction.CheckDate = Reader["CheckDate"].ToString();
                        viewEmployeeTransaction.SerialNo = number;
                        viewEmployeeTransactions.Add(viewEmployeeTransaction);
                        number++;
                    }
                    Reader.Close();
                }
                Connection.Close();
                return viewEmployeeTransactions;
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