using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Information_Retrieval
{
    public partial class Form1 : Form
    {
        //yaniv path//
        //private const string dicName = @"C:\info_ret\";
        //netanel path//
        private const string dicName = @"F:\לימודים\שנה ג\איחזור מידע\DOCS\";

        private MySqlConnection conn = DbConn.connect_to_MySQL();
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
            string queryString;
            MySqlCommand cmd;
            if (conn != null)
            {
                //conn.Close();
                //string text = System.IO.File.ReadAllText(@"C:\Users\netanels\Desktop\לימודים\פרויקט איחזור מידע\DOCS\Daniel7.txt");
                char[] delimiterChars = { ' ', ',', '.', ':', '\t', '\n', ';', '?' ,'\r' };

                List<string> words = new List<string>();
                words = fileText.ToLower().Split(delimiterChars).ToList<string>();

                //MessageBox.Show(words[21].ToString());
                //words.Remove(" ");
                int fileID = GetFileID(conn,fileName);
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
               
                foreach (KeyValuePair<string, int> entry in dictionary)
                {
                    string EZ_filePath = "'" + fileName + "'";
                    string EZ_word = "'" + entry.Key.Replace("'","\\'") + "'";
                    
                    if (checkIfExsit(fileName,entry.Key)) // function that check the key in the database
                        continue;
                    queryString = "INSERT INTO info_retrieval_db.words_tbl (file,word,count) VALUES" + "(" + EZ_filePath + "," + EZ_word + "," + entry.Value + ");" ;
                    try
                    {
                        //conn.Open();
                        cmd = new MySqlCommand(queryString,conn);
                        cmd.ExecuteNonQuery();

                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }

                Dictionary<string, HashSet<int>> wordsFiles = new Dictionary<string, HashSet<int>>();
                queryString = "SELECT * FROM info_retrieval_db.words_files;"; 
                cmd = new MySqlCommand(queryString, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string files = reader["files"].ToString();
                        HashSet<int> filesOfWords = new HashSet<int>();
                        foreach (string s in files.Split(','))
                        {
                            filesOfWords.Add(Int32.Parse(s));
                        }
                        wordsFiles.Add(reader["word"].ToString(), filesOfWords);
                    }
                    reader.Close();
                }

                foreach (KeyValuePair<string, int> entry in dictionary)
                {
                    if (wordsFiles.ContainsKey(entry.Key))
                    {
                        HashSet<int> filesID = wordsFiles[entry.Key];
                        if (!filesID.Contains(fileID))
                        {
                            StringBuilder filesString = new StringBuilder();
                            foreach(int i in filesID)
                            {
                                filesString.Append(i + ",");
                            }
                            filesString.Append(fileID);
                            queryString = "UPDATE info_retrieval_db.words_files SET files = '"
                                + filesString.ToString() + "' WHERE word = '" + entry.Key + "';";
                            try
                            {
                                cmd = new MySqlCommand(queryString, conn);
                                cmd.ExecuteNonQuery();
                            }
                            catch (MySqlException ex)
                            {
                                MessageBox.Show(queryString);
                                MessageBox.Show(ex.ToString());
                            }
                        }
                    }
                    else
                    {
                        reader.Close();
                        queryString = "INSERT INTO info_retrieval_db.words_files (word,files) VALUES" + "('" + entry.Key.Replace("'","\\'") + "','" + fileID + "');";
                        try
                        {
                            //conn.Open();
                            cmd = new MySqlCommand(queryString, conn);
                            cmd.ExecuteNonQuery();

                        }
                        catch (MySqlException ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
                if (conn != null)
                {
                    conn.Close();
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

        private int GetFileID(MySqlConnection conn, string fileName)
        {
            int fileID = -1;
            string queryString = "SELECT * FROM info_retrieval_db.files WHERE filename = " + "'" + fileName + "';";
            try
            {
                //conn.Open();
                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    fileID = reader.GetInt32("id");
                }
                else
                {
                    reader.Close();
                    queryString = "INSERT INTO info_retrieval_db.files (filename) VALUES" + "('" + fileName + "');";
                    try
                    {
                        //conn.Open();
                        cmd = new MySqlCommand(queryString,conn);
                        cmd.ExecuteNonQuery();
                        queryString = "SELECT * FROM info_retrieval_db.files WHERE filename = " + "'" + fileName + "';";
                        cmd = new MySqlCommand(queryString, conn);
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            fileID = reader.GetInt32("id");
                        }
                        reader.Close();
                    }
                    catch (MySqlException ex)
                    {
                        return fileID;
                    }
                }

            }
            catch (MySqlException ex)
            {
                return -1;
            }
            //finally
            //{
            //    if (conn != null)
            //    {
            //        conn.Close();
            //    }
            //}
            return fileID;
        }

        private bool checkIfExsit(string filePath, string key)
        {
            string queryString = "SELECT * FROM info_retrieval_db.words_tbl WHERE file = " + "'" + filePath  + "'" + "AND word = " + "'" + key + "'" + ";" ;
            try
            {
                //conn.Open();
                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                MySqlDataReader reader =  cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    return true;
                }
                else
                {
                    reader.Close();
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
          
            string query = txt_search.Text.Trim();
            ISet<string> filenames = new HashSet<string>();

            query = query.Replace("(", "( ");
            query = query.Replace(")", " )");
            string[] splitedquery = query.Split(' ');
            ISet<string> operands = new HashSet<string>();
            operands.Add("AND");
            operands.Add("OR");
            operands.Add("NOT");
            operands.Add("(");
            operands.Add(")");

            IDictionary<string, IDictionary<string, int>> fileWords = getFilesWords();

            DataTable dt = new DataTable();
            foreach (string fileName in fileWords.Keys)
            {
                StringBuilder result = new StringBuilder();
                foreach (string word in splitedquery)
                {
                    if (!operands.Contains(word))
                    {
                        result.Append(fileWords[fileName].ContainsKey(word) + " ");
                    }
                    else
                    {
                        result.Append(word + " ");
                    }
                }
                if ((bool)dt.Compute(result.ToString(), ""))
                {
                    filenames.Add(fileName);
                }

                
                //Console.WriteLine(filenames);
                //Console.WriteLine("hey");
            }
            foreach(string filename in filenames)
            {
                string finalPath = dicName + filename;
                Panel p = new Panel();
                LinkLabel l = new LinkLabel();
                Label lbl_intro = new Label();
                StreamReader s = new StreamReader(finalPath);
                l.Text = filename.ToUpper();
                l.Links.Add(0, l.Text.ToString().Length, finalPath);
                l.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(openTextFile);
                lbl_intro.Text = s.ReadLine();
                l.Size = new Size(85, 25);
                lbl_intro.Size = new Size(200, 30);
                p.Controls.Add(l);
                p.Controls.Add(lbl_intro);
                panel2.Controls.Add(p);
            }
        }

        //Dictionary<fileName, Dictionary<word,count>>
        private IDictionary<string, IDictionary<string, int>> getFilesWords()
        {
            string queryString = "SELECT * FROM info_retrieval_db.words_tbl;";
            IDictionary<string, IDictionary<string,int>> fileWords = new Dictionary<string, IDictionary<string,int>>();
            try
            {
                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    string fileName = reader["file"].ToString();
                    string word = reader["word"].ToString();
                    int count = Int32.Parse(reader["count"].ToString());
                    if (!fileWords.ContainsKey(fileName))
                    {
                        fileWords.Add(fileName, new Dictionary<string, int>());
                    }
                    fileWords[fileName].Add(word, count);
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
               
            }
            return fileWords;
        }

        private void openTextFile(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lnk = new LinkLabel();
            lnk = (LinkLabel)sender;
            lnk.Links[lnk.Links.IndexOf(e.Link)].Visited = true;
            string[] test = new string[] { "before" };
            Form2 form2 = new Form2(e.Link.LinkData.ToString(), test);
            form2.ShowDialog();
            //System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void יציאהToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
