namespace CodeGenerator
{
    partial class frmGeneratorSQL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGeneratorSQL));
            this.grbDataBaseName = new DevExpress.XtraEditors.GroupControl();
            this.floDataBaseName = new System.Windows.Forms.FlowLayoutPanel();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.floTableName = new System.Windows.Forms.FlowLayoutPanel();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.dgvTableInfo = new System.Windows.Forms.DataGridView();
            this.btnGeneric = new DevExpress.XtraEditors.SimpleButton();
            this.marqueeProgressBarControl1 = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.floStoredProcedures = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGetBus = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grbDataBaseName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.SuspendLayout();
            // 
            // grbDataBaseName
            // 
            this.grbDataBaseName.CaptionLocation = DevExpress.Utils.Locations.Top;
            this.grbDataBaseName.Controls.Add(this.floDataBaseName);
            this.grbDataBaseName.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grbDataBaseName.Location = new System.Drawing.Point(14, 15);
            this.grbDataBaseName.Margin = new System.Windows.Forms.Padding(4);
            this.grbDataBaseName.Name = "grbDataBaseName";
            this.grbDataBaseName.Size = new System.Drawing.Size(233, 565);
            this.grbDataBaseName.TabIndex = 0;
            this.grbDataBaseName.Text = "Data Base Name";
            // 
            // floDataBaseName
            // 
            this.floDataBaseName.AllowDrop = true;
            this.floDataBaseName.AutoScroll = true;
            this.floDataBaseName.Location = new System.Drawing.Point(6, 32);
            this.floDataBaseName.Margin = new System.Windows.Forms.Padding(4);
            this.floDataBaseName.Name = "floDataBaseName";
            this.floDataBaseName.Size = new System.Drawing.Size(222, 527);
            this.floDataBaseName.TabIndex = 2;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.floTableName);
            this.groupControl1.Location = new System.Drawing.Point(254, 15);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(239, 565);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Table Name";
            // 
            // floTableName
            // 
            this.floTableName.AllowDrop = true;
            this.floTableName.AutoScroll = true;
            this.floTableName.Location = new System.Drawing.Point(6, 32);
            this.floTableName.Margin = new System.Windows.Forms.Padding(4);
            this.floTableName.Name = "floTableName";
            this.floTableName.Size = new System.Drawing.Size(222, 527);
            this.floTableName.TabIndex = 3;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.dgvTableInfo);
            this.groupControl2.Location = new System.Drawing.Point(495, 15);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(4);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(546, 331);
            this.groupControl2.TabIndex = 2;
            this.groupControl2.Text = "Table Info";
            // 
            // dgvTableInfo
            // 
            this.dgvTableInfo.AllowUserToAddRows = false;
            this.dgvTableInfo.AllowUserToDeleteRows = false;
            this.dgvTableInfo.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvTableInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableInfo.Location = new System.Drawing.Point(6, 32);
            this.dgvTableInfo.Margin = new System.Windows.Forms.Padding(4);
            this.dgvTableInfo.Name = "dgvTableInfo";
            this.dgvTableInfo.ReadOnly = true;
            this.dgvTableInfo.RowHeadersWidth = 51;
            this.dgvTableInfo.Size = new System.Drawing.Size(534, 293);
            this.dgvTableInfo.TabIndex = 0;
            // 
            // btnGeneric
            // 
            this.btnGeneric.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGeneric.Appearance.Options.UseFont = true;
            this.btnGeneric.Location = new System.Drawing.Point(670, 353);
            this.btnGeneric.Margin = new System.Windows.Forms.Padding(4);
            this.btnGeneric.Name = "btnGeneric";
            this.btnGeneric.Size = new System.Drawing.Size(410, 76);
            this.btnGeneric.TabIndex = 3;
            this.btnGeneric.Text = "Generic Stored Procedure For This Table";
            this.btnGeneric.ToolTipAnchor = DevExpress.Utils.ToolTipAnchor.Object;
            this.btnGeneric.Click += new System.EventHandler(this.btnGeneric_Click);
            // 
            // marqueeProgressBarControl1
            // 
            this.marqueeProgressBarControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.marqueeProgressBarControl1.EditValue = "";
            this.marqueeProgressBarControl1.Location = new System.Drawing.Point(670, 452);
            this.marqueeProgressBarControl1.Margin = new System.Windows.Forms.Padding(4);
            this.marqueeProgressBarControl1.Name = "marqueeProgressBarControl1";
            // 
            // 
            // 
            this.marqueeProgressBarControl1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.marqueeProgressBarControl1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.marqueeProgressBarControl1.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.marqueeProgressBarControl1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.marqueeProgressBarControl1.Properties.ShowTitle = true;
            this.marqueeProgressBarControl1.Size = new System.Drawing.Size(410, 33);
            this.marqueeProgressBarControl1.TabIndex = 4;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(1124, 528);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(158, 52);
            this.simpleButton1.TabIndex = 6;
            this.simpleButton1.Text = "Next";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.floStoredProcedures);
            this.groupControl4.Location = new System.Drawing.Point(1042, 15);
            this.groupControl4.Margin = new System.Windows.Forms.Padding(4);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(239, 331);
            this.groupControl4.TabIndex = 8;
            this.groupControl4.Text = "Stored Procedures";
            // 
            // floStoredProcedures
            // 
            this.floStoredProcedures.AllowDrop = true;
            this.floStoredProcedures.AutoScroll = true;
            this.floStoredProcedures.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.floStoredProcedures.Location = new System.Drawing.Point(6, 30);
            this.floStoredProcedures.Margin = new System.Windows.Forms.Padding(4);
            this.floStoredProcedures.Name = "floStoredProcedures";
            this.floStoredProcedures.Size = new System.Drawing.Size(221, 295);
            this.floStoredProcedures.TabIndex = 3;
            // 
            // btnGetBus
            // 
            this.btnGetBus.Location = new System.Drawing.Point(501, 384);
            this.btnGetBus.Name = "btnGetBus";
            this.btnGetBus.Size = new System.Drawing.Size(163, 95);
            this.btnGetBus.TabIndex = 9;
            this.btnGetBus.Text = "tnGetAllBus";
            this.btnGetBus.UseVisualStyleBackColor = true;
            this.btnGetBus.Click += new System.EventHandler(this.btnGetBus_Click);
            // 
            // frmGeneratorSQL
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1312, 594);
            this.Controls.Add(this.btnGetBus);
            this.Controls.Add(this.groupControl4);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.btnGeneric);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.grbDataBaseName);
            this.Controls.Add(this.marqueeProgressBarControl1);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("frmGeneratorSQL.IconOptions.Image")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmGeneratorSQL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Base Info";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmGeneratorSQL_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmGeneratorSQL_FormClosed);
            this.Load += new System.EventHandler(this.frmGeneratorSQL_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grbDataBaseName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grbDataBaseName;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.FlowLayoutPanel floDataBaseName;
        private System.Windows.Forms.FlowLayoutPanel floTableName;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.DataGridView dgvTableInfo;
        private DevExpress.XtraEditors.SimpleButton btnGeneric;
        private DevExpress.XtraEditors.MarqueeProgressBarControl marqueeProgressBarControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private System.Windows.Forms.FlowLayoutPanel floStoredProcedures;
        private System.Windows.Forms.Button btnGetBus;
    }
}