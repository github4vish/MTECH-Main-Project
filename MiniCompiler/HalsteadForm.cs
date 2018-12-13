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
    public partial class HalsteadForm : Form
    {
        double m, N,m1, m2, N1, N2;
        double V,L,D,E,B,V1,B1,T;
        string s1,t,st;


        public Form RefToForm1 { get; set; }
        //string 
        //string verbatimStrings = @"""((\\[^\n]|[^""\n])*)""";
        string brace = @"[\p{Pe}\p{Ps}]";
        string opr = @"[\p{Sm}-/%]";
        string dt = @"(?<=[byte|sbyte|int|uint|short|ushort|long|ulong|float|double|char|bool|object|string|decimal][\s|,]\b)[\w\d]+";
        //string opr1 = @"[>=]{2]|[<>]{2]|[<=]{2]|[!=]{2]|[!>]{2]|[!<]{2]|[-|+|=|>|<|&||]{1,2}|[/*%]-[^/[/|*](.+)$]";

        string rt = @"\b(_\w+|[\w-[0-9_]]\w*)\b";
       // string opn = @"(?<=\b[>=]{2]|[<>]{2]|[<=]{2]|[!=]{2]|[!>]{2]|[!<]{2]|[-|+|=|>|<|&||]{1,2}|[/*%]-[^/[/|*](.+)$]\s*)\w+\b";
        string lopn = @"(?<=[\p{Sm}-/%]\s?)[a-zA-Z0-9]+";
        string ropn = @"[a-zA-Z0-9]+(?=\s?[\p{Sm}-/%])";
        
        string oprCount;
        string opnCount;
        
        public HalsteadForm()
        {
            InitializeComponent();
            // s = richTextBox1.Text;
        }

           private int regexp(string s)
        {
            int count = 0;
           // String cs;
            Match match = Regex.Match(t, s, RegexOptions.Singleline);
            foreach (var item in t)
            {

                if (match.Success)
                {
                    //if (match.Value == "cout" | match.Value == "cin")
                    //{
                    //    continue;
                    //}
                    richTextBox1.Select( match.Index, match.Length);
                    //richTextBox1.Select(match.Value,2);
                   // st = match.Value;
       

                    richTextBox1.SelectionBackColor = Color.Yellow; 
                    count++;
                    
                }
                match = match.NextMatch();
                
            }

            return count;
       
        }

           private int uniquematch(string str)
           {
               int uc = 0;
               var regex = new Regex(str);
               var matches = regex.Matches(t);
                uc = matches.OfType<Match>().Select(m => m.Value).Distinct().Count();
                return uc;
           }

        private void Home_Click(object sender, EventArgs e)
        {
            this.Close();
            this.RefToForm1.Show(); 
            
           
        }

        private void length_Click(object sender, EventArgs e)
        {
            vocabulary.Enabled=true;
             N=N1+N2;
            MessageBox.Show("The Program Length is calculated as N="+N);
        }

        private void vocabulary_Click(object sender, EventArgs e)
        {
            volume.Enabled = true;
             m=m1 + m2;
            MessageBox.Show("The Volcabulary of Program is calculated as m=" +m);
        }

        private void volume_Click(object sender, EventArgs e)
        {
            level.Enabled = true;
            V= N*Math.Log(m);
            MessageBox.Show("The Volume of Program is calculated as V=" + V);
        }

        private void level_Click(object sender, EventArgs e)
        {
            programdifficulty.Enabled = true;
            L=(2/m1)*(m2/N2);
            MessageBox.Show("The Level of Program is calculated as L=" + L);
        }

        private void programdifficulty_Click(object sender, EventArgs e)
        {
            effort.Enabled = true;
            D = (m1/2) * (N2 / m2);
            MessageBox.Show("The Difficulty of Program is calculated as D=" + D);
        }

        private void effort_Click(object sender, EventArgs e)
        {
            ErrorEstimate.Enabled = true;
            E = D * V;
            MessageBox.Show("The Effort of Program is calculated as E=" + E);
        }

        private void ErrorEstimate_Click(object sender, EventArgs e)
        {
            ProgrammingTime.Enabled = true;
            B1 = V / 3000;
            MessageBox.Show("The Error Estimate of Program is calculated as B1=" + B1);

        }

        private void ProgrammingTime_Click(object sender, EventArgs e)
        {
            NumberofDeliveredBugs.Enabled = true;
            T = E /18;
            MessageBox.Show("The Programming Time is calculated as T=" + T);
        }

        private void NumberofDeliveredBugs_Click(object sender, EventArgs e)
        {
            softwareidealvolume.Enabled = true;
            B = (Math.Pow(E,0.6) )/ 3000;
            MessageBox.Show("The Number of Delivered Bugs of Program is calculated as B=" + B);
        }

        private void softwareidealvolume_Click(object sender, EventArgs e)
        {

            V1 = (m1*N2 / 2*m2)*V;
            MessageBox.Show("The Software Ideal Volume is calculated as V*=" + V1);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // Create an OpenFileDialog object.
            OpenFileDialog openFile1 = new OpenFileDialog();
            //OpenFileDialog openFile2=new OpenFileDialog();

            // Initialize the filter to look for text files.
            openFile1.Filter = "CPP Files|*.cpp|CS Files|*.cs";

            // If the user selected a file, load its contents into the RichTextBox. 
            if (openFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                richTextBox1.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText);
            

           

        }

       

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            t=richTextBox1.Text;
             st = richTextBox1.SelectedText;

           
            //regexp(opn);
            //string pattern = @"\s-\s";

            //m1 = 1;
            //m1 = 1;
            //m2 = 1;
            //Regex.Matches(s, pattern).Count;
            //N2 = 2;


        }

        private void IdentifyOperators_Click(object sender, EventArgs e)
        {

            N1 = regexp(opr) + regexp(brace);
            m1 = uniquematch(opr) + uniquematch(brace);
            MessageBox.Show("total operators :"+N1+"\ntotal unique operators :"+m1);
        }

        private void IdentifyOperands_Click(object sender, EventArgs e)
        {
            N2 = regexp(lopn)+regexp(ropn);
            //int ml=uniquematch(lopn);
           // int mr = uniquematch(ropn);
            m2 = uniquematch(lopn) + uniquematch(ropn);
            MessageBox.Show("total operands :" + N2 + "\ntotal unique operands :" + m2);
            length.Enabled = true;
        }





        

    }
}
