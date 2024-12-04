using CodeGenAccess;
using MyCSharpAndSQLLib;
using System.Collections.Generic;
using System.Data;

namespace CodeGenBusiness
{
    public class clsColumn
    {
        public string ColumnName { set; get; }
        public string ColumnType { set; get; }  
        public bool IsAllowNull { set; get; }   
        public bool IsPrimaryKey { set; get; }
        public string Value { set; get; }

        //public bool IsForeignKey { set; get; }
        //public bool IsUinqueKey { set; get; } 


       public clsColumn(string ColumnName, string ColumnType, bool IsAllowNull, bool IsPrimaryKey)
        {
            this.ColumnName = ColumnName;
            this.ColumnType = ColumnType;
            this.IsAllowNull = IsAllowNull;
            this.IsPrimaryKey = IsPrimaryKey;
            this.Value = clsSql.GenerateTheValueOfVariable(ColumnType);
        }

       public static DataTable GetAllColumns(string DatabaseName, string TableName)
        {
            return clsColumns.GetAlColumns(DatabaseName, TableName);
        }

       public static List<clsColumn> GetAllColumnsAsList(string DatabaseName, string TableName)
        {
            DataTable dt = GetAllColumns(DatabaseName, TableName);
            clsColumn Column = null;
            bool IsPrimary = false;
            bool IsAllowNull = false;
            List<clsColumn> Columns = new List<clsColumn>();
            foreach(DataRow dr in dt.Rows)
            {
                IsAllowNull = dr["Allow_Nulls"].ToString().ToLower() == "yes";
                IsPrimary = dr["IsPrimary"].ToString().ToLower() == "yes";
                Column = new clsColumn(dr["COLUMN_NAME"].ToString(), clsSql.SqlToCsharbDataType(dr["DATA_TYPE"].ToString()), IsAllowNull, IsPrimary);
                
                Columns.Add(Column);

            }
            return Columns;
        }


    }



}
