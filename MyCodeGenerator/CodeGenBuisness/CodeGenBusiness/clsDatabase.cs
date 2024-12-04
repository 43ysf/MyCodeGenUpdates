using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGenAccess;
using CodeGenBuisness;

namespace CodeGenBusiness
{
    public class clsDatabase
    {
        public string DatabaseName { set; get; }
        public DataTable DatabaseTables { set; get; }

        public List<clsTable> TablesList { set; get; }
        public  static DataTable GetAllDataBases()
        {
            return  clsDatabases.GetAllDatabases();
        }

        public clsDatabase(string DatabaseName)
        {
            this.DatabaseName = DatabaseName;
            TablesList =  clsTable.GetAllTablesAsList(DatabaseName);
            DatabaseTables = clsTable.GetAllTables(DatabaseName);
            clsSetting.DatabaseName = DatabaseName;
            //clsSetting.GenerateConnectionString();
        }

        public async Task<bool> GenerateSotredProsdure()
        {


            foreach(clsTable tb in TablesList)
            {
                if (tb.StoredProcedures.Count > 0)
                    continue;

                if (await tb.GenerateAllStoredProcedure() == 0)
                {

                    return false;

                }
                tb.UpdateTableInfo();

            }
            return true;
        }
        public void GenerateDataAccess()
        {
            foreach (clsTable tb in TablesList)
            {
                tb.GenerateAllDtataAccessLayerMethods();
            }
        }

        public void GenerateDataBussiness()
        {
            foreach(clsTable tb in TablesList)
            {
                tb.GenerateAllBusinessLayerMethods();
            }
        }

        //With Sp
        public async Task GenerateAll()
        {
            foreach(clsTable tb in TablesList)
            {
                await tb.GenerateAllStoredProcedure();
                tb.UpdateTableInfo();
                await ProjectGenerator.CreateFullProject(tb.GenerateAllDtataAccessLayerMethods(),
                    tb.GenerateAllBusinessLayerMethods(), 
                    this.DatabaseName, tb.TableName, tb.CreateSettingClass(),
                    tb.ClassPrimaryFunction());
                
            }
            UpdateInfo();
        }

        //With Out Sp
        public async Task GenerateAllWithOutSP()
        {
            foreach (clsTable tb in TablesList)
            {
                tb.UpdateTableInfo();
                await ProjectGenerator.CreateFullProject(tb.GenerateAllDtataAccessLayerMethodsWithOutSP(), 
                                                         tb.GenerateAllBusinessLayerMethodsWithOutSP(), 
                                                         this.DatabaseName, tb.TableName,
                                                         tb.CreateSettingClass(),
                                                         tb.CRUD());

            }
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            TablesList = clsTable.GetAllTablesAsList(this.DatabaseName);
        }
    }
}
