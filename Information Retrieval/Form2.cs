using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
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
        private string _path;
        private string[] words;

        public Form2(string path,string[] serachWords)
        {
            InitializeComponent();
            _path = path;
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

                    //if (line.Split(' ').Contains(word))
                    if (line.IndexOf(word.Trim()) != -1)
                    {
                        int srt = rt_text.Find(word);
                        rt_text.Select(srt, word.Length);
                        rt_text.SelectionFont = new Font(rt_text.Font, FontStyle.Bold);
                    }
                }
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        

        private int linesPrinted;
        private string[] lines;

        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            char[] param = { '\n' };

            if (printDialog1.PrinterSettings.PrintRange == PrintRange.Selection)
            {
                lines = rt_text.SelectedText.Split(param);
            }
            else
            {
                lines = rt_text.Text.Split(param);
            }

            int i = 0;
            char[] trimParam = { '\r' };
            foreach (string s in lines)
            {
                lines[i++] = s.TrimEnd(trimParam);
            }
        }

        private void OnPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = e.MarginBounds.Left - 70;
            int y = e.MarginBounds.Top;
            Brush brush = new SolidBrush(rt_text.ForeColor);

            while (linesPrinted < lines.Length)
            {
                e.Graphics.DrawString(lines[linesPrinted++],
                    rt_text.Font, brush,x, y);
                y += 15;
                if (y >= e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            linesPrinted = 0;
            e.HasMorePages = false;
        }


    }
}
