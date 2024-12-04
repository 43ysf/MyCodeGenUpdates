using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGenerator
{
    public partial class frmCustomMessageBox : Form
    {
       public delegate void getResultPress(bool Result);
       public  getResultPress getReslt;

        public frmCustomMessageBox()
        {
            InitializeComponent();
        }

        private void frmCustomMessageBox_Load(object sender, EventArgs e)
        {
            labMessage.Text = "Do you want to create a function that searches by a specific column name?";
        }

        private void labMessage_MouseEnter(object sender, EventArgs e)
        {
            labMessage.Text = "هل تريد انشاء دالة تبحث عن طريق اسم عامود معين؟";
            labMessage.RightToLeft = RightToLeft.Yes;
            labMessage.TextAlign = ContentAlignment.TopCenter;


        }

        private void labMessage_MouseLeave(object sender, EventArgs e)
        {
            labMessage.Text = "Do you want to create a function that searches by a specific column name?";
            labMessage.RightToLeft = RightToLeft.No;
            labMessage.TextAlign = ContentAlignment.TopLeft;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            getReslt?.Invoke(true);
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            getReslt?.Invoke(false);
            this.Close();


        }
    }
}
