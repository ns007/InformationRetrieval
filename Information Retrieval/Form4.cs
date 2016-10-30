using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Information_Retrieval
{
    public partial class Form4 : Form
    {
        private MySqlCommand cmd;
        private MySqlConnection conn;
        public Form4(MySqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            readData();
        }

        private void readData()
        {
            string queryString = "SELECT * from info_retrieval_db.files ";
            cmd = new MySqlCommand(queryString, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                int irow = 0;
                while (reader.Read())
                {
                    DataGridViewRow dr = new DataGridViewRow();
                    dgv_files.Rows.Add(dr);
                    dgv_files[0, irow].Value = reader["id"].ToString();
                    dgv_files[1, irow].Value = reader["filename"].ToString();
                    dgv_files[2, irow].Value = reader["book"].ToString();
                    dgv_files[3, irow].Value = reader["chapter"].ToString();
                    dgv_files[4, irow].Value = reader["active"].ToString();
                    irow++;
                }
                reader.Close();
            }
        }

        private void dgv_files_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                int  key = Int32.Parse(dgv_files[0, e.RowIndex].Value.ToString());
                //שאליתא לעדכון 
                dgv_files[4, e.RowIndex].Value = dgv_files[4, e.RowIndex].Value.ToString().Equals("True") ? "False" : "True";
                string queryString = "update info_retrieval_db.files set active="+ dgv_files[4, e.RowIndex].Value + " where id="+key+ ";";
                cmd = new MySqlCommand(queryString, conn);
                cmd.ExecuteNonQuery();
                //TODO - Button Clicked - Execute Code Here
            }
        }
    }
}
