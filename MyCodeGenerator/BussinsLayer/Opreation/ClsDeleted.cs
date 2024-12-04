using SqlLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinsLayer
{
    public class ClsDeleted
    {

        // Business Layer..
        public static string GenerateDeleteMethod(DataTable dtColumnTable)
        {
            StringBuilder methodBuilder = new StringBuilder();
            DataRow firstRow = dtColumnTable.Rows[0];

            string firstColumnName = firstRow["ColumnName"].ToString();
            string firstDataType = ClsGloble.mapSqlTypeToCSharp(firstRow["DataType"].ToString());

            methodBuilder.AppendLine($"        public static Task<bool> Delete({firstDataType} {firstColumnName})");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine("            // Call DataAccess Layer");
            methodBuilder.AppendLine($"            return cls{ClsGloble.GetTableName}Data.Delete{ClsGloble.GetTableName}({firstColumnName});");
            methodBuilder.AppendLine("        }");

            return methodBuilder.ToString();
        }

        // Data Access Layer..
        public static string _GenerateMethodDelete(string StorName)
        {
            StringBuilder MethodBuilder = new StringBuilder();

            MethodBuilder.Append($"\tpublic static async Task<bool> Delete{ClsGloble.GetTableName}(");
            // DataRow dr = clsGlobleCsharp.GetStoredProcedureParameters(StorName).Rows[0];

            DataRow dr = ClsGloble.dataTable.Rows[0];
            string ColumnName = dr[0].ToString();
            string dataType = dr[1].ToString();
            string DataTypeCsharp = ClsGloble.mapSqlTypeToCSharp(dataType);

            MethodBuilder.Append($"{DataTypeCsharp} {ColumnName}");

            MethodBuilder.AppendLine($" ) ");
            MethodBuilder.AppendLine("\t{");

            MethodBuilder.AppendLine($"\t\tusing(SqlCommand command = new SqlCommand(\"{StorName}\"))");
            MethodBuilder.AppendLine("\t\t{");
            MethodBuilder.AppendLine("\t\t\t command.CommandType = CommandType.StoredProcedure;");
            MethodBuilder.AppendLine($"\t\t\tcommand.Parameters.AddWithValue(@\"{ColumnName}\", {ColumnName});");
            MethodBuilder.AppendLine("\t\treturn await clsPrimaryFunctions.Delete(command);");
            MethodBuilder.AppendLine("\t}");
            MethodBuilder.AppendLine("\t}");

            return MethodBuilder.ToString();
        }




        //project is not async

        //Business layer..
        public static string GenerateDeleteMethod()
        {
            StringBuilder methodBuilder = new StringBuilder();
            DataRow firstRow = ClsGloble.dataTable.Rows[0];
            string firstColumnName = firstRow["ColumnName"].ToString();
            string firstDataType = ClsGloble.mapSqlTypeToCSharp(firstRow["DataType"].ToString());

            methodBuilder.AppendLine($"        public static bool Delete({firstDataType} {firstColumnName})");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine("            // Call DataAccess Layer");
            methodBuilder.AppendLine($"            return cls{ClsGloble.GetTableName}Data.Delete{ClsGloble.GetTableName}({firstColumnName});");
            methodBuilder.AppendLine("        }");

            return methodBuilder.ToString();
        }


        //Data access Layer
        public static string _GenerateMethodDelete()
        {
            StringBuilder MethodBuilder = new StringBuilder();

            MethodBuilder.Append($"\tpublic static bool Delete{ClsGloble.GetTableName}(");

            DataRow dr = ClsGloble.dataTable.Rows[0];
            string ColumnName = dr[0].ToString();
            string dataType = dr[1].ToString();
            string DataTypeCsharp = ClsGloble.mapSqlTypeToCSharp(dataType);

            MethodBuilder.Append($"{DataTypeCsharp} {ColumnName}");

            MethodBuilder.AppendLine($" ) ");
            MethodBuilder.AppendLine("\t{");

            MethodBuilder.AppendLine($"\t\tstring query = @\"DELETE FROM {ClsGloble.GetTableName} WHERE {ColumnName} = @{ColumnName}\";");
            MethodBuilder.AppendLine("\t\tusing(SqlCommand command = new SqlCommand(query))");
            MethodBuilder.AppendLine("\t\t{");
            MethodBuilder.AppendLine($"\t\t\tcommand.Parameters.AddWithValue(\"@{ColumnName}\", {ColumnName});");
            MethodBuilder.AppendLine("\t\t\treturn CRUD.UpdateOrDelete(command);");

            MethodBuilder.AppendLine("\t\t{");

            return MethodBuilder.ToString();
        }

    }

}
