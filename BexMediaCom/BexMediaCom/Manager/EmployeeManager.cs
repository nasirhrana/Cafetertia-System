using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BexMediaCom.Gateway;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Manager
{
    public class EmployeeManager
    {
        EmployeeGateway employeeGateway = new EmployeeGateway();
        public bool IsEmployeeIdExist(string employeeId)
        {
            return employeeGateway.IsEmployeeIdExist(employeeId);
        }

        public bool Save(Employee employee)
        {
            if (IsEmployeeIdExist(employee.EmpId))
            {
                throw new Exception("Employee Already Exists");
            }

            return employeeGateway.Save(employee) > 0;
        }

        public List<Employee> GetEmployeeId()
        {
            return employeeGateway.GetEmployeeId();
        }

        public List<EmployeeView> GetAllEmployee()
        {
            return employeeGateway.GetAllEmployee();
        }

        public Employee GetEmployeeById(int? id)
        {
            return employeeGateway.GetEmployeeById(id);
        }

        public EmployeeMonitoring GetEmployeeByEmpId(string id)
        {
            return employeeGateway.GetEmployeeByEmpId(id);
        }
        public int GetFoodItemCostIdByTime(string id)
        {
            return employeeGateway.GetFoodItemCostIdByTime(id);
        }
        public ShowEmployeeMonitoring GetEmployeeByEmpId1(string id, int foodItemId)
        {
            return employeeGateway.GetEmployeeByEmpId1(id, foodItemId);
        }
        public ShowEmployeeMonitoring GetEmployeeByEmpId2(string id, int foodItemId)
        {
            return employeeGateway.GetEmployeeByEmpId2(id, foodItemId);
        }
        public ShowEmployeeMonitoring GetEmployeeByEmpId1235(string id, int foodItemId, string date)
        {
            return employeeGateway.GetEmployeeByEmpId1235(id, foodItemId, date);
        }

        public bool GetEmployeeByEmpId12(string id, int foodItemId)
        {
            return employeeGateway.GetEmployeeByEmpId12(id, foodItemId);
        }

        public bool GetEmployeeByEmpId123(string id, int foodItemId, string date)
        {
            return employeeGateway.GetEmployeeByEmpId123(id, foodItemId, date);
        }
        public EmployeeMonitoring GetListofuser(EmployeeCafeteriaTransaction cafeteriaa)
        {
            return employeeGateway.GetListofuser(cafeteriaa);
        }

        public bool IsEmployeeExist(string empId)
        {
            return employeeGateway.IsEmployeeExist(empId);
        }

        public bool UpdateEmployee(Employee employee)
        {
            return employeeGateway.UpdateEmployee(employee) > 0;
        }

        public bool UpdateEmployeeWithoutImage(Employee employee)
        {
            return employeeGateway.UpdateEmployeeWithoutImage(employee) > 0;
        }

        public List<CardRegistration> GetAllCardRegistrations()
        {
            return employeeGateway.GetAllCardRegistrations();
        }

        public string GetUserNumber()
        {
            return employeeGateway.GetUserNumber();
        }

        public List<Section> GetAllSection(int departmentId)
        {
            return employeeGateway.GetAllSection(departmentId);
        }

        public bool IsFoodItemTimeExist(string id, int foodItemId)
        {
            return employeeGateway.IsFoodItemTimeExist(id, foodItemId);
        }
    }
}