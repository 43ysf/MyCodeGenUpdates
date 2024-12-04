using CodeGenAccess;
using CodeGenBuisness;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CodeGenBusiness
{
    public partial class clsTable
    {
        public string con = clsSettings.connectionString;
        public string TableName { set; get; }
        public string DatabaseName { set; get; }
        public DataTable TableColumns { set; get; }
        public List<clsColumn> Columns { set; get; }
        public List<clsProcedure> StoredProcedures { set; get; } 
        public static  DataTable GetAllTables(string DatabaseName)
        {
            return  clsTables.GetAll(DatabaseName);
        }
        public clsTable(string DatabaseName, string TableName)
        {
            this.TableName = TableName;
            this.DatabaseName = DatabaseName;
            this.StoredProcedures = clsProcedure.GetAllProcedures(this.TableName);
            TableColumns = clsColumn.GetAllColumns(DatabaseName, TableName);
            Columns = clsColumn.GetAllColumnsAsList(DatabaseName, TableName);
        }


        //Properties
        public  static List<clsTable> GetAllTablesAsList(string DatabaseName)
        {
            List<clsTable> tables = new List<clsTable>();
            clsTable table = null;
            DataTable dt =  GetAllTables(DatabaseName);
            foreach(DataRow dr in dt.Rows)
            {

                table = new clsTable(DatabaseName, dr["TableName"].ToString());
                tables.Add(table);

            }

            return tables;
        }
        private clsColumn _GetPrimaryKeyColumn()
        {
            if (Columns[0].IsPrimaryKey)
            {
                return Columns[0];
            }
            foreach(clsColumn column in Columns)
            {
                if(column.IsPrimaryKey)
                    return column;
            }
            return null;
        }
        private clsColumn _GetNotPrimaryCol()
        {
            foreach(clsColumn column in Columns)
            {
                if (!column.IsPrimaryKey)
                    return column;
            }
            return Columns[1];
        }
        private string _GenerateMethodParameters(bool TakePrimaryKey, bool AddRef)
        {

            StringBuilder parameters = new StringBuilder();

            for(int i = 0; i < Columns.Count; i++)
            {
                if (TakePrimaryKey)
                {
                    if (Columns[i].IsPrimaryKey && AddRef)
                    {
                        parameters.Append($"{Columns[i].ColumnType} {Columns[i].ColumnName}");
                        if (i < Columns.Count - 1)
                            parameters.Append(", ");
                        continue;

                    }


                    if (AddRef)
                        parameters.Append($" ref {Columns[i].ColumnType} {Columns[i].ColumnName}");
                    else
                        parameters.Append($"{Columns[i].ColumnType} {Columns[i].ColumnName}");

                    if (i < Columns.Count - 1)
                        parameters.Append(", ");


                }
                else
                {
                    if (!Columns[i].IsPrimaryKey)
                    {
                        if(AddRef)
                            parameters.Append($"ref {Columns[i].ColumnType} {Columns[i].ColumnName}");
                        else
                            parameters.Append($"{Columns[i].ColumnType} {Columns[i].ColumnName}");


                        if (i < Columns.Count - 1)
                            parameters.Append(", ");

                    }
                }
            }

            return parameters.ToString();
        }
        private  string _GenerateReaderAssignments()
        {
            var assignmentBuilder = new StringBuilder();

            for (int i = 1; i < Columns.Count; i++)
            {
                // بناء السطر بناءً على نوع البيانات في C#
                if (Columns[i].IsAllowNull )
                {
                    if (Columns[i].ColumnType == "int" || Columns[i].ColumnType == "short")
                        assignmentBuilder.AppendLine($"\t\t\t\t{Columns[i].ColumnName} = reader[\"{Columns[i].ColumnName}\"] != DBNull.Value ? ({Columns[i].ColumnType})reader[\"{Columns[i].ColumnName}\"] : -1;");
                    else
                        assignmentBuilder.AppendLine($"\t\t\t\t{Columns[i].ColumnName} = reader[\"{Columns[i].ColumnName}\"] != DBNull.Value ? ({Columns[i].ColumnType})reader[\"{Columns[i].ColumnName}\"] : null;");

                }
                else
                {
                    assignmentBuilder.AppendLine($"\t\t\t\t{Columns[i].ColumnName} = ({Columns[i].ColumnType})reader[\"{Columns[i].ColumnName}\"];");
                }
            }

            return assignmentBuilder.ToString();
        }    
        public void UpdateTableInfo()
        {
            StoredProcedures = clsProcedure.GetAllProcedures(TableName);
        }

    }

}
