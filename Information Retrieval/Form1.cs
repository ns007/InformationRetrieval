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
        private const string dicName = @"C:\info_ret\";
        //netanel path//
        ////private const string dicName = @"F:\לימודים\שנה ג\איחזור מידע\DOCS\";

        private MySqlConnection conn = DbConn.connect_to_MySQL();
        //int x = 3; 
        private bool isAdmmin;
        private LoginForm loginForm;
        ISet<string> operands = new HashSet<string>();
        ISet<string> notOperands = new HashSet<string>();
        ISet<string> stopList = new HashSet<string>();
        public Form1(bool isAdmmin, LoginForm loginForm)
        {
            InitializeComponent();
            operands.Add("AND");
            operands.Add("OR");
            operands.Add("NOT");
            operands.Add("(");
            operands.Add(")");

            stopList.Add("that");
            stopList.Add("end");
            stopList.Add("for");
            stopList.Add("a");
            stopList.Add("the");
            stopList.Add("an");
            stopList.Add("like");
            stopList.Add("as");
            this.isAdmmin = isAdmmin;
            this.loginForm = loginForm;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!isAdmmin)
            {
                קליטתקובץלמאגרהנתוניםToolStripMenuItem.Visible = false;
                מחיקתקובץמןמאגרהנתוניםToolStripMenuItem.Visible = false;
            }
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
            openFileDialog1.Multiselect = true;

            // Process input if the user clicked OK.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Open the selected file to read.
                foreach(string s in openFileDialog1.FileNames)
                {
                    fileName = Path.GetFileName(s);
                    filePath = s;
                    File.Copy(filePath, dicName + fileName);
                    System.IO.Stream fileStream = openFileDialog1.OpenFile();
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(fileStream))
                    {
                        // Read the first line from the file and write it the textbox.
                        fileText = reader.ReadToEnd();

                    }
                    fileStream.Close();
                    saveFileInDB(fileText, fileName);
                }
                MessageBox.Show("הקבצים נקלטו בהצלחה");
            }
            
        }

        private void saveFileInDB(string fileText , string fileName)
        {
            string queryString;
            MySqlCommand cmd;
            if (conn != null)
            {
                char[] delimiterChars = { ' ', ',', '.', ':', '\t', '\n', ';', '?' ,'\r' };

                List<string> words = new List<string>();
                words = fileText.ToLower().Split(delimiterChars).ToList<string>();

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
                //if (conn != null)
                //{
                //    conn.Close();
                //}
                //MessageBox.Show("קובץ נקלט בהצלחה");
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
                    reader.Close();
                }
                else
                {
                    string[] splitedFileName = fileName.Split(' ');
                    reader.Close();
                    queryString = "INSERT INTO info_retrieval_db.files (filename, book, chapter,active) VALUES" + "('" + fileName + "','" + 
                        splitedFileName[0] + "'," + Int32.Parse(splitedFileName[1].Replace(".txt","")) + ",TRUE);";
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
            panel2.Controls.Clear();
            string query = txt_search.Text.Trim();
            ISet<string> filenames = new HashSet<string>();

            query = query.Replace("(", "( ");
            query = query.Replace(")", " )");
            string[] splitedquery = query.Split(' ');
            IDictionary<string, IDictionary<string, int>> fileWords = getFilesWords();
            List<string> list = new List<string>(query.Split(' '));
            for(int i = 0; i < list.Count; i++)
            {
                if (stopList.Contains(list[i]))
                {
                    if(i != 0)
                    {
                        list.Remove(list[i - 1]);
                        list.Remove(list[i - 1]);
                    }else
                    {
                        list.Remove(list[i]);
                    }
                }
            }
            DataTable dt = new DataTable();
            foreach (string fileName in fileWords.Keys)
            {
                StringBuilder result = new StringBuilder();
                foreach (string word in list)
                {
                    if (!operands.Contains(word))
                    {
                        result.Append(fileWords[fileName].ContainsKey(word) + " ");
                        notOperands.Add(word);
                    }
                    else
                    {
                        result.Append(word + " ");
                    }
                }
                if (result.ToString().Equals(""))
                {
                    result.Append("false");
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
                string queryString = "SELECT * FROM info_retrieval_db.files WHERE filename = " + "'" + filename + "';";
                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                bool isActive = false;
                if (reader.HasRows)
                {
                    reader.Read();
                    isActive = Boolean.Parse(reader["active"].ToString());
                    reader.Close();
                }

                if (isActive)
                {
                    string finalPath = dicName + filename;
                    Panel p = new Panel();
                    ToolTip tip = new ToolTip();
                    p.Size = new Size(784, 60);
                    LinkLabel l = new LinkLabel();
                    Label lbl_intro = new Label();
                    StreamReader s = new StreamReader(finalPath);
                    l.Text = filename.ToUpper();
                    l.Size = new Size(784, 25);
                    l.Links.Add(0, l.Text.ToString().Length, finalPath);
                    l.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(openTextFile);
                    // tokens: {"before":24, "after":}
                    StringBuilder sb = new StringBuilder();
                    int i = 0;
                    int appearances = 0;
                    sb.Append("Word Appearances Countings:\n\n");
                    foreach (string word in notOperands)
                    {
                        try
                        {
                            appearances = fileWords[filename][word];
                        }
                        catch (KeyNotFoundException knfe)
                        {
                            appearances = 0;
                        }
                        sb.Append("-" + word + ":\t" + appearances + "\n");
                    }
                    tip.SetToolTip(l, sb.ToString());
                    lbl_intro.Text = s.ReadLine();
                    lbl_intro.Location = new System.Drawing.Point(0, 30);
                    lbl_intro.Size = new Size(784, 30);
                    p.Controls.Add(l);
                    p.Controls.Add(lbl_intro);
                    panel2.Controls.Add(p);
                }
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
            string[] test = notOperands.ToArray(); // need to send array of all the words we search
            Form2 form2 = new Form2(e.Link.LinkData.ToString(), test);
            form2.ShowDialog();
            //System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void יציאהToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void מדריךשימושToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();


        }
        private void מחיקתקובץמןמאגרהנתוניםToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(conn);
            form4.ShowDialog();
        }

        private void התנתקToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            loginForm.clearUser();
            loginForm.Visible = true;
        }
    }
}
