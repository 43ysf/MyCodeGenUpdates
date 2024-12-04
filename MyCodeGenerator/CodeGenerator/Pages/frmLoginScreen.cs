using CodeGenBuisness;
using CodeGenerator.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGenerator.Pages
{
    public partial class frmLoginScreen : Form
    {
        public frmLoginScreen()
        {
            InitializeComponent();
        }




        Settings st = new Settings();
        static bool IsDark = false;
        public static string ThemeMode = "";

        private void _ApplyDarkMode()
        {
            if (IsDark == false)
            {
                ThemeMode = "Dark";
                this.BackColor = Color.Black;
                this.ForeColor = Color.White;
                lblCodeGenerator.ForeColor = Color.White;
                ptbImage.Image = Resources.crud;
                IsDark = true;

            }
            else
            {
                ThemeMode = "Ligth";
                this.BackColor = Color.White;
                this.ForeColor = Color.Black;
                lblCodeGenerator.ForeColor = Color.Black;

                ptbImage.Image = Resources.crud2;
                IsDark = false;
            }
        }

        private void txtUserID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserID.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserID, "Should Enter User ID!!");

            }
            else
                e.Cancel = false;
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Should Enter Password!!");

            }
            else
                e.Cancel = false;
        }

        private void txtServerName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtServerName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtServerName, "Should Enter ServerName!!");

            }
            else
                e.Cancel = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (st.Checked == false)
            {

                chRemmber.Checked = false;
            }
            else
            {

                txtUserID.Text = st.UserID;
                txtPassword.Text = st.Password;
                txtServerName.Text = st.ServerName;
                chRemmber.Checked = true;

            }
        }

        private void toggleSwitch2_Toggled(object sender, EventArgs e)
        {
            _ApplyDarkMode();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                string connectionString = $"Server={txtServerName.Text.Trim()};User Id={txtUserID.Text.Trim()};Password={txtPassword.Text.Trim()};";
                clsSetting.PrepareConnectionstring(txtServerName.Text.Trim(), txtUserID.Text.Trim(), txtPassword.Text.Trim());
                if (clsSetting.CheckConnection())
                {
                    frmMainScreen frm = new frmMainScreen();
                    this.Hide();
                    frm.ShowDialog();
                    Application.Exit();

                }
                else
                {
                    MessageBox.Show("Invalid connection", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Same Field Is Required!!", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void chRemmber_CheckedChanged(object sender, EventArgs e)
        {
            if (chRemmber.Checked)
            {
                st.Checked = true;
                st.UserID = txtUserID.Text;
                st.Password = txtPassword.Text;
                st.ServerName = txtServerName.Text;

            }
            else
            {
                st.Checked = false;
                st.UserID = "";
                st.Password = "";
                st.ServerName = "";


            }
            st.Save();


        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void labExit_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
    }
}
