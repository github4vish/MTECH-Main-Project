using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MiniCompiler
{
    public partial class TestForm : Form
    {
     String t,fn;


     public Form RefToForm1 { get; set; }
   public TestForm()
        {
            InitializeComponent();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {

            saveFileDialog1.ShowDialog();

            //saveFileDialog1.FileName
            fn = saveFileDialog1.FileName;
            string filename = fn;
            if (filename != "")
            {
                FileInfo fobj = new FileInfo(filename);
                if (!fobj.Exists)
                {
                    
                    StreamWriter sw = new StreamWriter(filename);
                    string content = t;
                    sw.Write(content);
                    sw.Close();
                    MessageBox.Show("Successfully Saved.");
                }
                else
                    MessageBox.Show("File already exists!");
            }

           
        
        }
    
       
    
    


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
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

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            t = richTextBox1.Text;
            button1.Enabled=true;
        }
           
        }
    }
