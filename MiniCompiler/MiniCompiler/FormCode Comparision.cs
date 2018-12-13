using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniCompiler
{
    public partial class Code_Comparision : Form
    {
         String t1, t2;
         int c1, c2;
        CompilationClass k = new CompilationClass();

        
        public Code_Comparision()
        {
            InitializeComponent();
            
        }

        private void Home_Click(object sender, EventArgs e)
        {
            StartForm s1 = new StartForm();
            s1.Show();
            
            this.Close();
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
                richTextBox1.LoadFile(openFile1.FileName,             RichTextBoxStreamType.PlainText);
            



            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            // Create an OpenFileDialog object.
            OpenFileDialog openFile1 = new OpenFileDialog();
            //OpenFileDialog openFile2=new OpenFileDialog();

            // Initialize the filter to look for text files.
            openFile1.Filter = "CS Files|*.cs";

            // If the user selected a file, load its contents into the RichTextBox. 
            if (openFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                richTextBox2.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText);
            
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {

        }

        private void Code_Comparision_Load(object sender, EventArgs e)
        {

        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            t1 = richTextBox1.Text;
            c1 = richTextBox1.Lines.Count();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            t2 = richTextBox2.Text;
            c2 = richTextBox2.Lines.Count();
        }


        private void Compare_Click(object sender, EventArgs e)
        {
            //Time Complexity//SpaceComplexity
            Stopwatch stopwatch = Stopwatch.StartNew(); //creates and start the instance of Stopwatch
            
            
            Process proc = Process.GetCurrentProcess();
            k.CompileCSharp(t1, "Sample.exe");
            long m1 = proc.PrivateMemorySize64;
            
            
            stopwatch.Stop();

            long s1=stopwatch.ElapsedMilliseconds;

            Stopwatch stopwatch2 = Stopwatch.StartNew(); //creates and start the instance of Stopwatch

            Process proc2 = Process.GetCurrentProcess();
            k.CompileCSharp(t2, "Sample.exe");            
            long m2 = proc2.PrivateMemorySize64;
            stopwatch2.Stop();

            long s2 = stopwatch2.ElapsedMilliseconds;
            
            
            
            

            if(s1>s2 && c1>c2 )
            {
                MessageBox.Show("program2 is better than program1");
            }

            else
            {
                MessageBox.Show("program1 is better than program2");
            }

        }

        
    }
}
