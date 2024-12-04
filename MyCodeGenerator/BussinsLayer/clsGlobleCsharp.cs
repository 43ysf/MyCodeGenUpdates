using SqlLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinsLayer
{
    public class clsGlobleCsharp
    {
        //public static string GetPathMyWorkDeskTop()
        //{
        //    string desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        //    string myWorkPath = Path.Combine(desktopFolder, "All My Work");
        //    return myWorkPath;
        //}

        // دالة لتعبئة براميتر اليثود Add.. and update...  مع نقطة بدابة  index البراميتر

        public static string GenerateMethodParameters(DataTable dataTable, int StartIndex = 0)
        {
            if (dataTable == null || dataTable.Rows.Count == 0)
                return string.Empty;

            StringBuilder parameters = new StringBuilder();
            int rowCount = dataTable.Rows.Count;

            for (int i = StartIndex; i <= rowCount - 1; i++)
            {
                DataRow row = dataTable.Rows[i];
                string columnName = row[0].ToString();
                string dataType = ClsGloble.mapSqlTypeToCSharp(row[1].ToString());

                parameters.Append($"{dataType} {columnName}");

                if (i < rowCount - 1)
                    parameters.Append(", ");
            }

            return parameters.ToString();
        }

        // دالة  لتعبئة براميتر ال SQLcommand..
        public static string GenerateSqlParameters(DataTable columnTable, int index)
        {
            if (columnTable == null || columnTable.Rows.Count == 0)
                return string.Empty;

            StringBuilder parametersBuilder = new StringBuilder();
            string columnName = "";


            int rowCount = columnTable.Rows.Count;

            for (int i = index; i <= rowCount - 1; i++)
            {
                DataRow row= columnTable.Rows[i];
                columnName = row[0].ToString();
               // string csharpDataType = ClsGloble.mapSqlTypeToCSharp(row[1].ToString());
               // bool requiresNullCheck = RequiresNullCheck(csharpDataType);

               // parametersBuilder.AppendLine(
               //requiresNullCheck
               //? $@"                command.Parameters.AddWithValue(""@{columnName}"", {columnName}==null ? (object)DBNull.Value:(object){columnName});"
               //: $@"                command.Parameters.AddWithValue(""@{columnName}"", {columnName});");

                parametersBuilder.AppendLine($@"                command.Parameters.AddWithValue(""@{columnName}"", {columnName});");

            }

           // string csharpDataType = ClsGloble.mapSqlTypeToCSharp(row[1].ToString());
           // bool requiresNullCheck = RequiresNullCheck(csharpDataType);

           // parametersBuilder.AppendLine(
           //requiresNullCheck
           //? $@"                command.Parameters.AddWithValue(""@{columnName}"", {columnName} ?? (object)DBNull.Value);"
           //: $@"                command.Parameters.AddWithValue(""@{columnName}"", {columnName});");



            return parametersBuilder.ToString();
        }

        //Check Type I's Null In SQL
        private static bool RequiresNullCheck(string dataType)
        {
            return dataType == "string" || dataType == "byte[]";
        }

        //Method Full Function Primeter
        public static string GenerateRefParameters(DataTable columnTable)
        {
            if (columnTable == null || columnTable.Rows.Count == 0)
                return string.Empty;

            StringBuilder parametersBuilder = new StringBuilder();

            for (int i = 1; i < columnTable.Rows.Count; i++)
            {
                DataRow row = columnTable.Rows[i];
                string columnName = row[0].ToString();
                string sqlDataType = row[1].ToString();
                string csharpDataType = ClsGloble.mapSqlTypeToCSharp(sqlDataType);

                parametersBuilder.Append($"ref {csharpDataType} {columnName}");

                if (i < columnTable.Rows.Count - 1)
                    parametersBuilder.Append(", ");
            }

            return parametersBuilder.ToString();
        }

        //
        public static string GenerateReaderAssignments(DataTable columnTable)
        {
            if (columnTable == null || columnTable.Rows.Count <= 1)
                return string.Empty;

            var assignmentBuilder = new StringBuilder();

            for (int i = 1; i < columnTable.Rows.Count; i++)
            {
                DataRow row = columnTable.Rows[i];
                string columnName = row[0].ToString();
                string sqlDataType = row[1].ToString();
                string csharpDataType = ClsGloble.mapSqlTypeToCSharp(sqlDataType);
                string allowNull = row[2].ToString();
                
                if (allowNull=="Yes")
                {
                    if (csharpDataType == "int" || csharpDataType == "short")
                        assignmentBuilder.AppendLine($"\t\t\t\t{columnName} = reader[\"{columnName}\"] != DBNull.Value ? ({csharpDataType})reader[\"{columnName}\"] : -1;");
                    else
                        assignmentBuilder.AppendLine($"\t\t\t\t{columnName} = reader[\"{columnName}\"] != DBNull.Value ? ({csharpDataType})reader[\"{columnName}\"] : null;");

                }
                else
                {
                    assignmentBuilder.AppendLine($"\t\t\t\t{columnName} = ({csharpDataType})reader[\"{columnName}\"];");
                }
            }

            return assignmentBuilder.ToString();
        }

        public static string GenerateTheValueOfVariable(string dataType)
        {
            switch (dataType.ToLower())
            {
                case "int": return "-1";
                case "string": return "\"\"";
                case "datetime": return "DateTime.MinValue";
                case "bool": return "false";
                case "varbinary(max)": return "null";
                case "float": return "0.0f";
                case "double": return "0.0";
                case "decimal": return "0.0m";
                case "char": return "'\\0'";
                default: return "null";
            }
        }

        // كلاس البرايمري فكشن
        public static string ClassPrimaryFunction()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using System;" +
                "\r\nusing System.Collections.Generic;" +
                "\r\nusing System.Data.SqlClient;" +
                "\r\nusing System.Data;" +
                "\r\nusing System.Diagnostics;" +
                "\r\nusing System.Linq;" +
                "\r\nusing System.Text;" +
                "\r\nusing System.Threading.Tasks;" +
                "\r\n\r\nnamespace PrimaryFunctions" +
                "\r\n{ " +
                "\r\n    public class clsPrimaryFunctions" +
                "\r\n    {" +
                "\r\n      " +
                "\r\n        public static void EntireInfoToEventLoge(string Information)" +
                "\r\n        {" +
                "\r\n            string SourceName = \"(clinic Management system)\";" +
                "\r\n            if (!EventLog.SourceExists(SourceName))" +
                "\r\n            {" +
                "\r\n                EventLog.CreateEventSource(SourceName, \"Application\");" +
                "\r\n            }" +
                "\r\n\r\n            EventLog.WriteEntry(SourceName, Information, EventLogEntryType.Error);" +
                "\r\n\r\n        }" +
                "\r\n        public static async Task<int?> Add(SqlCommand command)" +
                "\r\n        {" +
                "\r\n            int? ID = null;" +
                "\r\n\r\n            using (SqlConnection connection = new SqlConnection(clsConnectionString.Connection))" +
                "\r\n            {" +
                "\r\n                using (command.Connection = connection)" +
                "\r\n                {" +
                "\r\n                    try" +
                "\r\n                    {" +
                "\r\n                        await connection.OpenAsync();" +
                "\r\n\r\n                        object result = await command.ExecuteScalarAsync();" +
                "\r\n\r\n                        if (result != null && int.TryParse(result.ToString(), out int insertedID))" +
                "\r\n                        {" +
                "\r\n                            ID = insertedID;" +
                "\r\n                        }" +
                "\r\n\r\n                    }" +
                "\r\n\r\n                    catch (Exception ex)" +
                "\r\n                    {" +
                "\r\n                        EntireInfoToEventLoge(ex.Message);" +
                "\r\n                    }" +
                "\r\n\r\n                    return ID;" +
                "\r\n                }" +
                "\r\n            }" +
                "\r\n        }" +
                "\r\n        public static async Task<DataTable> Get(SqlCommand command)" +
                "\r\n        {" +
                "\r\n            DataTable GetData = new DataTable();" +
                "\r\n\r\n            using (SqlConnection connection = new SqlConnection(clsConnectionString.Connection))" +
                "\r\n            {" +
                "\r\n\r\n                using (command.Connection = connection)" +
                "\r\n                {" +
                "\r\n                    command.CommandType = CommandType.StoredProcedure;" +
                "\r\n                    try" +
                "\r\n                    {" +
                "\r\n                        await connection.OpenAsync();" +
                "\r\n\r\n                        using (SqlDataReader reader = await command.ExecuteReaderAsync())" +
                "\r\n                        {" +
                "\r\n\r\n                            if (reader.HasRows)" +
                "\r\n\r\n                            {" +
                "\r\n                                GetData.Load(reader);" +
                "\r\n\r\n                            }" +
                "\r\n                        }" +
                "\r\n                    }" +
                "\r\n\r\n                    catch (Exception ex)" +
                "\r\n                    {" +
                "\r\n                        EntireInfoToEventLoge(ex.Message);" +
                "\r\n                    }" +
                "\r\n                }" +
                "\r\n            }" +
                "\r\n\r\n            return GetData;" +
                "\r\n        }" +
                "\r\n        public static async Task<bool?> Update(SqlCommand command)" +
                "\r\n        {" +
                "\r\n            int? RowsAffected = null;" +
                "\r\n\r\n            using (SqlConnection connection = new SqlConnection(clsConnectionString.Connection))" +
                "\r\n            {" +
                "\r\n                using (command.Connection = connection)" +
                "\r\n                {" +
                "\r\n\r\n                    try" +
                "\r\n                    {" +
                "\r\n                        await connection.OpenAsync();" +
                "\r\n\r\n                        RowsAffected = await command.ExecuteNonQueryAsync();" +
                "\r\n\r\n                    }" +
                "\r\n                    catch (Exception ex)" +
                "\r\n                    {" +
                "\r\n                        EntireInfoToEventLoge(ex.Message);" +
                "\r\n                    }" +
                "\r\n                }" +
                "\r\n\r\n                return (RowsAffected > 0);" +
                "\r\n            }" +
                "\r\n        }" +
                "\r\n        public static async Task<bool> Delete(SqlCommand command)" +
                "\r\n        {" +
                "\r\n            int? RowsAffected = null;" +
                "\r\n\r\n            using (SqlConnection connection = new SqlConnection(clsConnectionString.Connection))" +
                "\r\n            {" +
                "\r\n\r\n                using (command.Connection = connection)" +
                "\r\n                {" +
                "\r\n\r\n                    try" +
                "\r\n                    {" +
                "\r\n                        connection.Open();" +
                "\r\n\r\n                        RowsAffected = await command.ExecuteNonQueryAsync();" +
                "\r\n\r\n                    }" +
                "\r\n                    catch (Exception ex)" +
                "\r\n                    {" +
                "\r\n                        EntireInfoToEventLoge(ex.Message);" +
                "\r\n                    }" +
                "\r\n\r\n                    return (RowsAffected > 0);" +
                "\r\n                }" +
                "\r\n            }" +
                "\r\n        }" +
                "\r\n        public static async Task<bool> Exist(SqlCommand command)" +
                "\r\n        {" +
                "\r\n\r\n            bool? isFound = null;" +
                "\r\n\r\n            using (SqlConnection connection = new SqlConnection(clsConnectionString.Connection))" +
                "\r\n            {" +
                "\r\n                using (command.Connection = connection)" +
                "\r\n                {" +
                "\r\n                    try" +
                "\r\n                    {" +
                "\r\n                        await connection.OpenAsync();" +
                "\r\n                        using (SqlDataReader reader = await command.ExecuteReaderAsync())" +
                "\r\n                        {" +
                "\r\n                            isFound = reader.HasRows;" +
                "\r\n\r\n                        }" +
                "\r\n                    }" +
                "\r\n                    catch (Exception ex)" +
                "\r\n                    {" +
                "\r\n                        EntireInfoToEventLoge(ex.Message);" +
                "\r\n                    }" +
                "\r\n                }" +
                "\r\n            }" +
                "\r\n\r\n            return isFound.Value;" +
                "\r\n        }" +
                "\r\n\r\n    }" +
                "\r\n}" +
                "\r\n");
            return builder.ToString();
        }

        public static DataTable GetStoredProcedureParameters(string storedProcedureName)
        {

            string query = $@"
   SELECT 
    SUBSTRING(PARAMETER_NAME, 2, LEN(PARAMETER_NAME) - 1) AS PARAMETER_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH,
CASE 
        WHEN CHARACTER_MAXIMUM_LENGTH IS NULL THEN 'Length is NULL'
        ELSE 'is NOT NULL'
    END AS LengthStatus
FROM 
    INFORMATION_SCHEMA.PARAMETERS
WHERE 
    SPECIFIC_NAME = '{storedProcedureName}'
    AND PARAMETER_MODE = 'IN'
ORDER BY 
    ORDINAL_POSITION;";


            using (SqlCommand command = new SqlCommand(query, ClsGloble.GetServer))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable resultTable = new DataTable();
                adapter.Fill(resultTable);
                return resultTable;
            }
            
        }

    }

}
