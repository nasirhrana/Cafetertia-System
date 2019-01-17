using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BexMediaCom.Gateway;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Manager
{
    public class DepartmentManager
    {
        DepartmentGateway departmentGateway = new DepartmentGateway();

        public bool Save(Department department)
        {

            if (IsNameExists(department.Name, department.DivisionId))
            {
                throw new Exception("Sorry!!!Department Already Exists");
            }

            return departmentGateway.Save(department) > 0;
        }

        public bool IsNameExists(string name, int divisionId )
        {
            return departmentGateway.IsNameExists(name, divisionId);
        }

        public List<DepartmentView> ShowAllDepartment()
        {
            return departmentGateway.ShowAllDepartment();
        }
        public List<Department> GetDepartmentId()
        {
            return departmentGateway.GetDepartmentId();
        }

        public Department GetDepartmentById(int? id)
        {
            return departmentGateway.GetDepartmentById(id);
        }

        public List<Department> GetAllDepartments(int divisionId)
        {
            return departmentGateway.GetAllDepartments(divisionId);
        }

        public int UpdateDepartment(Department department)
        {
            int result = 0;
            if (IsNameExists(department.Name, department.DivisionId))
            {
                result = 0;
            }
            else
            {
                result = departmentGateway.UpdateDepartment(department);
            }
            return result;
        }

        public int DeleteDepartment(int? id)
        {

            return departmentGateway.DeleteDepartment(id);
        }
    }
}