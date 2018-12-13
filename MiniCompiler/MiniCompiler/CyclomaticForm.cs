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
    public partial class CyclomaticForm : Form
    {
        int fp=0,lp=0;
        string nodes=null;
        int nodecount,edgecount,vg11,vg2,vg3;
        string t;
       
       // string predicate1 = @"^\s*if[(]\w*\s*[+/*-%]\w*\s*[)]\s*$";
        string predicate2 = @"^\s*for|^\s*while";
        string predicate1 = @"^\s*if";
       // string opr = @"[>=]{2]|[<>]{2]|[<=]{2]|[!=]{2]|[!>]{2]|[!<]{2]|[-|+|=|>|<|&||]{1,2}|[/*%]-[^/[/|*](.+)$]";


        public Form RefToForm1 { get; set; }
        public CyclomaticForm()
        {
            InitializeComponent();
        }

        private int regexp(string s)
        {
            int count = 0;
            // String cs;
            Match match = Regex.Match(t, s, RegexOptions.Multiline);
            foreach (var item in t)
            {

                if (match.Success)
                {
                    richTextBox1.Select(match.Index, match.Length);
                    //richTextBox1.Select(match.Value,2);
                    // st = match.Value;
                    richTextBox1.SelectionBackColor = Color.Yellow;
                    count++;

                }
                match = match.NextMatch();
            }

            return count;
        }

      

        private void Home_Click(object sender, EventArgs e)
        {
            this.RefToForm1.Show();
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // Create an OpenFileDialog object.
            OpenFileDialog openFile1 = new OpenFileDialog();
            //OpenFileDialog openFile2=new OpenFileDialog();

           // openFile1.DefaultExt = "*.cs";
            // Initialize the filter to look for text files.
            openFile1.Filter = "CPP Files|*.cpp|CS Files|*.cs|C Files|*.c";

            // If the user selected a file, load its contents into the RichTextBox. 
            if (openFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                richTextBox1.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText);

            if (richTextBox1.Modified==true)
                listBox1.Items.Clear();


        }

        private void IdentifyNodes_Click(object sender, EventArgs e)
        {
            GenerateGraph.Enabled = true;

            

            for (int i = 1; i <=fp; i++)
            {

                if (fp == i)
                {
                    nodecount = (i * 4) + i-1;
                    edgecount = nodecount+lp + i - 1;
                }
               
                                
            }

            if (fp == 0 && lp >=1)
                for (int i = 1; i <= lp; i++)
                {
                    nodecount = lp;
                    edgecount = nodecount + i-1 ;    
                }

            if (fp==0&&lp==0)
            {
                nodecount = 0;
                edgecount = 0;
            }

            nodes = "";
            for (int i = 1; i<=nodecount; i++)
            {
                nodes += ("  "+i).ToString();    
            }
            
            listBox1.Items.Add("node creation");
            listBox1.Items.Add("Total Nodes Created:"+nodecount);
            listBox1.Items.Add("Nodes Created are:");
            listBox1.Items.Add(nodes);
        }

        private void GenerateGraph_Click(object sender, EventArgs e)
        {
            vg1.Enabled = true;
            
            listBox1.Items.Add("edge creation");
            listBox1.Items.Add("Total Edges Created:" +edgecount);
        }

        private void vg1_Click(object sender, EventArgs e)
        {
            formulae.Enabled = true;
            listBox1.Items.Add("Region identifying");
            listBox1.Items.Add("Total regions Identified as:" + (fp + lp+ 1));
            MessageBox.Show("Cyclomaatic Complexity v(G)  first approach is: " + (fp +lp+ 1));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            predicate.Enabled = true;
            vg11 = edgecount - nodecount + 2;
            if (nodecount == 0)
                vg11 = 1;
            MessageBox.Show("From Second Approach v(G) is calculated as: " + vg11);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            fp = regexp(predicate1);
            lp = regexp(predicate2);
            MessageBox.Show("Cyclomaatic Complexity v(G) third approach is: " + (fp+lp + 1));
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            t = richTextBox1.Text;


            fp = regexp(predicate1);
            lp = regexp(predicate2);
        }
    }
}
