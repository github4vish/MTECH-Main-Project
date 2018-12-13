using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MiniCompiler
{
    public partial class FunctionalPoints : Form
    {
        string t;


       string ac = @"(public|private|protected)?\s*";
       string rt = @"(void|byte|sbyte|int|uint|short|ushort|long|ulong|float|double|char|bool|object|string|decimal)\s*";

       string ft = @"\w*[\p{P}\s\p{S}]*\s*\w+\d*[\p{P}\s\p{S}]*\s*\w+\d*[(][\p{P}\w*\s*,]*[)]$";



        //string rt = @"^\s*public|private|protected\s*\w*[)]\s*$";

    
      //  string end = @"";
        //string end = @"\b\w+(?=[)]\b)";

       public Form RefToForm1 { get; set; }
        int count;
        public FunctionalPoints()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // Create an OpenFileDialog object.
            OpenFileDialog openFile1 = new OpenFileDialog();
            //OpenFileDialog openFile2=new OpenFileDialog();

            // Initialize the filter to look for text files.
            openFile1.Filter = "CPP Files|*.cpp|C Files|*.c|CS Files|*.cs";

            // If the user selected a file, load its contents into the RichTextBox. 
            if (openFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                richTextBox1.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText);
        }


        private void regexp()
        {
            listBox1.Items.Clear();
             count = 0;
             Match match = Regex.Match(t, "[\n]"+ac+rt+ft , RegexOptions.Multiline);
            foreach (var item in t)
            {

                if (match.Success)
                {
                    richTextBox1.Select(match.Index, match.Length);
                    richTextBox1.SelectionBackColor = Color.Yellow; 

                    listBox1.Items.Add(match.Value);
                    count++;
                }
                match = match.NextMatch();
            }

            //return count;
        }

        private void Home_Click(object sender, EventArgs e)
        {
            this.Close();
            this.RefToForm1.Show();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            regexp();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Total number of Functional Points are: "+count);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            t = richTextBox1.Text;
        }
    }
}
