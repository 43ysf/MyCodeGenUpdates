using BussinsLayer;
using CodeGenBuisness;
using CodeGenBusiness;
using DevExpress.Utils;
using DevExpress.Utils.Extensions;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraEditors;
using SqlLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace CodeGenerator
{
    public partial class frmGeneratorSQL : DevExpress.XtraEditors.XtraForm
    {
        string SerachByName = "";
        frmLogin _frmLogin;
        bool ResultPress = false;
        bool cancealPress = false;
        clsTable _SelectedTable = null;

        private clsDatabase _DataBase = null;

        public frmGeneratorSQL()
        {
            InitializeComponent();

        }

        private void ShowTableInfo(clsTable table)
        {

            //_Table = new clsTable( _DataBase.DatabaseName,TableName);
            dgvTableInfo.DataSource = table.TableColumns;
            _SelectedTable = table;
            GetAllStoredProceduresForTable();

        }

        private void GetAllTableName()
        {

            if (floTableName.Controls.Count > 0)
            {
                floTableName.Controls.Clear();
            }

            foreach(clsTable table in _DataBase.TablesList)
            {
                SimpleButton button = new SimpleButton();
                string TableName = table.TableName;
                button.Text = TableName;
                button.Width = 180;
                button.Height = 30;
                button.Margin = new Padding(5);
                button.Click += (sender, e) =>
                {
                    ShowTableInfo(table);
                    //GetAllStoredProceduresForTable(TableName);
                    _SelectedTable = table; 

                };
                floTableName.Controls.Add(button);

            }

            //foreach (DataRow dr in _DataBase.DatabaseTables.Rows)
            //{
               
            //    SimpleButton button = new SimpleButton();
            //    string TableName = dr["TableName"].ToString();
            //    button.Text = TableName;
            //    button.Width = 180;
            //    button.Height = 30;
            //    button.Margin = new Padding(5);
            //    button.Click += (sender, e) =>
            //    {
            //        ShowTableInfo(TableName);
            //        GetAllStoredProceduresForTable(TableName);

            //    };
            //    floTableName.Controls.Add(button);                

            //}


        }

        private void GenerateAllBackend()
        {
            foreach(clsTable table in _DataBase.TablesList)
            {
                table.GetDataAccess();
                table.GetDataBusiness();
            }
        }
      

        private void GetDataBaseName()
        {

            DataTable dt = clsDatabase.GetAllDataBases();
            foreach(DataRow dr in dt.Rows)
            {
                SimpleButton button = new SimpleButton();
                string DatabaseName = dr["name"].ToString() ;
                button.Text = DatabaseName;
                button.Width = 190;
                button.Height = 30;
                button.Margin = new Padding(5);
                button.Click += (sender, e) =>
                {
                    _DataBase = new clsDatabase(DatabaseName); 
                    GetAllTableName();
                   // dgvTableInfo.DataSource = null;

                };
                floDataBaseName.Controls.Add(button);


            }

        }

        private void frmGeneratorSQL_Load(object sender, EventArgs e)
        {
            GetDataBaseName();
            marqueeProgressBarControl1.Visible = false;
        }

        //Check this Process Who..
        private void CheckIfThereIsProcess6()
        {
            frmCustomMessageBox messageBox = new frmCustomMessageBox();
            messageBox.getReslt += GetResultPress;
            messageBox.ShowDialog();


            if (ResultPress)
            {
                frmSerachByColumnName frm = new frmSerachByColumnName();
                frm.BackData += _BackData;
                frm.ShowDialog();
            }




        }

        private void GetResultPress(bool PressBtn)
        {
            ResultPress = PressBtn;
        }

        //Data Bakc From Form Input Column By Name .. Delegate.
        private void _BackData(object sender, string Serachbyname, bool Press)
        {
            SerachByName = Serachbyname;
            cancealPress = Press;

        }

        //After Prosses Done Show All StoredProceder
        private void GetAllStoredProceduresForTable(string TableName = "")
        {
            MessageBox.Show(_SelectedTable.DatabaseName);
            if (_SelectedTable.StoredProcedures != null)
            {
                floStoredProcedures.Controls.Clear();
               foreach(clsProcedure Pro in _SelectedTable.StoredProcedures)
                {
                    SimpleButton button = new SimpleButton();
                    button.Text = Pro.Name;
                    button.Width = 180;
                    button.Height = 30;
                    button.Margin = new Padding(5);
                    floStoredProcedures.Controls.Add(button);

                }
            }

        }

        private async Task StartProcess()
        {
            marqueeProgressBarControl1.Visible = true;
            marqueeProgressBarControl1.Text = $"Loding....";

            try
            {

                int result = await ClsGenericAllProceses.ProsessAll(SerachByName);
                int count = 1;

                if (result > 0)
                {
                    while (result > 0)
                    {
                        if (result == 1)
                            marqueeProgressBarControl1.Text = $" The Process is On Going....";
                        await Task.Delay(2000);
                        marqueeProgressBarControl1.Text = $" Prosses  {count}  Done";
                        await Task.Delay(2000);

                        result--;
                        count++;

                    }
                }
                else
                {
                    MessageBox.Show("Make sure you don't have a stored procedure for this table!", "Error");
                    marqueeProgressBarControl1.Text = "Erorr";
                    marqueeProgressBarControl1.BackColor = Color.Red;
                    marqueeProgressBarControl1.Properties.Appearance.ForeColor = Color.White;


                    await Task.Delay(2000);
                    marqueeProgressBarControl1.Visible = false;
                    return;
                }

                marqueeProgressBarControl1.Properties.MarqueeAnimationSpeed = 1000000;
                marqueeProgressBarControl1.BackColor = Color.Green;
                marqueeProgressBarControl1.Properties.Appearance.ForeColor = Color.White;
                marqueeProgressBarControl1.Text = $"operations were successfully completed.";
                await Task.Delay(1000);
                marqueeProgressBarControl1.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Server Erorr: {ex.Message}", "Error");
                marqueeProgressBarControl1.Text = "Erorr";
                marqueeProgressBarControl1.Properties.Appearance.BackColor = Color.Red;
                marqueeProgressBarControl1.Properties.Appearance.ForeColor = Color.White;

                await Task.Delay(1000);
                marqueeProgressBarControl1.Visible = false;


            }
            marqueeProgressBarControl1.Reset();
        }

        //Import BTN :-)
        private async void btnGeneric_Click(object sender, EventArgs e)
        {
            //CheckIfThereIsProcess6();

            //if (cancealPress && SerachByName != "")
            //{
            //    await StartProcess();
            //    GetAllStoredProceduresForTable(ClsGloble.GetTableName);
            //    MessageBox.Show("A folder has been created in the project, containing all the functions", "Successfully");
            //}
            //else if (!ResultPress)
            //{
            //    await StartProcess();
            //    GetAllStoredProceduresForTable(ClsGloble.GetTableName);
            //    MessageBox.Show("A folder has been created in the project, containing all the functions", "Successfully");

            //}

            if(await _SelectedTable.GenerateAllStoredProcedure() != 0)
            {
                MessageBox.Show("All Done");

                GetAllStoredProceduresForTable();

            }
            else
            {
                MessageBox.Show("Something Wrong");
            }


        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmGeneratorCsharp frm = new frmGeneratorCsharp(this);
            if (this.Visible)
                this.Hide();
            frm.ShowDialog();

        }

        private void frmGeneratorSQL_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();

        }

        private void frmGeneratorSQL_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void btnGetBus_Click(object sender, EventArgs e)
        {
            foreach(clsTable table in _DataBase.TablesList)
            {

               // MessageBox.Show(table._GenerateDeleteMethod());
                frmMyForm frm = new frmMyForm("yousif");
                frm.ShowDialog();

            }
        }


    }
}