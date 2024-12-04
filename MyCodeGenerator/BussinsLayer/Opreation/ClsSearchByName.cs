using SqlLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinsLayer
{
    public class ClsSearchByName
    {
        static DataTable _dtColumnTable = ClsGloble.dataTable;
        // Business Layer..
        public static string GenerateFindByNameMethod()
        {
            StringBuilder methodBuilder = new StringBuilder();

            // Get The First Column
            DataRow d = _dtColumnTable.Rows[1];
            string sColumnName = d["ColumnName"].ToString();
            string sDataType = ClsGloble.mapSqlTypeToCSharp(d["DataType"].ToString());

            methodBuilder.AppendLine($"\tpublic static cls{ClsGloble.GetTableName} FindByName({sDataType} {sColumnName})");
            methodBuilder.AppendLine("\t{");
            methodBuilder.AppendLine("\t\t// Call DataAccess Layer");

            // Initialize other columns with default values
            for (int i = 0; i < _dtColumnTable.Rows.Count; i++)
            {
                if (i == 1)
                {
                    continue; // Skip the first column used as the parameter
                }

                DataRow drow = _dtColumnTable.Rows[i];
                string columnName = drow["ColumnName"].ToString();
                string dataType = ClsGloble.mapSqlTypeToCSharp(drow["DataType"].ToString());
                string value = clsGlobleCsharp.GenerateTheValueOfVariable(dataType);

                methodBuilder.AppendLine($"\t\t{dataType} {columnName} = {value};");
            }

            methodBuilder.AppendLine();

            // Get the first column details for the FindByName method call

            DataRow dr = _dtColumnTable.Rows[0];
            string fColumnName = dr["ColumnName"].ToString();
            string DataType = ClsGloble.mapSqlTypeToCSharp(dr["DataType"].ToString());
            string Value = clsGlobleCsharp.GenerateTheValueOfVariable(DataType);

            if (_dtColumnTable.Rows.Count == 1)
            { methodBuilder.Append($"\t\tbool IsFound = cls{ClsGloble.GetTableName}Data.FindByName(ref {fColumnName}, {sColumnName}"); }
            else
            {
                methodBuilder.Append($"\t\tbool IsFound = cls{ClsGloble.GetTableName}Data.FindByName(ref {fColumnName}, {sColumnName},");

                for (int i = 2; i < _dtColumnTable.Rows.Count; i++)
                {
                    DataRow drows = _dtColumnTable.Rows[i];
                    string ColumnName = drows["ColumnName"].ToString();
                    string dataType = ClsGloble.mapSqlTypeToCSharp(drows["DataType"].ToString());
                    string value = clsGlobleCsharp.GenerateTheValueOfVariable(DataType);


                    methodBuilder.Append($"ref {ColumnName}");


                    if (i < _dtColumnTable.Rows.Count - 1)
                    { methodBuilder.Append(", "); }
                }
            }

            methodBuilder.AppendLine(");");
            methodBuilder.AppendLine("\t\tif (IsFound)");

            // Return new object if found
            methodBuilder.Append($"\t\t\treturn new cls{ClsGloble.GetTableName}(");
            for (int i = 0; i < _dtColumnTable.Rows.Count; i++)
            {
                DataRow row = _dtColumnTable.Rows[i];
                string columnName = row["ColumnName"].ToString();
                methodBuilder.Append(columnName);

                if (i < _dtColumnTable.Rows.Count - 1)
                    methodBuilder.Append(", ");
            }

            methodBuilder.AppendLine(");");
            methodBuilder.AppendLine("\t\telse");
            methodBuilder.AppendLine("\t\t\treturn null;");
            methodBuilder.AppendLine("\t}");

            return methodBuilder.ToString();


        }

        // Data Access Layer..
        public static string _GenerateMethodFindByName(string StorName)
        {
            StringBuilder MethodBuilder = new StringBuilder();

            MethodBuilder.Append($"\tpublic static bool Find(");

            for (int i = 0; i < _dtColumnTable.Rows.Count; i++)
            {
                DataRow d = _dtColumnTable.Rows[i];
                string ColumnName = d[0].ToString();
                string dataType = d[1].ToString();
                string DataTypeCsharp = ClsGloble.mapSqlTypeToCSharp(dataType);

                if (i == 1)
                {
                    MethodBuilder.Append($"{DataTypeCsharp} {ColumnName}");
                }
                else
                {
                    MethodBuilder.Append($" ref {DataTypeCsharp} {ColumnName}");
                }

                if (i < _dtColumnTable.Rows.Count - 1)
                {
                    MethodBuilder.Append($", ");
                }
            }
           

            MethodBuilder.AppendLine($" ) ");
            MethodBuilder.AppendLine("\t{");
            MethodBuilder.AppendLine("\t\tbool isFound = false;");
            MethodBuilder.AppendLine($"\t\tusing(SqlConnection connection = new SqlConnection(ClsSetting.ConnectionString))");
            MethodBuilder.AppendLine("\t\t{");
            DataRow dr = _dtColumnTable.Rows[1];
            string sColumnName = dr[0].ToString();
            string sDataType = dr[1].ToString();
            string sDataTypeCsharp = ClsGloble.mapSqlTypeToCSharp(sDataType);

            MethodBuilder.AppendLine($"\t\tusing(SqlCommand command = new SqlCommand(\"{StorName}\", connection))");
            MethodBuilder.AppendLine("\t\t{");
            MethodBuilder.AppendLine("\t\tcommand.CommandType = CommandType.StoredProcedure;");
            MethodBuilder.AppendLine($"\t\tcommand.Parameters.AddWithValue(\"@{sColumnName}\", {sColumnName});");
            MethodBuilder.AppendLine("\t\t\ttry");
            MethodBuilder.AppendLine("\t\t{");
            MethodBuilder.AppendLine("\t\t\tconnection.Open();");
            MethodBuilder.AppendLine("\t\t\tusing(SqlDataReader reader = command.ExecuteReader())");
            MethodBuilder.AppendLine("\t\t\t{");
            MethodBuilder.AppendLine("\t\t\tif (reader.Read())");
            MethodBuilder.AppendLine("\t\t\t{");
            MethodBuilder.AppendLine("\t\t\t\tisFound = true;");

            MethodBuilder.AppendLine(clsGlobleCsharp.GenerateReaderAssignments(_dtColumnTable));
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
        public static string GenerateFindByNameMethodNotAsync()
        {
            StringBuilder methodBuilder = new StringBuilder();

            // Get The First Column
            DataRow d = ClsGloble.dataTable.Rows[1];
            string sColumnName = d["ColumnName"].ToString();
            string sDataType = ClsGloble.mapSqlTypeToCSharp(d["DataType"].ToString());

            methodBuilder.AppendLine($"\tpublic static cls{ClsGloble.GetTableName} FindByName({sDataType} {sColumnName})");
            methodBuilder.AppendLine("\t{");
            methodBuilder.AppendLine("\t\t// Call DataAccess Layer");

            // Initialize other columns with default values
            for (int i = 0; i < ClsGloble.dataTable.Rows.Count; i++)
            {
                if (i == 1)
                {
                    continue; // Skip the first column used as the parameter
                }

                DataRow drow = ClsGloble.dataTable.Rows[i];
                string columnName = drow["ColumnName"].ToString();
                string dataType = ClsGloble.mapSqlTypeToCSharp(drow["DataType"].ToString());
                string value = clsGlobleCsharp.GenerateTheValueOfVariable(dataType);

                methodBuilder.AppendLine($"\t\t{dataType} {columnName} = {value};");
            }

            methodBuilder.AppendLine();

            // Get the first column details for the FindByName method call

            DataRow dr = ClsGloble.dataTable.Rows[0];
            string fColumnName = dr["ColumnName"].ToString();
            string DataType = ClsGloble.mapSqlTypeToCSharp(dr["DataType"].ToString());
            string Value = clsGlobleCsharp.GenerateTheValueOfVariable(DataType);

            if (ClsGloble.dataTable.Rows.Count == 1)
            { methodBuilder.Append($"\t\tbool IsFound = cls{ClsGloble.GetTableName}Data.FindByName(ref {fColumnName}, {sColumnName}"); }
            else
            {
                methodBuilder.Append($"\t\tbool IsFound = cls{ClsGloble.GetTableName}Data.FindByName(ref {fColumnName}, {sColumnName},");

                for (int i = 2; i < ClsGloble.dataTable.Rows.Count; i++)
                {
                    DataRow drows = ClsGloble.dataTable.Rows[i];
                    string ColumnName = drows["ColumnName"].ToString();
                    string dataType = ClsGloble.mapSqlTypeToCSharp(drows["DataType"].ToString());
                    string value = clsGlobleCsharp.GenerateTheValueOfVariable(DataType);


                    methodBuilder.Append($"ref {ColumnName}");


                    if (i < ClsGloble.dataTable.Rows.Count - 1)
                    { methodBuilder.Append(", "); }
                }
            }

            methodBuilder.AppendLine(");");
            methodBuilder.AppendLine("\t\tif (IsFound)");

            // Return new object if found
            methodBuilder.Append($"\t\t\treturn new cls{ClsGloble.GetTableName}(");
            for (int i = 0; i < ClsGloble.dataTable.Rows.Count; i++)
            {
                DataRow row = ClsGloble.dataTable.Rows[i];
                string columnName = row["ColumnName"].ToString();
                methodBuilder.Append(columnName);

                if (i < ClsGloble.dataTable.Rows.Count - 1)
                    methodBuilder.Append(", ");
            }

            methodBuilder.AppendLine(");");
            methodBuilder.AppendLine("\t\telse");
            methodBuilder.AppendLine("\t\t\treturn null;");
            methodBuilder.AppendLine("\t}");

            return methodBuilder.ToString();


        }

        //Data Access Layer..
        public static string _GenerateMethodFindByName()
        {
            StringBuilder MethodBuilder = new StringBuilder();

            MethodBuilder.Append($"\tpublic static bool FindByName(");

            for (int i = 0; i < _dtColumnTable.Rows.Count; i++)
            {
                DataRow d = _dtColumnTable.Rows[i];
                string ColumnName = d[0].ToString();
                string dataType = d[1].ToString();
                string DataTypeCsharp = ClsGloble.mapSqlTypeToCSharp(dataType);

                if (i == 1)
                {
                    MethodBuilder.Append($"{DataTypeCsharp} {ColumnName}");
                }
                else
                {
                    MethodBuilder.Append($" ref {DataTypeCsharp} {ColumnName}");



                }

                if (i < _dtColumnTable.Rows.Count - 1)
                {
                    MethodBuilder.Append($", ");
                }
            }

            MethodBuilder.AppendLine($" ) ");
            MethodBuilder.AppendLine("\t{");
            MethodBuilder.AppendLine("\t\tbool isFound = false;");
            MethodBuilder.AppendLine($"\t\tusnig(SqlConnection connection = new SqlConnection(\" {ClsGloble.conn} \"))");
            MethodBuilder.AppendLine("\t\t{");
            DataRow dr = _dtColumnTable.Rows[1];
            string sColumnName = dr[0].ToString();
            string sDataType = dr[1].ToString();
            string sDataTypeCsharp = ClsGloble.mapSqlTypeToCSharp(sDataType);

            MethodBuilder.AppendLine($"\t\t\tstring query = @\"SELECT * FROM {ClsGloble.GetTableName} WHERE {sColumnName} = @{sColumnName}\";");
            MethodBuilder.AppendLine("\t\t\tusing(SqlCommand command = new SqlCommand(query, connection))");
            MethodBuilder.AppendLine("\t\t\t{");
            MethodBuilder.AppendLine($"\t\t\t\tcommand.Parameters.AddWithValue(\"@{sColumnName}\", {sColumnName});");

            MethodBuilder.AppendLine("\t\t\t\ttry");
            MethodBuilder.AppendLine("\t\t\t\t{");
            MethodBuilder.AppendLine("\t\t\t\t\tconnection.Open();");
            MethodBuilder.AppendLine("\t\t\t\t\tusing(SqlDataReader reader = command.ExecuteReader())");
            MethodBuilder.AppendLine("\t\t\t\t\t{");
            MethodBuilder.AppendLine("\t\t\t\t\t\tif (reader.Read())");
            MethodBuilder.AppendLine("\t\t\t\t\t\t{");
            MethodBuilder.AppendLine("\t\t\t\t\t\t\tisFound = true;");

            for (int i = 0; i < _dtColumnTable.Rows.Count; i++)
            {
                DataRow d = _dtColumnTable.Rows[i];
                string ColumnName = d[0].ToString();
                string dataType = d[1].ToString();
                string DataTypeCsharp = ClsGloble.mapSqlTypeToCSharp(dataType);

                if (DataTypeCsharp != "string" && DataTypeCsharp != "byte[]")
                {

                    MethodBuilder.AppendLine($"\t\t\t\t\t\t\t{ColumnName} =({DataTypeCsharp}) reader[\"{ColumnName}\"];");
                }
                else

                { MethodBuilder.AppendLine($"\t\t\t\t\t\t\t{ColumnName} = reader[\"{ColumnName}\"] != DBNull.Value ? ({DataTypeCsharp})reader[\"{ColumnName}\"] : null;"); }


            }

            MethodBuilder.AppendLine("\t\t\t\t\t\t}");
            MethodBuilder.AppendLine("\t\t\t\t\t\t\telse");
            MethodBuilder.AppendLine("\t\t\t\t\t\t{");
            MethodBuilder.AppendLine("\t\t\t\t\t\t\tisFound = false;");
            MethodBuilder.AppendLine("\t\t\t\t\t\t}");
            MethodBuilder.AppendLine("\t\t\t\t\t}");
            MethodBuilder.AppendLine("\t\t\t\t}");
            MethodBuilder.AppendLine("\t\t\t\tcatch (Exception ex)");
            MethodBuilder.AppendLine("\t\t\t\t{");
            MethodBuilder.AppendLine("\t\t\t\t\t\tisFound = false;");
            MethodBuilder.AppendLine("\t\t\t\t}");
            MethodBuilder.AppendLine("\t\t\t}");
            MethodBuilder.AppendLine("\t\t}");

            MethodBuilder.AppendLine("\t\treturn isFound;");
            MethodBuilder.AppendLine("\t}");

            return MethodBuilder.ToString();
        }

    }
}
