namespace CodeGenerator.Pages
{
    partial class frmMainScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainScreen));
            this.pnMain = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.labExit = new System.Windows.Forms.Label();
            this.btDALandBLL = new System.Windows.Forms.Button();
            this.btSP = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnMain
            // 
            this.pnMain.Location = new System.Drawing.Point(0, 82);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(1091, 520);
            this.pnMain.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.labExit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1096, 35);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 33);
            this.label1.TabIndex = 1;
            this.label1.Text = "V.0.2 ( Beta )";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labExit
            // 
            this.labExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.labExit.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labExit.Location = new System.Drawing.Point(1051, 0);
            this.labExit.Name = "labExit";
            this.labExit.Size = new System.Drawing.Size(43, 33);
            this.labExit.TabIndex = 0;
            this.labExit.Text = "X";
            this.labExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labExit.Click += new System.EventHandler(this.labExit_Click);
            // 
            // btDALandBLL
            // 
            this.btDALandBLL.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btDALandBLL.Location = new System.Drawing.Point(235, 2);
            this.btDALandBLL.Name = "btDALandBLL";
            this.btDALandBLL.Size = new System.Drawing.Size(225, 38);
            this.btDALandBLL.TabIndex = 2;
            this.btDALandBLL.Text = "Generator DAL and BLL";
            this.btDALandBLL.UseVisualStyleBackColor = true;
            this.btDALandBLL.Click += new System.EventHandler(this.btDALandBLL_Click);
            // 
            // btSP
            // 
            this.btSP.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSP.Location = new System.Drawing.Point(2, 2);
            this.btSP.Name = "btSP";
            this.btSP.Size = new System.Drawing.Size(225, 38);
            this.btSP.TabIndex = 0;
            this.btSP.Text = "Generator SP && DAL_BLL";
            this.btSP.UseVisualStyleBackColor = true;
            this.btSP.Click += new System.EventHandler(this.btSP_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkSlateGray;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btSP);
            this.panel2.Controls.Add(this.btDALandBLL);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1096, 45);
            this.panel2.TabIndex = 0;
            // 
            // frmMainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 610);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMainScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMainScreen";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btDALandBLL;
        private System.Windows.Forms.Button btSP;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labExit;
        private System.Windows.Forms.Label label1;
    }
}