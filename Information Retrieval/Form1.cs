﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Information_Retrieval
{
    public partial class Form1 : Form
    {
        private const string dicName = @"F:\לימודים\שנה ג\איחזור מידע\DOCS\";
        //int x = 3; 
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void אודותToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("נתנאל שילה 201198058 \n יניב ספיר 201198355","מגישים");
        }

        private void קליטתקובץלמאגרהנתוניםToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = null, filePath = null, fileText = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;

            // Process input if the user clicked OK.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Open the selected file to read.
                fileName = Path.GetFileName(openFileDialog1.FileName);
                filePath = openFileDialog1.FileName;
                File.Copy(filePath, dicName + fileName);
                System.IO.Stream fileStream = openFileDialog1.OpenFile();
                using (System.IO.StreamReader reader = new System.IO.StreamReader(fileStream))
                {
                    // Read the first line from the file and write it the textbox.
                    fileText = reader.ReadToEnd();
                    
                }
                fileStream.Close();
            }
            saveFileInDB(fileText, fileName);
        }

        private void saveFileInDB(string fileText , string fileName)
        {
            if (DbConn.connect_to_MySQL() == true)
            {
                DbConn.conn.Close();
                //string text = System.IO.File.ReadAllText(@"C:\Users\netanels\Desktop\לימודים\פרויקט איחזור מידע\DOCS\Daniel7.txt");
                char[] delimiterChars = { ' ', ',', '.', ':', '\t', '\n', ';', '?' ,'\r' };

                List<string> words = new List<string>();
                words = fileText.ToLower().Split(delimiterChars).ToList<string>();
                
                //MessageBox.Show(words[21].ToString());
                //words.Remove(" ");

                Dictionary<string, int> dictionary = new Dictionary<string, int>();
                foreach (string word in words)
                {
                    if(word.Length < 1)
                        continue;
                    if (dictionary.ContainsKey(word))
                    {
                        dictionary[word] += 1;
                    }
                    else
                    {
                        dictionary.Add(word, 1);
                    }
                }
                
                foreach(KeyValuePair<string, int> entry in dictionary)
                {
                    string EZ_filePath = "'" + fileName + "'";
                    string EZ_word = "'" + entry.Key.Replace("'","\\'") + "'";
                    
                    if (checkIfExsit(fileName,entry.Key)) // function that check the key in the database
                        continue;
                    string queryString = "INSERT INTO info_retrieval_db.words_tbl (file,word,count) VALUES" + "(" + EZ_filePath + "," + EZ_word + "," + entry.Value + ");" ;
                    try
                    {
                        DbConn.conn.Open();
                        MySqlCommand cmd = new MySqlCommand(queryString,DbConn.conn);
                        cmd.ExecuteNonQuery();

                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        if (DbConn.conn != null)
                        {
                            DbConn.conn.Close();
                        }
                    }
                }
                MessageBox.Show("קובץ נקלט בהצלחה");
                //StringHelper z = new StringHelper();
            }
            else
            {
                MessageBox.Show("Database connetion error");
                return;
            }
        }


        private bool checkIfExsit(string filePath, string key)
        {
            string queryString = "SELECT * FROM info_retrieval_db.words_tbl WHERE file = " + "'" + filePath  + "'" + "AND word = " + "'" + key + "'" + ";" ;
            try
            {
                DbConn.conn.Open();
                MySqlCommand cmd = new MySqlCommand(queryString, DbConn.conn);
                MySqlDataReader reader =  cmd.ExecuteReader();
                if (reader.HasRows)
                    return true;
                else
                    return false;

            }
            catch (MySqlException ex)
            {
                return false;
            }
            finally
            {
                if (DbConn.conn != null)
                {
                    DbConn.conn.Close();
                }

            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            //לבדוק בדאטאבייס מה מתאים
            //string search = "'" + txt_search.Text.Trim() + "'";
            QueryBuilder1 qb = new QueryBuilder1();
            List<string> oper =  qb.GetQuery(txt_search.Text.Trim());
            string queryString = "SELECT * FROM info_retrieval_db.words_tbl WHERE word = " + "'" + txt_search.Text.Trim() + "'" + ";";
            if (DbConn.connect_to_MySQL() == true)
            {
                try
                {
                    //DbConn.conn.Open();
                    MySqlCommand cmd = new MySqlCommand(queryString, DbConn.conn);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string finalPath = dicName + reader["file"].ToString();
                            Panel p = new Panel();
                            LinkLabel l = new LinkLabel();
                            Label lbl_intro = new Label();
                            StreamReader s = new StreamReader(finalPath);
                            l.Text = reader["word"].ToString();
                            l.Links.Add(0, l.Text.ToString().Length, finalPath);
                            l.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(openTextFile);
                            lbl_intro.Text = s.ReadLine();
                            l.Size = new Size(85, 25);
                            lbl_intro.Size = new Size(200, 30);
                            p.Controls.Add(l);
                            p.Controls.Add(lbl_intro);
                            panel2.Controls.Add(p);
                            //panel2.Controls.SetChildIndex(l, 0);
                            //l.Show();
                        }
                    }
                    else
                        MessageBox.Show("There is no word in any file in the database");

                }
                catch (MySqlException ex)
                {
                    //return false;
                }
                finally
                {
                    if (DbConn.conn != null)
                    {
                        DbConn.conn.Close();
                    }

                }
            }

        }

        private void openTextFile(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = new LinkLabel();
            lnk = (LinkLabel)sender;
            lnk.Links[lnk.Links.IndexOf(e.Link)].Visited = true;
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void יציאהToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}