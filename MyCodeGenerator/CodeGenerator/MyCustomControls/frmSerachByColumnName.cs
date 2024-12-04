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
    public partial class frmSerachByColumnName : Form
    {
        public delegate void CallBackEventhandler(object sender, string ColumnName,bool Press);
        public event CallBackEventhandler BackData;

        public frmSerachByColumnName()
        {
            InitializeComponent();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            string Columnname = txtColumnName.Text.Trim();
            BackData?.Invoke(this, Columnname,true);
            this.Close();
        }

        private void frmSerachByColumnName_Load(object sender, EventArgs e)
        {
           // labelControl3.Text = "* Make sure you enter the same name as in the table.";
           // labelControl1.Text = "Enter the name of the column you want to search by";
            txtColumnName.Focus();


        }

        private void btnCanceal_Click(object sender, EventArgs e)
        {
            BackData?.Invoke(this,"", false);
            this.Close();
        }

        private void labelControl3_MouseEnter(object sender, EventArgs e)
        {
          //  labelControl3.Text = "* تأكد من إدخال نفس الاسم الموجود في الجدول.";
          //  labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

        }

        private void labelControl3_MouseLeave(object sender, EventArgs e)
        {
           // labelControl3.Text = "* Make sure you enter the same name as in the table.";
        }

        private void labelControl1_MouseEnter(object sender, EventArgs e)
        {
           // labelControl1.Text = "أدخل اسم العمود الذي تريد البحث به";

          //  labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        private void labelControl1_MouseLeave(object sender, EventArgs e)
        {
          //  labelControl1.Text = "Enter the name of the column you want to search by";
        }

      
    }
}