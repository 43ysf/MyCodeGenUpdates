using System.Data;
using System.Data.SqlClient;

namespace CodeGenAccess
{
    public static class clsParameters
    {
//        public static DataTable GetStoredProcedureParameters(string storedProcedureName)
//        {

//            string query = $@"
//   SELECT 
//    SUBSTRING(PARAMETER_NAME, 2, LEN(PARAMETER_NAME) - 1) AS PARAMETER_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH,
//CASE 
//        WHEN CHARACTER_MAXIMUM_LENGTH IS NULL THEN 'Length is NULL'
//        ELSE 'is NOT NULL'
//    END AS LengthStatus
//FROM 
//    INFORMATION_SCHEMA.PARAMETERS
//WHERE 
//    SPECIFIC_NAME = '{storedProcedureName}'
//    AND PARAMETER_MODE = 'IN'
//ORDER BY 
//    ORDINAL_POSITION;";


//            using (SqlCommand command = new SqlCommand(query, ClsGloble.GetServer))
//            {

//                SqlDataAdapter adapter = new SqlDataAdapter(command);
//                DataTable resultTable = new DataTable();
//                adapter.Fill(resultTable);
//                return resultTable;
//            }

//        }


        public static DataTable GetAllSotredProcedureParameters(string StoredProcedureName)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(clsSettings.connectionString);
            string Query = @"SELECT 
                            PARAMETER_NAME AS 'ParameterName', 
                            DATA_TYPE AS 'DataType'
                        FROM 
                            INFORMATION_SCHEMA.PARAMETERS
                        WHERE 
                            SPECIFIC_NAME = @StoredProcedureName;
                        ";
            SqlCommand cmd = new SqlCommand(Query, conn) ;
            cmd.Parameters.AddWithValue("@StoredProcedureName", StoredProcedureName);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }catch
            {
                dt = new DataTable();
            }
            finally
            { conn.Close(); }
            return dt;
        }

    }
}
