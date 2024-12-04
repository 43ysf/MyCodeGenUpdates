using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CodeGenAccess;
using CodeGenBuisness;
using Microsoft.SqlServer.Server;
using MyC_AndSQLLib;

namespace CodeGenBusiness
{
    public partial class clsTable
    {
        private string _GenerateAddNewForDataAccess(string StorName)
        {
            StringBuilder MethodBuilder = new StringBuilder();
            MethodBuilder.Append($"\tpublic static async Task<int?> AddNew{this.TableName}(");

            MethodBuilder.AppendLine(_GenerateMethodParameters(false, false) + ")");

            MethodBuilder.AppendLine("\t{");
            MethodBuilder.AppendLine($"\t\tusing(SqlCommand command = new SqlCommand(\"{StorName}\"))");
            MethodBuilder.AppendLine("\t\t\t{");
            MethodBuilder.AppendLine("\t\t\t\t\tcommand.CommandType = CommandType.StoredProcedure;");
            MethodBuilder.AppendLine(GenerateSqlParameters(false));
            MethodBuilder.AppendLine("\t\t\t\treturn await clsPrimaryFunctions.Add(command);");
            MethodBuilder.AppendLine("\t\t\t}");
            MethodBuilder.AppendLine("\t\t}");

            return MethodBuilder.ToString();
        }
        private string _GenerateMethodDelete(string StorName)
        {
            StringBuilder MethodBuilder = new StringBuilder();

            MethodBuilder.Append($"\tpublic static async Task<bool> Delete{this.TableName}(");
            // DataRow dr = clsGlobleCsharp.GetStoredProcedureParameters(StorName).Rows[0];
            clsColumn PrimaryCol = _GetPrimaryKeyColumn();
            MethodBuilder.Append($"{PrimaryCol.ColumnType} {PrimaryCol.ColumnName}");

            MethodBuilder.AppendLine($" ) ");
            MethodBuilder.AppendLine("\t{");

            MethodBuilder.AppendLine($"\t\tusing(SqlCommand command = new SqlCommand(\"{StorName}\"))");
            MethodBuilder.AppendLine("\t\t{");
            MethodBuilder.AppendLine("\t\t\t command.CommandType = CommandType.StoredProcedure;");
            MethodBuilder.AppendLine($"\t\t\tcommand.Parameters.AddWithValue(\"@{PrimaryCol.ColumnName}\", {PrimaryCol.ColumnName});");
            MethodBuilder.AppendLine("\t\treturn await clsPrimaryFunctions.Delete(command);");
            MethodBuilder.AppendLine("\t}");
            MethodBuilder.AppendLine("\t}");

            return MethodBuilder.ToString();
        }
        private string _GenerateMethodUpdate(string StorName)
        {
            StringBuilder MethodBuilder = new StringBuilder();
            MethodBuilder.Append($"\tpublic static async Task<bool?> Update{this.TableName}(");
            MethodBuilder.AppendLine(_GenerateMethodParameters(true, false) + ")");

            MethodBuilder.AppendLine("\t{");
            MethodBuilder.AppendLine($"\t\tusing(SqlCommand command = new SqlCommand(\"{StorName}\"))");
            MethodBuilder.AppendLine("\t\t{");
            MethodBuilder.AppendLine("\t\tcommand.CommandType= CommandType.StoredProcedure;");
            MethodBuilder.AppendLine(GenerateSqlParameters(true));
            MethodBuilder.AppendLine("\t\treturn await clsPrimaryFunctions.Update(command);");
            MethodBuilder.AppendLine("\t\t}");
            MethodBuilder.AppendLine("\t}");

            return MethodBuilder.ToString();
        }
        private string _GetAllForDataAccess(string StorName)
        {
            StringBuilder MethodBuilder = new StringBuilder();
            MethodBuilder.AppendLine($"\t\t\tpublic static async Task<DataTable> GetAll{this.TableName}()");
            MethodBuilder.AppendLine("\t\t\t{");
            MethodBuilder.AppendLine($"\t\t\t\t using(SqlCommand command = new SqlCommand(\"{StorName}\"))");
            MethodBuilder.AppendLine("\t\t\t{");
            MethodBuilder.AppendLine("\t\t\t\t\tcommand.CommandType = CommandType.StoredProcedure;");
            MethodBuilder.AppendLine("\t\t\t\t\treturn await clsPrimaryFunctions.Get(command);");
            MethodBuilder.AppendLine("\t\t\t\t}");
            MethodBuilder.AppendLine("\t\t\t}");
            return MethodBuilder.ToString();

        }
        private string _GetFindByIDForDataAccess(string StorName)
        {

            StringBuilder MethodBuilder = new StringBuilder();

            MethodBuilder.Append($"\tpublic static bool FindByID(");

            clsColumn PrimaryCol = _GetPrimaryKeyColumn();

            //MethodBuilder.Append($"{PrimaryCol.ColumnType} {PrimaryCol.ColumnName}, ");
            clsColumn PrimCol = _GetPrimaryKeyColumn();

            MethodBuilder.AppendLine(_GenerateMethodParameters(true, true) + ")");
            MethodBuilder.AppendLine("\t{");
            MethodBuilder.AppendLine("\t\tbool isFound = false;");
            MethodBuilder.AppendLine($"\t\tusing(SqlConnection connection = new SqlConnection(\"{clsSettings.connectionString}\"))");
            MethodBuilder.AppendLine("\t\t{");
            MethodBuilder.AppendLine($"\t\tusing(SqlCommand command = new SqlCommand(\"{StorName}\", connection))");
            MethodBuilder.AppendLine("\t\t{");
            MethodBuilder.AppendLine("\t\tcommand.CommandType = CommandType.StoredProcedure;");
            MethodBuilder.AppendLine($"\t\t\tcommand.Parameters.AddWithValue(\"@{PrimaryCol.ColumnName}\", {PrimaryCol.ColumnName});");
            MethodBuilder.AppendLine("\t\t\ttry");
            MethodBuilder.AppendLine("\t\t{");
            MethodBuilder.AppendLine("\t\t\tconnection.Open();");
            MethodBuilder.AppendLine("\t\t\tusing(SqlDataReader reader = command.ExecuteReader())");
            MethodBuilder.AppendLine("\t\t\t{");
            MethodBuilder.AppendLine("\t\t\tif (reader.Read())");
            MethodBuilder.AppendLine("\t\t\t{");
            MethodBuilder.AppendLine("\t\t\t\tisFound = true;");

            MethodBuilder.AppendLine(_GenerateReaderAssignments());
            MethodBuilder.AppendLine("\t\t\t}");
            MethodBuilder.AppendLine("\t\t\telse");
            MethodBuilder.AppendLine("\t\t\t{");
            MethodBuilder.AppendLine("\t\t\t\tisFound = false;");
            MethodBuilder.AppendLine("\t\t\t}");
            MethodBuilder.AppendLine("\t\t}");
            MethodBuilder.AppendLine("\t\t}");
            MethodBuilder.AppendLine("\t\tcatch (Exception ex)");
            MethodBuilder.AppendLine("\t\t{");
            MethodBuilder.AppendLine("\t\t\tisFound = false;");
            MethodBuilder.AppendLine("\t\t\tclsPrimaryFunctions.EntireInfoToEventLoge(ex.Message);");
            MethodBuilder.AppendLine("\t\t}");
            MethodBuilder.AppendLine("\t\t}");
            MethodBuilder.AppendLine("\t\t}");
            MethodBuilder.AppendLine("\t\treturn isFound;");
            MethodBuilder.AppendLine("\t}");

            return MethodBuilder.ToString();
        }

        //private string _GenerateMethodFindByName(string StorName)
        //{
        //    StringBuilder MethodBuilder = new StringBuilder();

        //    MethodBuilder.Append($"\tpublic static bool Find(");

        //    for (int i = 0; i < Columns.Count; i++)
        //    {

        //        if (i == 1)
        //        {
        //            MethodBuilder.Append($"{Columns[i].ColumnType} {Columns[i].ColumnName}");
        //        }
        //        else
        //        {
        //            MethodBuilder.Append($" ref {Columns[i].ColumnType} {Columns[i].ColumnName}");
        //        }

        //        if (i < Columns.Count - 1)
        //        {
        //            MethodBuilder.Append($", ");
        //        }
        //    }

        //    MethodBuilder.AppendLine($" ) ");
        //    MethodBuilder.AppendLine("\t{");
        //    MethodBuilder.AppendLine("\t\tbool isFound = false;");
        //    MethodBuilder.AppendLine($"\t\tusing(SqlConnection connection = new SqlConnection(\"{clsSettings.connectionString}\"))");
        //    MethodBuilder.AppendLine("\t\t{");

        //    MethodBuilder.AppendLine($"\t\tusing(SqlCommand command = new SqlCommand(\"{StorName}\", connection))");
        //    MethodBuilder.AppendLine("\t\t{");
        //    MethodBuilder.AppendLine("\t\tcommand.CommandType = CommandType.StoredProcedure;");
        //    MethodBuilder.AppendLine($"\t\tcommand.Parameters.AddWithValue(\"@{Columns[1].ColumnName}\", {Columns[1].ColumnName});");
        //    MethodBuilder.AppendLine("\t\t\ttry");
        //    MethodBuilder.AppendLine("\t\t{");
        //    MethodBuilder.AppendLine("\t\t\tconnection.Open();");
        //    MethodBuilder.AppendLine("\t\t\tusing(SqlDataReader reader = command.ExecuteReader())");
        //    MethodBuilder.AppendLine("\t\t\t{");
        //    MethodBuilder.AppendLine("\t\t\tif (reader.Read())");
        //    MethodBuilder.AppendLine("\t\t\t{");
        //    MethodBuilder.AppendLine("\t\t\t\tisFound = true;");

        //    MethodBuilder.AppendLine(_GenerateReaderAssignments());
        //    MethodBuilder.AppendLine("\t\t\t}");
        //    MethodBuilder.AppendLine("\t\t\telse");
        //    MethodBuilder.AppendLine("\t\t\t{");
        //    MethodBuilder.AppendLine("\t\t\t\tisFound = false;");
        //    MethodBuilder.AppendLine("\t\t\t}");
        //    MethodBuilder.AppendLine("\t\t}");
        //    MethodBuilder.AppendLine("\t\t}");
        //    MethodBuilder.AppendLine("\t\tcatch (Exception ex)");
        //    MethodBuilder.AppendLine("\t\t{");
        //    MethodBuilder.AppendLine("\t\t\tisFound = false;");
        //    MethodBuilder.AppendLine("\t\t\tclsPrimaryFunctions.EntireInfoToEventLoge(ex.Message);");
        //    MethodBuilder.AppendLine("\t\t}");
        //    MethodBuilder.AppendLine("\t\t}");
        //    MethodBuilder.AppendLine("\t\t}");
        //    MethodBuilder.AppendLine("\t\treturn isFound;");
        //    MethodBuilder.AppendLine("\t}");

        //    return MethodBuilder.ToString();

        //}
        private string GenerateSqlParameters(bool TakePrimary)
        {

            StringBuilder parametersBuilder = new StringBuilder();

            for (int i = 0; i < Columns.Count; i++)
            {
                if (TakePrimary)
                {

                    parametersBuilder.AppendLine($@"                command.Parameters.AddWithValue(""@{Columns[i].ColumnName}"", {Columns[i].ColumnName});");

                }
                else
                {
                    if (!Columns[i].IsPrimaryKey)
                        parametersBuilder.AppendLine($@"                command.Parameters.AddWithValue(""@{Columns[i].ColumnName}"", {Columns[i].ColumnName});");

                }

            }


            return parametersBuilder.ToString();
        }
        private string _HeadrClassForDataAccess()
        {
            StringBuilder classBuilder = new StringBuilder();

            classBuilder.AppendLine("using System;");
            classBuilder.AppendLine("using System.Data;");
            classBuilder.AppendLine("using System.Text;");
            classBuilder.AppendLine("using System.Linq;");
            classBuilder.AppendLine("using System.Data.SqlClient;");
            classBuilder.AppendLine("using System.Threading.Tasks;");
            classBuilder.AppendLine("");
            classBuilder.AppendLine($"\tpublic static class cls{this.TableName}Data");
            classBuilder.AppendLine("\t{");
            return classBuilder.ToString();
        }

        public string CreateSettingClass()
        {
            StringBuilder classBuilder = new StringBuilder();

            classBuilder.AppendLine("using System; ");
            classBuilder.AppendLine("using System.Data; ");
            classBuilder.AppendLine("using System.Text; ");
            classBuilder.AppendLine("using System.Data.SqlClient;\n");
            classBuilder.AppendLine();

            classBuilder.AppendLine("public class ClsSetting \n\n");
            classBuilder.AppendLine("{\n");
            classBuilder.AppendLine($"       public static string ConnectionString = \"{con}\";");
            classBuilder.AppendLine("}");



            return classBuilder.ToString();


        }

        public string ClassPrimaryFunction()
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
                "\r\n    public class clsPrimaryFunctions" +
                "\r\n    {" +
                "\r\n      " +
                "\r\n        public static void EntireInfoToEventLoge(string Information)" +
                "\r\n        {" +
                $"\r\n            string SourceName = \"({DatabaseName} system)\";" +
                "\r\n            if (!EventLog.SourceExists(SourceName))" +
                "\r\n            {" +
                "\r\n                EventLog.CreateEventSource(SourceName, \"Application\");" +
                "\r\n            }" +
                "\r\n\r\n            EventLog.WriteEntry(SourceName, Information, EventLogEntryType.Error);" +
                "\r\n\r\n        }" +
                "\r\n        public static async Task<int?> Add(SqlCommand command)" +
                "\r\n        {" +
                "\r\n            int? ID = null;" +
                "\r\n\r\n            using (SqlConnection connection = new SqlConnection(ClsSetting.ConnectionString))" +
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
                "\r\n\r\n            using (SqlConnection connection = new SqlConnection(ClsSetting.ConnectionString))" +
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
                "\r\n\r\n            using (SqlConnection connection = new SqlConnection(ClsSetting.ConnectionString))" +
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
                "\r\n\r\n            using (SqlConnection connection = new SqlConnection(ClsSetting.ConnectionString))" +
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
                "\r\n\r\n            using (SqlConnection connection = new SqlConnection(ClsSetting.ConnectionString))" +
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
                "\r\n");
            return builder.ToString();
        }

        public string GenerateAllDtataAccessLayerMethods()
        {

            StringBuilder classBuilder = new StringBuilder();

            classBuilder.AppendLine(_HeadrClassForDataAccess());
            var methodGenerators = new Dictionary<string, Func<string, string>>(StringComparer.OrdinalIgnoreCase)
            {
                { "add", _GenerateAddNewForDataAccess },
                { "update", _GenerateMethodUpdate },
                { "delete", _GenerateMethodDelete },
                { "getall", _GetAllForDataAccess },
                { "byid", _GetFindByIDForDataAccess },
                //{ "byname", _GenerateMethodFindByName }
            };

            foreach (clsProcedure procedure in StoredProcedures)
            {
                foreach (var pattern in methodGenerators.Keys)
                {
                    if (Regex.IsMatch(procedure.Name, pattern, RegexOptions.IgnoreCase))
                    {
                        classBuilder.AppendLine(methodGenerators[pattern](procedure.Name));
                        break;
                    }
                }
            }

            classBuilder.AppendLine("}");

            return classBuilder.ToString();
        }

    }
}
