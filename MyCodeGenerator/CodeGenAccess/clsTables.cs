using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace CodeGenAccess
{
    public static class clsTables
    {

        public static  DataTable GetAll(string DatabaseName)
        {
            string Query = $"USE {DatabaseName}; SELECT name AS TableName FROM sys.tables WHERE is_ms_shipped = 0 AND name <> 'sysdiagrams';";
            SqlCommand cmd = new SqlCommand(Query); 
            return  CRUD.GetAll(cmd);
        }
        public static void OpenSQLServerAndExecuteStoredProcedure(string storedProcedureName)
        {


            // Get the project directory and create the "SqlQuery" folder if it doesn't exist
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            string projectRootPath = Path.GetFullPath(Path.Combine(projectPath, @"..\..\.."));
            using (SqlConnection conn = new SqlConnection(clsSettings.connectionString))
            {

                string sqlQueryFolderPath = Path.Combine(projectRootPath, $"{conn} StoredProceduer");

                if (!Directory.Exists(sqlQueryFolderPath))
                {
                    Directory.CreateDirectory(sqlQueryFolderPath);
                }

                // Create the SQL file path inside the "SqlQuery" folder
                string sqlFilePath = Path.Combine(sqlQueryFolderPath, $"{storedProcedureName}.sql");

                // Create the SQL content to execute the stored procedure
                string sqlContent = $"USE [{conn}];\nEXEC {storedProcedureName};";

                try
                {
                    // Write the SQL content to the file
                    File.WriteAllText(sqlFilePath, sqlContent);


                }
                catch (Exception ex)
                {
                    // Handle any exceptions (logging, showing a message, etc.)
                }
            }
        }

        public static List<string> GetAllStoredProceduresForTable(string TableName)
        {

            List<string> StoredProcedures = new List<string>();
            string queryProcedures = $@"
                                    SELECT SPECIFIC_NAME 
                                    FROM INFORMATION_SCHEMA.ROUTINES
                                    WHERE ROUTINE_TYPE = 'PROCEDURE' 
                                    AND OBJECTPROPERTY(OBJECT_ID(SPECIFIC_NAME), 'IsMSShipped') = 0
                                    AND SPECIFIC_NAME LIKE '%{TableName.Remove(TableName.Length - 1)}%'";

            try
            {
                using (SqlConnection conn = new SqlConnection(clsSettings.connectionString))
                {


                    using (SqlCommand commandProcedures = new SqlCommand(queryProcedures, conn))
                    {
                        using (SqlDataReader reader = commandProcedures.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                string procedureName = reader.GetString(0);

                                if (!string.IsNullOrEmpty(procedureName))
                                {
                                    StoredProcedures.Add(procedureName);
                                }
                            }
                        }
                    }
                }

            }
            catch 
            {
            }
            return StoredProcedures;
        }

        public static async Task<bool> IsProcedureExists(string procedureName)
        {
            string query = @"
        SELECT COUNT(*)
        FROM sys.procedures
        WHERE name = @ProcedureName";

            using (SqlConnection connection = new SqlConnection(clsSettings.connectionString))
            {
                    bool IsExist = false;
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProcedureName", procedureName);

                        int count = (int)await command.ExecuteScalarAsync();
                        IsExist = count > 0;
                       
                    }
                }
                catch (SqlException ex)
                {
                    //EntireInfoToEventLoge(ex.Message);
                    IsExist = false;
                }
               
                return IsExist;
                
            }
        }



        public static async Task<bool> ExecuteQuery(Task<string> TaskName)
        {

            try
            {
                if ( string.IsNullOrEmpty(await TaskName))
                    return false;

                using (SqlConnection con = new SqlConnection(clsSettings.connectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand(await TaskName, con))
                    {
                        //command.Parameters.AddWithValue("@")
                        await command.ExecuteNonQueryAsync();


                    }

                     return true;
                }

            }
            catch (SqlException ex)
            {
                //EntireInfoToEventLoge(ex.Message);
            }


            return false;
        }







    }
}
