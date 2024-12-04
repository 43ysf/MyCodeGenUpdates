using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CodeGenAccess;

namespace CodeGenBuisness
{
    public class clsProcedure
    {
        public string Name { get; set; }
        public List<clsParameter> Parameters {set; get; }
        public clsProcedure(string Name)
        {
            this.Name = Name;
            Parameters = clsParameter.GetAllSotredProcedureParameteres(Name);
        }

        public static List<clsProcedure> GetAllProcedures(string TableName)
        {
            DataTable dt = clsProcedures.GetAll(TableName);
            List<clsProcedure> procedures = new List<clsProcedure>();
            
                foreach (DataRow dr in dt.Rows)
                {
                    clsProcedure procedure = new clsProcedure(dr["SPECIFIC_NAME"].ToString());
                    procedures.Add(procedure);
                }
                return procedures;
            
        }
        
    }
}
