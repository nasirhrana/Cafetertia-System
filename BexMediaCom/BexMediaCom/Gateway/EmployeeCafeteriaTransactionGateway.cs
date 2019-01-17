using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Gateway
{
    public class EmployeeCafeteriaTransactionGateway : CommonGateway
    {
        public int Save(EmployeeCafeteriaTransaction cafeteria)
        {
            try
            {
                cafeteria.ItemQuantity = 1;
              //  cafeteria.UnitRate = 0;
                Query =
                    "INSERT INTO EmployeeCafeteriaTransaction(EmpId, FoodItemCostId, ItemQuentity, CheckTime, CheckDate, UnitRate  ) VALUES(@EmpId, @FoodItemCostId, @ItemQuentity, @CheckTime, @CheckDate, @UnitRate )";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("EmpId", SqlDbType.VarChar);
                Command.Parameters["EmpId"].Value = cafeteria.EmpId;
                Command.Parameters.Add("FoodItemCostId", SqlDbType.Int);
                Command.Parameters["FoodItemCostId"].Value = cafeteria.FoodItemCostId;
                Command.Parameters.Add("ItemQuentity", SqlDbType.Int);
                Command.Parameters["ItemQuentity"].Value = cafeteria.ItemQuantity;
                Command.Parameters.Add("CheckTime", SqlDbType.Time);
                Command.Parameters["CheckTime"].Value = cafeteria.CheckTime;
                Command.Parameters.Add("CheckDate", SqlDbType.Date);
                Command.Parameters["CheckDate"].Value = cafeteria.CheckDate;
                Command.Parameters.Add("UnitRate", SqlDbType.Decimal);
                Command.Parameters["UnitRate"].Value = cafeteria.UnitRate;
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
       //  public int SaveRecord(string empId, int foodItemId)
        public int SaveRecord(EmployeeCafeteriaTransaction cafeteriaa)

        {
            try
            {
                cafeteriaa.ItemQuantity = 1;
              //  cafeteriaa.CheckDate = DateTime.Now.Date;
              //  cafeteriaa.UnitRate = 0;
                string statuss = "No";

                Query =
                    "INSERT INTO EmployeeCafeteriaTransaction(EmpId, FoodItemCostId, ItemQuentity, CheckTime, CheckDate, UnitRate, Status ) VALUES(@EmpId, @FoodItemCostId, @ItemQuentity, @CheckTime, @CheckDate, @UnitRate, @statuss )";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("EmpId", SqlDbType.VarChar);
                Command.Parameters["EmpId"].Value = cafeteriaa.EmpId;
                Command.Parameters.Add("FoodItemCostId", SqlDbType.Int);
                Command.Parameters["FoodItemCostId"].Value = cafeteriaa.FoodItemCostId;
                Command.Parameters.Add("ItemQuentity", SqlDbType.Int);
                Command.Parameters["ItemQuentity"].Value = cafeteriaa.ItemQuantity;
                Command.Parameters.Add("CheckTime", SqlDbType.VarChar);
                Command.Parameters["CheckTime"].Value = cafeteriaa.CheckTime;
                Command.Parameters.Add("CheckDate", SqlDbType.Date);
                Command.Parameters["CheckDate"].Value = cafeteriaa.CheckDate;
                Command.Parameters.Add("UnitRate", SqlDbType.Decimal);
                Command.Parameters["UnitRate"].Value = cafeteriaa.UnitRate;
                Command.Parameters.Add("Statuss", SqlDbType.VarChar);
                Command.Parameters["Statuss"].Value = statuss;
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
        public int SaveRecord1(EmployeeCafeteriaTransaction cafeteriaa)
        {
            try
            {
                cafeteriaa.ItemQuantity = 1;
                //  cafeteriaa.CheckDate = DateTime.Now.Date;
                //  cafeteriaa.UnitRate = 0;
                string statuss = "NotListed";

                Query =
                    "INSERT INTO EmployeeCafeteriaTransaction(EmpId, FoodItemCostId, ItemQuentity, CheckTime, CheckDate, UnitRate, Status ) VALUES(@EmpId, @FoodItemCostId, @ItemQuentity, @CheckTime, @CheckDate, @UnitRate, @statuss )";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("EmpId", SqlDbType.VarChar);
                Command.Parameters["EmpId"].Value = cafeteriaa.EmpId;
                Command.Parameters.Add("FoodItemCostId", SqlDbType.Int);
                Command.Parameters["FoodItemCostId"].Value = cafeteriaa.FoodItemCostId;
                Command.Parameters.Add("ItemQuentity", SqlDbType.Int);
                Command.Parameters["ItemQuentity"].Value = cafeteriaa.ItemQuantity;
                Command.Parameters.Add("CheckTime", SqlDbType.VarChar);
                Command.Parameters["CheckTime"].Value = cafeteriaa.CheckTime;
                Command.Parameters.Add("CheckDate", SqlDbType.Date);
                Command.Parameters["CheckDate"].Value = cafeteriaa.CheckDate;
                Command.Parameters.Add("UnitRate", SqlDbType.Decimal);
                Command.Parameters["UnitRate"].Value = cafeteriaa.UnitRate;
                Command.Parameters.Add("Statuss", SqlDbType.VarChar);
                Command.Parameters["Statuss"].Value = statuss;
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

        public int ConfirmMeal(EmployeeCafeteriaTransaction cafeteriaa)
        {
            try
            {
                cafeteriaa.ItemQuantity = 1;
                //  cafeteriaa.CheckDate = DateTime.Now.Date;
                //  cafeteriaa.UnitRate = 0;
                string statuss = "Yes";

                Query = @"UPDATE [dbo].[EmployeeCafeteriaTransaction]
   SET Status = '" + statuss + "' WHERE EmpId = '" + cafeteriaa.EmpId + "' AND FoodItemCostId = '" +
                        cafeteriaa.FoodItemCostId + "' AND CheckDate = '" + cafeteriaa.CheckDate + "' ";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();

                Command.Parameters.Add("Statuss", SqlDbType.VarChar);
                Command.Parameters["Statuss"].Value = statuss;

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

        public bool IsEmployeeExist(string empId, int foodItemId, string time, string date)
        {
            try
            {
                //string date = DateTime.Now.Date.ToString();
                //int index = date.IndexOf(' ');
                //string resultss = date.Substring(0, index);

                bool isemployeeIdexists = false;
                Query = "SELECT *FROM EmployeeCafeteriaTransaction WHERE EmpId='" + empId + "' and FoodItemCostId = '" + foodItemId + "' and CheckDate ='" + date + "' ";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    isemployeeIdexists = true;
                    if (isemployeeIdexists == false)
                    {
                        string EmpId = empId;
                    }
                }
                return isemployeeIdexists;
            }
            catch (Exception exception)
            {
                return false;
            }
            finally
            {
                Connection.Close();
            }

        }

        public bool IsEmployeeExist191(string empId, int foodItemId, string time, string date)
        {
            try
            {
                //string date = DateTime.Now.Date.ToString();
                //int index = date.IndexOf(' ');
                //string resultss = date.Substring(0, index);

                bool isemployeeIdexists = false;
                Query = "SELECT *FROM EmployeeCafeteriaTransaction WHERE EmpId='" + empId + "' and FoodItemCostId = '" +
                        foodItemId + "' and CheckDate ='" + date + "'and Status ='" + "No" + "'  ";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    isemployeeIdexists = true;
                    if (isemployeeIdexists == false)
                    {
                        string EmpId = empId;
                    }
                }
                return isemployeeIdexists;
            }
            catch (Exception exception)
            {
                return false;
            }
            finally
            {
                Connection.Close();
            }

        }

        public bool IsEmployeeExist0901(string empId, int foodItemId, string time, string date)
        {
            try
            {
                //string date = DateTime.Now.Date.ToString();
                //int index = date.IndexOf(' ');
                //string resultss = date.Substring(0, index);

                bool isemployeeIdexists = false;
                Query = "SELECT *FROM EmployeeCafeteriaTransaction WHERE EmpId='" + empId + "' and FoodItemCostId = '" +
                        foodItemId + "' and Status ='" + "No" + "' and CheckDate ='" + date + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    isemployeeIdexists = true;
                    if (isemployeeIdexists == false)
                    {
                        string EmpId = empId;
                    }
                }
                return isemployeeIdexists;
            }
            catch (Exception exception)
            {
                return false;
            }
            finally
            {
                Connection.Close();
            }

        }

        public bool CheckList(string empId, int foodItemId, string time, string date)
        {
            try
            {
                //string date = DateTime.Now.Date.ToString();
                //int index = date.IndexOf(' ');
                //string resultss = date.Substring(0, index);

                bool isemployeeIdexists = false;
                Query = "SELECT *FROM EmployeeCafeteriaTransaction WHERE EmpId='" + empId + "' and FoodItemCostId = '" + foodItemId + "' and CheckDate ='" + date + "' ";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    isemployeeIdexists = true;
                    if (isemployeeIdexists == false)
                    {
                        string EmpId = empId;
                    }
                }
                return isemployeeIdexists;
            }
            catch (Exception exception)
            {
                return false;
            }
            finally
            {
                Connection.Close();
            }

        }

        public bool IsEmployeeExist1(string empId, string time, string date)
        {
            try
            {
                //string date = DateTime.Now.Date.ToString();
                //int index = date.IndexOf(' ');
                //string resultss = date.Substring(0, index);

                bool isemployeeIdexists = false;
                Query = "SELECT *FROM EmployeeCafeteriaTransaction WHERE EmpId='" + empId + "' and CheckDate ='" + date + "' ";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    isemployeeIdexists = true;
                }
                return isemployeeIdexists;
            }
            catch (Exception exception)
            {
                return false;
            }
            finally
            {
                Connection.Close();
            }

        }

        public bool IsEmployeeExist101(string empId, string time, string date)
        {
            try
            {
                //string date = DateTime.Now.Date.ToString();
                //int index = date.IndexOf(' ');
                //string resultss = date.Substring(0, index);

                bool isemployeeIdexists = false;
                Query = "SELECT *FROM EmployeeCafeteriaTransaction WHERE EmpId='" + empId + "' and CheckDate ='" + date + "' ";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    isemployeeIdexists = true;
                }
                return isemployeeIdexists;
            }
            catch (Exception exception)
            {
                return false;
            }
            finally
            {
                Connection.Close();
            }

        }
        public bool IsEmployeeExist09011(string empId, string time, string date, int foodItemId)
        {
            try
            {
                //string date = DateTime.Now.Date.ToString();
                //int index = date.IndexOf(' ');
                //string resultss = date.Substring(0, index);

                bool isemployeeIdexists = false;
                Query = "SELECT *FROM EmployeeCafeteriaTransaction WHERE EmpId='" + empId + "' and FoodItemCostId = '" + foodItemId +
                        "' and CheckDate ='" + date + "' and Status ='" + "Yes" + "' ";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    isemployeeIdexists = true;
                }
                return isemployeeIdexists;
            }
            catch (Exception exception)
            {
                return false;
            }
            finally
            {
                Connection.Close();
            }

        }       
        public bool IsEmployeeExist090112(string empId, string time, string date, int foodItemId)
        {
            try
            {
                //string date = DateTime.Now.Date.ToString();
                //int index = date.IndexOf(' ');
                //string resultss = date.Substring(0, index);

                bool isemployeeIdexists = false;
                Query = "SELECT *FROM EmployeeCafeteriaTransaction WHERE EmpId='" + empId + "' and FoodItemCostId = '" + foodItemId +
                        "' and CheckDate ='" + date + "' and Status ='" + "NotListed" + "' ";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    isemployeeIdexists = true;
                }
                return isemployeeIdexists;
            }
            catch (Exception exception)
            {
                return false;
            }
            finally
            {
                Connection.Close();
            }

        }

        public List<EmployeeTransaction> GetDayTransaction(string date)
        {
            try
            {
              //  string date = DateTime.Now.Date.ToString();

                Query = @"SELECT e.Id, e.EmpId, Sum(e.ItemQuentity) as ItemQuentity,CONVERT(varchar, e.CheckDate, 23) as CheckDate,e.CheckTime, a.Name as EmployeeName, d.Name as DivisionName, p.Name as DepartmentName, c.SectionName as SectionName, f.Name as ItemName
FROM EmployeeCafeteriaTransaction e
Inner Join Employee a on a.EmpId = e.EmpId
Inner join Division d on d.id = a.DivisionId
Inner join Department p on p.id = a.DepartmentId
Inner join Section c on c.id = a.SectionId
Inner join FoodItemCost f on f.id = e.FoodItemCostId
WHERE CheckDate='" + date + "'" +
                        "Group By e.Id, e.EmpId,e.CheckDate,e.CheckTime, a.Name,d.Name,p.Name, c.SectionName, f.Name ";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<EmployeeTransaction>  employeeTransactions = new List<EmployeeTransaction>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        EmployeeTransaction employeeTransaction = new EmployeeTransaction();

                        employeeTransaction.Id = (int) Reader["Id"];
                        employeeTransaction.EmpId = Reader["EmpId"].ToString();
                        employeeTransaction.EmployeeName = Reader["EmployeeName"].ToString();
                        employeeTransaction.Division = Reader["DivisionName"].ToString();
                        employeeTransaction.Department = Reader["DepartmentName"].ToString();
                        employeeTransaction.Section = Reader["SectionName"].ToString();
                        employeeTransaction.ItemName = Reader["ItemName"].ToString();
                        employeeTransaction.CheckDate = Reader["CheckDate"].ToString();
                        employeeTransaction.CheckTime = Reader["CheckTime"].ToString();
                        employeeTransactions.Add(employeeTransaction);
                    }
                }
                return employeeTransactions;
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

        public int TransactionDelete(int? id)
        {
            try
            {
                Query = "DELETE FROM [dbo].[EmployeeCafeteriaTransaction] WHERE Id='" + id + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                int rowAffected = Command.ExecuteNonQuery();
                Connection.Close();

                return rowAffected;
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
    }
}