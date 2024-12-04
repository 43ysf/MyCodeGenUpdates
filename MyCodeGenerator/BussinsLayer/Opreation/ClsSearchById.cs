using SqlLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinsLayer
{
    public  class ClsSearchById
    {
        //Business Layer..
        public static string GenerateFindByIDMethod(DataTable dtColumnTable)
        {
            StringBuilder methodBuilder = new StringBuilder();
            DataRow firstRow = dtColumnTable.Rows[0];
            string firstColumnName = firstRow["ColumnName"].ToString();
            string firstDataType = ClsGloble.mapSqlTypeToCSharp(firstRow["DataType"].ToString());

            methodBuilder.AppendLine($"        public static cls{ClsGloble.GetTableName} Find({firstDataType} {firstColumnName})");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine("            // Call DataAccess Layer");

            for (int i = 1; i < dtColumnTable.Rows.Count; i++)
            {
                DataRow dr = dtColumnTable.Rows[i];
                string columnName = dr["ColumnName"].ToString();
                string dataType = ClsGloble.mapSqlTypeToCSharp(dr["DataType"].ToString());
                string value = clsGlobleCsharp.GenerateTheValueOfVariable(dataType);
                methodBuilder.AppendLine($"            {dataType} {columnName} = {value};");
            }

            methodBuilder.AppendLine();
            methodBuilder.Append($"\t        bool IsFound = cls{ClsGloble.GetTableName}Data.FindByID({firstColumnName}, ");

            for (int i = 1; i < dtColumnTable.Rows.Count; i++)
            {
                DataRow dr = dtColumnTable.Rows[i];
                string columnName = dr["ColumnName"].ToString();
                methodBuilder.Append($"ref {columnName}");

                if (i < dtColumnTable.Rows.Count - 1)
                    methodBuilder.Append(", ");
            }

            methodBuilder.AppendLine(");");
            methodBuilder.AppendLine("            if (IsFound)");
            methodBuilder.Append($"                return new cls{ClsGloble.GetTableName}(");

            for (int i = 0; i < dtColumnTable.Rows.Count; i++)
            {
                DataRow row = dtColumnTable.Rows[i];
                string columnName = row["ColumnName"].ToString();
                methodBuilder.Append(columnName);

                if (i < dtColumnTable.Rows.Count - 1)
                    methodBuilder.Append(", ");
            }

            methodBuilder.AppendLine(");");
            methodBuilder.AppendLine("            return null;");
            methodBuilder.AppendLine("        }");

            return methodBuilder.ToString();
        }

        // Data Access layer
        public static string _GenerateMethodFindByID(string StorName)
        {
            StringBuilder MethodBuilder = new StringBuilder();

            MethodBuilder.Append($"\tpublic static bool FindByID(");

            DataRow dr = ClsGloble.dataTable.Rows[0];
            string fColumnName = dr[0].ToString();
            string fDataType = dr[1].ToString();
            string fDataTypeCsharp = ClsGloble.mapSqlTypeToCSharp(fDataType);

            MethodBuilder.Append($"{fDataTypeCsharp} {fColumnName}, ");


            MethodBuilder.AppendLine(clsGlobleCsharp.GenerateRefParameters(ClsGloble.dataTable) + ")");
            MethodBuilder.AppendLine("\t{");
            MethodBuilder.AppendLine("\t\tbool isFound = false;");
            MethodBuilder.AppendLine($"\t\tusing(SqlConnection connection = new SqlConnection(\"{ClsGloble.conn}\"))");
            MethodBuilder.AppendLine("\t\t{");
            MethodBuilder.AppendLine($"\t\tusing(SqlCommand command = new SqlCommand(\"{StorName}\", connection))");
            MethodBuilder.AppendLine("\t\t{");
            MethodBuilder.AppendLine("\t\tcommand.CommandType = CommandType.StoredProcedure;");
            MethodBuilder.AppendLine($"\t\t\tcommand.Parameters.AddWithValue(\"@{fColumnName}\", {fColumnName});");
            MethodBuilder.AppendLine("\t\t\ttry");
            MethodBuilder.AppendLine("\t\t{");
            MethodBuilder.AppendLine("\t\t\tconnection.Open();");
            MethodBuilder.AppendLine("\t\t\tusing(SqlDataReader reader = command.ExecuteReader())");
            MethodBuilder.AppendLine("\t\t\t{");
            MethodBuilder.AppendLine("\t\t\tif (reader.Read())");
            MethodBuilder.AppendLine("\t\t\t{");
            MethodBuilder.AppendLine("\t\t\t\tisFound = true;");

            MethodBuilder.AppendLine(clsGlobleCsharp.GenerateReaderAssignments(ClsGloble.dataTable));
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


        //Projec Is Not Async

        //Business Layer..
        public static string GenerateFindByIDMethod()
        {
            StringBuilder methodBuilder = new StringBuilder();
            DataRow firstRow = ClsGloble.dataTable.Rows[0];
            string firstColumnName = firstRow["ColumnName"].ToString();
            string firstDataType = ClsGloble.mapSqlTypeToCSharp(firstRow["DataType"].ToString());

            methodBuilder.AppendLine($"        public static cls{ClsGloble.GetTableName} FindByID({firstDataType} {firstColumnName})");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine("            // Call DataAccess Layer");

            for (int i = 1; i < ClsGloble.dataTable.Rows.Count; i++)
            {
                DataRow dr = ClsGloble.dataTable.Rows[i];
                string columnName = dr["ColumnName"].ToString();
                string dataType = ClsGloble.mapSqlTypeToCSharp(dr["DataType"].ToString());
                string value = clsGlobleCsharp.GenerateTheValueOfVariable(dataType);
                methodBuilder.AppendLine($"            {dataType} {columnName} = {value};");
            }

            methodBuilder.AppendLine();
            methodBuilder.Append($"\t        bool IsFound = cls{ClsGloble.GetTableName}Data.FindByID({firstColumnName}, ");

            for (int i = 1; i < ClsGloble.dataTable.Rows.Count; i++)
            {
                DataRow dr = ClsGloble.dataTable.Rows[i];
                string columnName = dr["ColumnName"].ToString();
                methodBuilder.Append($"ref {columnName}");

                if (i < ClsGloble.dataTable.Rows.Count - 1)
                    methodBuilder.Append(", ");
            }

            methodBuilder.AppendLine(");");
            methodBuilder.AppendLine("            if (IsFound)");
            methodBuilder.Append($"                return new cls{ClsGloble.GetTableName}(");

            for (int i = 0; i < ClsGloble.dataTable.Rows.Count; i++)
            {
                DataRow row = ClsGloble.dataTable.Rows[i];
                string columnName = row["ColumnName"].ToString();
                methodBuilder.Append(columnName);

                if (i < ClsGloble.dataTable.Rows.Count - 1)
                    methodBuilder.Append(", ");
            }

            methodBuilder.AppendLine(");");
            methodBuilder.AppendLine("            return null;");
            methodBuilder.AppendLine("        }");

            return methodBuilder.ToString();
        }

        //Data Access Layer..
        public static string _GenerateMethodFindByID()
        {
            StringBuilder MethodBuilder = new StringBuilder();

            MethodBuilder.Append($"\tpublic static bool FindByID(");

            DataRow dr = ClsGloble.dataTable.Rows[0];
            string fColumnName = dr[0].ToString();
            string fDataType = dr[1].ToString();
            string fDataTypeCsharp = ClsGloble.mapSqlTypeToCSharp(fDataType);

            MethodBuilder.Append($"{fDataTypeCsharp} {fColumnName}, ");

            for (int i = 1; i < ClsGloble.dataTable.Rows.Count; i++)
            {
                DataRow d = ClsGloble.dataTable.Rows[i];
                string ColumnName = d[0].ToString();
                string dataType = d[1].ToString();
                string DataTypeCsharp = ClsGloble.mapSqlTypeToCSharp(dataType);
                MethodBuilder.Append($"ref {DataTypeCsharp} {ColumnName}");

                if (i < ClsGloble.dataTable.Rows.Count - 1)
                {
                    MethodBuilder.Append(", ");
                }
            }

            MethodBuilder.AppendLine($" ) ");
            MethodBuilder.AppendLine("\t{");
            MethodBuilder.AppendLine("\t\tbool isFound = false;");
            MethodBuilder.AppendLine($"\t\tusing(SqlConnection connection = new SqlConnection(\"{ClsGloble.conn}\"))");
            MethodBuilder.AppendLine("\t\t{");
            MethodBuilder.AppendLine($"\t\t\tstring query = @\"SELECT * FROM {ClsGloble.GetTableName} WHERE {fColumnName} = @{fColumnName}\";");
            MethodBuilder.AppendLine("\t\t\tusing(SqlCommand command = new SqlCommand(query, connection))");
            MethodBuilder.AppendLine("\t\t\t{");
            MethodBuilder.AppendLine($"\t\t\t\tcommand.Parameters.AddWithValue(\"@{fColumnName}\", {fColumnName});");

            MethodBuilder.AppendLine("\t\t\t\ttry");
            MethodBuilder.AppendLine("\t\t\t\t{");
            MethodBuilder.AppendLine("\t\t\t\t\tconnection.Open();");
            MethodBuilder.AppendLine("\t\t\t\t\tusing(SqlDataReader reader = command.ExecuteReader())");
            MethodBuilder.AppendLine("\t\t\t\t\t{");
            MethodBuilder.AppendLine("\t\t\t\t\t\tif (reader.Read())");
            MethodBuilder.AppendLine("\t\t\t\t\t\t{");
            MethodBuilder.AppendLine("\t\t\t\t\t\t\tisFound = true;");

            for (int i = 1; i < ClsGloble.dataTable.Rows.Count; i++)
            {
                DataRow d = ClsGloble.dataTable.Rows[i];
                string ColumnName = d[0].ToString();
                string dataType = d[1].ToString();
                string DataTypeCsharp = ClsGloble.mapSqlTypeToCSharp(dataType);
                if (DataTypeCsharp != "string" && DataTypeCsharp != "byte[]")
                {

                    MethodBuilder.AppendLine($"\t\t\t\t\t\t{ColumnName} =({DataTypeCsharp}) reader[\"{ColumnName}\"];");
                }
                else

                { MethodBuilder.AppendLine($"\t\t\t\t\t\t{ColumnName} = reader[\"{ColumnName}\"] != DBNull.Value ? ({DataTypeCsharp})reader[\"{ColumnName}\"] : null;"); }
            }

            MethodBuilder.AppendLine("\t\t\t\t\t\t}");
            MethodBuilder.AppendLine("\t\t\t\t\t\t\telse");
            MethodBuilder.AppendLine("\t\t\t\t\t\t{");
            MethodBuilder.AppendLine("\t\t\t\t\t\t\tisFound = false;");
            MethodBuilder.AppendLine("\t\t\t\t\t\t}");
            MethodBuilder.AppendLine("\t\t\t\t\t}");
            MethodBuilder.AppendLine("\t\t\t\tcatch (Exception ex)");
            MethodBuilder.AppendLine("\t\t\t\t{");
            MethodBuilder.AppendLine("\t\t\t\t\tisFound = false;");
            MethodBuilder.AppendLine("\t\t\t\t}");
            MethodBuilder.AppendLine("\t\t\t}");
            MethodBuilder.AppendLine("\t\t}");
            MethodBuilder.AppendLine("\t\treturn isFound;");
            MethodBuilder.AppendLine("\t}");

            return MethodBuilder.ToString();
        }

    }
}
