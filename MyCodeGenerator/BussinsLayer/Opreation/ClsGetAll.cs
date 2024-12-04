using SqlLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinsLayer
{
    public class clsGetAll
    {
        // Business Layer..
        public static string GenerateGetAll()
        {
            StringBuilder methodBuilder = new StringBuilder();
            methodBuilder.AppendLine($"        public static async Task<DataTable> GetAll{ClsGloble.GetTableName}()");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine($"            return await cls{ClsGloble.GetTableName}Data.GetAll{ClsGloble.GetTableName}();");
            methodBuilder.AppendLine("        }");

            return methodBuilder.ToString();
        }

        // Data Access Layer..
        public static string _GenerateMethodGetAll(string StorName)
        {
            StringBuilder MethodBuilder = new StringBuilder();
            MethodBuilder.AppendLine($"\t\t\tpublic static async Task<DataTable> GetAll{ClsGloble.GetTableName}()");
            MethodBuilder.AppendLine("\t\t\t{");
            MethodBuilder.AppendLine($"\t\t\t\t using(SqlCommand command = new SqlCommand(\"{StorName}\"))");
            MethodBuilder.AppendLine("\t\t\t{");
            MethodBuilder.AppendLine("\t\t\t\t\tcommand.CommandType = CommandType.StoredProcedure;");
            MethodBuilder.AppendLine("\t\t\t\t\treturn await clsPrimaryFunctions.Get(command);");
            MethodBuilder.AppendLine("\t\t\t\t}");
            MethodBuilder.AppendLine("\t\t\t}");
            return MethodBuilder.ToString();

        }



        //Priject is not async/await

        //Business Layer
        public static string GenerateIsNotAsyncGetAll()
        {
            StringBuilder methodBuilder = new StringBuilder();
            methodBuilder.AppendLine($"        public static DataTable GetAll{ClsGloble.GetTableName}()");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine($"            return cls{ClsGloble.GetTableName}Data.GetAll{ClsGloble.GetTableName}();");
            methodBuilder.AppendLine("        }");

            return methodBuilder.ToString();
        }


        //Data Access Layer
        public static string _GenerateMethodIsNotAsyncGetAll()
        {
            StringBuilder MethodBuilder = new StringBuilder();
            MethodBuilder.AppendLine($"\tpublic static DataTable GetAll{ClsGloble.GetTableName}()");
            MethodBuilder.AppendLine("\t{");
            MethodBuilder.AppendLine($"\t\tstring query = @\"SELECT * FROM {ClsGloble.GetTableName}\";");
            MethodBuilder.AppendLine("\t\tusing(SqlCommand command = new SqlCommand(query))");
            MethodBuilder.AppendLine("\t\t{");
            MethodBuilder.AppendLine("\t\t\treturn CRUD.GetAll(command);");
            MethodBuilder.AppendLine("\t\t}");
            return MethodBuilder.ToString();
        }

    }
}
