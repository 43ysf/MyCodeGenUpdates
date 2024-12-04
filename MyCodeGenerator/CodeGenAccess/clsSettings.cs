using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenAccess
{
    public static class clsSettings
    {

        public static string connectionString { set; get; }

        public static bool CheckConnection()
        {
            bool IsConnection = false;
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                
                try
                {
                    con.Open();
                    IsConnection = true;
                }
                catch
                {
                    IsConnection = false;
                }
               
            }
            return IsConnection;
        }

    }

}
