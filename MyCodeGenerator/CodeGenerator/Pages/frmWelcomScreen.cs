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
    public partial class frmWelcomScreen : Form
    {
        public frmWelcomScreen()
        {
            InitializeComponent();
            this.labelCopyright.Text = "Copyright © " + DateTime.Now.Year.ToString();

        }

        private async void frmWelcomScreen_Load(object sender, EventArgs e)
        {
            progressBar1.MarqueeAnimationSpeed = 50;
            await Task.Delay(3000);
            frmLoginScreen lod = new frmLoginScreen();
            this.Hide();
            lod.ShowDialog();

        }
    }
}
