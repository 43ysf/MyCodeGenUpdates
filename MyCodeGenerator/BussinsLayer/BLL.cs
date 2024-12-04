using SqlLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinsLayer
{
    public class BLL
    {

        private static DataTable dtColumnTable = ClsGloble.dataTable;
       
        public static string GenerateAllBusinessLayerMethods(int rank = 0)
        {
           
            StringBuilder classBuilder = new StringBuilder();

            classBuilder.AppendLine(HederClass());
            classBuilder.AppendLine();

            // Generate properties
            foreach (DataRow dr in dtColumnTable.Rows)
            {
                classBuilder.AppendLine(GenerateProperty(dr));
            }

            // Public Constructor
            classBuilder.AppendLine($"        public cls{ClsGloble.GetTableName}()");
            classBuilder.AppendLine("        {");
            foreach (DataRow dr in dtColumnTable.Rows)
            {
                classBuilder.AppendLine(GeneratePropertyForConstructor(dr));
            }

            classBuilder.AppendLine("          this.Mode = enMode.addNew;");
            classBuilder.AppendLine("        }");
            classBuilder.AppendLine();

            // Private constructor
            classBuilder.Append($"        private cls{ClsGloble.GetTableName}(");
            classBuilder.Append(GeneratePrivateConstructor());
            
           
            // Add method
            if(rank  == 0)
            {
                classBuilder.AppendLine(ClsCreate.GenerateMethodAddNew(dtColumnTable));
                classBuilder.AppendLine(ClsUpdate.GenerateUpdateMethod(dtColumnTable));
                classBuilder.AppendLine(clsSaveMethod.GenerateSaveMethod());
                classBuilder.AppendLine(ClsSearchById.GenerateFindByIDMethod(dtColumnTable));
                classBuilder.AppendLine(ClsSearchByName.GenerateFindByNameMethod());
                classBuilder.AppendLine(clsGetAll.GenerateGetAll());
                classBuilder.AppendLine(ClsDeleted.GenerateDeleteMethod(dtColumnTable));
            }
            else
            {
                classBuilder.AppendLine(ClsCreate.GenerateMethodBusinessAddNew());
                classBuilder.AppendLine(ClsUpdate.GenerateUpdateMethod());
                classBuilder.AppendLine(clsSaveMethod.GenerateSaveMethodIsNotAsync());
                classBuilder.AppendLine(ClsSearchById.GenerateFindByIDMethod());
                classBuilder.AppendLine(ClsSearchByName.GenerateFindByNameMethodNotAsync());
                classBuilder.AppendLine(clsGetAll.GenerateIsNotAsyncGetAll());
                classBuilder.AppendLine(ClsDeleted.GenerateDeleteMethod());
            }

            classBuilder.AppendLine("    }");

            return classBuilder.ToString();

        }

        private static string HederClass()
        {
            StringBuilder classBuilder = new StringBuilder();

            classBuilder.AppendLine("using System;");
            classBuilder.AppendLine("using System.Data;");
            classBuilder.AppendLine("using System.Text;");
            classBuilder.AppendLine("using DataAccessLayer;");
            classBuilder.AppendLine("using System.Threading.Tasks;");

            classBuilder.AppendLine();

            classBuilder.AppendLine($"    public class cls{ClsGloble.GetTableName}");
            classBuilder.AppendLine("    {");
            classBuilder.AppendLine("        public enum enMode { addNew = 0, update = 1 }");
            classBuilder.AppendLine("        public enMode Mode = enMode.addNew;");
            classBuilder.AppendLine();
            return classBuilder.ToString();
        }
        private static string GenerateProperty(DataRow dr)
        {
            string columnName = dr["ColumnName"].ToString();
            string dataType = ClsGloble.mapSqlTypeToCSharp(dr["DataType"].ToString());
            return $"        public {dataType} {columnName} {{ get; set; }}";
        }

        private static string GeneratePropertyForConstructor(DataRow dr)
        {
            string columnName = dr["ColumnName"].ToString();
            string dataType = ClsGloble.mapSqlTypeToCSharp(dr["DataType"].ToString());
            return $"         this.{columnName} = {clsGlobleCsharp.GenerateTheValueOfVariable(dataType)};";
        }

        // Private constructor
        public static string GeneratePrivateConstructor()
        {
            if (dtColumnTable == null || dtColumnTable.Rows.Count == 0)
                return string.Empty;

            StringBuilder constructorBuilder = new StringBuilder();

            // توليد التوقيع الخاص بالدالة
            constructorBuilder.AppendLine(GenerateParameterList() + ")");
            constructorBuilder.AppendLine("{");

            // توليد تعيين الخصائص داخل جسم الدالة
            constructorBuilder.AppendLine(GenerateAssignments());
            constructorBuilder.AppendLine("         this.Mode = enMode.update;");
            constructorBuilder.AppendLine("        }");
            return constructorBuilder.ToString();

        }

        private static string GenerateParameterList()
        {
            StringBuilder parameterListBuilder = new StringBuilder();

            for (int i = 0; i < dtColumnTable.Rows.Count; i++)
            {
                DataRow row = dtColumnTable.Rows[i];
                string columnName = row["ColumnName"].ToString();
                string dataType = ClsGloble.mapSqlTypeToCSharp(row["DataType"].ToString());

                parameterListBuilder.Append($"{dataType} {columnName}");
                if (i < dtColumnTable.Rows.Count - 1)
                    parameterListBuilder.Append(", ");
            }

            return parameterListBuilder.ToString();
        }

        private static string GenerateAssignments()
        {
            StringBuilder assignmentsBuilder = new StringBuilder();

            foreach (DataRow row in dtColumnTable.Rows)
            {
                string columnName = row["ColumnName"].ToString();
                assignmentsBuilder.AppendLine($"    this.{columnName} = {columnName};");
            }

            return assignmentsBuilder.ToString();
        }

       
    }
}
