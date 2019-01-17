using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Gateway
{
    public class SectionGateway: CommonGateway
    {

        public int SaveSaction(Section section)
        {
            try
            {
                Query = "INSERT INTO Section(DepartmentId, SectionName) VALUES(@DepartmentId, @SectionName)";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("DepartmentId", SqlDbType.Int);
                Command.Parameters["DepartmentId"].Value = section.DepartmentId;
                Command.Parameters.Add("SectionName", SqlDbType.VarChar);
                Command.Parameters["SectionName"].Value = section.SectionName;
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

        public List<SectionView> GetAllSection()
        {
            try
            {
                Query = "SELECT s.Id, s.SectionName, d.Name FROM Section s INNER JOIN Department d on d.Id=s.DepartmentId";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<SectionView> sections = new List<SectionView>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        SectionView section = new SectionView();
                        section.Id = Convert.ToInt32(Reader["Id"]);
                        section.DepartmentName = Reader["Name"].ToString();
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

        public Section GetSectionById(int? id)
        {
            try
            {
                Query =
                    "SELECT s.Id, s.DepartmentId, s.SectionName, d.Name as DepartmentName, d.Id as DepartmentId, n.Name as DivisionName, n.Id as DivisionId FROM Section s INNER JOIN Department d on d.Id = s.DepartmentId INNER JOIN Division n on n.Id = d.DivisionId Where s.Id = " +
                    id + "";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                Section section = new Section();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        Section section1 = new Section();
                        section1.Id = Convert.ToInt32(Reader["Id"]);
                        section1.DepartmentId = (int)Reader["DepartmentId"];
                        section1.DepartmentName = Reader["DepartmentName"].ToString();
                        section1.DivisionId = (int)Reader["DivisionId"];

                        section1.DivisionName = Reader["DivisionName"].ToString();

                        section1.SectionName = Reader["SectionName"].ToString();
                        section = section1;
                    }
                    Reader.Close();
                }
                return section;
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

        public int UpdateSection(Section section)
        {
            try
            {
                Query = @"UPDATE Section
   SET [DepartmentId] = @DepartmentId
,[SectionName] = @SectionName
 WHERE Id = '" + section.Id + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("DepartmentId", SqlDbType.Int);
                Command.Parameters["DepartmentId"].Value = section.DepartmentId;
                Command.Parameters.Add("SectionName", SqlDbType.VarChar);
                Command.Parameters["SectionName"].Value = section.SectionName;
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

        public bool IsSectionExists(Section section)
        {
            try
            {
                bool isNumberExists = false;
                Query = "SELECT*FROM Section WHERE DepartmentId='" + section.DepartmentId + "' and SectionName = '" + section.SectionName + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    isNumberExists = true;
                }
                return isNumberExists;
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

        public List<Department> GetAllDepartments()
        {
            try
            {
                Query = "SELECT*FROM Department";
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
    }
}