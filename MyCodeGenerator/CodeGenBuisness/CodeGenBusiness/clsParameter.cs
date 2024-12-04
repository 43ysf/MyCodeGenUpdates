using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGenAccess;
using MyCSharpAndSQLLib;

namespace CodeGenBuisness
{
    public class clsParameter
    {
        public string Name { get; set; }
        public string DataType { get; set; }    
        //public string SotredProcedureName { get; set; } 
        public clsParameter( string Name, string DatType)
        {
            //this.SotredProcedureName = SotredProcedureName;
            this.Name = Name;
            this.DataType = DatType;
            
        }


        public static DataTable GetAllSotredProcedure(string StoredProcedure)
        {
            return clsParameters.GetAllSotredProcedureParameters(StoredProcedure);
        }

        public static List<clsParameter> GetAllSotredProcedureParameteres(string StoredProcedureName)
        {
            DataTable dt = GetAllSotredProcedure(StoredProcedureName);
            List<clsParameter> parameters = new List<clsParameter>();
            foreach(DataRow dr in dt.Rows)
            {
                clsParameter parameter = new clsParameter(dr["ParameterName"].ToString(), clsSql.SqlToCsharbDataType( dr["DataType"].ToString()));
                parameters.Add(parameter);

            }
            return parameters;

        }



    }
}
