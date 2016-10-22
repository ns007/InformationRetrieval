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
    public partial class Form2 : Form
    {
        private string textFile;
        private string[] words;
        public Form2(string path,string[] serachWords)
        {
            InitializeComponent();
            textFile = File.ReadAllText(path);
            words = serachWords;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            rt_text.Text = textFile;
            foreach (string word in words)  
            {
                foreach (string line in rt_text.Lines)
                {
                    if (line.Contains(word))
                    {
                        int srt = rt_text.Find(word);
                        rt_text.Select(srt, word.Length);
                        rt_text.SelectionFont = new Font(rt_text.Font, FontStyle.Bold);
                    }
                }
            }
        }
    }
}
