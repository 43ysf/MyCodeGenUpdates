using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;

namespace CodeGenAccess
{
    public static  class clsDatabases
    {
        public  static DataTable GetAllDatabases()
        {
            string Query = "SELECT name FROM sys.databases WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb');;";
            SqlCommand cmd = new SqlCommand(Query);

            return  CRUD.GetAll(cmd);
        }

    }
}