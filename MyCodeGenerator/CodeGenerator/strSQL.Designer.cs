namespace CodeGenerator
{
    partial class strSQL
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnGenrealAll = new System.Windows.Forms.Button();
            this.floDataBaseName = new System.Windows.Forms.FlowLayoutPanel();
            this.panDataBaesHeader = new System.Windows.Forms.Panel();
            this.labDatabaseName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labTableName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.floTableName = new System.Windows.Forms.FlowLayoutPanel();
            this.panDgv = new System.Windows.Forms.Panel();
            this.dgvTableInfo = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panSpHeader = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.floStoredProcedures = new System.Windows.Forms.FlowLayoutPanel();
            this.labMessage = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.panDataBaesHeader.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panDgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableInfo)).BeginInit();
            this.panel3.SuspendLayout();
            this.panSpHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenrealAll
            // 
            this.btnGenrealAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenrealAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenrealAll.Location = new System.Drawing.Point(437, 279);
            this.btnGenrealAll.Name = "btnGenrealAll";
            this.btnGenrealAll.Size = new System.Drawing.Size(396, 48);
            this.btnGenrealAll.TabIndex = 29;
            this.btnGenrealAll.Text = "Genret All BackEnd";
            this.btnGenrealAll.UseVisualStyleBackColor = true;
            this.btnGenrealAll.Click += new System.EventHandler(this.btnGenerateAll);
            this.btnGenrealAll.MouseHover += new System.EventHandler(this.btnGenerateAll_MouseHover);
            // 
            // floDataBaseName
            // 
            this.floDataBaseName.AutoScroll = true;
            this.floDataBaseName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.floDataBaseName.Location = new System.Drawing.Point(3, 39);
            this.floDataBaseName.Name = "floDataBaseName";
            this.floDataBaseName.Size = new System.Drawing.Size(221, 481);
            this.floDataBaseName.TabIndex = 31;
            // 
            // panDataBaesHeader
            // 
            this.panDataBaesHeader.BackColor = System.Drawing.Color.DarkCyan;
            this.panDataBaesHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panDataBaesHeader.Controls.Add(this.labDatabaseName);
            this.panDataBaesHeader.Controls.Add(this.label2);
            this.panDataBaesHeader.Location = new System.Drawing.Point(3, 3);
            this.panDataBaesHeader.Name = "panDataBaesHeader";
            this.panDataBaesHeader.Padding = new System.Windows.Forms.Padding(1);
            this.panDataBaesHeader.Size = new System.Drawing.Size(221, 36);
            this.panDataBaesHeader.TabIndex = 33;
            // 
            // labDatabaseName
            // 
            this.labDatabaseName.AutoSize = true;
            this.labDatabaseName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labDatabaseName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDatabaseName.ForeColor = System.Drawing.Color.White;
            this.labDatabaseName.Location = new System.Drawing.Point(119, 10);
            this.labDatabaseName.Name = "labDatabaseName";
            this.labDatabaseName.Size = new System.Drawing.Size(36, 16);
            this.labDatabaseName.TabIndex = 3;
            this.labDatabaseName.Text = "None";
            this.labDatabaseName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Data Base Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkCyan;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labTableName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(224, 3);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(1);
            this.panel1.Size = new System.Drawing.Size(200, 36);
            this.panel1.TabIndex = 35;
            // 
            // labTableName
            // 
            this.labTableName.AutoSize = true;
            this.labTableName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labTableName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTableName.ForeColor = System.Drawing.Color.White;
            this.labTableName.Location = new System.Drawing.Point(88, 9);
            this.labTableName.Name = "labTableName";
            this.labTableName.Size = new System.Drawing.Size(36, 16);
            this.labTableName.TabIndex = 2;
            this.labTableName.Text = "None";
            this.labTableName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(4, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Table Name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // floTableName
            // 
            this.floTableName.AutoScroll = true;
            this.floTableName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.floTableName.Location = new System.Drawing.Point(224, 39);
            this.floTableName.Name = "floTableName";
            this.floTableName.Size = new System.Drawing.Size(200, 481);
            this.floTableName.TabIndex = 34;
            // 
            // panDgv
            // 
            this.panDgv.Controls.Add(this.dgvTableInfo);
            this.panDgv.Location = new System.Drawing.Point(424, 40);
            this.panDgv.Name = "panDgv";
            this.panDgv.Size = new System.Drawing.Size(439, 232);
            this.panDgv.TabIndex = 36;
            // 
            // dgvTableInfo
            // 
            this.dgvTableInfo.AllowUserToAddRows = false;
            this.dgvTableInfo.AllowUserToDeleteRows = false;
            this.dgvTableInfo.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvTableInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTableInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTableInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTableInfo.Location = new System.Drawing.Point(0, 0);
            this.dgvTableInfo.Name = "dgvTableInfo";
            this.dgvTableInfo.ReadOnly = true;
            this.dgvTableInfo.Size = new System.Drawing.Size(439, 232);
            this.dgvTableInfo.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkCyan;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(424, 3);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(1);
            this.panel3.Size = new System.Drawing.Size(439, 36);
            this.panel3.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(185, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 18);
            this.label4.TabIndex = 1;
            this.label4.Text = "Table Info";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panSpHeader
            // 
            this.panSpHeader.BackColor = System.Drawing.Color.DarkCyan;
            this.panSpHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panSpHeader.Controls.Add(this.label5);
            this.panSpHeader.Location = new System.Drawing.Point(863, 3);
            this.panSpHeader.Name = "panSpHeader";
            this.panSpHeader.Padding = new System.Windows.Forms.Padding(1);
            this.panSpHeader.Size = new System.Drawing.Size(218, 36);
            this.panSpHeader.TabIndex = 39;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(60, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 18);
            this.label5.TabIndex = 2;
            this.label5.Text = "Stord Procedures";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // floStoredProcedures
            // 
            this.floStoredProcedures.AutoScroll = true;
            this.floStoredProcedures.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.floStoredProcedures.Location = new System.Drawing.Point(862, 39);
            this.floStoredProcedures.Name = "floStoredProcedures";
            this.floStoredProcedures.Size = new System.Drawing.Size(219, 481);
            this.floStoredProcedures.TabIndex = 38;
            // 
            // labMessage
            // 
            this.labMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(478, 375);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(309, 75);
            this.labMessage.TabIndex = 40;
            this.labMessage.Text = "label6";
            this.labMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labMessage.Visible = false;
            // 
            // progressBar
            // 
            this.progressBar.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.progressBar.Location = new System.Drawing.Point(481, 332);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(309, 26);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 41;
            this.progressBar.Visible = false;
            // 
            // strSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.labMessage);
            this.Controls.Add(this.panSpHeader);
            this.Controls.Add(this.floStoredProcedures);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panDgv);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.floTableName);
            this.Controls.Add(this.panDataBaesHeader);
            this.Controls.Add(this.floDataBaseName);
            this.Controls.Add(this.btnGenrealAll);
            this.Name = "strSQL";
            this.Size = new System.Drawing.Size(1091, 571);
            this.Load += new System.EventHandler(this.strSQL_Load);
            this.panDataBaesHeader.ResumeLayout(false);
            this.panDataBaesHeader.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panDgv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableInfo)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panSpHeader.ResumeLayout(false);
            this.panSpHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGenrealAll;
        private System.Windows.Forms.FlowLayoutPanel floDataBaseName;
        private System.Windows.Forms.Panel panDataBaesHeader;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel floTableName;
        private System.Windows.Forms.Panel panDgv;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panSpHeader;
        private System.Windows.Forms.FlowLayoutPanel floStoredProcedures;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labTableName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labDatabaseName;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.DataGridView dgvTableInfo;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}
