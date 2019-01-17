using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Gateway
{
    public class DepartmentGateway:CommonGateway
    {
        public int Save(Department department)
        {
            try
            {
                Query = "INSERT INTO Department(Name, DivisionId) VALUES(@Name, @DivisionId)";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("Name", SqlDbType.VarChar);
                Command.Parameters["Name"].Value = department.Name;
                Command.Parameters.Add("DivisionId", SqlDbType.Int);
                Command.Parameters["DivisionId"].Value = department.DivisionId;
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

        public bool IsNameExists(string name, int divisionId)
        {
            try
            {
                bool isNameExists = false;
                Query = "SELECT*FROM Department WHERE Name='" + name + "' and DivisionId ='" + divisionId + "'";
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

        public List<DepartmentView> ShowAllDepartment()
        {
            try
            {
                Query = @"SELECT d.Id, d.Name, e.Name as DivisionName
FROM Department d
INNER JOIN Division e on e.Id = d.DivisionId";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<DepartmentView> departments = new List<DepartmentView>();
                int number = 1;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        DepartmentView department = new DepartmentView();
                        department.Id = Convert.ToInt32(Reader["Id"].ToString());
                        department.DepartmentName = Reader["Name"].ToString();
                        department.DivisionName = Reader["DivisionName"].ToString();
                        department.Number = number;
                        departments.Add(department);
                        number++;
                    }
                    Reader.Close();
                }
                return departments;
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

        public List<Department> GetDepartmentId()
        {
            try
            {
                Query = @"SELECT d.Id, d.Name, e.Name as DivisionName
FROM Department d
INNER JOIN Division e on e.Id = d.DivisionId";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<Department> departments = new List<Department>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        Department department = new Department();
                        department.Id = Convert.ToInt32(Reader["Id"].ToString());
                        department.Name = Reader["Name"].ToString();
                        departments.Add(department);
                    }
                    Reader.Close();
                }
                return departments;
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
        public List<Department> GetAllDepartments(int divisionId)
        {
            try
            {
                Query = "SELECT*FROM Department Where DivisionId = '" + divisionId + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<Department> departments = new List<Department>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        Department department = new Department();
                        department.Id = Convert.ToInt32(Reader["Id"].ToString());
                        department.Name = Reader["Name"].ToString();
                        departments.Add(department);
                    }
                    Reader.Close();
                }
                Connection.Close();
                return departments;
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

        public Department GetDepartmentById(int? id)
        {
            try
            {
                Query = "SELECT*FROM Department Where Id = " + id + "";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                Department departments = new Department();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        Department department = new Department();
                        department.Id = Convert.ToInt32(Reader["Id"]);
                        department.Name = Reader["Name"].ToString();
                        departments = department;
                    }
                    Reader.Close();
                }
                return departments;
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

        public int UpdateDepartment(Department department)
        {
            try
            {
                Query = @"UPDATE Department
   SET [Name] = @Name
      ,[DivisionId] = @DivisionId
 WHERE Id = '" + department.Id + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("Name", SqlDbType.VarChar);
                Command.Parameters["Name"].Value = department.Name;
                Command.Parameters.Add("DivisionId", SqlDbType.Int);
                Command.Parameters["DivisionId"].Value = department.DivisionId;
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

        public int DeleteDepartment(int? id)
        {
            try
            {
                Query = @"DELETE FROM Department
 WHERE Id = '" + id + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
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