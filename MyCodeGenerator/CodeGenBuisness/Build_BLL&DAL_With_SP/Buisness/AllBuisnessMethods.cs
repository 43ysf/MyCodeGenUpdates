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
        private string _GenerateMethodAddNew()
        {
            StringBuilder methodBuilder = new StringBuilder();
            clsColumn PrimCol = _GetPrimaryKeyColumn();


            methodBuilder.AppendLine($"        private async Task<bool> _AddNew{this.TableName}()");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine("            // Call DataAccess Layer");
            methodBuilder.Append($"\t        this.{PrimCol.ColumnName} = (int) await cls{this.TableName}Data.AddNew{this.TableName}(");


            for (int i = 0; i < Columns.Count; i++)
            {
                if (!(Columns[i].IsPrimaryKey && Columns[i].ColumnType == "int"))
                {
                    methodBuilder.Append($"this.{Columns[i].ColumnName}");

                    if (i < Columns.Count - 1)
                        methodBuilder.Append(",");

                }

            }


            methodBuilder.AppendLine(");");
            methodBuilder.AppendLine($"            return (this.{PrimCol.ColumnName} != -1);");
            methodBuilder.AppendLine("        }");

            return methodBuilder.ToString();
        }
        private string _GenerateDeleteMethod()
        {
            StringBuilder methodBuilder = new StringBuilder();
            clsColumn primaryCol = _GetPrimaryKeyColumn();


            methodBuilder.AppendLine($"        public static Task<bool> Delete({primaryCol.ColumnType} {primaryCol.ColumnName})");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine("            // Call DataAccess Layer");
            methodBuilder.AppendLine($"            return cls{this.TableName}Data.Delete{this.TableName}({primaryCol.ColumnName});");
            methodBuilder.AppendLine("        }");

            return methodBuilder.ToString();
        }
        public void GenertorBLL()
        {

        }
        private string _GenerateGetAll()
        {
            StringBuilder methodBuilder = new StringBuilder();
            methodBuilder.AppendLine($"        public static async Task<DataTable> GetAll{this.TableName}()");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine($"            return await cls{this.TableName}Data.GetAll{this.TableName}();");
            methodBuilder.AppendLine("        }");

            return methodBuilder.ToString();
        }
        private string _GenerateSaveMethod()
        {
            StringBuilder methodBuilder = new StringBuilder();
            methodBuilder.AppendLine("        public async Task<bool> Save()");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine("            switch (Mode)");
            methodBuilder.AppendLine("            {");
            methodBuilder.AppendLine("                case enMode.addNew:");
            methodBuilder.AppendLine("                        Mode = enMode.update;");
            methodBuilder.AppendLine($"                     return await _AddNew{this.TableName}();");
            methodBuilder.AppendLine("                case enMode.update:");
            methodBuilder.AppendLine($"                    return await _Update{this.TableName}();");

            methodBuilder.AppendLine("            }");
            methodBuilder.AppendLine("            return false;");
            methodBuilder.AppendLine("        }");
            return methodBuilder.ToString();
        }
        private string _GenerateFindByIDMethod()
        {
            StringBuilder methodBuilder = new StringBuilder();
            clsColumn PrimCol = _GetPrimaryKeyColumn();

            methodBuilder.AppendLine($"        public static cls{this.TableName} Find({PrimCol.ColumnType} {PrimCol.ColumnName})");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine("            // Call DataAccess Layer");

            foreach (clsColumn col in Columns)
            {
                if (!col.IsPrimaryKey)
                    methodBuilder.AppendLine($"            {col.ColumnType} {col.ColumnName} = {col.Value};");

            }

            methodBuilder.AppendLine();
            methodBuilder.Append($"\t        bool IsFound = cls{this.TableName}Data.FindByID({PrimCol.ColumnName}, ");

            for (int i = 0; i < Columns.Count; i++)
            {
                if (Columns[i] != PrimCol)
                {
                    methodBuilder.Append($"ref {Columns[i].ColumnName}");
                    if (i < Columns.Count - 1)
                    {
                        methodBuilder.Append(", ");

                    }


                }
            }

            methodBuilder.AppendLine(");");
            methodBuilder.AppendLine("            if (IsFound)");
            methodBuilder.Append($"                return new cls{this.TableName}(");

            for (int i = 0; i < Columns.Count; i++)
            {
                methodBuilder.Append(Columns[i].ColumnName);

                if (i < Columns.Count - 1)
                    methodBuilder.Append(", ");
            }

            methodBuilder.AppendLine(");");
            methodBuilder.AppendLine("            return null;");
            methodBuilder.AppendLine("        }");

            return methodBuilder.ToString();
        }

        //private string _GenerateFindByNameMethod()
        //{
        //    StringBuilder methodBuilder = new StringBuilder();

        //    // Get The First Column
        //    clsColumn ColNotPrim = _GetNotPrimaryCol();
        //    clsColumn PrimaryCol = _GetPrimaryKeyColumn();

        //    methodBuilder.AppendLine($"\tpublic static cls{this.TableName} FindByName({ColNotPrim.ColumnType} {ColNotPrim.ColumnName})");
        //    methodBuilder.AppendLine("\t{");
        //    methodBuilder.AppendLine("\t\t// Call DataAccess Layer");

        //    // Initialize other columns with default values
        //    for (int i = 0; i < Columns.Count; i++)
        //    {
        //        if (Columns[i] == ColNotPrim)
        //        {
        //            continue; // Skip the first column used as the parameter
        //        }


        //        methodBuilder.AppendLine($"\t\t{Columns[i].ColumnType} {Columns[i].ColumnName} = {Columns[i].Value};");
        //    }

        //    methodBuilder.AppendLine();

        //    // Get the first column details for the FindByName method call


        //    methodBuilder.Append($"\t\tbool IsFound = cls{this.TableName}Data.FindByName(ref {PrimaryCol.ColumnName}, {ColNotPrim.ColumnName},");

        //    for (int i = 2; i < Columns.Count; i++)
        //    {
        //        methodBuilder.Append($"ref {Columns[i].ColumnName}");


        //        if (i < Columns.Count - 1)
        //            methodBuilder.Append(", ");
        //    }


        //    methodBuilder.AppendLine(");");
        //    methodBuilder.AppendLine("\t\tif (IsFound)");

        //    // Return new object if found
        //    methodBuilder.Append($"\t\t\treturn new cls{this.TableName}(");
        //    for (int i = 0; i < Columns.Count; i++)
        //    {
        //        methodBuilder.Append(Columns[i].ColumnName);

        //        if (i < Columns.Count - 1)
        //            methodBuilder.Append(", ");
        //    }

        //    methodBuilder.AppendLine(");");
        //    methodBuilder.AppendLine("\t\telse");
        //    methodBuilder.AppendLine("\t\t\treturn null;");
        //    methodBuilder.AppendLine("\t}");

        //    return methodBuilder.ToString();


        //}
        private string _GenerateUpdateMethod()
        {
            StringBuilder methodBuilder = new StringBuilder();
            methodBuilder.AppendLine($"        private async Task<bool> _Update{this.TableName}()");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine("            // Call DataAccess Layer");
            methodBuilder.Append($"\t        return await cls{this.TableName}Data.Update{this.TableName}(");

            for (int i = 0; i < Columns.Count; i++)
            {
                methodBuilder.Append($"this.{Columns[i].ColumnName}");

                if (i < Columns.Count - 1)
                    methodBuilder.Append(", ");
            }
            methodBuilder.AppendLine(") ?? false;");
            methodBuilder.AppendLine("        }");

            return methodBuilder.ToString();
        }
        private string _HeaderForBuisness()
        {
            StringBuilder classBuilder = new StringBuilder();

            classBuilder.AppendLine("using System;");
            classBuilder.AppendLine("using System.Data;");
            classBuilder.AppendLine("using System.Text;");
            classBuilder.AppendLine("using DataAccessLayer;");
            classBuilder.AppendLine("using System.Threading.Tasks;");

            classBuilder.AppendLine();

            classBuilder.AppendLine($"public class cls{this.TableName}");
            classBuilder.AppendLine("\t{");
            classBuilder.AppendLine("\tpublic enum enMode { addNew = 0, update = 1 }");
            classBuilder.AppendLine("\tpublic enMode Mode = enMode.addNew;");
            classBuilder.AppendLine();
            return classBuilder.ToString();
        }
        private string _GenerateProperty(clsColumn Column)
        {
            return $"        public {Column.ColumnType} {Column.ColumnName} {{ get; set; }}";
        }
        private static string _GeneratePropertyForConstructor(clsColumn Col)
        {
            return $"\t\tthis.{Col.ColumnName} = {Col.Value};";
        }
        private string _GenerateParameterList()
        {
            StringBuilder parameterListBuilder = new StringBuilder();

            for (int i = 0; i < Columns.Count; i++)
            {

                parameterListBuilder.Append($"{Columns[i].ColumnType} {Columns[i].ColumnName}");
                if (i < Columns.Count - 1)
                    parameterListBuilder.Append(", ");
            }

            return parameterListBuilder.ToString();
        }
        private string _GenerateAssignments()
        {
            StringBuilder assignmentsBuilder = new StringBuilder();

            foreach (clsColumn Col in Columns)
            {
                assignmentsBuilder.AppendLine($"\t\t\t\tthis.{Col.ColumnName} = {Col.ColumnName};");
            }

            return assignmentsBuilder.ToString();
        }
        private string _GeneratePrivateConstructor()
        {

            StringBuilder constructorBuilder = new StringBuilder();

            // توليد التوقيع الخاص بالدالة
            constructorBuilder.AppendLine(_GenerateParameterList() + ")");
            constructorBuilder.AppendLine("{");

            // توليد تعيين الخصائص داخل جسم الدالة
            constructorBuilder.AppendLine(_GenerateAssignments());
            constructorBuilder.AppendLine("\t\t\t\t\tthis.Mode = enMode.update;");
            constructorBuilder.AppendLine("        }");
            return constructorBuilder.ToString();

        }
        public string GenerateAllBusinessLayerMethods()
        {

            StringBuilder classBuilder = new StringBuilder();

            classBuilder.AppendLine(_HeaderForBuisness());
            classBuilder.AppendLine();

            // Generate properties
            foreach (clsColumn col in Columns)
            {
                classBuilder.AppendLine(_GenerateProperty(col));
            }

            // Public Constructor
            classBuilder.AppendLine($"\tpublic cls{this.TableName}()");
            classBuilder.AppendLine("\t{");
            foreach (clsColumn col in Columns)
            {
                classBuilder.AppendLine(_GeneratePropertyForConstructor(col));
            }

            classBuilder.AppendLine("\t\tthis.Mode = enMode.addNew;");
            classBuilder.AppendLine("\t}");
            classBuilder.AppendLine();

            // Private constructor
            classBuilder.Append($"\tprivate cls{this.TableName}(");
            classBuilder.Append(_GeneratePrivateConstructor());

            classBuilder.AppendLine();
            // Add method
            classBuilder.AppendLine(_GenerateMethodAddNew());
            classBuilder.AppendLine(_GenerateDeleteMethod());
            classBuilder.AppendLine(_GenerateFindByIDMethod());
            //classBuilder.AppendLine(_GenerateFindByNameMethod());
            classBuilder.AppendLine(_GenerateGetAll());
            classBuilder.AppendLine(_GenerateSaveMethod());
            classBuilder.AppendLine(_GenerateUpdateMethod());

            classBuilder.AppendLine("}");

            return classBuilder.ToString();

        }

    }
}
