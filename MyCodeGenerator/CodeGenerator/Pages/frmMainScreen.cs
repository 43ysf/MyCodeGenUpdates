using System;
using System.Windows.Forms;

namespace CodeGenerator.Pages
{
    public partial class frmMainScreen : Form
    {
        public frmMainScreen()
        {
            InitializeComponent();
        }

        private void btSP_Click(object sender, EventArgs e)
        {
            strSQL st = new strSQL();
            pnMain.Controls.Clear();
            st._Mode = strSQL.enMode.SP;
            pnMain.Controls.Add(st);
            st.Show();

        }

        private void btDALandBLL_Click(object sender, EventArgs e)
        {
            strSQL st = new strSQL();
            pnMain.Controls.Clear();
            st._Mode = strSQL.enMode.Get_DALL_BLL;
            pnMain.Controls.Add(st);
        }

        private void labExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
