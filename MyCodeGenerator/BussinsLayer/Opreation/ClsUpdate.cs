using SqlLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinsLayer
{
    public class ClsUpdate
    {
        //Business Layer..
        public static string GenerateUpdateMethod(DataTable dtColumnTable)
        {
            StringBuilder methodBuilder = new StringBuilder();
            methodBuilder.AppendLine($"        private async Task<bool> _Update{ClsGloble.GetTableName}()");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine("            // Call DataAccess Layer");
            methodBuilder.Append($"\t        return await cls{ClsGloble.GetTableName}Data.Update{ClsGloble.GetTableName}(");

            for (int i = 0; i < dtColumnTable.Rows.Count; i++)
            {
                DataRow dr = dtColumnTable.Rows[i];
                string columnName = dr["ColumnName"].ToString();
                methodBuilder.Append($"this.{columnName}");

                if (i < dtColumnTable.Rows.Count - 1)
                    methodBuilder.Append(", ");
            }
            methodBuilder.AppendLine(") ?? false;");
            methodBuilder.AppendLine("        }");

            return methodBuilder.ToString();
        }

        //Data Access Layer 
        public static string _GenerateMethodUpdate(string StorName)
        {
            StringBuilder MethodBuilder = new StringBuilder();
            MethodBuilder.Append($"\tpublic static async Task<bool?> Update{ClsGloble.GetTableName}(");
            MethodBuilder.AppendLine(clsGlobleCsharp.GenerateMethodParameters(ClsGloble.dataTable, 0) + ")");

            MethodBuilder.AppendLine("\t{");
            MethodBuilder.AppendLine($"\t\tusing(SqlCommand command = new SqlCommand(\"{StorName}\"))");
            MethodBuilder.AppendLine("\t\t{");
            MethodBuilder.AppendLine("\t\tcommand.CommandType= CommandType.StoredProcedure;");
            MethodBuilder.AppendLine(clsGlobleCsharp.GenerateSqlParameters(clsGlobleCsharp.GetStoredProcedureParameters(StorName), 0));
            MethodBuilder.AppendLine("\t\treturn await clsPrimaryFunctions.Update(command);");
            MethodBuilder.AppendLine("\t\t}");
            MethodBuilder.AppendLine("\t}");

            return MethodBuilder.ToString();
        }


        //Project Is Not Async 

        //Business Layer..
        public static string GenerateUpdateMethod()
        {
            StringBuilder methodBuilder = new StringBuilder();
            methodBuilder.AppendLine($"        private bool _Update{ClsGloble.GetTableName}()");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine("            // Call DataAccess Layer");
            methodBuilder.Append($"\t        return cls{ClsGloble.GetTableName}Data.Update{ClsGloble.GetTableName}(");

            for (int i = 0; i < ClsGloble.dataTable.Rows.Count; i++)
            {
                DataRow dr = ClsGloble.dataTable.Rows[i];
                string columnName = dr["ColumnName"].ToString();
                methodBuilder.Append($"this.{columnName}");

                if (i < ClsGloble.dataTable.Rows.Count - 1)
                    methodBuilder.Append(", ");
            }
            methodBuilder.AppendLine(");");
            methodBuilder.AppendLine("        }");

            return methodBuilder.ToString();
        }


        //Data Access Layer..
        public static string _GenerateMethodUpdate()
        {
            StringBuilder MethodBuilder = new StringBuilder();
            MethodBuilder.Append($"\tpublic static bool Update{ClsGloble.GetTableName}(");

            for (int i = 0; i <= ClsGloble.dataTable.Rows.Count - 1; i++)
            {
                DataRow dr = ClsGloble.dataTable.Rows[i];
                string ColumnName = dr[0].ToString();
                string DataTypeCshrip = ClsGloble.mapSqlTypeToCSharp(dr[1].ToString());
                MethodBuilder.Append($"{DataTypeCshrip} {ColumnName}");
                if (i < ClsGloble.dataTable.Rows.Count - 1) MethodBuilder.Append(", ");
            }

            MethodBuilder.AppendLine(")");
            MethodBuilder.AppendLine("\t{");


            MethodBuilder.Append($"\t\tstring query = @\"UPDATE {ClsGloble.GetTableName} SET ");

            for (int i = 1; i <= ClsGloble.dataTable.Rows.Count - 1; i++)
            {
                MethodBuilder.Append($"{ClsGloble.dataTable.Rows[i][0]}=@{ClsGloble.dataTable.Rows[i][0]}");
                if (i < ClsGloble.dataTable.Rows.Count - 1) MethodBuilder.Append(", ");
            }

            DataRow dfirst = ClsGloble.dataTable.Rows[0];
            string fColumnName = dfirst[0].ToString();
            MethodBuilder.AppendLine($" WHERE {fColumnName}=@{fColumnName};\";");
            MethodBuilder.AppendLine("\t\tusing(SqlCommand command = new SqlCommand(query))");

            for (int i = 0; i < ClsGloble.dataTable.Rows.Count; i++)
            {
                DataRow d = ClsGloble.dataTable.Rows[i];
                string ColumnName = d[0].ToString();
                string dataType = d[1].ToString();
                string DataTypeCsharp = ClsGloble.mapSqlTypeToCSharp(dataType);

                if (DataTypeCsharp != "string" && DataTypeCsharp != "byte[]")
                {

                    MethodBuilder.AppendLine($"\t\t\t\t command.Parameters.AddWithValue(\"@{ColumnName}\",{ColumnName});");
                }
                else

                {

                    MethodBuilder.AppendLine($"\t\t\t\t command.Parameters.AddWithValue(\"@{ColumnName}\",{ColumnName}?? (object)DBNull.Value);");
                }
            }

            MethodBuilder.AppendLine("\t\t\treturn CRUD.UpdateOrDelete(command);");
            MethodBuilder.AppendLine("\t\t}");

            return MethodBuilder.ToString();
        }

    }
}
