using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenAccess
{
    public static class clsProcedures
    {
        public static DataTable GetAll(string TableName)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(clsSettings.connectionString);
            string Query = $@"SELECT SPECIFIC_NAME 
                              FROM INFORMATION_SCHEMA.ROUTINES
                              WHERE ROUTINE_TYPE = 'PROCEDURE' 
                              AND OBJECTPROPERTY(OBJECT_ID(SPECIFIC_NAME), 'IsMSShipped') = 0
                              AND SPECIFIC_NAME LIKE '%{TableName.Remove(TableName.Length - 1)}%'";
            SqlCommand cmd = new SqlCommand(Query, con);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    dt.Load(reader);
                    con.Close();
                }
            }
            catch
            {
                dt = null;
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
    }
}
