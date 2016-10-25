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
        List<Color> colors = new List<Color>();

        public Form2(string path,string[] serachWords)
        {
            InitializeComponent();
            _path = path;
            textFile = File.ReadAllText(path);
            textFile = textFile.Replace("\n", "");
            words = serachWords;
            colors.Add(Color.Blue);
            colors.Add(Color.Red);
            colors.Add(Color.Green);
            colors.Add(Color.Purple);
            colors.Add(Color.RosyBrown);
            colors.Add(Color.Orange);
            colors.Add(Color.Turquoise);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            rt_text.Text = textFile;
            int i = 0;
            foreach (string word in words)
            {
                foreach (int num in AllIndexesOf(textFile, word))
                {
                    rt_text.Select(num, word.Length);
                    rt_text.SelectionFont = new Font(rt_text.Font, FontStyle.Bold);
                    rt_text.SelectionColor = colors[i];
                }
                i++;
            }
        }

        public List<int> AllIndexesOf(string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
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
