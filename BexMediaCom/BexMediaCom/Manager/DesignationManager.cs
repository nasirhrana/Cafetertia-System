using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BexMediaCom.Gateway;
using BexMediaCom.Models;

namespace BexMediaCom.Manager
{
    public class DesignationManager
    {
        DesignationGateway designationGateway = new DesignationGateway();

        public bool Save(Designation designation)
        {

            if (IsNameExists(designation.Name))
            {
                throw new Exception("Sorry!!!Designation Already Exists");
            }

            return designationGateway.Save(designation) > 0;
        }

        public bool IsNameExists(string name)
        {
            return designationGateway.IsNameExists(name);
        }

        public List<Designation> ShowAllDesignation()
        {
            return designationGateway.ShowAllDesignation();
        }

        public List<Designation> GetAllDesignations()
        {
            return designationGateway.GetAllDesignations();
        }

        public Designation GetDesignationById(int? id)
        {
            return designationGateway.GetDesignationById(id);
        }

        public int UpdateDesignation(Designation designation)
        {
            int result = 0;
            if (IsNameExists(designation.Name))
            {
                result = 0;
            }
            else
            {
                result = designationGateway.UpdateDesignation(designation);
            }
            return result;
        }
    }
}