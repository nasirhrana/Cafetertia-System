using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using BexMediaCom.Models;

namespace BexMediaCom.Gateway
{
    public class ViewEmployeeTransactionGateway : CommonGateway
    {
        public List<ViewEmployeeTransaction> ViewAllEmployeeTransaction(string name, string startdate, string enddate, int breakfast, int lunch, int dinner, int sehuri, string all)
        {
            try
            {
                if (all == "All")
                {
                    if (name == "All Employee")
                    {
                        Query = @"SELECT a.EmpId,a.Name,a.Proximity, d.Name as DivisionName, s.Name as DepartmentName, g.Name as DesignationName, f.Name as FoodItemName, Sum(e.ItemQuentity) as ItemQuentity, e.UnitRate, convert(varchar, e.CheckDate, 23) as CheckDate
FROM EmployeeCafeteriaTransaction e
INNER JOIN FoodItemCost f on f.Id = e.FoodItemCostId
INNER JOIN Employee a on a.EmpId=e.EmpId
INNER JOIN Division d on d.Id = a.DivisionId
INNER JOIN Department s on s.Id = a.DepartmentId
INNER JOIN Designation g on g.Id = a.DesignationId
INNER JOIN FoodItemCost c on c.Id = e.FoodItemCostId
WHERE CheckDate >= '" + startdate + "' AND CheckDate   <= '" + enddate + "'" +
              "Group By a.EmpId,a.Name,a.Proximity, d.Name,s.Name, g.Name, f.Name,e.UnitRate, e.CheckDate";
                    }
                    else
                    {
                        Query = @"SELECT a.EmpId,a.Name,a.Proximity, d.Name as DivisionName, s.Name as DepartmentName, g.Name as DesignationName, f.Name as FoodItemName, Sum(e.ItemQuentity) as ItemQuentity, e.UnitRate, convert(varchar, e.CheckDate, 23) as CheckDate
FROM EmployeeCafeteriaTransaction e
INNER JOIN FoodItemCost f on f.Id = e.FoodItemCostId
INNER JOIN Employee a on a.EmpId=e.EmpId
INNER JOIN Division d on d.Id = a.DivisionId
INNER JOIN Department s on s.Id = a.DepartmentId
INNER JOIN Designation g on g.Id = a.DesignationId
INNER JOIN FoodItemCost c on c.Id = e.FoodItemCostId
WHERE  a.EmpId = '" + name + "' AND CheckDate >= '" + startdate + "' AND CheckDate   <= '" + enddate + "'" +
              "Group By a.EmpId,a.Name,a.Proximity, d.Name,s.Name, g.Name, f.Name,e.UnitRate, e.CheckDate";
                    }
              
                }
                else
                {
                    if (name == "All Employee")
                    {
                        Query = @"SELECT a.EmpId,a.Name,a.Proximity, d.Name as DivisionName, s.Name as DepartmentName, g.Name as DesignationName, f.Name as FoodItemName, Sum(e.ItemQuentity) as ItemQuentity, e.UnitRate, convert(varchar, e.CheckDate, 23) as CheckDate
FROM EmployeeCafeteriaTransaction e
INNER JOIN FoodItemCost f on f.Id = e.FoodItemCostId
INNER JOIN Employee a on a.EmpId=e.EmpId
INNER JOIN Division d on d.Id = a.DivisionId
INNER JOIN Department s on s.Id = a.DepartmentId
INNER JOIN Designation g on g.Id = a.DesignationId
INNER JOIN FoodItemCost c on c.Id = e.FoodItemCostId
WHERE CheckDate >= '" + startdate + "' AND CheckDate   <= '" + enddate + "' and ( FoodItemCostId = '" + breakfast + "' OR FoodItemCostId = '" + lunch + "' OR FoodItemCostId = '" + dinner + "' OR FoodItemCostId = '" + sehuri + "')" +
                  "Group By a.EmpId,a.Name,a.Proximity, d.Name,s.Name, g.Name, f.Name,e.UnitRate, e.CheckDate";
                    }
                    else
                    {
                        Query = @"SELECT a.EmpId,a.Name,a.Proximity, d.Name as DivisionName, s.Name as DepartmentName, g.Name as DesignationName, f.Name as FoodItemName, Sum(e.ItemQuentity) as ItemQuentity, e.UnitRate, convert(varchar, e.CheckDate, 23) as CheckDate
FROM EmployeeCafeteriaTransaction e
INNER JOIN FoodItemCost f on f.Id = e.FoodItemCostId
INNER JOIN Employee a on a.EmpId=e.EmpId
INNER JOIN Division d on d.Id = a.DivisionId
INNER JOIN Department s on s.Id = a.DepartmentId
INNER JOIN Designation g on g.Id = a.DesignationId
INNER JOIN FoodItemCost c on c.Id = e.FoodItemCostId
WHERE  a.EmpId = '" + name + "' AND CheckDate >= '" + startdate + "' AND CheckDate   <= '" + enddate + "' and ( FoodItemCostId = '" + breakfast + "' OR FoodItemCostId = '" + lunch + "' OR FoodItemCostId = '" + dinner + "' OR FoodItemCostId = '" + sehuri + "')" +
                  "Group By a.EmpId,a.Name,a.Proximity, d.Name,s.Name, g.Name, f.Name,e.UnitRate, e.CheckDate";
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
                        viewEmployeeTransaction.Name = Reader["Name"].ToString();
                        viewEmployeeTransaction.Division = Reader["DivisionName"].ToString();
                        viewEmployeeTransaction.Department = Reader["DepartmentName"].ToString();
                        viewEmployeeTransaction.Designation = Reader["DesignationName"].ToString();
                        viewEmployeeTransaction.FoodItem = Reader["FoodItemName"].ToString();
                        viewEmployeeTransaction.ItemQty = Convert.ToInt32(Reader["ItemQuentity"]);
                        viewEmployeeTransaction.ItemCost = Convert.ToDecimal(Reader["UnitRate"]);

                        viewEmployeeTransaction.SubTotal = viewEmployeeTransaction.ItemCost * viewEmployeeTransaction.ItemQty;

                        viewEmployeeTransaction.Total = viewEmployeeTransaction.ItemCost * viewEmployeeTransaction.ItemQty;

                        viewEmployeeTransaction.CardNumber = Reader["Proximity"].ToString();
                        viewEmployeeTransaction.CheckDate = Reader["CheckDate"].ToString();
                        viewEmployeeTransaction.SerialNo = number;
                        viewEmployeeTransactions.Add(viewEmployeeTransaction);
                        number++;
                    }
                    Reader.Close();
                }
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

        public List<Employee> AutoSearch(string name)
        {
            try
            {
                Query =
                    @"SELECT e.Id,e.EmpId, e.Name,d.Name as DivisionName, p.Name as DepartmentName,s.SectionName as SectionName,g.Name as DesignationName
FROM Employee e
INNER JOIN Division d on d.Id = e.DivisionId
INNER JOIN Department p on p.Id = e.DepartmentId
INNER JOIN Section s on s.Id = e.SectionId
INNER JOIN Designation g on g.Id = e.DesignationId
WHERE e.Name LIKE '%" + name + "%'";
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
                        employee.Id = (int) Reader["Id"];
                        employee.Name = Reader["Name"].ToString();
                        employee.Proximity = Reader["DivisionName"].ToString();
                        employee.DivisionName = Reader["DepartmentName"].ToString();
                        employee.EmailId = Reader["SectionName"].ToString();
                        employee.DesignationName = Reader["DesignationName"].ToString();
                        employee.EmpId = Reader["EmpId"].ToString();
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
    }
}