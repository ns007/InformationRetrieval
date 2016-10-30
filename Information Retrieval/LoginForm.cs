using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Information_Retrieval
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            Form1 form1;
            if (txt_username.Text.Trim().ToLower().Equals("admin"))
            {
                this.Visible = false;
                form1 = new Form1(true, this);
                form1.ShowDialog();
            }
            else if(txt_username.Text.Trim().ToLower().Equals("user"))
            {
                this.Visible = false;
                form1 = new Form1(false, this);
                form1.ShowDialog();
            }
            else
            {
                MessageBox.Show("No Such User, Please Try Different User");
                txt_username.Text = "";

            }
 
            
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        public void clearUser()
        {
            txt_username.Text = "";
        }
    }
}
