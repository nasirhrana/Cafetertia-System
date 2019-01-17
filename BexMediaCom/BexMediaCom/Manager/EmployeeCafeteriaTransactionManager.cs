using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BexMediaCom.Gateway;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Manager
{
    public class EmployeeCafeteriaTransactionManager
    {
        private EmployeeCafeteriaTransactionGateway _employee = new EmployeeCafeteriaTransactionGateway();
        public bool Save(EmployeeCafeteriaTransaction cafeteria)
        {
            return _employee.Save(cafeteria) > 0;
        }

        public int SaveRecord(EmployeeCafeteriaTransaction cafeteriaa)
        {
            return _employee.SaveRecord(cafeteriaa);
        }
        public int SaveRecord1(EmployeeCafeteriaTransaction cafeteriaa)
        {
            return _employee.SaveRecord1(cafeteriaa);
        }
        public int ConfirmMeal(EmployeeCafeteriaTransaction cafeteriaa)
        {
            return _employee.ConfirmMeal(cafeteriaa);
        }
        public bool IsEmployeeExist(string empId, int foodItemId, string time, string date)
        {
            return _employee.IsEmployeeExist(empId, foodItemId, time, date);
        }

        public bool IsEmployeeExist191(string empId, int foodItemId, string time, string date)
        {
            return _employee.IsEmployeeExist191(empId, foodItemId, time, date);
        }
        public bool IsEmployeeExist0901(string empId, int foodItemId, string time, string date)
        {
            return _employee.IsEmployeeExist0901(empId, foodItemId, time, date);
        }

        public bool CheckList(string empId, int foodItemId, string time, string date)
        {
            return _employee.CheckList(empId, foodItemId, time, date);
        }
        public bool IsEmployeeExist1(string empId, string time, string date)
        {
            return _employee.IsEmployeeExist1(empId ,time, date);
        }
        public bool IsEmployeeExist101(string empId, string time, string date)
        {
            return _employee.IsEmployeeExist101(empId, time, date);
        }

        public bool IsEmployeeExist09011(string empId, string time, string date, int foodItemId)
        {
            return _employee.IsEmployeeExist09011(empId, time, date, foodItemId);
        }       
        public bool IsEmployeeExist090112(string empId, string time, string date, int foodItemId)
        {
            return _employee.IsEmployeeExist090112(empId, time, date, foodItemId);
        }
        public List<EmployeeTransaction> GetDayTransaction(string date)
        {
            return _employee.GetDayTransaction(date);
        }

        public int TransactionDelete(int? id)
        {
            return _employee.TransactionDelete(id);
        }
    }
}