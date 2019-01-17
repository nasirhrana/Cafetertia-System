using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BexMediaCom.Models;

namespace BexMediaCom.Gateway
{
    public class DesignationGateway : CommonGateway
    {
        public int Save(Designation designation)
        {
            try
            {
                Query = "INSERT INTO Designation(Name) VALUES(@Name)";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("Name", SqlDbType.VarChar);
                Command.Parameters["Name"].Value = designation.Name;
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
                Query = "SELECT*FROM Designation WHERE Name='" + name + "'";
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

        public List<Designation> ShowAllDesignation()
        {
            try
            {
                Query = "SELECT*FROM Designation";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<Designation> designations = new List<Designation>();
                int number = 1;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        Designation designation = new Designation();
                        designation.Id = Convert.ToInt32(Reader["Id"].ToString());
                        designation.Name = Reader["Name"].ToString();
                        designation.Number = number;
                        designations.Add(designation);
                        number++;
                    }
                    Reader.Close();
                }
                return designations;
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

        public List<Designation> GetAllDesignations()
        {
            try
            {
                Query = "SELECT*FROM Designation";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<Designation> designations = new List<Designation>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        Designation designation = new Designation();
                        designation.Id = Convert.ToInt32(Reader["Id"].ToString());
                        designation.Name = Reader["Name"].ToString();
                        designations.Add(designation);
                    }
                    Reader.Close();
                }
                return designations;
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

        public Designation GetDesignationById(int? id)
        {
            try
            {
                Query = "SELECT * FROM Designation Where Id = " + id + "";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                Designation designations = new Designation();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        Designation designation = new Designation();
                        designation.Id = Convert.ToInt32(Reader["Id"]);
                        designation.Name = Reader["Name"].ToString();
                        designations = designation;
                    }
                    Reader.Close();
                }
                return designations;
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

        public int UpdateDesignation(Designation designation)
        {
            try
            {
                Query = @"UPDATE Designation
   SET [Name] = @Name
 WHERE Id = '" + designation.Id + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("Name", SqlDbType.VarChar);
                Command.Parameters["Name"].Value = designation.Name;
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