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

namespace CodeGenBusiness
{
    public partial class clsTable
    {
        private string GenerateMethodDataAccessAddNew()
        {
            StringBuilder MethodBuilder = new StringBuilder();
            MethodBuilder.Append($"\tpublic static int AddNew{this.TableName}(");

            MethodBuilder.AppendLine(_GenerateMethodParameters(false, false) + ")");


            MethodBuilder.AppendLine("\t{");

            MethodBuilder.Append($"\t\tstring query = @\"INSERT INTO {this.TableName} (");

            for (int i = 1; i < Columns.Count; i++)
            {

                MethodBuilder.Append(Columns[i].ColumnName);
                if (i < Columns.Count - 1)
                    MethodBuilder.Append(',');

            }

            MethodBuilder.Append(") VALUES (");
            for (int i = 1; i < Columns.Count; i++)
            {

                MethodBuilder.Append($"@{Columns[i].ColumnName}");
                if (i < Columns.Count - 1)
                    MethodBuilder.Append(',');

            }

            MethodBuilder.AppendLine("); SELECT SCOPE_IDENTITY();\";");
            MethodBuilder.AppendLine("\t\tusing(SqlCommand command = new SqlCommand(query))");
            MethodBuilder.AppendLine("\t\t{");

            for (int i = 1; i < Columns.Count; i++)
            {
                MethodBuilder.AppendLine($"\t\t\t command.Parameters.AddWithValue(\"@{Columns[i].ColumnName}\",{Columns[i].ColumnName});");

                if (i < Columns.Count - 1)
                    MethodBuilder.Append(',');
            }

            MethodBuilder.AppendLine("\t\t\treturn CRUD.AddNew(command);");
            MethodBuilder.AppendLine("\t\t}");
            MethodBuilder.AppendLine("\t}");
            return MethodBuilder.ToString();
        }
        private string _GenerateMethodUpdate()
        {
            StringBuilder MethodBuilder = new StringBuilder();
            MethodBuilder.Append($"\tpublic static bool Update{this.TableName}(");

            MethodBuilder.Append(_GenerateMethodParameters(true, false));

            MethodBuilder.Append(")");
            MethodBuilder.AppendLine("\t{");


            MethodBuilder.Append($"\t\tstring query = @\"UPDATE {this.TableName} SET ");

            for (int i = 1; i < Columns.Count; i++)
            {
                MethodBuilder.AppendLine(Columns[i].ColumnName + "=" + $"@{Columns[i].ColumnName}");
                if (i < Columns.Count - 1)
                    MethodBuilder.Append(",");
            }

            clsColumn pk = _GetPrimaryKeyColumn();

            MethodBuilder.AppendLine($" WHERE {pk.ColumnName}=@{pk.ColumnName};\";");
            MethodBuilder.AppendLine("\t\tusing(SqlCommand command = new SqlCommand(query))");
            MethodBuilder.AppendLine("\t\t{");
            foreach (clsColumn col in Columns)
            {
                if (!col.IsAllowNull)
                {
                    MethodBuilder.AppendLine($"\t\t\t\t command.Parameters.AddWithValue(\"@{col.ColumnName}\",{col.ColumnName});");
                }
                else
                {
                    MethodBuilder.AppendLine($"\t\t\t\t command.Parameters.AddWithValue(\"@{col.ColumnName}\",{col.ColumnName}?? (object)DBNull.Value);");
                }
            }

            MethodBuilder.AppendLine("\t\t\treturn CRUD.UpdateOrDelete(command);");
            MethodBuilder.AppendLine("\t\t}");
            MethodBuilder.AppendLine("\t}");

            return MethodBuilder.ToString();
        }
        private string _GenerateMethodFindByID()
        {
            StringBuilder MethodBuilder = new StringBuilder();

            MethodBuilder.Append($"\tpublic static bool FindByID(");

            clsColumn PK = _GetPrimaryKeyColumn();

            MethodBuilder.AppendLine(_GenerateMethodParameters(true, true) + ")");


            MethodBuilder.AppendLine("\t{");
            MethodBuilder.AppendLine("\t\tbool isFound = false;");
            MethodBuilder.AppendLine($"\t\tusing(SqlConnection connection = new SqlConnection(ClsSetting.ConnectionString))");
            MethodBuilder.AppendLine("\t\t{");
            MethodBuilder.AppendLine($"\t\t\tstring query = @\"SELECT * FROM {this.TableName} WHERE {PK.ColumnName} = @{PK.ColumnName}\";");
            MethodBuilder.AppendLine("\t\t\tusing(SqlCommand command = new SqlCommand(query, connection))");
            MethodBuilder.AppendLine("\t\t\t{");
            MethodBuilder.AppendLine($"\t\t\t\tcommand.Parameters.AddWithValue(\"@{PK.ColumnName}\", {PK.ColumnName});");

            MethodBuilder.AppendLine("\t\t\t\ttry");
            MethodBuilder.AppendLine("\t\t\t\t{");
            MethodBuilder.AppendLine("\t\t\t\t\tconnection.Open();");
            MethodBuilder.AppendLine("\t\t\t\t\tusing(SqlDataReader reader = command.ExecuteReader())");
            MethodBuilder.AppendLine("\t\t\t\t\t{");
            MethodBuilder.AppendLine("\t\t\t\t\t\tif (reader.Read())");
            MethodBuilder.AppendLine("\t\t\t\t\t\t{");
            MethodBuilder.AppendLine("\t\t\t\t\t\t\tisFound = true;");

            for (int i = 0; i < Columns.Count; i++)
            {
                if (!Columns[i].IsPrimaryKey)
                {

                    if (!Columns[i].IsAllowNull)
                    {
                        MethodBuilder.AppendLine($"\t\t\t\t\t\t{Columns[i].ColumnName} =({Columns[i].ColumnType}) reader[\"{Columns[i].ColumnName}\"];");
                    }
                    else
                    {
                        MethodBuilder.AppendLine($"\t\t\t\t\t\t{Columns[i].ColumnName} = reader[\"{Columns[i].ColumnName}\"] != DBNull.Value ? ({Columns[i].ColumnType})reader[\"{Columns[i].ColumnName}\"] : null;");
                    }
                }

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

        //private string _GenerateMethodFindByName()
        //{
        //    StringBuilder MethodBuilder = new StringBuilder();

        //    MethodBuilder.Append($"\tpublic static bool FindByName(");
        //    clsColumn PK = _GetPrimaryKeyColumn();

        //    MethodBuilder.AppendLine(_GenerateMethodParameters(true, true) + ")");


        //    MethodBuilder.AppendLine("\t{");
        //    MethodBuilder.AppendLine("\t\tbool isFound = false;");
        //    MethodBuilder.AppendLine($"\t\tusnig(SqlConnection connection = new SqlConnection(ClsSetting.ConnectionString))");
        //    MethodBuilder.AppendLine("\t\t{");

        //    MethodBuilder.AppendLine($"\t\t\tstring query = @\"SELECT * FROM {this.TableName} WHERE {PK.ColumnName} = @{PK.ColumnName}\";");
        //    MethodBuilder.AppendLine("\t\t\tusing(SqlCommand command = new SqlCommand(query, connection))");
        //    MethodBuilder.AppendLine("\t\t\t{");
        //    MethodBuilder.AppendLine($"\t\t\t\tcommand.Parameters.AddWithValue(\"@{PK.ColumnName}\", {PK.ColumnName});");

        //    MethodBuilder.AppendLine("\t\t\t\ttry");
        //    MethodBuilder.AppendLine("\t\t\t\t{");
        //    MethodBuilder.AppendLine("\t\t\t\t\tconnection.Open();");
        //    MethodBuilder.AppendLine("\t\t\t\t\tusing(SqlDataReader reader = command.ExecuteReader())");
        //    MethodBuilder.AppendLine("\t\t\t\t\t{");
        //    MethodBuilder.AppendLine("\t\t\t\t\t\tif (reader.Read())");
        //    MethodBuilder.AppendLine("\t\t\t\t\t\t{");
        //    MethodBuilder.AppendLine("\t\t\t\t\t\t\tisFound = true;");


        //    for (int i = 0; i < Columns.Count; i++)
        //    {
        //        if (!Columns[i].IsPrimaryKey)
        //        {

        //            if (!Columns[i].IsAllowNull)
        //            {
        //                MethodBuilder.AppendLine($"\t\t\t\t\t\t{Columns[i].ColumnName} =({Columns[i].ColumnType}) reader[\"{Columns[i].ColumnName}\"];");
        //            }
        //            else
        //            {
        //                MethodBuilder.AppendLine($"\t\t\t\t\t\t{Columns[i].ColumnName} = reader[\"{Columns[i].ColumnName}\"] != DBNull.Value ? ({Columns[i].ColumnType})reader[\"{Columns[i].ColumnName}\"] : null;");
        //            }
        //        }

        //    }

        //    MethodBuilder.AppendLine("\t\t\t\t\t\t}");
        //    MethodBuilder.AppendLine("\t\t\t\t\t\telse");
        //    MethodBuilder.AppendLine("\t\t\t\t\t\t{");
        //    MethodBuilder.AppendLine("\t\t\t\t\t\t\tisFound = false;");
        //    MethodBuilder.AppendLine("\t\t\t\t\t\t}");
        //    MethodBuilder.AppendLine("\t\t\t\t\t}");
        //    MethodBuilder.AppendLine("\t\t\t\t}");
        //    MethodBuilder.AppendLine("\t\t\t\tcatch (Exception ex)");
        //    MethodBuilder.AppendLine("\t\t\t\t{");
        //    MethodBuilder.AppendLine("\t\t\t\t\t\tisFound = false;");
        //    MethodBuilder.AppendLine("\t\t\t\t}");
        //    MethodBuilder.AppendLine("\t\t\t}");
        //    MethodBuilder.AppendLine("\t\t}");

        //    MethodBuilder.AppendLine("\t\treturn isFound;");
        //    MethodBuilder.AppendLine("\t}");

        //    return MethodBuilder.ToString();
        //}

        private string _GenerateMethodIsNotAsyncGetAll()
        {
            StringBuilder MethodBuilder = new StringBuilder();
            MethodBuilder.AppendLine($"\tpublic static DataTable GetAll{this.TableName}()");
            MethodBuilder.AppendLine("\t{");
            MethodBuilder.AppendLine($"\t\tstring query = @\"SELECT * FROM {this.TableName}\";");
            MethodBuilder.AppendLine("\t\tusing(SqlCommand command = new SqlCommand(query))");
            MethodBuilder.AppendLine("\t\t{");
            MethodBuilder.AppendLine("\t\t\treturn CRUD.GetAll(command);");
            MethodBuilder.AppendLine("\t\t}");
            MethodBuilder.AppendLine("\t}");
            return MethodBuilder.ToString();
        }
        private string _GenerateMethodDelete()
        {
            StringBuilder MethodBuilder = new StringBuilder();

            MethodBuilder.Append($"\tpublic static bool Delete{this.TableName}(");

            clsColumn PK = _GetPrimaryKeyColumn();

            MethodBuilder.Append($"{PK.ColumnType} {PK.ColumnName}");

            MethodBuilder.Append($" ) ");
            MethodBuilder.AppendLine("\t{");

            MethodBuilder.AppendLine($"\t\tstring query = @\"DELETE FROM {this.TableName} WHERE {PK.ColumnName} = @{PK.ColumnName}\";");
            MethodBuilder.AppendLine("\t\tusing(SqlCommand command = new SqlCommand(query))");
            MethodBuilder.AppendLine("\t\t{");
            MethodBuilder.AppendLine($"\t\t\tcommand.Parameters.AddWithValue(\"@{PK.ColumnName}\", {PK.ColumnName});");
            MethodBuilder.AppendLine("\t\t\treturn CRUD.UpdateOrDelete(command);");

            MethodBuilder.AppendLine("\t\t}");
            MethodBuilder.AppendLine("\t}");
            return MethodBuilder.ToString();
        }

        public string GenerateAllDtataAccessLayerMethodsWithOutSP()
        {

            StringBuilder classBuilder = new StringBuilder();

            classBuilder.AppendLine(_HeadrClassForDataAccess());

            classBuilder.AppendLine(GenerateMethodDataAccessAddNew());
            classBuilder.AppendLine(_GenerateMethodUpdate());
            classBuilder.AppendLine(_GenerateMethodFindByID());
            //classBuilder.AppendLine(_GenerateMethodFindByName());
            classBuilder.AppendLine(_GenerateMethodIsNotAsyncGetAll());
            classBuilder.AppendLine(_GenerateMethodDelete());




            classBuilder.AppendLine("\t}");

            return classBuilder.ToString();
        }

        public string CRUD()
        {
            StringBuilder classBuilder = new StringBuilder();

            classBuilder.AppendLine("using System;\r\n" +
                "using System.Data;\r\n" +
                "using System.Data.SqlClient;\r\n" +
                "using System.Runtime.InteropServices;\r\n\r\n" +
                "namespace DVLD_DataAccess\r\n" +
                "{\r\n   " +
                " public static class CRUD\r\n  " +
                " {\r\n      " +
                "private static SqlConnection _Connection = new SqlConnection(ClsSetting.ConnectionString);\r\n   " +
                "public static int AddNew(SqlCommand command)\r\n    " +
                "{\r\n\r\n       " +
                "command.Connection = _Connection;\r\n\r\n   " +
                "      int ID = -1;\r\n        " +
                " try\r\n        " +
                "  {\r\n            " +
                "    _Connection.Open();\r\n       " +
                "     object Reuslt = command.ExecuteScalar();\r\n" +
                "     if(Reuslt != null && int.TryParse(Reuslt.ToString(), out int R))\r\n" +
                "   {\r\n" +
                "                    ID = R;\r\n" +
                "                }\r\n\r\n            }\r\n " +
                "           catch (Exception ex)\r\n" +
                "            {\r\n                Console.WriteLine(ex.Message);\r\n            }\r\n " +
                "           finally\r\n           " +
                " {\r\n                " +
                "_Connection.Close();\r\n     " +
                "       }\r\n            return ID;\r\n        }\r\n\r\n  " +
                "      public static DataTable GetAll(SqlCommand command)\r\n        {\r\n  " +
                "          DataTable dt = new DataTable();\r\n            command.Connection = _Connection;\r\n  " +
                "          try\r\n            {\r\n                _Connection.Open();\r\n        " +
                "        SqlDataReader reader = command.ExecuteReader();\r\n                if (reader.HasRows)\r\n    " +
                "            {\r\n                    dt.Load(reader);\r\n                    reader.Close();\r\n     " +
                "           }\r\n            } catch (Exception ex)\r\n            {\r\n               //Console.WriteLine(ex.Message);\r\n    " +
                "        }\r\n            finally\r\n            {\r\n                _Connection.Close();\r\n\r\n    " +
                "        }\r\n            return dt;\r\n        }\r\n\r\n        public static bool UpdateOrDelete(SqlCommand command)\r\n  " +
                "      {\r\n            command.Connection = _Connection;\r\n            int RowsAffected = 0;\r\n   " +
                "         try\r\n            {\r\n                _Connection.Open();\r\n                \r\n       " +
                "         RowsAffected = command.ExecuteNonQuery();\r\n        " +
                "    }catch(Exception ex)\r\n            {\r\n                Console.WriteLine(ex.Message);\r\n   " +
                "         }\r\n            finally\r\n            {\r\n                _Connection.Close();\r\n            }\r\n     " +
                "       return RowsAffected > 0;\r\n        }\r\n\r\n\r\n        public static  bool IsExist(SqlCommand command)\r\n    " +
                "    {\r\n            command.Connection = _Connection;   \r\n\r\n            bool isFound = false;\r\n            try\r\n  " +
                "          {\r\n                _Connection.Open();\r\n                SqlDataReader reader = command.ExecuteReader();\r\n   " +
                "             isFound = reader.HasRows;\r\n                reader.Close();\r\n            }\r\n            catch (Exception ex)\r\n         " +
                "   {\r\n                Console.WriteLine(ex.Message);\r\n            }\r\n            finally\r\n            {\r\n                _Connection.Close();\r\n\r\n            }\r\n   " +
                "         return isFound;\r\n\r\n        }\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n  " +
                "  }\r\n" +
                "}");


            return classBuilder.ToString();
        }

    }
}
