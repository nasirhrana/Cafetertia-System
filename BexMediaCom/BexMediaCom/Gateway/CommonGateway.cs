using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace BexMediaCom.Gateway
{
    public class CommonGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["BexMediaCom"].ConnectionString;
        public SqlConnection Connection { set; get; }
        public SqlCommand Command { set; get; }
        public SqlDataReader Reader { set; get; }
        public string Query { set; get; }

        public CommonGateway()
        {
            Connection = new SqlConnection(connectionString);
            Command = new SqlCommand();
        }
    }
}