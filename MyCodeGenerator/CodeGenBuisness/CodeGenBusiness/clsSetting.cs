using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGenAccess;
namespace CodeGenBuisness
{
    public static class clsSetting
    {
        private static string _databaseName;
        public static string DatabaseName
        {

            set
            {
                _databaseName = value;
                clsSettings.connectionString = $"Server={server};Database={DatabaseName};User ID ={UserID}; Password={Password}";

            }
            get
            {
                return _databaseName;
            }


        }
        public static string server { set; get; }
        public static string UserID { set; get; }
        public static string Password { set; get; }
        
        public static void PrepareConnectionstring(string severName, string userID, string password)
        {
            server = severName ;
            UserID = userID;
            Password = password ;
            clsSettings.connectionString = $"Server={server};User ID ={UserID}; Password={Password}";
        }

        public static bool CheckConnection()
        {
            return clsSettings.CheckConnection();
        }

    }
}
