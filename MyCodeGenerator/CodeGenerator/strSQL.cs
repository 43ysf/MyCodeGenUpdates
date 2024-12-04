using CodeGenBuisness;
using CodeGenBusiness;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGenerator
{
    public partial class strSQL : UserControl
    {
     
        Stopwatch stopwatch = new Stopwatch();
        clsDatabase _DataBase = null;
        clsTable _SelectedTable = null;

        public enum enMode { SP = 0, Get_DALL_BLL = 1}
        public  enMode _Mode = enMode.SP;
        public strSQL()
        {
            InitializeComponent();
        }

        private void strSQL_Load(object sender, EventArgs e)
        {
            GetAllDataBases();
            switch (_Mode)
            {
                case enMode.Get_DALL_BLL:
                    floStoredProcedures.Visible = false;
                    panSpHeader.Visible = false;

                    break;


                case enMode.SP:
                    floStoredProcedures.Visible = true;
                    panSpHeader.Visible = true;

                    break;




            }
        }

        private void ShowTableInfo(clsTable table)
        {
            

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

            foreach (clsTable table in _DataBase.TablesList)
            {
                Button button = new Button();
                string TableName = table.TableName;
                button.Text = TableName;
                button.Width = 180;
                button.Height = 30;
                button.BackColor = Color.White;
                button.ForeColor = Color.Black;
                button.Margin = new Padding(5);
                button.Click += (sender, e) =>
                {
                    ShowTableInfo(table);
                    _SelectedTable = table;
                    labTableName.Text= TableName;

                };
                floTableName.Controls.Add(button);

            }

        }

        private void GetAllDataBases()
        {
            

            DataTable dt = clsDatabase.GetAllDataBases();
            foreach(DataRow dr in dt.Rows)
            {
                string DatabaseName = dr["name"].ToString();


                if (!string.IsNullOrEmpty(DatabaseName))
                {
                    Button button = new Button();
                    button.Text = DatabaseName;
                    button.Width = 200;
                    button.Height = 30;
                    button.BackColor = Color.White;
                    button.ForeColor = Color.Black;
                    button.Margin = new Padding(5);
                    button.Click += (sender, e) =>
                    {
                        if (dgvTableInfo.Rows.Count > 0||floStoredProcedures.Controls.Count>0)
                        {
                            dgvTableInfo.DataSource = null;
                            floStoredProcedures.Controls.Clear();
                        }
                        labTableName.Text = "None";
                        _DataBase = new clsDatabase(DatabaseName);
                        
                       labDatabaseName.Text= DatabaseName;

                        GetAllTableName();


                    };
                    floDataBaseName.Controls.Add(button);


                }
                else
                    MessageBox.Show("Server Is Empty!!");

            }


        }

        private void GetAllStoredProceduresForTable()
        {
            _SelectedTable.UpdateTableInfo();
            if (_SelectedTable.StoredProcedures != null)
            {
                floStoredProcedures.Controls.Clear();
                foreach (clsProcedure Pro in _SelectedTable.StoredProcedures)
                {
                    Button button = new Button();
                    button.Text = Pro.Name;
                    button.Width = 200;
                    button.Height = 30;
                    button.BackColor = Color.White;
                    button.ForeColor = Color.Black;
                    button.Margin = new Padding(5);
                    floStoredProcedures.Controls.Add(button);

                }
            }

        }

        private async Task ProcessCompleted()
        {
            progressBar.MarqueeAnimationSpeed = 100;
            MessageBox.Show("Successfully Prosess Taked " + stopwatch.ElapsedMilliseconds + " ms");

            await Task.Delay(1000);
            progressBar.Visible = false;
        }

        private void btnGenerateAll_MouseHover(object sender, EventArgs e)
        {
            switch (_Mode)
            {
                case enMode.Get_DALL_BLL:
                    labMessage.Text = "*Note: Only backend layers will be built without a Stored Procedures.";
                    labMessage.Visible = true;
                    break;


                case enMode.SP:
                    labMessage.Text = "*Note: All backends, including the Stored Procedures, will be built, and the layers(DAL,BLL) will be built accordingly.";
                    labMessage.Visible = true;
                    break;
            }
        }


        //Import BTN :-)
        private async void btnGenerateAll(object sender, EventArgs e)
        {
            stopwatch.Start();
            switch (_Mode)
            {
                case enMode.SP:
                    progressBar.Visible = true;

                    try
                    {
                        if (_DataBase == null)
                            return;
                        await Task.WhenAll(_DataBase.GenerateAll());
                        stopwatch.Stop();

                        await ProcessCompleted();
                        
                       GetAllStoredProceduresForTable();


                    }
                    catch (Exception ex)
                    {
                        progressBar.Visible = false;
                        MessageBox.Show("Erorr: " + ex.Message);
                    }
                    break;


                case enMode.Get_DALL_BLL:
                    if (_SelectedTable == null)
                    {
                        MessageBox.Show("You should choose tabel first");
                        return;
                    }
                    try
                    {
                        progressBar.Visible = true;

                        string DAL = _SelectedTable.GenerateAllDtataAccessLayerMethodsWithOutSP();
                        string BLL = _SelectedTable.GenerateAllBusinessLayerMethodsWithOutSP();

                        await Task.WhenAll(_DataBase.GenerateAllWithOutSP());
                        stopwatch.Stop();

                        await ProcessCompleted();


                    }
                    catch (Exception ex)
                    {
                        progressBar.Visible = false;
                        MessageBox.Show("Erorr: " + ex.Message);
                    }
                    break;
            }


        }
    }

}

