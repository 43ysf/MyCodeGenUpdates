using System.Data;
using System.Data.SqlClient;

namespace CodeGenAccess
{
    public static class clsColumns
    {
        public static  DataTable GetAlColumns(string DatabaseName, string TableName)
        {
            string Query = $@"Use {DatabaseName};
                            
                           WITH PrimaryKeys AS (
            SELECT 
                col.name AS COLUMN_NAME
            FROM 
                sys.key_constraints kc
            INNER JOIN 
                sys.index_columns ic ON kc.parent_object_id = ic.object_id AND kc.unique_index_id = ic.index_id
            INNER JOIN 
                sys.columns col ON ic.object_id = col.object_id AND ic.column_id = col.column_id
            WHERE 
                kc.type = 'pk' -- تحديد الأعمدة التي تشكل المفتاح الأساسي
                AND OBJECT_NAME(kc.parent_object_id) = @TableName -- اسم الجدول المطلوب
        )
        SELECT 
            c.COLUMN_NAME,
            c.DATA_TYPE,
            c.IS_NULLABLE AS Allow_Nulls, 
          
            CASE 
                WHEN pk.COLUMN_NAME IS NOT NULL THEN 'Yes'
                ELSE 'No'
            END AS IsPrimary
        FROM 
            INFORMATION_SCHEMA.COLUMNS c
        LEFT JOIN 
            PrimaryKeys pk ON c.COLUMN_NAME = pk.COLUMN_NAME
        WHERE 
            c.TABLE_NAME = @TableName ;" ;

            SqlCommand cmd = new SqlCommand(Query);
            cmd.Parameters.AddWithValue("@TableName", TableName);
            return  CRUD.GetAll(cmd);
        }

        public static bool FindByName( string TableName, string ColumnName, ref string DataType, ref bool AllowsNull, ref bool PrimaryKey)
        {
            bool IsFound  = false; 
            SqlConnection con = new SqlConnection(clsSettings.connectionString);
            string Query = @"SELECT  DATA_TYPE AS 'DataType',
						CASE WHEN IS_NULLABLE = 'YES' THEN '1' ELSE '0' END AS 'AllowsNull', 
						CASE WHEN COLUMN_NAME IN 
								(
					                SELECT COLUMN_NAME 
					                FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
					                WHERE TABLE_NAME = @TableName
					            ) THEN '1' ELSE '0' END AS 'PrimaryKey'
                                    FROM INFORMATION_SCHEMA.COLUMNS 
                                    WHERE TABLE_NAME = @TableName and COLUMN_NAME = @ColumnName";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@TableName", TableName);
            cmd.Parameters.AddWithValue("@ColumnName", ColumnName);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    DataType = reader["DataType"].ToString();
                    AllowsNull = (bool)reader["AllwosNull"];
                    PrimaryKey = (bool)reader["PrimaryKey"];
                    IsFound = true;
                }
                reader.Close();
            }
            catch
            {
                IsFound = false;
            }
            finally
            {
                con.Close();
            }
            return IsFound;

        }


    }
}
