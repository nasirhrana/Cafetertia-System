using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BexMediaCom.Models;

namespace BexMediaCom.Gateway
{
    public class FoodItemCostGateway : CommonGateway
    {
        public int Save(FoodItemCost foodItemCost)
        {
            try
            {
                Query = "INSERT INTO FoodItemCost(Name,Cost,ItemDescription) VALUES(@Name,@Cost,@ItemDescription)";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("Name", SqlDbType.VarChar);
                Command.Parameters["Name"].Value = foodItemCost.Name;
                Command.Parameters.Add("Cost", SqlDbType.Decimal);
                Command.Parameters["Cost"].Value = foodItemCost.Cost;
                Command.Parameters.Add("ItemDescription", SqlDbType.VarChar);
                Command.Parameters["ItemDescription"].Value = foodItemCost.ItemDescription;
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

        public bool IsFoodExists(string name)
        {
            try
            {
                bool isFoodExists = false;
                Query = "SELECT*FROM FoodItemCost WHERE Name='" + name + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    isFoodExists = true;
                }
                return isFoodExists;
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

        public List<FoodItemCost> ShowAllFoodItemCost()
        {
            try
            {
                Query = "SELECT*FROM FoodItemCost";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<FoodItemCost> foodItemCosts = new List<FoodItemCost>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        FoodItemCost foodItemCost = new FoodItemCost();
                        foodItemCost.Id = Convert.ToInt32(Reader["Id"].ToString());
                        foodItemCost.Name = Reader["Name"].ToString();
                        foodItemCost.Cost = Convert.ToDecimal(Reader["Cost"].ToString());
                        foodItemCost.ItemDescription = Reader["ItemDescription"].ToString();
                        foodItemCosts.Add(foodItemCost);
                    }
                    Reader.Close();
                }
                return foodItemCosts;
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

        public List<FoodItemCost> GetAllFoodItemCosts()
        {
            try
            {
                Query = "SELECT*FROM FoodItemCost";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<FoodItemCost> foodItemCosts = new List<FoodItemCost>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        FoodItemCost foodItemCost = new FoodItemCost();
                        foodItemCost.Id = Convert.ToInt32(Reader["Id"].ToString());
                        foodItemCost.Name = Reader["Name"].ToString();
                        foodItemCost.Cost = Convert.ToDecimal(Reader["Cost"].ToString());
                        foodItemCost.ItemDescription = Reader["ItemDescription"].ToString();
                        foodItemCosts.Add(foodItemCost);
                    }
                    Reader.Close();
                }
                return foodItemCosts;
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

        public List<FoodItemCost> GetFoodItemCost()
        {
            try
            {
                Query = "SELECT*FROM FoodItemCost";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<FoodItemCost> foodItemCosts = new List<FoodItemCost>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        FoodItemCost foodItemCost = new FoodItemCost();
                        foodItemCost.Id = Convert.ToInt32(Reader["Id"].ToString());
                        foodItemCost.Name = Reader["Name"].ToString();
                        foodItemCosts.Add(foodItemCost);
                    }
                    Reader.Close();
                }
                return foodItemCosts;
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

        public List<ViewFoodItemTimeSchedule> ViewFoodItemTimeSchedule()
        {
            try
            {
                Query = "SELECT * FROM viewFoodItemTimeSchedules";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<ViewFoodItemTimeSchedule> viewFoodItemTimeSchedules = new List<ViewFoodItemTimeSchedule>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        ViewFoodItemTimeSchedule foodItemTimeSchedule = new ViewFoodItemTimeSchedule();
                        foodItemTimeSchedule.Id = Convert.ToInt32(Reader["Id"].ToString());
                        foodItemTimeSchedule.Name = Reader["Name"].ToString();
                        foodItemTimeSchedule.StartTime = Reader["StartTime"].ToString();
                        foodItemTimeSchedule.EndTime = Reader["EndTime"].ToString();
                        viewFoodItemTimeSchedules.Add(foodItemTimeSchedule);
                    }
                    Reader.Close();
                }
                return viewFoodItemTimeSchedules;
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