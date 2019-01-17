using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BexMediaCom.Gateway;
using BexMediaCom.Models;

namespace BexMediaCom.Manager
{
    public class DivisionManager
    {
        DivisionGateway divisionGateway = new DivisionGateway();

        public bool Save(Division division)
        {

            if (IsNameExists(division.Name))
            {
                throw new Exception("Sorry!!!Name Already Exists");
            }

            return divisionGateway.Save(division) > 0;
        }

        public bool IsNameExists(string name)
        {
            return divisionGateway.IsNameExists(name);
        }

        public List<Division> ShowAllDivision()
        {
            return divisionGateway.ShowAllDivision();
        }

        public List<Division> GetAllDivisions()
        {
            return divisionGateway.GetAllDivisions();
        }

        public Division GetDivisionById(int? id)
        {
            return divisionGateway.GetDivisionById(id);
        }

        public int UpdateDivision(Division division)
        {
            int message = 0;
            if (IsNameExists(division.Name))
            {
                message = 0;
            }
            else
            {
                message = divisionGateway.UpdateDivision(division);

            }
            return message;
        }
    }
}