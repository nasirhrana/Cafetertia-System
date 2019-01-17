using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Gateway
{
    public class CheckStatusGateway: CommonGateway
    {
        public List<CheckStatus> GetAllResuest(string empId)
        {
            try
            {
                Query = @"SELECT s.Id, s.ItemId, CONVERT(varchar, s.Date, 23) as Date , s.Status, s.Quantity, f.Name
FROM SendOrder s
INNER JOIN FoodItemCost f on f.Id = s.ItemId Where EmpId = '" + empId + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<CheckStatus> statuses = new List<CheckStatus>();
                int number = 1;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        CheckStatus check = new CheckStatus();

                        check.Id = Convert.ToInt32(Reader["Id"].ToString());
                        check.ItemId = (int)Reader["ItemId"];
                        check.Date = Reader["Date"].ToString();
                        check.Item = Reader["Name"].ToString();
                        if (check.Item == "CustomMenu")
                        {
                            check.Item = "Custom Menu";
                        }
                        check.Status = Reader["Status"].ToString();
                        if (check.Status == "Submit")
                        {
                            check.Status = "Pending";
                        }
                        if (check.Status == "Reject")
                        {
                            check.Status = "Rejected";
                        }
                        check.Quantity = Reader["Quantity"].ToString();
                        check.Number = number;
                        statuses.Add(check);
                        number++;
                    }
                    Reader.Close();
                }
                return statuses;
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

        public List<CheckStatus> GetOneResuest(int id)
        {
            try
            {
                Query = @"SELECT s.Id, s.ItemId, CONVERT(varchar, s.Date, 23) as Date , s.Status, s.Quantity, f.Name
FROM SendOrder s
INNER JOIN FoodItemCost f on f.Id = s.ItemId Where s.Id = '" + id + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<CheckStatus> statuses = new List<CheckStatus>();
                int number = 1;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        CheckStatus check = new CheckStatus();

                        check.Id = Convert.ToInt32(Reader["Id"].ToString());
                        check.ItemId = (int)Reader["ItemId"];
                        check.Date = Reader["Date"].ToString();
                        check.Item = Reader["Name"].ToString();
                        check.Status = Reader["Status"].ToString();
                        check.Quantity = Reader["Quantity"].ToString();
                        check.Number = number;
                        statuses.Add(check);
                        number++;
                    }
                    Reader.Close();
                }
                return statuses;
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
        public int CancelOrder(int id)
        {
            
            try
            {
                string status = "Cancel";

                Query = @"UPDATE SendOrder
   SET [Status] = @status
 WHERE Id = '" + id + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("status", SqlDbType.VarChar);
                Command.Parameters["status"].Value = status;
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
            return 0;
        }

        public string GetApplicationTime(int id)
        {
            try
            {
                Query = @"SELECT StartTime FROM CafeTimeSchedule Where ItemId = '" + id + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                DateTime? time = null;
                string time1 = "";
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        time1 = Reader["StartTime"].ToString();

                    }
                    Reader.Close();
                }
                //string time2 = DateTime.Now.ToString("yyyy-MM-dd");
                //string time3 = time2 + " " + time1;
                //time = Convert.ToDateTime(time3);
                return time1;
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

        public string GetApplicationDate(int id)
        {
            try
            {
                Query = @"SELECT CONVERT(varchar, Date, 23) as Date  FROM SendOrder Where Id = '" + id + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                DateTime? time = null;
                string time1 = "";
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        time1 = Reader["Date"].ToString();

                    }
                    Reader.Close();
                }
                //string time2 = DateTime.Now.ToString("yyyy-MM-dd");
                //string time3 = time2 + " " + time1;
                //time = Convert.ToDateTime(time3);
                return time1;
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

        public List<CheckStatus> GetAllMealSchedul(string empId)
        {
            try
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd");

                Query = @"SELECT s.Id, s.ItemId, CONVERT(varchar, s.Date, 23) as Date , s.Status, s.Quantity, f.Name
FROM MealSchedule s
INNER JOIN FoodItemCost f on f.Id = s.ItemId Where EmpId = '" + empId + "' AND Status = '" +
                        "Pending" + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<CheckStatus> statuses = new List<CheckStatus>();
                int number = 1;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        CheckStatus check = new CheckStatus();

                        check.Id = Convert.ToInt32(Reader["Id"].ToString());
                        check.ItemId = (int)Reader["ItemId"];
                        check.Date = Reader["Date"].ToString();
                        check.Item = Reader["Name"].ToString();
                        if (check.Item == "CustomMenu")
                        {
                            check.Item = "Custom Menu";
                        }
                        check.Status = Reader["Status"].ToString();
                        if (check.Status == "Submit")
                        {
                            check.Status = "Pending";
                        }
                        if (check.Status == "Reject")
                        {
                            check.Status = "Rejected";
                        }
                        check.Quantity = Reader["Quantity"].ToString();
                        check.Number = number;
                        statuses.Add(check);
                        number++;
                    }
                    Reader.Close();
                }
                return statuses;
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

        public List<CheckStatus> GetOneResuest1(int id)
        {
            try
            {
                Query = @"SELECT s.Id, s.ItemId, CONVERT(varchar, s.Date, 23) as Date , s.Status, s.Quantity, f.Name
FROM MealSchedule s
INNER JOIN FoodItemCost f on f.Id = s.ItemId Where s.Id = '" + id + "' AND Status = '" + "Pending" + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<CheckStatus> statuses = new List<CheckStatus>();
                int number = 1;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        CheckStatus check = new CheckStatus();

                        check.Id = Convert.ToInt32(Reader["Id"].ToString());
                        check.ItemId = (int)Reader["ItemId"];
                        check.Date = Reader["Date"].ToString();
                        check.Item = Reader["Name"].ToString();
                        check.Status = Reader["Status"].ToString();
                        check.Quantity = Reader["Quantity"].ToString();
                        check.Number = number;
                        statuses.Add(check);
                        number++;
                    }
                    Reader.Close();
                }
                return statuses;
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

        public int CancelOrder1(int id)
        {

            try
            {
                string status = "Cancel";

                Query = @"UPDATE MealSchedule
   SET [Status] = @status
 WHERE Id = '" + id + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("status", SqlDbType.VarChar);
                Command.Parameters["status"].Value = status;
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
            return 0;
        }
    }
}