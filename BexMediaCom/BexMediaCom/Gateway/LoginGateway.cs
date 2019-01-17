using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Gateway
{
    public class LoginGateway : CommonGateway
    {
        public List<Login> GetLogin(Login login1)
        {


            try
            {
                Query = @"SELECT e.Id, e.EmpId, e.Name, e.EmailId as Email, p.Password, u.RoleName, r.UserRoleId, e.EmployeePhotograph, d.Id as DptId
FROM Employee e 
INNER JOIN EmployeePassword p on p.EmployeeId = e.Id
INNER JOIN EmployeeRole r on r.EmployeeId = e.Id
INNER JOIN UserRole u on u.Id = r.UserRoleId 
INNER JOIN Department d on d.Id = e.DepartmentId
WHERE e.UserNumber = '" + login1.UserNumber + "'or e.EmailId = '" + login1.UserNumber + "' and p.Password = '" + login1.Password + "' and Status = '"+ 1 +"' ";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<Login> Logins = new List<Login>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        Login login = new Login();

                        login.Id = (int) Reader["Id"];
                        login.DptId = (int)Reader["DptId"];
                        login.Name = Reader["Name"].ToString();
                        login.Email = Reader["Email"].ToString();
                        login.RoleName = Reader["RoleName"].ToString();
                        login.EmpId = Reader["EmpId"].ToString();
                        login.Password = Reader["Password"].ToString();
                        login.EmployeePhotograph = Reader["EmployeePhotograph"].ToString();
                        login.UserRoleId = (int)Reader["UserRoleId"];

                        Logins.Add(login);
                    }
                    Reader.Close();
                }
                return Logins;
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

        public List<Employee> GetAllEmployee()
        {
            try
            {
                Query = @"SELECT Id, Name FROM Employee";
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
                        employee.Id = Convert.ToInt32(Reader["Id"].ToString());
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

        public List<UserRole> GetAllRole()
        {
            try
            {
                Query = @"SELECT * FROM UserRole";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<UserRole> userRoles = new List<UserRole>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        UserRole role = new UserRole();
                        role.Id = Convert.ToInt32(Reader["Id"].ToString());
                        role.RoleName = Reader["RoleName"].ToString();
                        userRoles.Add(role);
                    }
                    Reader.Close();
                }
                return userRoles;
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

        public int RoleAssign(RoleAssign role)
        {
            try
            {
                Query = @"UPDATE EmployeeRole
   SET UserRoleId = @UserRoleId 
WHERE EmployeeId = '" + role.EmployeeId + "'";

                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("UserRoleId", SqlDbType.Int);
                Command.Parameters["UserRoleId"].Value = role.RoleId;
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