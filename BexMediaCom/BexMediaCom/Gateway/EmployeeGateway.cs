using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Gateway
{
    public class EmployeeGateway : CommonGateway
    {
        public bool IsEmployeeIdExist(object employeeId)
        {
            try
            {
                bool isemployeeIdexists = false;
                Query = "SELECT *FROM Employee WHERE EmpId='" + employeeId + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    isemployeeIdexists = true;
                }
                return isemployeeIdexists;
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

        public int Save(Employee employee)
        {
            if (employee.SectionId == 0)
            {
                employee.SectionId = 68;
            }
            if (employee.EmailId == null)
            {
                employee.EmailId = employee.UserNumber;
            }
            try
            {
               
                int number = employee.Number;

                Query =
                    "INSERT INTO Employee(EmpId,Name,DesignationId,DepartmentId,SectionId,DivisionId,UserNumber,EmployeePhotograph,EmailId,JoinDate,Proximity,Status) VALUES(@EmpId, @Name,@DesignationId,@DepartmentId,@SectionId,@DivisionId,@UserNumber,@EmployeePhotograph,@EmailId,@JoinDate,@Proximity,@Status); SELECT SCOPE_IDENTITY() as Id";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("EmpId", SqlDbType.VarChar);
                Command.Parameters["EmpId"].Value = employee.EmpId;
                Command.Parameters.Add("Name", SqlDbType.VarChar);
                Command.Parameters["Name"].Value = employee.Name;
                Command.Parameters.Add("DesignationId", SqlDbType.Int);
                Command.Parameters["DesignationId"].Value = employee.DesignationId;
                Command.Parameters.Add("DepartmentId", SqlDbType.Int);
                Command.Parameters["DepartmentId"].Value = employee.DepartmentId;
                Command.Parameters.Add("SectionId", SqlDbType.Int);
                Command.Parameters["SectionId"].Value = employee.SectionId;
                Command.Parameters.Add("DivisionId", SqlDbType.Int);
                Command.Parameters["DivisionId"].Value = employee.DivisionId;
                Command.Parameters.Add("UserNumber", SqlDbType.VarChar);
                Command.Parameters["UserNumber"].Value = employee.UserNumber;
                Command.Parameters.Add("EmployeePhotograph", SqlDbType.VarChar);
                Command.Parameters["EmployeePhotograph"].Value = employee.UploadFile;
                Command.Parameters.Add("EmailId", SqlDbType.VarChar);
                Command.Parameters["EmailId"].Value = employee.EmailId;
                Command.Parameters.Add("JoinDate", SqlDbType.Date);
                Command.Parameters["JoinDate"].Value = employee.JoinDate;
                Command.Parameters.Add("Proximity", SqlDbType.VarChar);
                Command.Parameters["Proximity"].Value = employee.Proximity;
                Command.Parameters.Add("Status", SqlDbType.Bit);
                Command.Parameters["Status"].Value = employee.Status;

                Connection.Open();
              //  int rowsAffected = Command.ExecuteNonQuery();
                string employeeId = Command.ExecuteScalar().ToString();

                Connection.Close();

                if (employeeId != null)
                {

                    string password = "123456";
                    Query = @"INSERT INTO [dbo].[EmployeePassword]
           ([EmployeeId]
           ,[Password])
     VALUES ('" + employeeId + "', '" + password + "')";

                    Command.CommandText = Query;
                    Command.Connection = Connection;
                    Command.Parameters.Clear();
                    Command.Parameters.Add("employeeId", SqlDbType.Int);
                    Command.Parameters["employeeId"].Value = employeeId;
                    Command.Parameters.Add("Password", SqlDbType.VarChar);
                    Command.Parameters["Password"].Value = password;
                    Connection.Open();
                    Command.ExecuteNonQuery();
                    Connection.Close();

                    int roleId = 2;
                    Query = @"INSERT INTO [dbo].[EmployeeRole]
           ([EmployeeId]
           ,[UserRoleId])
     VALUES ('" + employeeId + "', '" + roleId + "')";

                    Command.CommandText = Query;
                    Command.Connection = Connection;
                    Command.Parameters.Clear();
                    Command.Parameters.Add("EmployeeId", SqlDbType.Int);
                    Command.Parameters["EmployeeId"].Value = employeeId;
                    Command.Parameters.Add("UserRoleId", SqlDbType.Int);
                    Command.Parameters["UserRoleId"].Value = roleId;
                    Connection.Open();
                    Command.ExecuteNonQuery();
                    Connection.Close();
                }
                return 1;
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

        public List<Employee> GetEmployeeId()
        {
            try
            {
                Query = "SELECT*FROM Employee";
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
                        employee.EmpId = Reader["EmpId"].ToString();
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

        public List<EmployeeView> GetAllEmployee()
        {
            try
            {
                Query =
                    @"SELECT e.Id, e.EmpId, e.Name as EmployeeName,s.SectionName, d.Name as DesignationName, p.Name as DepartmentName, v.Name as DivisionName, e.EmployeePhotograph, e.Proximity, e.Status
FROM Employee e
INNER JOIN Designation d on d.Id = e.DesignationId
INNER JOIN Section s on s.Id = e.SectionId
INNER JOIN Department p on p.Id = e.DepartmentId
INNER JOIN Division v on v.Id = e.DivisionId";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<EmployeeView> employees = new List<EmployeeView>();
                int number = 1;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        EmployeeView employee = new EmployeeView();

                        employee.Id = Convert.ToInt32(Reader["Id"].ToString());
                        employee.EmpId = (Reader["EmpId"].ToString());
                        employee.Name = Reader["EmployeeName"].ToString();
                        employee.Designation = Reader["DesignationName"].ToString();
                        employee.Division = Reader["DivisionName"].ToString();
                        employee.SectionName = Reader["SectionName"].ToString();
                        employee.Department = Reader["DepartmentName"].ToString();
                        employee.Proximity = Reader["Proximity"].ToString();
                       int status = (int) Reader["Status"];
                        if (status == 1)
                        {
                            employee.Status = "Active";
                        }
                        else
                        {
                            employee.Status = "Inactive";
                        }
                        employee.UploadFile = Reader["EmployeePhotograph"].ToString();
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

        public Employee GetEmployeeById(int? id)
        {
            try
            {
                Query = @"SELECT e.Id, e.EmpId, e.Name, e.DesignationId,e.DepartmentId, e.SectionId, e.DivisionId, e.UserNumber, e.EmployeePhotograph, e.EmailId,CONVERT(varchar, e.JoinDate, 23) as JoinDate, e.Proximity, e.Status, d.Name as DivisionName, p.Name as DepartmentName, s.SectionName, g.Name as DesignatinName
From Employee e
INNER JOIN Division d on d.Id = e.DivisionId
INNER JOIN Department p on p.Id = e.DepartmentId
INNER JOIN Section s on s.Id = e.SectionId
INNER JOIN Designation g on g.Id = e.DesignationId 
Where e.Id = " + id + "";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                Employee employees = new Employee();
                int Number = 1;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        Employee employee = new Employee();

                        employee.Id = Convert.ToInt32(Reader["Id"]);
                        employee.EmpId = Reader["EmpId"].ToString();
                        employee.Name = Reader["Name"].ToString();
                        employee.JoinDate = Reader["JoinDate"].ToString();
                        employee.DivisionId = (int)Reader["DivisionId"];
                        employee.DepartmentId = (int)Reader["DepartmentId"];
                        employee.SectionId = (int)Reader["SectionId"];
                        employee.DesignationId = (int)Reader["DesignationId"];
                        employee.Proximity = Reader["Proximity"].ToString();

                        employee.UploadFile = Reader["EmployeePhotograph"].ToString();
                        employee.DivisionName = Reader["DivisionName"].ToString();
                        employee.DepartmentName = Reader["DepartmentName"].ToString();
                        employee.SectionName = Reader["SectionName"].ToString();
                        employee.DesignationName = Reader["DesignatinName"].ToString();
                        employee.UserNumber = Reader["UserNumber"].ToString();
                        string email = Reader["EmailId"].ToString();
                        if (email != "")
                        {
                            employee.EmailId = email;
                        }
                        else
                        {
                            employee.EmailId = employee.UserNumber;
                        }

                        employee.Number = Number;
                        employees = employee;
                        Number++;
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

        public EmployeeMonitoring GetEmployeeByEmpId(string id)
        {
            try
            {
                Query = @"SELECT e.Id, e.EmpId, e.Name, e.JoinDate, d.Name as DivisionName, p.Name as DepartmentName, g.Name as DesignationName, e.Proximity, e.Status, e.EmailId, e.EmployeePhotograph
FROM Employee e
INNER JOIN Division d on d.Id = e.DivisionId
INNER JOIN Department p on p.Id = e.DepartmentId
INNER JOIN Designation g on g.Id = e.DesignationId
Where e.Proximity = '" + id + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                EmployeeMonitoring employees = new EmployeeMonitoring();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        EmployeeMonitoring employee = new EmployeeMonitoring();

                        employee.Id = Convert.ToInt32(Reader["Id"]);
                        employee.EmpId = Reader["EmpId"].ToString();
                        employee.Name = Reader["Name"].ToString();
                        employee.JoinDate = (DateTime)Reader["JoinDate"];
                        employee.Division = Reader["DivisionName"].ToString();
                        employee.Department = Reader["DepartmentName"].ToString();
                        employee.Designation = Reader["DesignationName"].ToString();
                        employee.CardNumber = Reader["Proximity"].ToString();
                        // employee.Status = Convert.ToBoolean(Reader["Status"].ToString());
                        employee.EmailId = Reader["EmailId"].ToString();
                        employee.UploadFile = Reader["EmployeePhotograph"].ToString();
                        employees = employee;
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
        public int GetFoodItemCostIdByTime(string id)
        {
            try
            {
                Query = @"SELECT ItemId FROM CafeTimeSchedule
Where StartTime <= '"+id+"' and EndTime >= '"+id+"'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                int foodItemId = 0;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {


                        foodItemId = Convert.ToInt32(Reader["ItemId"]);
 
             
                    }
                    Reader.Close();
                }
                return foodItemId;
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
        public ShowEmployeeMonitoring GetEmployeeByEmpId1(string id, int foodItemId)
        {
            try
            {
                string date = DateTime.Now.Date.ToString();
                Query = @"SELECT e.Id, e.EmpId, e.Name, e.JoinDate, d.Name as DivisionName, p.Name as DepartmentName, g.Name as DesignationName, e.Proximity, e.Status, e.EmailId, e.EmployeePhotograph, t.Status as FoodStatus
FROM Employee e
INNER JOIN Division d on d.Id = e.DivisionId
INNER JOIN Department p on p.Id = e.DepartmentId
INNER JOIN Designation g on g.Id = e.DesignationId
INNER JOIN EmployeeCafeteriaTransaction t on t.EmpId = e.EmpId
Where e.Proximity = '" + id + "' and t.FoodItemCostId = '" + foodItemId + "' and t.CheckDate = '" + date + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                ShowEmployeeMonitoring employees = new ShowEmployeeMonitoring();
                int number = 1;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        ShowEmployeeMonitoring employee = new ShowEmployeeMonitoring();

                        employee.Id = Convert.ToInt32(Reader["Id"]);
                        employee.EmpId = Reader["EmpId"].ToString();
                        employee.Name = Reader["Name"].ToString();
                        employee.JoinDate = (DateTime)Reader["JoinDate"];
                        employee.Division = Reader["DivisionName"].ToString();
                        employee.Department = Reader["DepartmentName"].ToString();
                        employee.Designation = Reader["DesignationName"].ToString();
                        employee.CardNumber = Reader["Proximity"].ToString();
                        // employee.Status = Convert.ToBoolean(Reader["Status"].ToString());
                        employee.EmailId = Reader["EmailId"].ToString();
                        employee.Statuss = Reader["FoodStatus"].ToString();
                        employee.UploadFile = Reader["EmployeePhotograph"].ToString();
                        employee.Number = number;
                        employees = employee;
                        number++;
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

        public ShowEmployeeMonitoring GetEmployeeByEmpId2(string id, int foodItemId)
        {
            try
            {
                string date = DateTime.Now.Date.ToString();
                Query = @"SELECT e.Id, e.EmpId, e.Name, e.JoinDate, d.Name as DivisionName, p.Name as DepartmentName, g.Name as DesignationName, e.Proximity, e.Status, e.EmailId, e.EmployeePhotograph
FROM Employee e
INNER JOIN Division d on d.Id = e.DivisionId
INNER JOIN Department p on p.Id = e.DepartmentId
INNER JOIN Designation g on g.Id = e.DesignationId
Where e.Proximity = '" + id + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                ShowEmployeeMonitoring employees = new ShowEmployeeMonitoring();
                int number = 1;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        ShowEmployeeMonitoring employee = new ShowEmployeeMonitoring();

                        employee.Id = Convert.ToInt32(Reader["Id"]);
                        employee.EmpId = Reader["EmpId"].ToString();
                        employee.Name = Reader["Name"].ToString();
                        employee.JoinDate = (DateTime)Reader["JoinDate"];
                        employee.Division = Reader["DivisionName"].ToString();
                        employee.Department = Reader["DepartmentName"].ToString();
                        employee.Designation = Reader["DesignationName"].ToString();
                        employee.CardNumber = Reader["Proximity"].ToString();
                        // employee.Status = Convert.ToBoolean(Reader["Status"].ToString());
                        employee.EmailId = Reader["EmailId"].ToString();
                       // employee.Statuss = Reader["FoodStatus"].ToString();
                        employee.UploadFile = Reader["EmployeePhotograph"].ToString();
                        employee.Number = number;
                        employees = employee;
                        number++;
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

        public ShowEmployeeMonitoring GetEmployeeByEmpId1235(string id, int foodItemId, string date )
        {
            try
            {
               // string date = DateTime.Now.Date.ToString();
                Query = @"SELECT e.Id, e.EmpId, e.Name, e.JoinDate, d.Name as DivisionName, p.Name as DepartmentName, g.Name as DesignationName, e.Proximity, e.Status, e.EmailId, e.EmployeePhotograph
FROM Employee e
INNER JOIN Division d on d.Id = e.DivisionId
INNER JOIN Department p on p.Id = e.DepartmentId
INNER JOIN Designation g on g.Id = e.DesignationId
INNER JOIN EmployeeCafeteriaTransaction t on t.EmpId = e.EmpId
Where e.Proximity = '" + id + "' and t.FoodItemCostId = '" + foodItemId + "' and t.CheckDate = '" + date + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                ShowEmployeeMonitoring employees = new ShowEmployeeMonitoring();
                int number = 1;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        ShowEmployeeMonitoring employee = new ShowEmployeeMonitoring();

                        employee.Id = Convert.ToInt32(Reader["Id"]);
                        employee.EmpId = Reader["EmpId"].ToString();
                        employee.Name = Reader["Name"].ToString();
                        employee.JoinDate = (DateTime)Reader["JoinDate"];
                        employee.Division = Reader["DivisionName"].ToString();
                        employee.Department = Reader["DepartmentName"].ToString();
                        employee.Designation = Reader["DesignationName"].ToString();
                        employee.CardNumber = Reader["Proximity"].ToString();
                        // employee.Status = Convert.ToBoolean(Reader["Status"].ToString());
                        employee.EmailId = Reader["EmailId"].ToString();
                        employee.UploadFile = Reader["EmployeePhotograph"].ToString();
                        employee.Number = number;
                        employees = employee;
                        number++;
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

        public bool GetEmployeeByEmpId12(string id, int foodItemId)
        {
            try
            {
                string date = DateTime.Now.Date.ToString();
                Query = @"SELECT e.Id, e.EmpId, e.Name, e.JoinDate, d.Name as DivisionName, p.Name as DepartmentName, g.Name as DesignationName, e.Proximity, e.Status, e.EmailId, e.EmployeePhotograph
FROM Employee e
INNER JOIN Division d on d.Id = e.DivisionId
INNER JOIN Department p on p.Id = e.DepartmentId
INNER JOIN Designation g on g.Id = e.DesignationId
INNER JOIN EmployeeCafeteriaTransaction t on t.EmpId = e.EmpId
Where e.Proximity = '" + id + "' and t.FoodItemCostId = '" + foodItemId + "' and t.CheckDate = '" + date + "'and ( t.Status = '" + "No" + "' OR t.Status = '" + "Yes" + "') ";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                ShowEmployeeMonitoring employees = new ShowEmployeeMonitoring();
                bool status = false;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        status = true;
                    }
                    Reader.Close();
                }
                return status;
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

        public bool GetEmployeeByEmpId123(string id, int foodItemId, string date)
        {
            try
            {
               // string date = DateTime.Now.Date.ToString();
                Query = @"SELECT e.Id, e.EmpId, e.Name, e.JoinDate, d.Name as DivisionName, p.Name as DepartmentName, g.Name as DesignationName, e.Proximity, e.Status, e.EmailId, e.EmployeePhotograph
FROM Employee e
INNER JOIN Division d on d.Id = e.DivisionId
INNER JOIN Department p on p.Id = e.DepartmentId
INNER JOIN Designation g on g.Id = e.DesignationId
INNER JOIN EmployeeCafeteriaTransaction t on t.EmpId = e.EmpId
Where e.Proximity = '" + id + "' and t.FoodItemCostId = '" + foodItemId + "' and t.CheckDate = '" + date + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                ShowEmployeeMonitoring employees = new ShowEmployeeMonitoring();
                bool status = false;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        status = true;
                    }
                    Reader.Close();
                }
                return status;
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

        public EmployeeMonitoring GetListofuser(EmployeeCafeteriaTransaction cafeteriaa)
        {
            try
            {
                Query = @"SELECT e.EmpId, e.Name, e.JoinDate, p.Name, d.Name, g.Name, c.Number, e.Status, e.EmailId, e.EmployeePhotograph,t.ItemQuentity, t.CheckDate, t.CheckTime
FROM EmployeeCafeteriaTransaction t
INNER JOIN Employee e on e.Id = t.EmpId
INNER JOIN Department p on p.Id = e.DepartmentId
INNER JOIN Division d on d.Id = e.DivisionId
INNER JOIN Designation g on g.Id = e.DesignationId
INNER JOIN CardReg c on c.Id = e.CardId
Where t.EmpId = '" + cafeteriaa.EmpId + "' and t.CheckDate = '" + cafeteriaa.CheckDate + "' and t.CheckTime = '" + cafeteriaa.CheckTime + "' and t.FoodItemCostId = '" + cafeteriaa.FoodItemCostId + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                EmployeeMonitoring employees = new EmployeeMonitoring();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        EmployeeMonitoring employee = new EmployeeMonitoring();

                        employee.Id = Convert.ToInt32(Reader["Id"]);
                        employee.EmpId = Reader["EmpId"].ToString();
                        employee.Name = Reader["Name"].ToString();
                        employee.JoinDate = (DateTime)Reader["JoinDate"];
                        employee.Division = Reader["DivisionName"].ToString();
                        employee.Department = Reader["DepartmentName"].ToString();
                        employee.Designation = Reader["DesignationName"].ToString();
                        employee.CardNumber = Reader["Number"].ToString();
                        // employee.Status = Convert.ToBoolean(Reader["Status"].ToString());
                        employee.EmailId = Reader["EmailId"].ToString();
                        employee.UploadFile = Reader["EmployeePhotograph"].ToString();
                        employees = employee;
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

        public bool IsEmployeeExist(string employeeId)
        {
            try
            {
                string date = DateTime.Now.Date.ToString();
                int index = date.IndexOf(' ');
                string resultss = date.Substring(0, index);

                bool isemployeeIdexists = false;
                Query = "SELECT *FROM EmployeeCafeteriaTransaction WHERE EmpId='" + employeeId + "' and CheckDate = '" + resultss + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    isemployeeIdexists = true;
                }
                return isemployeeIdexists;
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

        public int UpdateEmployee(Employee employee)
        {
            //employee.Section = employee.Proximity;
            try
            {
                Query = @"UPDATE [dbo].[Employee]
   SET [EmpId] = @EmpId
      ,[Name] =  @Name
      ,[DesignationId] = @DesignationId
      ,[DepartmentId] =  @DepartmentId
      ,[SectionId] = @SectionId
      ,[DivisionId] =  @DivisionId
      ,[EmployeePhotograph] = @UploadFile
      ,[EmailId] = @EmailId
      ,[JoinDate] = @JoinDate
      ,[Proximity] = @Proximity
      ,[Status] = @Status
WHERE Id = '" + employee.Id + "'";

                    Command.CommandText = Query;
                    Command.Connection = Connection;
                    Command.Parameters.Clear();
                    Command.Parameters.Add("EmpId", SqlDbType.VarChar);
                    Command.Parameters["EmpId"].Value = employee.EmpId;
                    Command.Parameters.Add("Name", SqlDbType.VarChar);
                    Command.Parameters["Name"].Value = employee.Name;
                    Command.Parameters.Add("DesignationId", SqlDbType.Int);
                    Command.Parameters["DesignationId"].Value = employee.DesignationId;
                    Command.Parameters.Add("DepartmentId", SqlDbType.Int);
                    Command.Parameters["DepartmentId"].Value = employee.DepartmentId;
                    Command.Parameters.Add("SectionId", SqlDbType.VarChar);
                    Command.Parameters["SectionId"].Value = employee.SectionId;
                    Command.Parameters.Add("DivisionId", SqlDbType.Int);
                    Command.Parameters["DivisionId"].Value = employee.DivisionId;
                    Command.Parameters.Add("UploadFile", SqlDbType.VarChar);
                    Command.Parameters["UploadFile"].Value = employee.UploadFile;
                    Command.Parameters.Add("EmailId", SqlDbType.VarChar);
                    Command.Parameters["EmailId"].Value = employee.EmailId;
                    Command.Parameters.Add("JoinDate", SqlDbType.Date);
                    Command.Parameters["JoinDate"].Value = employee.JoinDate;
                    Command.Parameters.Add("Proximity", SqlDbType.VarChar);
                    Command.Parameters["Proximity"].Value = employee.Proximity;
                    Command.Parameters.Add("Status", SqlDbType.Bit);
                    Command.Parameters["Status"].Value = employee.Status;

                    Connection.Open();
                    int rowsAffected = Command.ExecuteNonQuery();
                    Connection.Close();

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

        public int UpdateEmployeeWithoutImage(Employee employee)
        {
           // employee.Section = employee.Proximity;
            try
            {
                if (employee.EmailId == null)
                {
                    Query = @"UPDATE [dbo].[Employee]
   SET [EmpId] = @EmpId
      ,[Name] =  @Name
      ,[DesignationId] = @DesignationId
      ,[DepartmentId] =  @DepartmentId
      ,[SectionId] = @SectionId
      ,[DivisionId] =  @DivisionId
      ,[JoinDate] = @JoinDate
      ,[Proximity] = @Proximity
      ,[Status] = @Status
WHERE Id = '" + employee.Id + "'";

                    Command.CommandText = Query;
                    Command.Connection = Connection;
                    Command.Parameters.Clear();
                    Command.Parameters.Add("EmpId", SqlDbType.VarChar);
                    Command.Parameters["EmpId"].Value = employee.EmpId;
                    Command.Parameters.Add("Name", SqlDbType.VarChar);
                    Command.Parameters["Name"].Value = employee.Name;
                    Command.Parameters.Add("DesignationId", SqlDbType.Int);
                    Command.Parameters["DesignationId"].Value = employee.DesignationId;
                    Command.Parameters.Add("DepartmentId", SqlDbType.Int);
                    Command.Parameters["DepartmentId"].Value = employee.DepartmentId;
                    Command.Parameters.Add("SectionId", SqlDbType.VarChar);
                    Command.Parameters["SectionId"].Value = employee.SectionId;
                    Command.Parameters.Add("DivisionId", SqlDbType.Int);
                    Command.Parameters["DivisionId"].Value = employee.DivisionId;
                    Command.Parameters.Add("JoinDate", SqlDbType.Date);
                    Command.Parameters["JoinDate"].Value = employee.JoinDate;
                    Command.Parameters.Add("Proximity", SqlDbType.VarChar);
                    Command.Parameters["Proximity"].Value = employee.Proximity;
                    Command.Parameters.Add("Status", SqlDbType.Bit);
                    Command.Parameters["Status"].Value = employee.Status;

                    Connection.Open();
                    int rowsAffected = Command.ExecuteNonQuery();
                    Connection.Close();

                    return rowsAffected;
                }
                else
                {
                    Query = @"UPDATE [dbo].[Employee]
   SET [EmpId] = @EmpId
      ,[Name] =  @Name
      ,[DesignationId] = @DesignationId
      ,[DepartmentId] =  @DepartmentId
      ,[SectionId] = @SectionId
      ,[DivisionId] =  @DivisionId
      ,[EmailId] = @EmailId
      ,[JoinDate] = @JoinDate
      ,[Proximity] = @Proximity
      ,[Status] = @Status
WHERE Id = '" + employee.Id + "'";

                    Command.CommandText = Query;
                    Command.Connection = Connection;
                    Command.Parameters.Clear();
                    Command.Parameters.Add("EmpId", SqlDbType.VarChar);
                    Command.Parameters["EmpId"].Value = employee.EmpId;
                    Command.Parameters.Add("Name", SqlDbType.VarChar);
                    Command.Parameters["Name"].Value = employee.Name;
                    Command.Parameters.Add("DesignationId", SqlDbType.Int);
                    Command.Parameters["DesignationId"].Value = employee.DesignationId;
                    Command.Parameters.Add("DepartmentId", SqlDbType.Int);
                    Command.Parameters["DepartmentId"].Value = employee.DepartmentId;
                    Command.Parameters.Add("SectionId", SqlDbType.VarChar);
                    Command.Parameters["SectionId"].Value = employee.SectionId;
                    Command.Parameters.Add("DivisionId", SqlDbType.Int);
                    Command.Parameters["DivisionId"].Value = employee.DivisionId;
                    Command.Parameters.Add("EmailId", SqlDbType.VarChar);
                    Command.Parameters["EmailId"].Value = employee.EmailId;
                    Command.Parameters.Add("JoinDate", SqlDbType.Date);
                    Command.Parameters["JoinDate"].Value = employee.JoinDate;
                    Command.Parameters.Add("Proximity", SqlDbType.VarChar);
                    Command.Parameters["Proximity"].Value = employee.Proximity;
                    Command.Parameters.Add("Status", SqlDbType.Bit);
                    Command.Parameters["Status"].Value = employee.Status;

                    Connection.Open();
                    int rowsAffected = Command.ExecuteNonQuery();
                    Connection.Close();

                    return rowsAffected;
                }



                
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


        public List<CardRegistration> GetAllCardRegistrations()
        {
            try
            {
                Query = "SELECT*FROM CardReg Where Status = '" + "Inactive" + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<CardRegistration> cardRegistrations = new List<CardRegistration>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        CardRegistration cardRegistration = new CardRegistration();
                        cardRegistration.Id = Convert.ToInt32(Reader["Id"].ToString());
                        cardRegistration.Number = Reader["Number"].ToString();
                        cardRegistrations.Add(cardRegistration);
                    }
                    Reader.Close();
                }
                return cardRegistrations;
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

        public string GetUserNumber()
        {
            int number1 = 0;
            String query = @"SELECT COUNT(*) as number FROM Employee";

            SqlCommand com = new SqlCommand(query, Connection);
            Connection.Open();
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                number1 = (int)reader["number"];

            }
            reader.Close();
            Connection.Close();


            int countDepartmentSId = number1 + 1;
            int ZeroToBeAdded = 6 - countDepartmentSId.ToString().Length;
            string number = "";
            for (int i = 0; i < ZeroToBeAdded; i++)
            {
                number += 0;
            }
            return number + countDepartmentSId;

        }

        public List<Section> GetAllSection(int departmentId)
        {
            try
            {
                Query = "SELECT * FROM Section Where DepartmentId = '" + departmentId + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<Section> sections = new List<Section>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        Section section = new Section();
                        section.Id = Convert.ToInt32(Reader["Id"].ToString());
                        section.SectionName = Reader["SectionName"].ToString();
                        sections.Add(section);
                    }
                    Reader.Close();
                }
                return sections;
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

        public bool IsFoodItemTimeExist(string id, int foodItemId)
        {
            try
            {
                Query = @"SELECT ItemId FROM CafeTimeSchedule
Where StartTime <= '" + id + "' and EndTime >= '" + id + "' and ItemId = '" + foodItemId + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                bool isExist = false;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {

                        isExist = true;
                    }
                    Reader.Close();
                }
                return isExist;
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
    }
}