using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BexMediaCom.Models;

namespace BexMediaCom.Gateway
{
    public class DailyBazarGateway : CommonGateway
    {
        public int SaveBazar(DailyBazar bazar)
        {
            try
            {
                Query = @"INSERT INTO DailyBazar(Date, Amount, ItemId) VALUES(@Date, @Amount, @ItemId )";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("Date", SqlDbType.Date);
                Command.Parameters["Date"].Value = bazar.Date;
                Command.Parameters.Add("Amount", SqlDbType.Decimal);
                Command.Parameters["Amount"].Value = bazar.Amount;
                Command.Parameters.Add("ItemId", SqlDbType.Int);
                Command.Parameters["ItemId"].Value = bazar.ItemId;

                Connection.Open();
                int rowsAffected = Command.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception exception)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
            
        }

        public int UpdateBazar(DailyBazar bazar)
        {
            try
            {
                Query = @"UPDATE DailyBazar
   SET [Amount] = @Amount
 WHERE Date = '" + bazar.Date + "' and ItemId = '" + bazar.ItemId + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("Amount", SqlDbType.Decimal);
                Command.Parameters["Amount"].Value = bazar.Amount;
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

        public bool IsBazarExist(DailyBazar bazar)
        {
            try
            {
                bool isNameExists = false;
                Query = "SELECT*FROM DailyBazar WHERE ItemId = '"+bazar.ItemId+"' and Date='" + bazar.Date + "'";
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

        public DailyBazar GetTotalAmount(string date, int itemId)
        {
            try
            {
                Query = @"SELECT Amount FROM DailyBazar WHERE Date = '" + date + "' and ItemId = '" + itemId + "'";

                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                DailyBazar dailyBazars = null;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        DailyBazar dailyBazar = new DailyBazar();
                        dailyBazar.Amount = Convert.ToDouble(Reader["Amount"]);
                        dailyBazars = dailyBazar;
                    }
                    Reader.Close();
                }
                return dailyBazars;
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

        public DailyBazar GetTotalUser(string date, int itemId)
        {
            try
            {
                Query = @"SELECT SUM(ItemQuentity) as TotalUser
  FROM EmployeeCafeteriaTransaction 
WHERE CheckDate = '" + date + "' and FoodItemCostId = '" + itemId + "'";

                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                DailyBazar dailyBazars = null;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        DailyBazar dailyBazar = new DailyBazar();
                        dailyBazar.TotalUser = (int) (Reader["TotalUser"]);
                        dailyBazars = dailyBazar;
                    }
                    Reader.Close();
                }
                return dailyBazars;
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

        public int UpdateRate(DailyBazar bazar)
        {
            try
            {
                int rowsAffected = 0;
                int rowsAffected1 = 0;

                Query = @"UPDATE EmployeeCafeteriaTransaction
   SET UnitRate = @UnitRate
 WHERE CheckDate = '" + bazar.Date + "' and FoodItemCostId = '" + bazar.ItemId + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("UnitRate", SqlDbType.Decimal);
                Command.Parameters["UnitRate"].Value = bazar.UnitRate;
                Connection.Open();
                rowsAffected = Command.ExecuteNonQuery();
                Connection.Close();

             
                    Query = @"UPDATE DepartmentCafeteriaTransaction
   SET UnitRate = @UnitRate
 WHERE CheckDate = '" + bazar.Date + "' and FoodItemCostId = '" + bazar.ItemId + "'";
                    Command.CommandText = Query;
                    Command.Connection = Connection;
                    Command.Parameters.Clear();
                    Command.Parameters.Add("UnitRate", SqlDbType.Decimal);
                    Command.Parameters["UnitRate"].Value = bazar.UnitRate;
                    Connection.Open();
                     rowsAffected1 = Command.ExecuteNonQuery();

                
                if (rowsAffected > 0 || rowsAffected1 > 0)
                {
                    return 1;
                }
                return 0;
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


        public DailyBazar GetTotalUser1(string date, int itemId)
        {
            try
            {
                Query = @"SELECT SUM(ItemQuantity) as TotalUser
  FROM DepartmentCafeteriaTransaction 
WHERE CheckDate = '" + date + "' and FoodItemCostId = '" + itemId + "'";

                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                DailyBazar dailyBazars = null;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        DailyBazar dailyBazar = new DailyBazar();
                        dailyBazar.TotalDepartmentUser = (int)Reader["TotalUser"];
                        dailyBazars = dailyBazar;
                    }
                    Reader.Close();
                }
                return dailyBazars;
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