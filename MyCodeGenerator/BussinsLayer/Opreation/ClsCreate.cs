using SqlLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinsLayer
{
    public class ClsCreate
    {
        //Business Layer..
        public static string GenerateMethodAddNew(DataTable dtColumnTable)
        {
            StringBuilder methodBuilder = new StringBuilder();
            methodBuilder.AppendLine($"        private async Task<bool> _AddNew{ClsGloble.GetTableName}()");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine("            // Call DataAccess Layer");
            methodBuilder.Append($"\t        this.{dtColumnTable.Rows[0]["ColumnName"]} = (int) await cls{ClsGloble.GetTableName}Data.AddNew{ClsGloble.GetTableName}(");

            for (int i = 1; i < dtColumnTable.Rows.Count; i++)
            {
                DataRow dr = dtColumnTable.Rows[i];
                string columnName = dr["ColumnName"].ToString();
                methodBuilder.Append($"this.{columnName}");

                if (i < dtColumnTable.Rows.Count - 1)
                    methodBuilder.Append(",");
            }
            methodBuilder.AppendLine(");");
            methodBuilder.AppendLine($"            return (this.{dtColumnTable.Rows[0]["ColumnName"]} != -1);");
            methodBuilder.AppendLine("        }");

            return methodBuilder.ToString();
        }

        // Data Access Layer 
        public static string _GenerateMethodAddNew(string StorName)
        {
            StringBuilder MethodBuilder = new StringBuilder();
            MethodBuilder.Append($"\tpublic static async Task<int?> AddNew{ClsGloble.GetTableName}(");

            MethodBuilder.AppendLine(clsGlobleCsharp.GenerateMethodParameters(ClsGloble.dataTable, 1) + ")");

            MethodBuilder.AppendLine("\t{");
            MethodBuilder.AppendLine($"\t\tusing(SqlCommand command = new SqlCommand(\"{StorName}\"))");
            MethodBuilder.AppendLine("\t\t\t{");
            MethodBuilder.AppendLine("\t\t\t\t\tcommand.CommandType = CommandType.StoredProcedure;");
            MethodBuilder.AppendLine(clsGlobleCsharp.GenerateSqlParameters(clsGlobleCsharp.GetStoredProcedureParameters(StorName), 1));
            MethodBuilder.AppendLine("\t\t\t\treturn await clsPrimaryFunctions.Add(command);");
            MethodBuilder.AppendLine("\t\t\t}");
            MethodBuilder.AppendLine("\t\t}");

            return MethodBuilder.ToString();
        }




        // create projec is not async/await

        //Business layer..
        public static string GenerateMethodBusinessAddNew()
        {
            StringBuilder methodBuilder = new StringBuilder();
            methodBuilder.AppendLine($"        private bool _AddNew{ClsGloble.GetTableName}()");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine("            // Call DataAccess Layer");
            methodBuilder.Append($"\t        this.{ClsGloble.dataTable.Rows[0]["ColumnName"]} = cls{ClsGloble.GetTableName}Data.AddNew{ClsGloble.GetTableName}(");

            for (int i = 1; i < ClsGloble.dataTable.Rows.Count; i++)
            {
                DataRow dr = ClsGloble.dataTable.Rows[i];
                string columnName = dr["ColumnName"].ToString();
                methodBuilder.Append($"this.{columnName}");

                if (i < ClsGloble.dataTable.Rows.Count - 1)
                    methodBuilder.Append(",");
            }
            methodBuilder.AppendLine(");");
            methodBuilder.AppendLine($"            return (this.{ClsGloble.dataTable.Rows[0]["ColumnName"]} != -1);");
            methodBuilder.AppendLine("        }");

            return methodBuilder.ToString();
        }

        //Data access layer
        public static string _GenerateMethodDataAccessAddNew()
        {
            StringBuilder MethodBuilder = new StringBuilder();
            MethodBuilder.Append($"\tpublic static int AddNew{ClsGloble.GetTableName}(");

            MethodBuilder.AppendLine(clsGlobleCsharp.GenerateMethodParameters(ClsGloble.dataTable, 1) + ")");

            MethodBuilder.AppendLine(")");
            MethodBuilder.AppendLine("\t{");

            MethodBuilder.Append($"\t\tstring query = @\"INSERT INTO {ClsGloble.GetTableName} (");
            for (int i = 1; i <= ClsGloble.dataTable.Rows.Count - 1; i++)
            {
                MethodBuilder.Append($"{ClsGloble.dataTable.Rows[i][0]}");
                if (i < ClsGloble.dataTable.Rows.Count - 1) MethodBuilder.Append(", ");
            }

            MethodBuilder.Append(") VALUES (");
            for (int i = 1; i <= ClsGloble.dataTable.Rows.Count - 1; i++)
            {
                MethodBuilder.Append($"@{ClsGloble.dataTable.Rows[i][0]}");
                if (i < ClsGloble.dataTable.Rows.Count - 1) MethodBuilder.Append(", ");
            }

            MethodBuilder.AppendLine("); SELECT SCOPE_IDENTITY();\";");
            MethodBuilder.AppendLine("\t\tusing(SqlCommand command = new SqlCommand(query))");
            MethodBuilder.AppendLine("\t\t{");

            for (int i = 1; i < ClsGloble.dataTable.Rows.Count; i++)
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

            MethodBuilder.AppendLine("\t\t\treturn CRUD.AddNew(command);");
            MethodBuilder.AppendLine("\t\t}");

            return MethodBuilder.ToString();
        }


    }
}
