using SqlLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BussinsLayer
{
    public class DAL
    {

        static DataTable _dtColumnTable = ClsGloble.dataTable;

        public static string GenerateAllDtataAccessLayerMethods(int rank = 0)
        {
          
            StringBuilder classBuilder = new StringBuilder();

            classBuilder.AppendLine(HederClass());
            if(rank == 0)
            {
                var methodGenerators = new Dictionary<string, Func<string, string>>(StringComparer.OrdinalIgnoreCase)
                {
                    { "add", ClsCreate._GenerateMethodAddNew },
                    { "update", ClsUpdate._GenerateMethodUpdate },
                    { "delete", ClsDeleted._GenerateMethodDelete },
                    { "getall", clsGetAll._GenerateMethodGetAll },
                    { "byid", ClsSearchById._GenerateMethodFindByID },
                    { "byname", ClsSearchByName._GenerateMethodFindByName }
                };

                foreach (var procedure in ClsGloble.strList)
                {
                    foreach (var pattern in methodGenerators.Keys)
                    {
                        if (Regex.IsMatch(procedure, pattern, RegexOptions.IgnoreCase))
                        {
                            classBuilder.AppendLine(methodGenerators[pattern](procedure));
                            break;
                        }
                    }
                }

            }
            else
            {
                classBuilder.AppendLine(ClsCreate._GenerateMethodDataAccessAddNew());
                classBuilder.AppendLine(ClsUpdate._GenerateMethodUpdate());
                classBuilder.AppendLine(ClsDeleted._GenerateMethodDelete());
                classBuilder.AppendLine(clsGetAll._GenerateMethodIsNotAsyncGetAll());
                classBuilder.AppendLine(ClsSearchById._GenerateMethodFindByID());
                classBuilder.AppendLine(ClsSearchByName._GenerateMethodFindByName());
                
                
            }

            classBuilder.AppendLine("}");

            return classBuilder.ToString();
        }

        private static string HederClass()
        {
            StringBuilder classBuilder = new StringBuilder();

            classBuilder.AppendLine("using System;");
            classBuilder.AppendLine("using System.Data;");
            classBuilder.AppendLine("using System.Text;");
            classBuilder.AppendLine("using System.Linq;");
            classBuilder.AppendLine("using System.Data.SqlClient;");
            classBuilder.AppendLine("using System.Threading.Tasks;");
            classBuilder.AppendLine("");

            classBuilder.AppendLine($"\tpublic class cls{ClsGloble.GetTableName}Data");
            classBuilder.AppendLine("\t{");
            return classBuilder.ToString();
        }

       public static string CreateClsSetting()
        {
            StringBuilder classBuilder = new StringBuilder();

            classBuilder.AppendLine("using System;");
            classBuilder.AppendLine("using System.Data;");
            classBuilder.AppendLine("using System.Text;");
            classBuilder.AppendLine("using System.Data.SqlClient;\n");
            classBuilder.AppendLine();
          
            classBuilder.AppendLine("public class ClsSetting \n\n");
            classBuilder.AppendLine("{\n");
            classBuilder.AppendLine("        public static string ConnectionString = $\"{ClsGloble.GetServer.ConnectionString}; Password={ClsGloble.Password};\";\r\n");
            classBuilder.AppendLine("}");



            return classBuilder.ToString() ;


        }
    }

}
