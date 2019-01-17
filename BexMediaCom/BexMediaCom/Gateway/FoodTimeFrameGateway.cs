using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BexMediaCom.Models;

namespace BexMediaCom.Gateway
{
    public class FoodTimeFrameGateway : CommonGateway
    {
        public int Save(FoodTimeFrame foodTimeFrame)
        {
            try
            {
                Query = "INSERT INTO CafeTimeSchedule(ItemId,StartTime,EndTime) VALUES(@ItemId,@StartTime,@EndTime)";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("ItemId", SqlDbType.Int);
                Command.Parameters["ItemId"].Value = foodTimeFrame.FoodItemCostId;
                Command.Parameters.Add("StartTime", SqlDbType.Time);
                Command.Parameters["StartTime"].Value = foodTimeFrame.StartTime;
                Command.Parameters.Add("EndTime", SqlDbType.Time);
                Command.Parameters["EndTime"].Value = foodTimeFrame.EndTime;
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

        public FoodTimeFrame GetFoodTimeFrameById(int? id)
        {
            try
            {
                Query = "SELECT*FROM CafeTimeSchedule Where Id = " + id + "";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                FoodTimeFrame foodTime = new FoodTimeFrame();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        FoodTimeFrame food = new FoodTimeFrame();
                        food.Id = Convert.ToInt32(Reader["Id"]);
                        food.FoodItemCostId = (int)Reader["ItemId"];
                        food.StartTime = Reader["StartTime"].ToString();
                        food.EndTime = Reader["EndTime"].ToString();
                        foodTime = food;
                    }
                    Reader.Close();
                }
                return foodTime;
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

        public bool IsNameExists(FoodTimeFrame foodTimeFrame)
        {
            try
            {
                bool isNameExists = false;
                Query = @"SELECT ItemId FROM CafeTimeSchedule
Where StartTime <= '" + foodTimeFrame.StartTime + "' and EndTime >= '" + foodTimeFrame.EndTime + "' and ItemId='" +
                        foodTimeFrame.FoodItemCostId + "' and Id != '" + foodTimeFrame.Id + "'";

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

        public bool IsTimeExists(FoodTimeFrame foodTimeFrame)
        {
            try
            {
                bool isNameExists = false;
                Query = @"SELECT ItemId FROM CafeTimeSchedule
Where StartTime >= '" + foodTimeFrame.StartTime + "' and StartTime <= '" + foodTimeFrame.EndTime + "' OR (EndTime >= '" +
                        foodTimeFrame.EndTime + "' and EndTime <= '" + foodTimeFrame.EndTime + "')";

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

        public int UpdatefoodTimeFrame(FoodTimeFrame foodTimeFrame)
        {
            try
            {
                Query = @"UPDATE CafeTimeSchedule
   SET [ItemId] = @FoodItemCostId
      ,[StartTime] = @StartTime
      ,[EndTime] = @EndTime
 WHERE Id = '" + foodTimeFrame.Id + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("FoodItemCostId", SqlDbType.Int);
                Command.Parameters["FoodItemCostId"].Value = foodTimeFrame.FoodItemCostId;
                Command.Parameters.Add("StartTime", SqlDbType.Time);
                Command.Parameters["StartTime"].Value = foodTimeFrame.StartTime;
                Command.Parameters.Add("EndTime", SqlDbType.VarChar);
                Command.Parameters["EndTime"].Value = foodTimeFrame.EndTime;
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
    }
}