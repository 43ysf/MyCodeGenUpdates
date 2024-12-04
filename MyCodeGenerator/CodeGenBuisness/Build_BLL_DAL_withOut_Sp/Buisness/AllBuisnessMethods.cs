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
        private string GenerateMethodBusinessAddNew()
        {
            StringBuilder methodBuilder = new StringBuilder();
            clsColumn PK = _GetPrimaryKeyColumn();
            methodBuilder.AppendLine($"        private bool _AddNew{this.TableName}()");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine("            // Call DataAccess Layer");
            methodBuilder.Append($"\t        this.{PK.ColumnName} = cls{this.TableName}Data.AddNew{this.TableName}(");

            methodBuilder.Append(_GenerateMethodParameters(false, false));

            methodBuilder.AppendLine(");");
            methodBuilder.AppendLine($"            return (this.{PK.ColumnName} != -1);");
            methodBuilder.AppendLine("        }");

            return methodBuilder.ToString();
        }
        private string GenerateUpdateMethod()
        {
            StringBuilder methodBuilder = new StringBuilder();
            methodBuilder.AppendLine($"        private bool _Update{this.TableName}()");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine("            // Call DataAccess Layer");
            methodBuilder.Append($"\t        return cls{this.TableName}Data.Update{this.TableName}(");

            methodBuilder.Append(_GenerateMethodParameters(true, false));

            methodBuilder.AppendLine(");");
            methodBuilder.AppendLine("        }");

            return methodBuilder.ToString();
        }
        private string GenerateFindByIDMethod()
        {
            StringBuilder methodBuilder = new StringBuilder();

            clsColumn PK = _GetPrimaryKeyColumn();
            methodBuilder.AppendLine($"        public static cls{this.TableName} FindByID({PK.ColumnType} {PK.ColumnName})");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine("            // Call DataAccess Layer");

            foreach (clsColumn col in Columns)
            {
                if (!col.IsPrimaryKey)
                {
                    if (clsSql.SqlToCsharbDataType(col.ColumnType) is string)
                    {
                        methodBuilder.AppendLine($"\t\t {col.ColumnName}=\"\";");

                    }
                    else
                        methodBuilder.AppendLine($"\t\t{col.ColumnName}=0;");

                }
            }

            methodBuilder.AppendLine();
            methodBuilder.Append($"\t        bool IsFound = cls{this.TableName}Data.FindByID(");

            methodBuilder.Append(_GenerateMethodParameters(true, true));
            methodBuilder.AppendLine(");");
            methodBuilder.AppendLine("            if (IsFound)");
            methodBuilder.Append($"                return new cls{this.TableName}(");

            methodBuilder.Append(_GenerateMethodParameters(true, false));


            methodBuilder.AppendLine(");");
            methodBuilder.AppendLine("            return null;");
            methodBuilder.AppendLine("        }");

            return methodBuilder.ToString();
        }
        //private string GenerateFindByNameMethodNotAsync()
        //{
        //    StringBuilder methodBuilder = new StringBuilder();
        //    clsColumn Pk = _GetPrimaryKeyColumn();
        //    methodBuilder.AppendLine($"\tpublic static cls{this.TableName} FindByName({Pk.ColumnType} {Pk.ColumnName})");
        //    methodBuilder.AppendLine("\t{");
        //    methodBuilder.AppendLine("\t\t// Call DataAccess Layer");

        //    // Initialize other columns with default values
        //    foreach (clsColumn col in Columns)
        //    {
        //        if (!col.IsPrimaryKey)
        //        {
        //            if (clsSql.SqlToCsharbDataType(col.ColumnType) is string)
        //            {
        //                methodBuilder.AppendLine($"\t\t{col.ColumnName}= \"\";");

        //            }
        //            else
        //                methodBuilder.AppendLine($"\t\t{col.ColumnName}=0;");

        //        }
        //    }

        //    methodBuilder.AppendLine();

        //    // Get the first column details for the FindByName method call

        //    methodBuilder.Append($"\t\tbool IsFound = cls{this.TableName}Data.FindByName(");

        //    methodBuilder.Append(_GenerateMethodParameters(true, true));

        //    methodBuilder.AppendLine(");");
        //    methodBuilder.AppendLine("\t\tif (IsFound)");

        //    // Return new object if found
        //    methodBuilder.Append($"\t\t\treturn new cls{this.TableName}(");
        //    methodBuilder.Append(_GenerateMethodParameters(true, false));


        //    methodBuilder.AppendLine(");");
        //    methodBuilder.AppendLine("\t\telse");
        //    methodBuilder.AppendLine("\t\t\treturn null;");
        //    methodBuilder.AppendLine("\t}");

        //    return methodBuilder.ToString();


        //}
        private string GenerateIsNotAsyncGetAll()
        {
            StringBuilder methodBuilder = new StringBuilder();
            methodBuilder.AppendLine($"        public static DataTable GetAll{this.TableName}()");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine($"            return cls{this.TableName}Data.GetAll{this.TableName}();");
            methodBuilder.AppendLine("        }");

            return methodBuilder.ToString();
        }
        private string GenerateDeleteMethod()
        {
            StringBuilder methodBuilder = new StringBuilder();

            clsColumn Pk = _GetPrimaryKeyColumn();

            methodBuilder.AppendLine($"        public static bool Delete({Pk.ColumnType} {Pk.ColumnName})");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine("            // Call DataAccess Layer");
            methodBuilder.AppendLine($"            return cls{this.TableName}Data.Delete{this.TableName}({Pk.ColumnName});");
            methodBuilder.AppendLine("        }");

            return methodBuilder.ToString();
        }
        private string GenerateSaveMethodIsNotAsync()
        {
            StringBuilder methodBuilder = new StringBuilder();
            methodBuilder.AppendLine("        public bool Save()");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine("            switch (Mode)");
            methodBuilder.AppendLine("            {");
            methodBuilder.AppendLine("                case enMode.addNew:");
            methodBuilder.AppendLine($"                    if (_AddNew{this.TableName}())");
            methodBuilder.AppendLine("                    {");
            methodBuilder.AppendLine("                        Mode = enMode.update;");
            methodBuilder.AppendLine("                        return true;");
            methodBuilder.AppendLine("                    }");
            methodBuilder.AppendLine("                    else");
            methodBuilder.AppendLine("                    {");
            methodBuilder.AppendLine("                        return false;");
            methodBuilder.AppendLine("                    }");
            methodBuilder.AppendLine("                case enMode.update:");
            methodBuilder.AppendLine($"                    return _Update{this.TableName}();");
            methodBuilder.AppendLine("            }");
            methodBuilder.AppendLine("            return false;");
            methodBuilder.AppendLine("        }");
            return methodBuilder.ToString();
        }
        public string GenerateAllBusinessLayerMethodsWithOutSP()
        {

            StringBuilder classBuilder = new StringBuilder();

            classBuilder.AppendLine(_HeaderForBuisness());
            classBuilder.AppendLine();

            // Generate properties
            foreach (clsColumn dr in Columns)
            {
                classBuilder.AppendLine(_GenerateProperty(dr));
            }

            // Public Constructor
            classBuilder.AppendLine($"        public cls{this.TableName}()");
            classBuilder.AppendLine("        {");
            foreach (clsColumn dr in Columns)
            {
                classBuilder.AppendLine(_GeneratePropertyForConstructor(dr));
            }

            classBuilder.AppendLine("\t\t\t\t\tthis.Mode = enMode.addNew;");
            classBuilder.AppendLine("        }");
            classBuilder.AppendLine();

            // Private constructor
            classBuilder.Append($"        private cls{this.TableName}(");
            classBuilder.Append(_GeneratePrivateConstructor());


            // method BLL

            classBuilder.AppendLine(GenerateMethodBusinessAddNew());
            classBuilder.AppendLine(GenerateUpdateMethod());
            classBuilder.AppendLine(GenerateSaveMethodIsNotAsync());
            classBuilder.AppendLine(GenerateFindByIDMethod());
            //classBuilder.AppendLine(GenerateFindByNameMethodNotAsync());
            classBuilder.AppendLine(GenerateIsNotAsyncGetAll());
            classBuilder.AppendLine(GenerateDeleteMethod());


            classBuilder.AppendLine("\t}");

            return classBuilder.ToString();

        }

    }
}
