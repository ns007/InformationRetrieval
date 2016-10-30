namespace Information_Retrieval
{
    partial class Form4
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_header = new System.Windows.Forms.Label();
            this.dgv_files = new System.Windows.Forms.DataGridView();
            this.מזהה = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_files)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_header);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 75);
            this.panel1.TabIndex = 1;
            // 
            // lbl_header
            // 
            this.lbl_header.AutoSize = true;
            this.lbl_header.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lbl_header.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lbl_header.Location = new System.Drawing.Point(221, 13);
            this.lbl_header.Name = "lbl_header";
            this.lbl_header.Size = new System.Drawing.Size(343, 42);
            this.lbl_header.TabIndex = 0;
            this.lbl_header.Text = "information retrieval";
            // 
            // dgv_files
            // 
            this.dgv_files.AllowUserToAddRows = false;
            this.dgv_files.AllowUserToDeleteRows = false;
            this.dgv_files.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_files.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.מזהה,
            this.Column1,
            this.Column3,
            this.Column4,
            this.Column2});
            this.dgv_files.Location = new System.Drawing.Point(12, 103);
            this.dgv_files.Name = "dgv_files";
            this.dgv_files.ReadOnly = true;
            this.dgv_files.Size = new System.Drawing.Size(760, 330);
            this.dgv_files.TabIndex = 2;
            this.dgv_files.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_files_CellContentClick);
            // 
            // מזהה
            // 
            this.מזהה.HeaderText = "KEY";
            this.מזהה.Name = "מזהה";
            this.מזהה.Visible = false;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "שם הקובץ";
            this.Column1.Name = "Column1";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "ספר";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "פרק";
            this.Column4.Name = "Column4";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "סטטוס";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.dgv_files);
            this.Controls.Add(this.panel1);
            this.Name = "Form4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form4";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_files)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_header;
        private System.Windows.Forms.DataGridView dgv_files;
        private System.Windows.Forms.DataGridViewTextBoxColumn מזהה;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewButtonColumn Column2;
    }
}