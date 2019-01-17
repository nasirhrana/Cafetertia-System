using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BexMediaCom.Models;

namespace BexMediaCom.Gateway
{
    public class ViewItemWiseReportGateway : CommonGateway
    {
        public List<FoodItemCost> GetItem()
        {
            try
            {
                Query = "SELECT * FROM FoodItemCost";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<FoodItemCost> itemCosts = new List<FoodItemCost>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        FoodItemCost item = new FoodItemCost();
                        item.Id = Convert.ToInt32(Reader["Id"].ToString());
                        item.Name = Reader["Name"].ToString();
                        itemCosts.Add(item);
                    }
                    Reader.Close();
                }
                return itemCosts;
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

        public List<ViewDepartmentTransaction> GetItemWishEmployeeReport(int type, string startdate, string enddate, string name, int breakfast, int lunch, int dinner, int sehuri, string all)
        {
            try
            {
                if (name == "All Employee")
                {
                    if (all == "All")
                    {
                        Query = @"SELECT a.EmpId,a.Name as EmployeeName,a.Proximity, d.Name as DivisionName, s.Name as DepartmentName, g.Name as DesignationName, f.Name as FoodItemName, Sum(e.ItemQuentity) as ItemQuentity, e.UnitRate, convert(varchar, e.CheckDate, 23) as CheckDate
FROM EmployeeCafeteriaTransaction e
INNER JOIN FoodItemCost f on f.Id = e.FoodItemCostId
INNER JOIN Employee a on a.EmpId=e.EmpId
INNER JOIN Division d on d.Id = a.DivisionId
INNER JOIN Department s on s.Id = a.DepartmentId
INNER JOIN Designation g on g.Id = a.DesignationId
INNER JOIN FoodItemCost c on c.Id = e.FoodItemCostId
WHERE  CheckDate >= '" + startdate + "' AND CheckDate   <= ' " + enddate + "' " +
                                " Group By a.EmpId,a.Name,a.Proximity, d.Name,s.Name, g.Name, f.Name,e.UnitRate, e.CheckDate";
                    }
                    else
                    {
                        Query = @"SELECT a.EmpId,a.Name as EmployeeName,a.Proximity, d.Name as DivisionName, s.Name as DepartmentName, g.Name as DesignationName, f.Name as FoodItemName, Sum(e.ItemQuentity) as ItemQuentity, e.UnitRate, convert(varchar, e.CheckDate, 23) as CheckDate
FROM EmployeeCafeteriaTransaction e
INNER JOIN FoodItemCost f on f.Id = e.FoodItemCostId
INNER JOIN Employee a on a.EmpId=e.EmpId
INNER JOIN Division d on d.Id = a.DivisionId
INNER JOIN Department s on s.Id = a.DepartmentId
INNER JOIN Designation g on g.Id = a.DesignationId
INNER JOIN FoodItemCost c on c.Id = e.FoodItemCostId
WHERE  CheckDate >= '" + startdate + "' AND CheckDate   <= ' " + enddate + "'  AND (e.FoodItemCostId = '" + breakfast + "' OR e.FoodItemCostId= '" + lunch +
                                "' OR e.FoodItemCostId= '" + dinner + "' OR e.FoodItemCostId= '" + sehuri +
                                "') Group By a.EmpId,a.Name,a.Proximity, d.Name,s.Name, g.Name, f.Name,e.UnitRate, e.CheckDate";
                    }
                }
                else
                {
                    if (all == "All")
                    {
                        Query = @"SELECT a.EmpId,a.Name as EmployeeName,a.Proximity, d.Name as DivisionName, s.Name as DepartmentName, g.Name as DesignationName, f.Name as FoodItemName, Sum(e.ItemQuentity) as ItemQuentity, e.UnitRate, convert(varchar, e.CheckDate, 23) as CheckDate
FROM EmployeeCafeteriaTransaction e
INNER JOIN FoodItemCost f on f.Id = e.FoodItemCostId
INNER JOIN Employee a on a.EmpId=e.EmpId
INNER JOIN Division d on d.Id = a.DivisionId
INNER JOIN Department s on s.Id = a.DepartmentId
INNER JOIN Designation g on g.Id = a.DesignationId
INNER JOIN FoodItemCost c on c.Id = e.FoodItemCostId
WHERE  CheckDate >= '" + startdate + "' AND CheckDate   <= ' " + enddate + "' AND a.Name = '" + name +
                                "' Group By a.EmpId,a.Name,a.Proximity, d.Name,s.Name, g.Name, f.Name,e.UnitRate, e.CheckDate";
                    }
                    else
                    {
                        Query = @"SELECT a.EmpId,a.Name as EmployeeName,a.Proximity, d.Name as DivisionName, s.Name as DepartmentName, g.Name as DesignationName, f.Name as FoodItemName, Sum(e.ItemQuentity) as ItemQuentity, e.UnitRate, convert(varchar, e.CheckDate, 23) as CheckDate
FROM EmployeeCafeteriaTransaction e
INNER JOIN FoodItemCost f on f.Id = e.FoodItemCostId
INNER JOIN Employee a on a.EmpId=e.EmpId
INNER JOIN Division d on d.Id = a.DivisionId
INNER JOIN Department s on s.Id = a.DepartmentId
INNER JOIN Designation g on g.Id = a.DesignationId
INNER JOIN FoodItemCost c on c.Id = e.FoodItemCostId
WHERE  CheckDate >= '" + startdate + "' AND CheckDate   <= ' " + enddate + "' AND a.Name = '" + name +
                                "' AND (e.FoodItemCostId = '" + breakfast + "' OR e.FoodItemCostId= '" + lunch +
                                "' OR e.FoodItemCostId= '" + dinner + "' OR e.FoodItemCostId= '" + sehuri +
                                "') Group By a.EmpId,a.Name,a.Proximity, d.Name,s.Name, g.Name, f.Name,e.UnitRate, e.CheckDate";
                    }
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
                        viewDepartmentTransaction.PIN = Reader["EmpId"].ToString();
                        viewDepartmentTransaction.EmployeeName = Reader["EmployeeName"].ToString();
                        viewDepartmentTransaction.Designation = Reader["DesignationName"].ToString();
                        viewDepartmentTransaction.Division = Reader["DivisionName"].ToString();
                        viewDepartmentTransaction.Department = Reader["DepartmentName"].ToString();
                        viewDepartmentTransaction.FoodItem = Reader["FoodItemName"].ToString();
                        viewDepartmentTransaction.CheckDate = Reader["CheckDate"].ToString();
                        
                        viewDepartmentTransaction.ItemQty = (int)Reader["ItemQuentity"];
                        decimal value1 = (decimal)Reader["UnitRate"];
                        value1 = System.Math.Round(value1, 2);
                        viewDepartmentTransaction.ItemCost = value1;


                        decimal value = viewDepartmentTransaction.ItemCost * viewDepartmentTransaction.ItemQty;
                       value = System.Math.Round(value, 2);

                        viewDepartmentTransaction.Total = value;

                        viewDepartmentTransactions.Add(viewDepartmentTransaction);
                        number++;
                    }
                    Reader.Close();
                }
                Connection.Close();
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

        public List<ViewDepartmentTransaction> GetItemWishDepartmentReport(int type, string startdate, string enddate, string name, int breakfast, int lunch, int dinner, int sehuri, string all)
        {
            try
            {
                if (all == "All")
                {
                    Query = @"SELECT Sum(e.ItemQuentity) as ItemQuentity, AVG(e.UnitRate) as UnitRate, e.EmpId, a.Name as EmployeeName, a.Proximity,d.Name as DivisionName, s.Name as DepartmentName, g.Name as DesignationName
FROM EmployeeCafeteriaTransaction e
INNER JOIN FoodItemCost f on f.Id = e.FoodItemCostId
INNER JOIN Employee a on a.EmpId=e.EmpId
INNER JOIN Division d on d.Id = a.DivisionId
INNER JOIN Department s on s.Id = a.DepartmentId
INNER JOIN Designation g on g.Id = a.DesignationId
WHERE CheckDate >= '" + startdate + "' AND CheckDate   <= '" + enddate + "' AND s.Id = '" + name + "' AND e.FoodItemCostId != '" + 8 + "'" +
                            "Group By e.EmpId, a.Name, a.Proximity, d.Name, s.Name, g.Name";

                }
                else
                {
                    Query =
                        @"SELECT Sum(e.ItemQuentity) as ItemQuentity, AVG(e.UnitRate) as UnitRate, e.EmpId, a.Name as EmployeeName, a.Proximity,d.Name as DivisionName, s.Name as DepartmentName, g.Name as DesignationName
FROM EmployeeCafeteriaTransaction e
INNER JOIN FoodItemCost f on f.Id = e.FoodItemCostId
INNER JOIN Employee a on a.EmpId=e.EmpId
INNER JOIN Division d on d.Id = a.DivisionId
INNER JOIN Department s on s.Id = a.DepartmentId
INNER JOIN Designation g on g.Id = a.DesignationId
WHERE CheckDate >= '" + startdate + "' AND CheckDate   <= ' " + enddate + "' AND s.Id = '" + name +
                        "' AND (e.FoodItemCostId = '" + breakfast + "' OR e.FoodItemCostId = '" + lunch +
                        "' OR e.FoodItemCostId= '" + dinner + "' OR e.FoodItemCostId= '" + sehuri +
                        "') Group By e.EmpId, a.Name, a.Proximity, d.Name, s.Name, g.Name";
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
                        viewDepartmentTransaction.PIN = Reader["EmpId"].ToString();
                        viewDepartmentTransaction.EmployeeName = Reader["EmployeeName"].ToString();
                        viewDepartmentTransaction.Designation = Reader["DesignationName"].ToString();
                        viewDepartmentTransaction.Department = Reader["DepartmentName"].ToString();
                        viewDepartmentTransaction.Division = Reader["DivisionName"].ToString();
                        //viewDepartmentTransaction.FoodItem = Reader["FoodItemName"].ToString();
                       // viewDepartmentTransaction.CheckDate = Reader["CheckDate"].ToString();
                        decimal value1 = (decimal)Reader["UnitRate"];
                        value1 = System.Math.Round(value1, 2);
                        viewDepartmentTransaction.ItemCost = value1;

                        viewDepartmentTransaction.ItemQty = (int)Reader["ItemQuentity"];
                        decimal value = viewDepartmentTransaction.ItemCost * viewDepartmentTransaction.ItemQty;
                        value = System.Math.Round(value, 2);
                        viewDepartmentTransaction.Total = value;

                        viewDepartmentTransactions.Add(viewDepartmentTransaction);
                        number++;
                    }
                    Reader.Close();
                }
                Connection.Close();
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

        public List<Employee> AutoSearch(string name)
        {
            try
            {
                Query =
                    @"SELECT e.Id, e.EmpId, e.Name, e.Proximity, d.Name as DivisionName, a.Name as DepartmentName, n.Name as DesignationName
FROM Employee e
INNER JOIN Division d on d.Id=e.DivisionId
INNER JOIN Department a on a.Id=e.DepartmentId
INNER JOIN Designation n on n.Id=e.DesignationId
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
                        employee.Id = (int)Reader["Id"];
                        employee.Name = Reader["Name"].ToString();
                        employee.Date = Reader["EmpId"].ToString();
                        employee.Proximity = Reader["Proximity"].ToString();
                        employee.DivisionName = Reader["DivisionName"].ToString();
                        employee.EmailId = Reader["DepartmentName"].ToString();
                        employee.EmpId = Reader["DesignationName"].ToString();
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

        public List<Employee> AutoSearchDepartment(string name, int divisionId)
        {
            try
            {
                Query =
                    @"SELECT Id, Name FROM Department
WHERE DivisionId = '" + divisionId + "'AND Name LIKE '%" + name + "%'";
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
                        employee.Name = Reader["Name"].ToString();
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