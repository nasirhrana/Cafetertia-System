using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Gateway
{
    public class SendOrderAndCheckNeedGateway : CommonGateway
    {
        public int SendOrder(SendOrder sendOrder)
        {
            sendOrder.Status = "Submit";
            try
            {
                Query = "INSERT INTO SendOrder(Date,EmpId,ItemId,Status) VALUES(@Date,@EmpId,@ItemId,@Status)";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("Date", SqlDbType.Date);
                Command.Parameters["Date"].Value = sendOrder.Date;
                Command.Parameters.Add("EmpId", SqlDbType.VarChar);
                Command.Parameters["EmpId"].Value = sendOrder.EmpId;
                Command.Parameters.Add("ItemId", SqlDbType.Int);
                Command.Parameters["ItemId"].Value = sendOrder.ItemId;
                Command.Parameters.Add("Status", SqlDbType.VarChar);
                Command.Parameters["Status"].Value = sendOrder.Status;
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

        public bool IsSendOrderExists(SendOrder sendOrder)
        {
            sendOrder.Status = "Submit";
            try
            {
                bool isNameExists = false;
                Query = "SELECT * FROM SendOrder WHERE EmpId = '" + sendOrder.EmpId + "' and ItemId = '" + sendOrder.ItemId + "' and Status = '" + sendOrder.Status + "'";
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

        public int UpdateSendOrder(SendOrder sendOrder)
        {
            try
            {
                 int id = 0;
                Query = "SELECT * FROM SendOrder WHERE EmpId = '" + sendOrder.EmpId + "' and ItemId = '" + sendOrder.ItemId + "' and Status = '" + sendOrder.Status + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    id = Convert.ToInt32(Reader["Id"].ToString());

                }
                Reader.Close();
                Connection.Close();

                Query = @"UPDATE [dbo].[SendOrder]
   SET [Date] = @Date
      ,[EmpId] = @EmpId
      ,[ItemId] = @ItemId
      ,[Status] = @Status
 WHERE Id = '" + id + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("Date", SqlDbType.Date);
                Command.Parameters["Date"].Value = sendOrder.Date;
                Command.Parameters.Add("EmpId", SqlDbType.VarChar);
                Command.Parameters["EmpId"].Value = sendOrder.EmpId;
                Command.Parameters.Add("ItemId", SqlDbType.Int);
                Command.Parameters["ItemId"].Value = sendOrder.ItemId;
                Command.Parameters.Add("Status", SqlDbType.VarChar);
                Command.Parameters["Status"].Value = sendOrder.Status;
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

        public List<CheckOrder> CheckNeed()
        {
            try
            {
                Query =
                    @"SELECT s.Id, s.EmpId, s.Status,s.Date,s.typeId,s.Quantity,s.CustomMenu, t.Name as ItemName, e.Name as EmployeeName, d.Name as DivisionName, p.Name as DepartmentName, g.Name as DesignationName
FROM SendOrder s
INNER JOIN Employee e on e.EmpId = s.EmpId
INNER JOIN Division d on d.Id = e.DivisionId
INNER JOIN Department p on p.Id = e.DepartmentId
INNER JOIN Designation g on g.Id = e.DesignationId
INNER JOIN FoodItemCost t on t.Id = s.ItemId
Where s.Status = '" + "Submit" + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<CheckOrder> checkOrders = new List<CheckOrder>();
                while (Reader.Read())
                {
                    CheckOrder checkOrder = new CheckOrder();
                    checkOrder.Id = (int)Reader["Id"];
                    checkOrder.TypeId = (int)Reader["typeId"];
                    checkOrder.Quantity = (int)Reader["Quantity"];
                    checkOrder.ItemName = Reader["ItemName"].ToString();
                    checkOrder.Name = Reader["EmployeeName"].ToString();
                    checkOrder.EmpId = Reader["EmpId"].ToString();
                    checkOrder.Date = Reader["Date"].ToString();
                    checkOrder.Division = Reader["DivisionName"].ToString();
                    checkOrder.Department = Reader["DepartmentName"].ToString();
                    checkOrder.Designation = Reader["DesignationName"].ToString();
                    checkOrder.Status = Reader["Status"].ToString();
                    checkOrder.CustomMenu = Reader["CustomMenu"].ToString();

                    if (checkOrder.CustomMenu == "")
                    {
                        checkOrder.CustomMenu = "Regular item";
                    }
                    checkOrders.Add(checkOrder);

                }
                Reader.Close();
                return checkOrders;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                 Connection.Close();
            }

        }

        public List<CheckOrder> GetDepartmentRequest()
        {
            try
            {
                Query =
                    @"SELECT s.Id, s.DptId, s.Status,s.Quantity, s.Date,s.TypeId,s.CustomMenu, t.Name as ItemName,p.Name as DepartmentName, d.Name as DivisionName
FROM DepartmentSendOrder s
INNER JOIN Department p on p.Id = s.DptId
INNER JOIN Division d on d.Id = p.DivisionId
INNER JOIN FoodItemCost t on t.Id = s.ItemId
Where s.Status = '" + "Submit" + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<CheckOrder> checkOrders = new List<CheckOrder>();
                while (Reader.Read())
                {
                    CheckOrder checkOrder = new CheckOrder();
                    checkOrder.Id = (int)Reader["Id"];
                    checkOrder.TypeId = (int)Reader["typeId"];
                    checkOrder.Quantity = (int)Reader["Quantity"];
                    checkOrder.ItemName = Reader["ItemName"].ToString();
                    checkOrder.EmpId = Reader["DptId"].ToString();
                    checkOrder.Date = Reader["Date"].ToString();
                    checkOrder.Division = Reader["DivisionName"].ToString();
                    checkOrder.Department = Reader["DepartmentName"].ToString();
                    checkOrder.Status = Reader["Status"].ToString();
                    checkOrder.CustomMenu = Reader["CustomMenu"].ToString();
                    if (checkOrder.CustomMenu == "")
                    {
                        checkOrder.CustomMenu = "Regular item";
                    }
                    checkOrders.Add(checkOrder);

                }
                Reader.Close();
                return checkOrders;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }

        }

        public int ConfirmCheckNeed(int id)
        {
           

            try
            {
                Query = @"SELECT *FROM SendOrder Where Id = '"+id+"'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
               CheckOrder checkOrders = new CheckOrder();
                while (Reader.Read())
                {
                    CheckOrder checkOrder = new CheckOrder();
                    checkOrder.Id = (int)Reader["Id"];
                    checkOrder.Date = Reader["Date"].ToString();
                    checkOrder.Quantity = (int) Reader["Quantity"];
                    checkOrder.EmpId = Reader["EmpId"].ToString();
                    checkOrder.ItemId = (int)Reader["ItemId"];
                    checkOrder.TypeId = (int)Reader["TypeId"];

                    checkOrders = checkOrder;

                }
                Reader.Close();
                Connection.Close();//data collect

                if (checkOrders.TypeId == 1)
                {
                    for (int i = 1; i <= checkOrders.Quantity; i++)
                    {
                        int ItemQuentity = 1;
                        string CheckTime = DateTime.Now.TimeOfDay.ToString();
                        int UnitRate = 0;
                        Query = @"INSERT INTO EmployeeCafeteriaTransaction(EmpId,FoodItemCostId,ItemQuentity,CheckTime,CheckDate,UnitRate) VALUES(@EmpId, @ItemId, @ItemQuentity, @CheckTime, @Date, @UnitRate )";
                        Command.CommandText = Query;
                        Command.Connection = Connection;
                        Command.Parameters.Clear();
                        Command.Parameters.Add("EmpId", SqlDbType.VarChar);
                        Command.Parameters["EmpId"].Value = checkOrders.EmpId;
                        Command.Parameters.Add("ItemId", SqlDbType.Int);
                        Command.Parameters["ItemId"].Value = checkOrders.ItemId; ;
                        Command.Parameters.Add("ItemQuentity", SqlDbType.Int);
                        Command.Parameters["ItemQuentity"].Value = ItemQuentity;
                        Command.Parameters.Add("CheckTime", SqlDbType.Time);
                        Command.Parameters["CheckTime"].Value = CheckTime;
                        Command.Parameters.Add("Date", SqlDbType.Date);
                        Command.Parameters["Date"].Value = checkOrders.Date; ; ;
                        Command.Parameters.Add("UnitRate", SqlDbType.Int);
                        Command.Parameters["UnitRate"].Value = UnitRate;

                        Connection.Open();
                        int rowsAffected = Command.ExecuteNonQuery();
                        Connection.Close();
                    }
                }
                if (checkOrders.TypeId == 2)
                {
                    int ItemQuentity = 1;
                    string CheckTime = DateTime.Now.TimeOfDay.ToString();
                    int UnitRate = 0;
                    Query = @"INSERT INTO DepartmentCafeteriaTransaction(DeptId,FoodItemCostId,ItemQuantity,CheckTime,CheckDate) VALUES(@EmpId, @ItemId, @ItemQuentity, @CheckTime, @Date )";
                    Command.CommandText = Query;
                    Command.Connection = Connection;
                    Command.Parameters.Clear();
                    Command.Parameters.Add("EmpId", SqlDbType.VarChar);
                    Command.Parameters["EmpId"].Value = checkOrders.EmpId;
                    Command.Parameters.Add("ItemId", SqlDbType.Int);
                    Command.Parameters["ItemId"].Value = checkOrders.ItemId; ;
                    Command.Parameters.Add("ItemQuentity", SqlDbType.Int);
                    Command.Parameters["ItemQuentity"].Value = ItemQuentity;
                    Command.Parameters.Add("CheckTime", SqlDbType.Time);
                    Command.Parameters["CheckTime"].Value = CheckTime;
                    Command.Parameters.Add("Date", SqlDbType.Int);
                    Command.Parameters["Date"].Value = checkOrders.Date; ; ;
                    //Command.Parameters.Add("UnitRate", SqlDbType.Int);
                    //Command.Parameters["UnitRate"].Value = UnitRate;

                    Connection.Open();
                    int rowsAffected = Command.ExecuteNonQuery();
                    Connection.Close();
                }

               


                string Status = "Confirm";
                string query = @"UPDATE SendOrder
   SET Status = '" + Status + "' WHERE Id = '" + id + "'";
                SqlCommand command = new SqlCommand(query, Connection);
                Connection.Open();
                int rowAffected = command.ExecuteNonQuery();
                Connection.Close();//update order table


           
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }
            return 0;
        }

        public int ConfirmCheckNeed1(int id)
        {


            try
            {
                Query = @"SELECT *FROM DepartmentSendOrder Where Id = '" + id + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                CheckOrder checkOrders = new CheckOrder();
                while (Reader.Read())
                {
                    CheckOrder checkOrder = new CheckOrder();
                    checkOrder.Id = (int)Reader["Id"];
                    checkOrder.Quantity = (int)Reader["Quantity"];
                    checkOrder.Date = Reader["Date"].ToString();
                    checkOrder.DptId = (int) Reader["DptId"];
                    checkOrder.ItemId = (int)Reader["ItemId"];
                    checkOrder.TypeId = (int)Reader["TypeId"];

                    checkOrders = checkOrder;

                }
                Reader.Close();
                Connection.Close();//data collect

                if (checkOrders.TypeId == 1)
                {
                    int ItemQuentity = 1;
                    string CheckTime = DateTime.Now.TimeOfDay.ToString();
                    int UnitRate = 0;
                    Query = @"INSERT INTO EmployeeCafeteriaTransaction(EmpId,FoodItemCostId,ItemQuentity,CheckTime,CheckDate,UnitRate) VALUES(@EmpId, @ItemId, @ItemQuentity, @CheckTime, @Date, @UnitRate )";
                    Command.CommandText = Query;
                    Command.Connection = Connection;
                    Command.Parameters.Clear();
                    Command.Parameters.Add("EmpId", SqlDbType.VarChar);
                    Command.Parameters["EmpId"].Value = checkOrders.EmpId;
                    Command.Parameters.Add("ItemId", SqlDbType.Int);
                    Command.Parameters["ItemId"].Value = checkOrders.ItemId; ;
                    Command.Parameters.Add("ItemQuentity", SqlDbType.Int);
                    Command.Parameters["ItemQuentity"].Value = ItemQuentity;
                    Command.Parameters.Add("CheckTime", SqlDbType.Time);
                    Command.Parameters["CheckTime"].Value = CheckTime;
                    Command.Parameters.Add("Date", SqlDbType.Date);
                    Command.Parameters["Date"].Value = checkOrders.Date; ; ;
                    Command.Parameters.Add("UnitRate", SqlDbType.Int);
                    Command.Parameters["UnitRate"].Value = UnitRate;

                    Connection.Open();
                    int rowsAffected = Command.ExecuteNonQuery();
                    Connection.Close();
                }
                if (checkOrders.TypeId == 2)
                {
                    for (int i = 1; i <= checkOrders.Quantity; i++)
                    {
                        int ItemQuentity = 1;
                        string CheckTime = DateTime.Now.TimeOfDay.ToString();
                        int UnitRate = 0;
                        Query = @"INSERT INTO DepartmentCafeteriaTransaction(DeptId,FoodItemCostId,ItemQuantity,CheckTime,CheckDate,UnitRate) VALUES(@DptId, @ItemId, @ItemQuentity, @CheckTime, @Date, @UnitRate )";
                        Command.CommandText = Query;
                        Command.Connection = Connection;
                        Command.Parameters.Clear();
                        Command.Parameters.Add("DptId", SqlDbType.VarChar);
                        Command.Parameters["DptId"].Value = checkOrders.DptId;
                        Command.Parameters.Add("ItemId", SqlDbType.Int);
                        Command.Parameters["ItemId"].Value = checkOrders.ItemId; ;
                        Command.Parameters.Add("ItemQuentity", SqlDbType.Int);
                        Command.Parameters["ItemQuentity"].Value = ItemQuentity;
                        Command.Parameters.Add("CheckTime", SqlDbType.Time);
                        Command.Parameters["CheckTime"].Value = CheckTime;
                        Command.Parameters.Add("Date", SqlDbType.Date);
                        Command.Parameters["Date"].Value = checkOrders.Date; ; ;
                        Command.Parameters.Add("UnitRate", SqlDbType.Int);
                        Command.Parameters["UnitRate"].Value = UnitRate;

                        Connection.Open();
                        int rowsAffected = Command.ExecuteNonQuery();
                        Connection.Close();
                    }
                }




                string Status = "Confirm";
                string query = @"UPDATE DepartmentSendOrder
   SET Status = '" + Status + "' WHERE Id = '" + id + "'";
                SqlCommand command = new SqlCommand(query, Connection);
                Connection.Open();
                int rowAffected = command.ExecuteNonQuery();
                Connection.Close();//update order table



            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }
            return 0;
        }
        public int RejectCheckNeed(int id)
        {
            string Status = "Reject";
            string query = @"UPDATE SendOrder
   SET Status = '" + Status + "' WHERE Id = '" + id + "'";

            try
            {
                SqlCommand command = new SqlCommand(query, Connection);
                Connection.Open();
                int rowAffected = command.ExecuteNonQuery();

                return rowAffected;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }

        }

        public int RejectCheckNeed1(int id)
        {
            string Status = "Reject";
            string query = @"UPDATE DepartmentSendOrder
   SET Status = '" + Status + "' WHERE Id = '" + id + "'";

            try
            {
                SqlCommand command = new SqlCommand(query, Connection);
                Connection.Open();
                int rowAffected = command.ExecuteNonQuery();

                return rowAffected;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }

        }

        public List<ShowNeed> GetTotalUser()
        {
            int totaluser = 0;
            string lunchRequest1 = null;
            string lunchRequest2 = null;
            int lunchRequest = 0;

            string breakfastRequest1 = null;
            string breakfastRequest2 = null;
            int dinnerRequest = 0;

            string dinnerRequest1 = null;
            string dinnerRequest2 = null;
            int breakfastRequest = 0;

            string lateNightDinner1 = null;
            string lateNightDinner2 = null;
            int lateNightDinner = 0;
            try
            {
                Query =
                    @"SELECT COUNT(Id) as Id FROM Employee Where Status = '" + 1 + "' ";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    totaluser = (int)Reader["Id"];

                }
                Reader.Close();
                Connection.Close(); 
                //collect lunchRequest
                Query =
                    @"SELECT Sum(Quantity) as Id FROM SendOrder Where Date = '" + DateTime.Now + "' and Status = '" +
                    "Confirm" + "' and ItemId = '" + 6 + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        lunchRequest1 = Reader["Id"].ToString();
                    }
                    Reader.Close();
                }
              
                Connection.Close();
                //..............
                Query =
                  @"SELECT Sum(Quantity) as Id FROM DepartmentSendOrder Where Date = '" + DateTime.Now + "' and Status = '" +
                  "Confirm" + "' and ItemId = '" + 6 + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        lunchRequest2 = Reader["Id"].ToString();
                    }
                    Reader.Close();
                }
                Connection.Close();
                if (lunchRequest1 != "" && lunchRequest2 != "") 
                {
                    lunchRequest = Convert.ToInt32(lunchRequest1) + Convert.ToInt32(lunchRequest2);
                }
                if (lunchRequest1 != "" && lunchRequest2 == "")
                {
                    lunchRequest = Convert.ToInt32(lunchRequest1);
                }
                if (lunchRequest1 == "" && lunchRequest2 != "")
                {
                    lunchRequest = Convert.ToInt32(lunchRequest2);
                }
                if (lunchRequest1 == "" && lunchRequest2 == "")
                {
                    lunchRequest = 0;
                }
               
                //collect dinnerRequest
                Query =
                    @"SELECT Sum(Quantity) as Id FROM SendOrder Where Date = '" + DateTime.Now + "' and Status = '" +
                    "Confirm" + "' and ItemId = '" + 7 + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {

                    dinnerRequest1 = Reader["Id"].ToString();

                }
                Reader.Close();
                Connection.Close(); 

                //..................
                Query =
                 @"SELECT Sum(Quantity) as Id FROM DepartmentSendOrder Where Date = '" + DateTime.Now + "' and Status = '" +
                 "Confirm" + "' and ItemId = '" + 7 + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        dinnerRequest2 = Reader["Id"].ToString();
                    }
                    Reader.Close();
                }
                Connection.Close();
                if (dinnerRequest1 != "" && dinnerRequest2 != "")
                {
                    dinnerRequest = Convert.ToInt32(dinnerRequest1) + Convert.ToInt32(dinnerRequest2);
                }
                if (dinnerRequest1 != "" && dinnerRequest2 == "")
                {
                    dinnerRequest = Convert.ToInt32(dinnerRequest1);
                }
                if (dinnerRequest1 == "" && dinnerRequest2 != "")
                {
                    dinnerRequest = Convert.ToInt32(dinnerRequest2);
                }
                if (dinnerRequest1 == "" && dinnerRequest2 == "")
                {
                    dinnerRequest = 0;
                }

                //collect breakfastRequest
                Query =
                    @"SELECT Sum(Quantity) as Id FROM SendOrder Where Date = '" + DateTime.Now + "' and Status = '" +
                    "Confirm" + "' and ItemId = '" + 8 + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        breakfastRequest1 = Reader["Id"].ToString();
                    }
                    Reader.Close();
                }
                Connection.Close(); 
                //.....................
                Query =
 @"SELECT Sum(Quantity) as Id FROM DepartmentSendOrder Where Date = '" + DateTime.Now + "' and Status = '" +
 "Confirm" + "' and ItemId = '" + 8 + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        breakfastRequest2 = Reader["Id"].ToString();
                    }
                    Reader.Close();
                }
                Connection.Close();
                if (breakfastRequest1 != "" && breakfastRequest2 != "")
                {
                    breakfastRequest = Convert.ToInt32(breakfastRequest1) + Convert.ToInt32(breakfastRequest2);
                }
                if (breakfastRequest1 != "" && breakfastRequest2 == "")
                {
                    breakfastRequest = Convert.ToInt32(breakfastRequest1);
                }
                if (breakfastRequest1 == "" && breakfastRequest2 != "")
                {
                    breakfastRequest = Convert.ToInt32(breakfastRequest2);
                }
                if (breakfastRequest1 == "" && breakfastRequest2 == "")
                {
                    breakfastRequest = 0;
                }


                //collect lateNightDinner
                Query =
                    @"SELECT Sum(Quantity) as Id FROM SendOrder Where Date = '" + DateTime.Now + "' and Status = '" +
                    "Confirm" + "' and ItemId = '" + 9 + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        lateNightDinner1 = Reader["Id"].ToString();
                    }
                    Reader.Close();
                }
                Connection.Close(); 

                //.....................
                Query =
@"SELECT Sum(Quantity) as Id FROM DepartmentSendOrder Where Date = '" + DateTime.Now + "' and Status = '" +
"Confirm" + "' and ItemId = '" + 9 + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        lateNightDinner2 = Reader["Id"].ToString();
                    }
                    Reader.Close();
                }
                Connection.Close();
                if (lateNightDinner1 != "" && lateNightDinner2 != "")
                {
                    lateNightDinner = Convert.ToInt32(lateNightDinner1) + Convert.ToInt32(lateNightDinner2);
                }
                if (lateNightDinner1 != "" && lateNightDinner2 == "")
                {
                    lateNightDinner = Convert.ToInt32(lateNightDinner1);
                }
                if (lateNightDinner1 == "" && lateNightDinner2 != "")
                {
                    lateNightDinner = Convert.ToInt32(lateNightDinner2);
                }
                if (lateNightDinner1 == "" && lateNightDinner2 == "")
                {
                    lateNightDinner = 0;
                }


                int lunch = lunchRequest;
                int dinner = dinnerRequest;
                int breakfast = breakfastRequest;
                int lateNightDinners = lateNightDinner;
                //List<ShowNeed> showNeeds = new List<ShowNeed>();
                //ShowNeed showNeed = new ShowNeed();

                //showNeed.TotalUser = totaluser.ToString();
                //showNeed.LunchRequest = lunch.ToString();
                //showNeed.DinnerRequest = dinner.ToString();
                //showNeed.BreakfastRequest = breakfast.ToString();
                //showNeed.LateNightDinner = lateNightDinners.ToString();
                //showNeeds.Add(showNeed);


                string date = DateTime.Now.ToString("yyyy-MM-dd");

                Query = @"SELECT COUNT(s.Id) as TotalLunch
FROM MealSchedule s
Where ItemId = '" + 6 + "' AND Status = '" + "Pending" + "' AND Date = '" + date + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        lunch += (int)Reader["TotalLunch"];
                    }
                    Reader.Close();
                    Connection.Close();
                }

                Query = @"SELECT COUNT(s.Id) as TotalDinner
FROM MealSchedule s
Where ItemId = '" + 7 + "' AND Status = '" + "Pending" + "' AND Date = '" + date + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        dinner += (int)Reader["TotalDinner"];
                    }
                    Reader.Close();
                    Connection.Close();
                }

                Query = @"SELECT COUNT(s.Id) as TotalBreakfast
FROM MealSchedule s
Where ItemId = '" + 8 + "' AND Status = '" + "Pending" + "' AND Date = '" + date + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        breakfast += (int)Reader["TotalBreakfast"];
                    }
                    Reader.Close();
                    Connection.Close();
                }

                Query = @"SELECT COUNT(s.Id) as TotalSehri
FROM MealSchedule s
Where ItemId = '" + 9 + "' AND Status = '" + "Pending" + "' AND Date = '" + date + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        lateNightDinner += (int)Reader["TotalSehri"];
                    }
                    Reader.Close();
                    Connection.Close();
                }
                List<ShowNeed> showNeeds = new List<ShowNeed>();
                ShowNeed showNeed = new ShowNeed();

                showNeed.TotalUser = totaluser.ToString();
                showNeed.LunchRequest = lunch.ToString();
                showNeed.DinnerRequest = dinner.ToString();
                showNeed.BreakfastRequest = breakfast.ToString();
                showNeed.LateNightDinner = lateNightDinners.ToString();
                showNeeds.Add(showNeed);
                return showNeeds;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }

        }

        public List<Employee> AutoSearchDepartment(string name)
        {
            try
            {
                Query =
                    @"SELECT Id, Name FROM Department
WHERE Name LIKE '%" + name + "%'";
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

        public int SendOrderSave(int type, string CheckDate, string name, int breakfast, int lunch, int dinner, int sehuri, int snacks, int tea, int quantity, string menu, int menu1)
        {
            int unitRate = 0;
            string status = "Submit";
           // string submiteTime = DateTime.Now.TimeOfDay.ToString();
           // DateTime submiteTime = DateTime.Now;
                    
                        if (breakfast != 0)
                        {

                            Query = @"INSERT INTO SendOrder(Date,EmpId,ItemId,Status,TypeId, quantity) VALUES(@CheckDate, @name, @breakfast, @Status, @type, @quantity )";
                            Command.CommandText = Query;
                            Command.Connection = Connection;
                            Command.Parameters.Clear();
                            Command.Parameters.Add("name", SqlDbType.VarChar);
                            Command.Parameters["name"].Value = name;
                            Command.Parameters.Add("breakfast", SqlDbType.Int);
                            Command.Parameters["breakfast"].Value = breakfast;
                            Command.Parameters.Add("Status", SqlDbType.VarChar);
                            Command.Parameters["Status"].Value = status;
                            Command.Parameters.Add("type", SqlDbType.Int);
                            Command.Parameters["type"].Value = type;
                            Command.Parameters.Add("CheckDate", SqlDbType.Date);
                            Command.Parameters["CheckDate"].Value = CheckDate;
                            Command.Parameters.Add("Quantity", SqlDbType.Int);
                            Command.Parameters["Quantity"].Value = quantity;
                          
                            Connection.Open();
                            int rowsAffected = Command.ExecuteNonQuery();
                            Connection.Close();

                        }
                  
                    if (lunch != 0)
                    {

                        Query = @"INSERT INTO SendOrder(Date,EmpId,ItemId,Status,TypeId, quantity) VALUES(@CheckDate, @name, @lunch, @Status, @type, @quantity )";
                        Command.CommandText = Query;
                        Command.Connection = Connection;
                        Command.Parameters.Clear();
                        Command.Parameters.Add("name", SqlDbType.VarChar);
                        Command.Parameters["name"].Value = name;
                        Command.Parameters.Add("lunch", SqlDbType.Int);
                        Command.Parameters["lunch"].Value = lunch;
                        Command.Parameters.Add("Status", SqlDbType.VarChar);
                        Command.Parameters["Status"].Value = status;
                        Command.Parameters.Add("type", SqlDbType.Int);
                        Command.Parameters["type"].Value = type;
                        Command.Parameters.Add("CheckDate", SqlDbType.Date);
                        Command.Parameters["CheckDate"].Value = CheckDate;
                        Command.Parameters.Add("Quantity", SqlDbType.Int);
                        Command.Parameters["Quantity"].Value = quantity;
                    
                        Connection.Open();
                        int rowsAffected = Command.ExecuteNonQuery();
                        Connection.Close();
                    }
                    if (dinner != 0)
                    {

                        Query = @"INSERT INTO SendOrder(Date,EmpId,ItemId,Status,TypeId, quantity) VALUES(@CheckDate, @name, @dinner, @Status, @type, @quantity )";
                        Command.CommandText = Query;
                        Command.Connection = Connection;
                        Command.Parameters.Clear();
                        Command.Parameters.Add("name", SqlDbType.VarChar);
                        Command.Parameters["name"].Value = name;
                        Command.Parameters.Add("dinner", SqlDbType.Int);
                        Command.Parameters["dinner"].Value = dinner;
                        Command.Parameters.Add("Status", SqlDbType.VarChar);
                        Command.Parameters["Status"].Value = status;
                        Command.Parameters.Add("type", SqlDbType.Int);
                        Command.Parameters["type"].Value = type;
                        Command.Parameters.Add("CheckDate", SqlDbType.Date);
                        Command.Parameters["CheckDate"].Value = CheckDate;
                        Command.Parameters.Add("Quantity", SqlDbType.Int);
                        Command.Parameters["Quantity"].Value = quantity;
                    
                        Connection.Open();
                        int rowsAffected = Command.ExecuteNonQuery();
                        Connection.Close();
                    }
                    if (sehuri != 0)
                    {

                        Query = @"INSERT INTO SendOrder(Date,EmpId,ItemId,Status,TypeId, quantity) VALUES(@CheckDate, @name, @sehuri, @Status, @type, @quantity )";
                        Command.CommandText = Query;
                        Command.Connection = Connection;
                        Command.Parameters.Clear();
                        Command.Parameters.Add("name", SqlDbType.VarChar);
                        Command.Parameters["name"].Value = name;
                        Command.Parameters.Add("sehuri", SqlDbType.Int);
                        Command.Parameters["sehuri"].Value = sehuri;
                        Command.Parameters.Add("Status", SqlDbType.VarChar);
                        Command.Parameters["Status"].Value = status;
                        Command.Parameters.Add("type", SqlDbType.Int);
                        Command.Parameters["type"].Value = type;
                        Command.Parameters.Add("CheckDate", SqlDbType.Date);
                        Command.Parameters["CheckDate"].Value = CheckDate;
                        Command.Parameters.Add("Quantity", SqlDbType.Int);
                        Command.Parameters["Quantity"].Value = quantity;
                   
                        Connection.Open();
                        int rowsAffected = Command.ExecuteNonQuery();
                        Connection.Close();
                    }
                    if (snacks != 0)
                    {

                        Query = @"INSERT INTO SendOrder(Date,EmpId,ItemId,Status,TypeId, quantity) VALUES(@CheckDate, @name, @snacks, @Status, @type, @quantity )";
                        Command.CommandText = Query;
                        Command.Connection = Connection;
                        Command.Parameters.Clear();
                        Command.Parameters.Add("name", SqlDbType.VarChar);
                        Command.Parameters["name"].Value = name;
                        Command.Parameters.Add("snacks", SqlDbType.Int);
                        Command.Parameters["snacks"].Value = snacks;
                        Command.Parameters.Add("Status", SqlDbType.VarChar);
                        Command.Parameters["Status"].Value = status;
                        Command.Parameters.Add("type", SqlDbType.Int);
                        Command.Parameters["type"].Value = type;
                        Command.Parameters.Add("CheckDate", SqlDbType.Date);
                        Command.Parameters["CheckDate"].Value = CheckDate;
                        Command.Parameters.Add("Quantity", SqlDbType.Int);
                        Command.Parameters["Quantity"].Value = quantity;
                
                        Connection.Open();
                        int rowsAffected = Command.ExecuteNonQuery();
                        Connection.Close();
                    }
                    if (tea != 0)
                    {

                        Query = @"INSERT INTO SendOrder(Date,EmpId,ItemId,Status,TypeId, quantity) VALUES(@CheckDate, @name, @tea, @Status, @type, @quantity )";
                        Command.CommandText = Query;
                        Command.Connection = Connection;
                        Command.Parameters.Clear();
                        Command.Parameters.Add("name", SqlDbType.VarChar);
                        Command.Parameters["name"].Value = name;
                        Command.Parameters.Add("tea", SqlDbType.Int);
                        Command.Parameters["tea"].Value = tea;
                        Command.Parameters.Add("Status", SqlDbType.VarChar);
                        Command.Parameters["Status"].Value = status;
                        Command.Parameters.Add("type", SqlDbType.Int);
                        Command.Parameters["type"].Value = type;
                        Command.Parameters.Add("CheckDate", SqlDbType.Date);
                        Command.Parameters["CheckDate"].Value = CheckDate;
                        Command.Parameters.Add("Quantity", SqlDbType.Int);
                        Command.Parameters["Quantity"].Value = quantity;
               
                        Connection.Open();
                        int rowsAffected = Command.ExecuteNonQuery();
                        Connection.Close();
                    }
                    if (menu != null)
                    {

                        Query = @"INSERT INTO SendOrder(Date,EmpId,ItemId,Status,TypeId, quantity, CustomMenu) VALUES(@CheckDate, @name, @menu1, @Status, @type, @quantity, @menu )";
                        Command.CommandText = Query;
                        Command.Connection = Connection;
                        Command.Parameters.Clear();
                        Command.Parameters.Add("name", SqlDbType.VarChar);
                        Command.Parameters["name"].Value = name;
                        Command.Parameters.Add("menu1", SqlDbType.Int);
                        Command.Parameters["menu1"].Value = menu1;
                        Command.Parameters.Add("Status", SqlDbType.VarChar);
                        Command.Parameters["Status"].Value = status;
                        Command.Parameters.Add("type", SqlDbType.Int);
                        Command.Parameters["type"].Value = type;
                        Command.Parameters.Add("CheckDate", SqlDbType.Date);
                        Command.Parameters["CheckDate"].Value = CheckDate;
                        Command.Parameters.Add("Quantity", SqlDbType.Int);
                        Command.Parameters["Quantity"].Value = quantity;
                        Command.Parameters.Add("menu", SqlDbType.VarChar);
                        Command.Parameters["menu"].Value = menu;

                        Connection.Open();
                        int rowsAffected = Command.ExecuteNonQuery();
                        Connection.Close();
                    }

                return 0;

            }

        public int DepartmentSendOrderSave(int type, string CheckDate, int breakfast, int lunch, int dinner, int sehuri, int snacks, int tea, string Id, string name, int quantity, string menu, int menu1)
        {
            
            int unitRate = 0;
            string status = "Submit";

                if (breakfast != 0)
                {

                    Query = @"INSERT INTO DepartmentSendOrder(Date,DptId,ItemId,Status,TypeId, Quantity, EmpId) VALUES(@CheckDate, @Id, @breakfast, @Status, @type, @quantity, @name )";
                    Command.CommandText = Query;
                    Command.Connection = Connection;
                    Command.Parameters.Clear();
                    Command.Parameters.Add("Id", SqlDbType.Int);
                    Command.Parameters["Id"].Value = Id;
                    Command.Parameters.Add("breakfast", SqlDbType.Int);
                    Command.Parameters["breakfast"].Value = breakfast;
                    Command.Parameters.Add("Status", SqlDbType.VarChar);
                    Command.Parameters["Status"].Value = status;
                    Command.Parameters.Add("type", SqlDbType.Int);
                    Command.Parameters["type"].Value = type;
                    Command.Parameters.Add("CheckDate", SqlDbType.Date);
                    Command.Parameters["CheckDate"].Value = CheckDate;
                    Command.Parameters.Add("quantity", SqlDbType.Int);
                    Command.Parameters["quantity"].Value = quantity;
                    Command.Parameters.Add("name", SqlDbType.VarChar);
                    Command.Parameters["name"].Value = name;

                    Connection.Open();
                    int rowsAffected = Command.ExecuteNonQuery();
                    Connection.Close();

                }
                if (lunch != 0)
                {


                    Query = @"INSERT INTO DepartmentSendOrder(Date,DptId,ItemId,Status,TypeId, Quantity, EmpId) VALUES(@CheckDate, @Id, @lunch, @Status, @type, @quantity, @name )";
                    Command.CommandText = Query;
                    Command.Connection = Connection;
                    Command.Parameters.Clear();
                    Command.Parameters.Add("Id", SqlDbType.Int);
                    Command.Parameters["Id"].Value = Id;
                    Command.Parameters.Add("lunch", SqlDbType.Int);
                    Command.Parameters["lunch"].Value = lunch;
                    Command.Parameters.Add("Status", SqlDbType.VarChar);
                    Command.Parameters["Status"].Value = status;
                    Command.Parameters.Add("type", SqlDbType.Int);
                    Command.Parameters["type"].Value = type;
                    Command.Parameters.Add("CheckDate", SqlDbType.Date);
                    Command.Parameters["CheckDate"].Value = CheckDate;
                    Command.Parameters.Add("quantity", SqlDbType.Int);
                    Command.Parameters["quantity"].Value = quantity;
                    Command.Parameters.Add("name", SqlDbType.VarChar);
                    Command.Parameters["name"].Value = name;

                    Connection.Open();
                    int rowsAffected = Command.ExecuteNonQuery();
                    Connection.Close();
                }
                if (dinner != 0)
                {


                    Query = @"INSERT INTO DepartmentSendOrder(Date,DptId,ItemId,Status,TypeId, Quantity, EmpId) VALUES(@CheckDate, @Id, @dinner, @Status, @type, @quantity, @name )";
                    Command.CommandText = Query;
                    Command.Connection = Connection;
                    Command.Parameters.Clear();
                    Command.Parameters.Add("Id", SqlDbType.Int);
                    Command.Parameters["Id"].Value = Id;
                    Command.Parameters.Add("dinner", SqlDbType.Int);
                    Command.Parameters["dinner"].Value = dinner;
                    Command.Parameters.Add("Status", SqlDbType.VarChar);
                    Command.Parameters["Status"].Value = status;
                    Command.Parameters.Add("type", SqlDbType.Int);
                    Command.Parameters["type"].Value = type;
                    Command.Parameters.Add("CheckDate", SqlDbType.Date);
                    Command.Parameters["CheckDate"].Value = CheckDate;
                    Command.Parameters.Add("quantity", SqlDbType.Int);
                    Command.Parameters["quantity"].Value = quantity;
                    Command.Parameters.Add("name", SqlDbType.VarChar);
                    Command.Parameters["name"].Value = name;

                    Connection.Open();
                    int rowsAffected = Command.ExecuteNonQuery();
                    Connection.Close();
                }
                if (sehuri != 0)
                {


                    Query = @"INSERT INTO DepartmentSendOrder(Date,DptId,ItemId,Status,TypeId, Quantity, EmpId) VALUES(@CheckDate, @Id, @sehuri, @Status, @type, @quantity, @name )";
                    Command.CommandText = Query;
                    Command.Connection = Connection;
                    Command.Parameters.Clear();
                    Command.Parameters.Add("Id", SqlDbType.Int);
                    Command.Parameters["Id"].Value = Id;
                    Command.Parameters.Add("sehuri", SqlDbType.Int);
                    Command.Parameters["sehuri"].Value = sehuri;
                    Command.Parameters.Add("Status", SqlDbType.VarChar);
                    Command.Parameters["Status"].Value = status;
                    Command.Parameters.Add("type", SqlDbType.Int);
                    Command.Parameters["type"].Value = type;
                    Command.Parameters.Add("CheckDate", SqlDbType.Date);
                    Command.Parameters["CheckDate"].Value = CheckDate;
                    Command.Parameters.Add("quantity", SqlDbType.Int);
                    Command.Parameters["quantity"].Value = quantity;
                    Command.Parameters.Add("name", SqlDbType.VarChar);
                    Command.Parameters["name"].Value = name;

                    Connection.Open();
                    int rowsAffected = Command.ExecuteNonQuery();
                    Connection.Close();
                }
                if (snacks != 0)
                {


                    Query = @"INSERT INTO DepartmentSendOrder(Date,DptId,ItemId,Status,TypeId, Quantity, EmpId) VALUES(@CheckDate, @Id, @snacks, @Status, @type, @quantity, @name )";
                    Command.CommandText = Query;
                    Command.Connection = Connection;
                    Command.Parameters.Clear();
                    Command.Parameters.Add("Id", SqlDbType.Int);
                    Command.Parameters["Id"].Value = Id;
                    Command.Parameters.Add("snacks", SqlDbType.Int);
                    Command.Parameters["snacks"].Value = snacks;
                    Command.Parameters.Add("Status", SqlDbType.VarChar);
                    Command.Parameters["Status"].Value = status;
                    Command.Parameters.Add("type", SqlDbType.Int);
                    Command.Parameters["type"].Value = type;
                    Command.Parameters.Add("CheckDate", SqlDbType.Date);
                    Command.Parameters["CheckDate"].Value = CheckDate;
                    Command.Parameters.Add("quantity", SqlDbType.Int);
                    Command.Parameters["quantity"].Value = quantity;
                    Command.Parameters.Add("name", SqlDbType.VarChar);
                    Command.Parameters["name"].Value = name;

                    Connection.Open();
                    int rowsAffected = Command.ExecuteNonQuery();
                    Connection.Close();
                }
                if (tea != 0)
                {


                    Query = @"INSERT INTO DepartmentSendOrder(Date,DptId,ItemId,Status,TypeId, Quantity, EmpId) VALUES(@CheckDate, @Id, @tea, @Status, @type, @quantity, @name )";
                    Command.CommandText = Query;
                    Command.Connection = Connection;
                    Command.Parameters.Clear();
                    Command.Parameters.Add("Id", SqlDbType.Int);
                    Command.Parameters["Id"].Value = Id;
                    Command.Parameters.Add("tea", SqlDbType.Int);
                    Command.Parameters["tea"].Value = tea;
                    Command.Parameters.Add("Status", SqlDbType.VarChar);
                    Command.Parameters["Status"].Value = status;
                    Command.Parameters.Add("type", SqlDbType.Int);
                    Command.Parameters["type"].Value = type;
                    Command.Parameters.Add("CheckDate", SqlDbType.Date);
                    Command.Parameters["CheckDate"].Value = CheckDate;
                    Command.Parameters.Add("quantity", SqlDbType.Int);
                    Command.Parameters["quantity"].Value = quantity;
                    Command.Parameters.Add("name", SqlDbType.VarChar);
                    Command.Parameters["name"].Value = name;

                    Connection.Open();
                    int rowsAffected = Command.ExecuteNonQuery();
                    Connection.Close();
                }
                if (menu != null)
                {


                    Query = @"INSERT INTO DepartmentSendOrder(Date,DptId,ItemId,Status,TypeId, Quantity, EmpId, CustomMenu) VALUES(@CheckDate, @Id, @menu1, @Status, @type, @quantity, @name, @menu )";
                    Command.CommandText = Query;
                    Command.Connection = Connection;
                    Command.Parameters.Clear();
                    Command.Parameters.Add("Id", SqlDbType.Int);
                    Command.Parameters["Id"].Value = Id;
                    Command.Parameters.Add("menu1", SqlDbType.Int);
                    Command.Parameters["menu1"].Value = menu1;
                    Command.Parameters.Add("Status", SqlDbType.VarChar);
                    Command.Parameters["Status"].Value = status;
                    Command.Parameters.Add("type", SqlDbType.Int);
                    Command.Parameters["type"].Value = type;
                    Command.Parameters.Add("CheckDate", SqlDbType.Date);
                    Command.Parameters["CheckDate"].Value = CheckDate;
                    Command.Parameters.Add("quantity", SqlDbType.Int);
                    Command.Parameters["quantity"].Value = quantity;
                    Command.Parameters.Add("name", SqlDbType.VarChar);
                    Command.Parameters["name"].Value = name;
                    Command.Parameters.Add("menu", SqlDbType.VarChar);
                    Command.Parameters["menu"].Value = menu;

                    Connection.Open();
                    int rowsAffected = Command.ExecuteNonQuery();
                    Connection.Close();
                }

            return 0;

        }

        public List<SubmitedApplicationInfo> GetUserEmail(int? id)
        {
           
            try
            {

                Query =
                    @"SELECT s.EmpId, convert(varchar, s.Date, 23) as Date, s.Quantity, e.EmailId, e.Name EmployeeName, f.Name as ItemName
FROM SendOrder s
INNER JOIN Employee e on e.EmpId = s.EmpId
INNER JOIN FoodItemCost f on f.Id = s.ItemId
Where s.Id = '" + id + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<SubmitedApplicationInfo> userInfo = new List<SubmitedApplicationInfo>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        SubmitedApplicationInfo user = new SubmitedApplicationInfo();

                        user.EmpId = Reader["EmpId"].ToString();
                        user.EmployeeName = Reader["EmployeeName"].ToString();
                        user.Email = Reader["EmailId"].ToString();
                        user.Date = Reader["Date"].ToString();
                        user.ItemName = Reader["ItemName"].ToString();
                        user.Quantity = Reader["Quantity"].ToString();

                        userInfo.Add(user);
                    }
                    Reader.Close();
                }
                return userInfo;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }

        }

        public List<SubmitedApplicationInfo> GetUserEmail1(int? id)
        {

            try
            {

                Query =
                    @"SELECT s.EmpId, convert(varchar, s.Date, 23) as Date, s.Quantity, e.EmailId, e.Name EmployeeName, f.Name as ItemName
FROM DepartmentSendOrder s
INNER JOIN Employee e on e.EmpId = s.EmpId
INNER JOIN FoodItemCost f on f.Id = s.ItemId
Where s.Id = '" + id + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<SubmitedApplicationInfo> userInfo = new List<SubmitedApplicationInfo>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        SubmitedApplicationInfo user = new SubmitedApplicationInfo();

                        user.EmpId = Reader["EmpId"].ToString();
                        user.EmployeeName = Reader["EmployeeName"].ToString();
                        user.Email = Reader["EmailId"].ToString();
                        user.Date = Reader["Date"].ToString();
                        user.ItemName = Reader["ItemName"].ToString();
                        user.Quantity = Reader["Quantity"].ToString();

                        userInfo.Add(user);
                    }
                    Reader.Close();
                }
                return userInfo;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                Connection.Close();
            }

        }
    }
}