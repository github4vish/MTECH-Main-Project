using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace MiniCompiler
{
    public partial class Performance_of_code : Form
    {
       public int c;
        String t;
       public long s,m;
       public int l;
        CompilationClass k = new CompilationClass();

        public Form RefToForm1 { get; set; }
        public Performance_of_code()
        {
            InitializeComponent();
            
            

        }

        private void Home_Click(object sender, EventArgs e)
        {
            this.Close();
            this.RefToForm1.Show();
            
        }

        

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // Create an OpenFileDialog object.
            OpenFileDialog openFile1 = new OpenFileDialog();
            //OpenFileDialog openFile2=new OpenFileDialog();

            // Initialize the filter to look for text files.
            openFile1.Filter = "CS Files|*.cs";

            // If the user selected a file, load its contents into the RichTextBox. 
            if (openFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                richTextBox1.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText);
            
        }

        

        private void SLOCandFP_Click(object sender, EventArgs e)
        {
            c = richTextBox1.Lines.Count();
            MessageBox.Show("The Lines of code is calculated as :"+c);
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
            t = richTextBox1.Text;
            //carr = Convert.ToString(richTextBox1.Text);
         //   arr=richTextBox1.Lines;
        }

        private void TimeComplexity_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = Stopwatch.StartNew(); //creates and start the instance of Stopwatch
//your sample code
            k.CompileCSharp(t, "Sample.exe");

stopwatch.Stop();
s = stopwatch.ElapsedMilliseconds;
MessageBox.Show("\n  The Time Complexity for the given program is:"+s+" MilliSeconds");

//LinesOfCode.Enabled = true;
            
        }

        private void OverallPerformance_Click(object sender, EventArgs e)
        {
            MessageBox.Show("\n OVERALL PERFORMANCE \n Time Complexity:"+s+"\n Space Complexity:"+m+"\n Time Sorce Lines of Code:"+c);
        }

        private void SpaceCompleity_Click(object sender, EventArgs e)
        {


            Process proc = Process.GetCurrentProcess();
            proc.Refresh();
            k.CompileCSharp(t, "Sample.exe");
            
            m = proc.PrivateMemorySize64;
            MessageBox.Show("\n  The Time Complexity for the given program is:" + m + " bytes");


            //TimeComplexity.Enabled = true;
        }

        

        private void overallPerformance_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("\n OVERALL PERFORMANCE \n Time Complexity:" + s + "\n Space Complexity:" + m );
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        

        
    }
}
