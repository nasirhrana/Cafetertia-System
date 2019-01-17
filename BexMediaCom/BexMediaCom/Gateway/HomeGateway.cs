using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Gateway
{
    public class HomeGateway: CommonGateway
    {
        public List<UserInfo> UserInfo(string empId)
        {
            try
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd");

                string year = DateTime.Now.Year.ToString();
                string month = DateTime.Now.Month.ToString();
                string day = DateTime.Now.Day.ToString();

                int daay = Convert.ToInt32(day);

                if (daay < 21)
                {

                    if (month == "1")
                    {
                        month = "12";
                        int yer = Convert.ToInt32(year);
                        year = (yer - 1).ToString();

                    }
                    else
                    {
                        int month10 = Convert.ToInt32(month);
                        month = (month10 - 1).ToString();
                    }
                }

                string startDate = year + "-" + month + "-" + "21";
                string endDate = date;



                Query = @"SELECT SUM(UnitRate) as TotalCost, SUM(ItemQuentity) as ItemQuentity, AVG(UnitRate) as AvgUnitRate
FROM EmployeeCafeteriaTransaction
Where EmpId = '" + empId + "' AND CheckDate >= '" + startDate + "' AND CheckDate   <= '" + endDate + "' AND ( FoodItemCostId = '" + 6 + "' OR FoodItemCostId = '" + 7 + "' OR FoodItemCostId = '" + 9 + "')";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<UserInfo> userInfos = new List<UserInfo>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        UserInfo user = new UserInfo();

                        user.TotalCost = Reader["TotalCost"].ToString();
                        user.TotalMeil = Reader["ItemQuentity"].ToString();
                        //= 
                        decimal value1 = (decimal) Reader["AvgUnitRate"];
                        value1 = System.Math.Round(value1, 2);
                        user.AvgUnitRate = value1.ToString();
                        userInfos.Add(user);
                    }
                    Reader.Close();
                }
                return userInfos;
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

        public List<UserInfo> Notification(string empId)
        {
            try
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd");


                string year1 = date.Substring(0, 4);
                string month1 = date.Substring(5, 2);
                string day1 = date.Substring(7, 2);
                string dates = year1 + "-" + month1 + "-" + day1;

                string startDate = year1 + "-" + month1 + "-" + "01";
                string endDate = date;


                Query = @"SELECT COUNT(Id) as Id
FROM SendOrder
Where EmpId = '" + empId + "' AND Status = '" + "Submit" + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<UserInfo> userInfos = new List<UserInfo>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        UserInfo user = new UserInfo();

                        user.EmaiNotification =Reader["Id"].ToString();


                        userInfos.Add(user);
                    }
                    Reader.Close();
                }
                return userInfos;
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

        public List<UserInfo> Notification1(string empId)
        {
            try
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd");


                string year1 = date.Substring(0, 4);
                string month1 = date.Substring(5, 2);
                string day1 = date.Substring(7, 2);
                string dates = year1 + "-" + month1 + "-" + day1;

                string startDate = year1 + "-" + month1 + "-" + "01";
                string endDate = date;


                Query = @"SELECT COUNT(Id) as Id
FROM DepartmentSendOrder
Where EmpId = '" + empId + "' AND Status = '" + "Submit" + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<UserInfo> userInfos = new List<UserInfo>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        UserInfo user = new UserInfo();

                        user.EmaiNotification = Reader["Id"].ToString();


                        userInfos.Add(user);
                    }
                    Reader.Close();
                }
                return userInfos;
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

        public List<UserInfo> Notification5()
        {
            try
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd");


                string year1 = date.Substring(0, 4);
                string month1 = date.Substring(5, 2);
                string day1 = date.Substring(7, 2);
                string dates = year1 + "-" + month1 + "-" + day1;

                string startDate = year1 + "-" + month1 + "-" + "01";
                string endDate = date;


                Query = @"SELECT COUNT(Id) as Id
FROM SendOrder
Where Status = '" + "Submit" + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<UserInfo> userInfos = new List<UserInfo>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        UserInfo user = new UserInfo();

                        user.EmaiNotification = Reader["Id"].ToString();


                        userInfos.Add(user);
                    }
                    Reader.Close();
                }
                return userInfos;
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

        public List<UserInfo> Notification6()
        {
            try
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd");


                string year1 = date.Substring(0, 4);
                string month1 = date.Substring(5, 2);
                string day1 = date.Substring(7, 2);
                string dates = year1 + "-" + month1 + "-" + day1;

                string startDate = year1 + "-" + month1 + "-" + "01";
                string endDate = date;


                Query = @"SELECT COUNT(Id) as Id
FROM DepartmentSendOrder
Where Status = '" + "Submit" + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<UserInfo> userInfos = new List<UserInfo>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        UserInfo user = new UserInfo();

                        user.EmaiNotification = Reader["Id"].ToString();


                        userInfos.Add(user);
                    }
                    Reader.Close();
                }
                return userInfos;
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