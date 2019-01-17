using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Sql;
using System.Linq;
using System.Web;
using BexMediaCom.Gateway;
using BexMediaCom.Models;
using BexMediaCom.ViewModels;

namespace BexMediaCom.Manager
{
    public class SectionManager
    {
        private SectionGateway _sectionGateway = new SectionGateway();

        public string SaveSection(Section section)
        {
            string msg = null;
            if (IsSectionExists(section))
            {
                msg = "Sorry!, Section Name Already Exists Under This Depaerment";
            }
            else
            {
                int result = _sectionGateway.SaveSaction(section);

                if (result > 0)
                {
                    msg = "Section Save Successfully";
                }
                else
                {
                    msg = "Section Save Failed";
                }
              
            }

            return msg;
        }

        public List<SectionView> GetAllSection()
        {
            return _sectionGateway.GetAllSection();
        }

        public Section GetSectionById(int? id)
        {
            return _sectionGateway.GetSectionById(id);
        }

        public string UpdateSection(Section section)
        {
            string msg = null;
            if (IsSectionExists(section))
            {
                msg = "Sorry!, Section Name Already Exists Under This Depaerment";
            }
            else
            {
                int result = _sectionGateway.UpdateSection(section);

                if (result > 0)
                {
                    msg = "Section Update Successfully";
                }
                else
                {
                    msg = "Section Save Failed";
                }

            }

            return msg;
        }

        public bool IsSectionExists(Section section)
        {
            return _sectionGateway.IsSectionExists(section);
        }
        public List<Department> GetAllDepartments()
        {
            return _sectionGateway.GetAllDepartments();
        }
    }
}