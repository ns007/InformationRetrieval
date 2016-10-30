namespace Information_Retrieval
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.קובץToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.קליטתקובץלמאגרהנתוניםToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.מחיקתקובץמןמאגרהנתוניםToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.יציאהToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.עזרהToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.אודותToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.מדריךשימושToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.lbl_msg = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.התנתקToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_header);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 75);
            this.panel1.TabIndex = 0;
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
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.קובץToolStripMenuItem,
            this.עזרהToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 76);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // קובץToolStripMenuItem
            // 
            this.קובץToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.קליטתקובץלמאגרהנתוניםToolStripMenuItem,
            this.מחיקתקובץמןמאגרהנתוניםToolStripMenuItem,
            this.התנתקToolStripMenuItem,
            this.יציאהToolStripMenuItem});
            this.קובץToolStripMenuItem.Name = "קובץToolStripMenuItem";
            this.קובץToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.קובץToolStripMenuItem.Text = "קובץ";
            // 
            // קליטתקובץלמאגרהנתוניםToolStripMenuItem
            // 
            this.קליטתקובץלמאגרהנתוניםToolStripMenuItem.Name = "קליטתקובץלמאגרהנתוניםToolStripMenuItem";
            this.קליטתקובץלמאגרהנתוניםToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.קליטתקובץלמאגרהנתוניםToolStripMenuItem.Text = "קליטת קובץ למאגר הנתונים";
            this.קליטתקובץלמאגרהנתוניםToolStripMenuItem.Click += new System.EventHandler(this.קליטתקובץלמאגרהנתוניםToolStripMenuItem_Click);
            // 
            // מחיקתקובץמןמאגרהנתוניםToolStripMenuItem
            // 
            this.מחיקתקובץמןמאגרהנתוניםToolStripMenuItem.Name = "מחיקתקובץמןמאגרהנתוניםToolStripMenuItem";
            this.מחיקתקובץמןמאגרהנתוניםToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.מחיקתקובץמןמאגרהנתוניםToolStripMenuItem.Text = "מחיקת קובץ מן מאגר הנתונים";
            this.מחיקתקובץמןמאגרהנתוניםToolStripMenuItem.Click += new System.EventHandler(this.מחיקתקובץמןמאגרהנתוניםToolStripMenuItem_Click);
            // 
            // יציאהToolStripMenuItem
            // 
            this.יציאהToolStripMenuItem.Name = "יציאהToolStripMenuItem";
            this.יציאהToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.יציאהToolStripMenuItem.Text = "יציאה";
            this.יציאהToolStripMenuItem.Click += new System.EventHandler(this.יציאהToolStripMenuItem_Click);
            // 
            // עזרהToolStripMenuItem
            // 
            this.עזרהToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.אודותToolStripMenuItem,
            this.מדריךשימושToolStripMenuItem});
            this.עזרהToolStripMenuItem.Name = "עזרהToolStripMenuItem";
            this.עזרהToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.עזרהToolStripMenuItem.Text = "עזרה";
            // 
            // אודותToolStripMenuItem
            // 
            this.אודותToolStripMenuItem.Name = "אודותToolStripMenuItem";
            this.אודותToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.אודותToolStripMenuItem.Text = "אודות";
            this.אודותToolStripMenuItem.Click += new System.EventHandler(this.אודותToolStripMenuItem_Click);
            // 
            // מדריךשימושToolStripMenuItem
            // 
            this.מדריךשימושToolStripMenuItem.Name = "מדריךשימושToolStripMenuItem";
            this.מדריךשימושToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.מדריךשימושToolStripMenuItem.Text = "מדריך למשתמש";
            this.מדריךשימושToolStripMenuItem.Click += new System.EventHandler(this.מדריךשימושToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_search);
            this.groupBox1.Controls.Add(this.txt_search);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.groupBox1.ForeColor = System.Drawing.Color.DarkRed;
            this.groupBox1.Location = new System.Drawing.Point(0, 103);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(784, 55);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "חיפוש";
            // 
            // btn_search
            // 
            this.btn_search.ForeColor = System.Drawing.Color.Black;
            this.btn_search.Location = new System.Drawing.Point(396, 18);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 25);
            this.btn_search.TabIndex = 1;
            this.btn_search.Text = "חיפוש";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(477, 20);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(299, 21);
            this.txt_search.TabIndex = 0;
            // 
            // lbl_msg
            // 
            this.lbl_msg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_msg.Location = new System.Drawing.Point(0, 436);
            this.lbl_msg.Name = "lbl_msg";
            this.lbl_msg.Size = new System.Drawing.Size(784, 26);
            this.lbl_msg.TabIndex = 3;
            this.lbl_msg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(0, 164);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(784, 285);
            this.panel2.TabIndex = 4;
            // 
            // התנתקToolStripMenuItem
            // 
            this.התנתקToolStripMenuItem.Name = "התנתקToolStripMenuItem";
            this.התנתקToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.התנתקToolStripMenuItem.Text = "התנתק";
            this.התנתקToolStripMenuItem.Click += new System.EventHandler(this.התנתקToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lbl_msg);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(800, 500);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "מסך הבית";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_header;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem קובץToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem עזרהToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem קליטתקובץלמאגרהנתוניםToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.Label lbl_msg;
        private System.Windows.Forms.ToolStripMenuItem אודותToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel panel2;
        private System.Windows.Forms.ToolStripMenuItem יציאהToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem מדריךשימושToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem מחיקתקובץמןמאגרהנתוניםToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem התנתקToolStripMenuItem;
    }
}

