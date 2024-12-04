using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharpAndSQLLib
{
    public static  class clsSql
    {

        public static string CsharbToSqlDataType(string csharpType)
        {
            switch (csharpType.ToLower())
            {
                case "int":
                    return "INT";
                case "long":
                    return "BIGINT";
                case "short":
                    return "SMALLINT";
                case "byte":
                    return "TINYINT";
                case "bool":
                    return "BIT";
                case "float":
                    return "FLOAT";
                case "double":
                    return "REAL";
                case "decimal":
                    return "DECIMAL";
                case "datetime":
                    return "DATETIME";
                case "timespan":
                    return "TIME";
                case "string":
                    return "NVARCHAR(MAX)";
                case "byte[]":
                    return "VARBINARY(MAX)";
                case "object":
                    return "SQL_VARIANT";
                default:
                    return "NVARCHAR(255)";
            }
        }

        public static string GenerateTheValueOfVariable(string dataType)
        {
            switch (dataType.ToLower())
            {
                case "int": return "-1";
                case "string": return "\"\"";
                case "datetime": return "DateTime.MinValue";
                case "bool": return "false";
                case "varbinary(max)": return "null";
                case "float": return "0.0f";
                case "double": return "0.0";
                case "decimal": return "0.0m";
                case "char": return "'\\0'";
                default: return "null";
            }

        }

        public static string SqlToCsharbDataType(string dataTypeSQL)
        {

            switch (dataTypeSQL.ToLower())
            {
                case "int":
                    return "int";
                case "bigint":
                    return "long";
                case "smallint":
                    return "int";
                case "tinyint":
                    return "byte";
                case "bit":
                    return "bool";
                case "float":
                    return "float";
                case "real":
                    return "double";
                case "decimal":
                case "numeric":
                case "money":
                case "smallmoney":
                    return "decimal";
                case "datetime":
                case "smalldatetime":
                    return "DateTime";
                case "char":
                case "nchar":
                case "varchar":
                case "nvarchar":
                case "text":
                case "ntext":
                    return "string";
                case "binary":
                case "varbinary":
                case "image":
                    return "byte[]";
                default:
                    return "string";
            }
        }



    }
}
