using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Gateway
{
    public class MealScheduleGateway: CommonGateway
    {
        public int SaveMealSchedule(MealDate meal)
        {
            try
            {
                Query = "INSERT INTO MealSchedule(EmpId, Date, ItemId, Status, Quantity) VALUES(@EmpId, @Date, @ItemId, @Status, @Quantity)";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();

                Command.Parameters.Add("EmpId", SqlDbType.VarChar);
                Command.Parameters["EmpId"].Value = meal.EmpId;
                Command.Parameters.Add("Date", SqlDbType.Date);
                Command.Parameters["Date"].Value = meal.Date;
                Command.Parameters.Add("ItemId", SqlDbType.Int);
                Command.Parameters["ItemId"].Value = meal.ItemId;
                Command.Parameters.Add("Status", SqlDbType.VarChar);
                Command.Parameters["Status"].Value = meal.Status;
                Command.Parameters.Add("Quantity", SqlDbType.Int);
                Command.Parameters["Quantity"].Value = meal.Quantity;

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

        public bool SaveMealScheduleIsExist(MealDate meal)
        {
            try
            {
                bool isNameExists = false;
                Query = "SELECT*FROM MealSchedule WHERE EmpId='" + meal.EmpId + "' AND Date='" + meal.Date + "' AND ItemId='" + meal.ItemId + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    isNameExists = true;
                }
                return isNameExists;
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

        public List<EmployeeCafeteriaTransaction> GetTodaysList()
        {
            try
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd");

                Query = "SELECT * FROM MealSchedule Where Date = '" + date + "' AND Status = '" + "Pending" + "' ";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<EmployeeCafeteriaTransaction> tdaysLists = new List<EmployeeCafeteriaTransaction>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        EmployeeCafeteriaTransaction list = new EmployeeCafeteriaTransaction();

                        list.Id = Convert.ToInt32(Reader["Id"]);
                        list.EmpId = Reader["EmpId"].ToString();
                        list.CheckDate = Reader["Date"].ToString();
                        list.FoodItemCostId = (int) Reader["ItemId"];
                        list.Status = Reader["Status"].ToString();
                        list.ItemQuantity = (int) Reader["Quantity"];

                        tdaysLists.Add(list);
                    }
                    Reader.Close();
                    Connection.Close();
                }
                foreach (var item in tdaysLists)
                {
                    string status = "Confirm";
                    int id = item.Id;

                    Query = @"UPDATE MealSchedule
   SET Status = @status
 WHERE Id = '" + id + "'";

                    Command.CommandText = Query;
                    Command.Connection = Connection;
                    Command.Parameters.Clear();

                    Command.Parameters.Add("status", SqlDbType.VarChar);
                    Command.Parameters["status"].Value = status;

                    Connection.Open();
                    int rowsAffected = Command.ExecuteNonQuery();
                    Connection.Close();
                }

                return tdaysLists;
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