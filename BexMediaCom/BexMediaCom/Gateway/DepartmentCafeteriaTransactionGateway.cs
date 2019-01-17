using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BexMediaCom.Models;

namespace BexMediaCom.Gateway
{
    public class DepartmentCafeteriaTransactionGateway : CommonGateway
    {
        public int Save(DepartmentCafeteriaTransaction department)
        {
            try
            {
                double UnitRate = 0;
                Query =
                    "INSERT INTO DepartmentCafeteriaTransaction(DeptId,FoodItemCostId,ItemQuantity,CheckTime,CheckDate, UnitRate) VALUES(@DeptId, @FoodItemCostId, @ItemQuantity, @CheckTime, @CheckDate ,@UnitRate)";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("DeptId", SqlDbType.Int);
                Command.Parameters["DeptId"].Value = department.DeptId;
                Command.Parameters.Add("FoodItemCostId", SqlDbType.Int);
                Command.Parameters["FoodItemCostId"].Value = department.FoodItemCostId;
                Command.Parameters.Add("ItemQuantity", SqlDbType.Int);
                Command.Parameters["ItemQuantity"].Value = department.ItemQuantity;
                Command.Parameters.Add("CheckTime", SqlDbType.Time);
                Command.Parameters["CheckTime"].Value = department.CheckTime;
                Command.Parameters.Add("CheckDate", SqlDbType.Date);
                Command.Parameters["CheckDate"].Value = department.CheckDate;
                Command.Parameters.Add("UnitRate", SqlDbType.Decimal);
                Command.Parameters["UnitRate"].Value = UnitRate;

                Connection.Open();
                int rowsAffected = Command.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception exception)
            {
                return 0;
            }
            finally
            {
                Connection.Close();
            }

        }

        public List<ViewDepartmentTransaction> GetDepartmentTransaction(string name, string startdate, string enddate, int breakfast, int lunch, int dinner, int sehuri, string all)
        {
            try
            {
                if (all == "All")
                {
                    Query = @"SELECT d.Name as DptName, f.Name as FoodName, AVG(e.UnitRate) as UnitRate, Sum(e.ItemQuantity) as ItemQuantity,convert(varchar, e.CheckDate, 23) as CheckDate, s.Name as DivisionName
FROM DepartmentCafeteriaTransaction e
INNER JOIN Department d on d.Id = e.DeptId 
INNER JOIN Division s on s.Id = d.DivisionId
INNER JOIN FoodItemCost f on f.Id = e.FoodItemCostId
WHERE  d.Name = '" + name + "' AND CheckDate >= '" + startdate + "' AND CheckDate   <= '" + enddate +
                    "'Group By d.Name, f.Name, e.UnitRate,e.CheckDate, s.Name";
                }
                else
                {
                    Query = @"SELECT d.Name as DptName, f.Name as FoodName, AVG(e.UnitRate) as UnitRate, Sum(e.ItemQuantity) as ItemQuantity,convert(varchar, e.CheckDate, 23) as CheckDate, s.Name as DivisionName
FROM DepartmentCafeteriaTransaction e
INNER JOIN Department d on d.Id = e.DeptId 
INNER JOIN Division s on s.Id = d.DivisionId
INNER JOIN FoodItemCost f on f.Id = e.FoodItemCostId
WHERE  d.Name = '" + name + "' AND CheckDate >= '" + startdate + "' AND CheckDate   <= '" + enddate + "' AND ( FoodItemCostId = '" +
                        breakfast + "' OR FoodItemCostId= '" + lunch + "' OR FoodItemCostId= '" + dinner + "' OR FoodItemCostId = '" + sehuri +
                        "') Group By d.Name, f.Name, e.UnitRate,e.CheckDate, s.Name";
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
                        viewDepartmentTransaction.CheckDate = Reader["CheckDate"].ToString();
                        viewDepartmentTransaction.Department = Reader["DptName"].ToString();
                        viewDepartmentTransaction.Division = Reader["DivisionName"].ToString();
                        viewDepartmentTransaction.FoodItem = Reader["FoodName"].ToString();
                        viewDepartmentTransaction.ItemCost = (decimal)Reader["UnitRate"];
                        viewDepartmentTransaction.ItemQty = (int)Reader["ItemQuantity"];
                        viewDepartmentTransaction.SubTotal = viewDepartmentTransaction.ItemCost * viewDepartmentTransaction.ItemQty;
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

        public List<Employee> AutoSearch(string name, int divisionId)
        {
            try
            {
                Query =
                    @"SELECT Name FROM Department
WHERE DivisionId = '" + divisionId + "' AND Name LIKE '%" + name + "%'";
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