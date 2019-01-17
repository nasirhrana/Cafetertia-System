using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BexMediaCom.Models;

namespace BexMediaCom.Gateway
{
    public class DivisionGateway:CommonGateway
    {
        public int Save(Division division)
        {
            try
            {
                Query = "INSERT INTO Division(Name) VALUES(@Name)";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("Name", SqlDbType.VarChar);
                Command.Parameters["Name"].Value = division.Name;
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

        public bool IsNameExists(string name)
        {
            try
            {
                bool isNameExists = false;
                Query = "SELECT*FROM Division WHERE Name='" + name + "'";
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

        public List<Division> ShowAllDivision()
        {
            try
            {
                Query = "SELECT*FROM Division";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<Division> divisions = new List<Division>();
                int number = 1;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        Division division = new Division();
                        division.Id = Convert.ToInt32(Reader["Id"].ToString());
                        division.Name = Reader["Name"].ToString();
                        division.Number = number;
                        divisions.Add(division);
                        number++;
                    }
                    Reader.Close();
                }
                return divisions;
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

        public List<Division> GetAllDivisions()
        {
            try
            {
                Query = "SELECT*FROM Division";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<Division> divisions = new List<Division>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        Division division = new Division();
                        division.Id = Convert.ToInt32(Reader["Id"].ToString());
                        division.Name = Reader["Name"].ToString();
                        divisions.Add(division);
                    }
                    Reader.Close();
                }
                return divisions;
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

        public Division GetDivisionById(int? id)
        {
            try
            {
                Query = "SELECT * FROM Division Where Id = " + id + "";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                Division divisions = new Division();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        Division division = new Division();
                        division.Id = Convert.ToInt32(Reader["Id"]);
                        division.Name = Reader["Name"].ToString();
                        divisions = division;
                    }
                    Reader.Close();
                }
                return divisions;
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

        public int UpdateDivision(Division division)
        {
            try
            {
                Query = @"UPDATE Division
   SET [Name] = @Name
 WHERE Id = '" + division.Id + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("Name", SqlDbType.VarChar);
                Command.Parameters["Name"].Value = division.Name;
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