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
    public partial class LOC : Form
    {
        String t,count;
        int c,sloc,eloc,lloc,nc;
        double kl, ml;
        int nl, bo, bs, lc, bc, bl;
        string blockComments = @"/\*(?>(?:(?>[^*]+)|\*(?!/))*)\*/";
        string lineComments = @"//(.*?)\r?\n";
        string blankLines = @"^\s*$";
        string brace = @"^\s*[\p{Pe}\p{Ps}]";
        string braceOpen = @"^\s*{\s*$";
        string braceClose = @"^\s*}\s*$";
        
        string strings = @"""((\\[^\n]|[^""\n])*)""";
        string verbatimStrings = @"@(""[^""]*"")+";


        public Form RefToForm1 { get; set; }
        public LOC()
        {
            InitializeComponent();


        }

       

        private void SLOC_Click(object sender, EventArgs e)
        {
           // nc = 0;
           // string[] cc=new string[1000];

            
            bc = regexp(blockComments);
            lc = regexp(lineComments);


    //        string noComments = Regex.Replace(t,
    //            blockComments + "|" + lineComments + "|" + strings + "|" + verbatimStrings,
    //me =>
    //{
    //    if (me.Value.StartsWith("/*") || me.Value.StartsWith("//"))
    //        return me.Value.StartsWith("//") ? Environment.NewLine : "";
    //    // Keep the literal strings
    //    return me.Value;
    //},
    //RegexOptions.Singleline);


          //  nc = noComments.Split(Environment.NewLine).Count();


            sloc = c - (bc+lc);

            MessageBox.Show("\n The Total Source lines of Code is: " + sloc + "\n Total lines: " + c + "\n number of comments: " + (bc + lc));
        }

        private int regexp(String s)
        {
            //richTextBox1.SelectionColor = Color.White;
          // richTextBox1.SelectionBackColor = Color.White; 
            //foreach (var item in t)
            //{
            //    richTextBox1.DeselectAll();
            //}
            int count = 0;
            Match match = Regex.Match(t, s, RegexOptions.Multiline);
            foreach (var item in t)
            {

                if (match.Success)
                {
                    richTextBox1.Select(match.Index, match.Length);
                    
                    richTextBox1.SelectionBackColor = Color.Yellow; 
                    count++;
                }
                match = match.NextMatch();
            }

            return count;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // Create an OpenFileDialog object.
            /*asdas*/

            OpenFileDialog openFile1 = new OpenFileDialog();
            //OpenFileDialog openFile2=new OpenFileDialog();

            // Initialize the filter to look for text files.
            openFile1.Filter = "CPP Files|*.cpp|CS Files|*.cs|C Files|*.c";

            // If the user selected a file, load its contents into the RichTextBox. 
            if (openFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                richTextBox1.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText);
            

            
        }

        private void Home_Click(object sender, EventArgs e)
        {
            
            
            this.Close();
            this.RefToForm1.Show();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            t = richTextBox1.Text;
            c = richTextBox1.Lines.Count();
            updateNumberLabel();
            
        }

        private void ELOC_Click(object sender, EventArgs e)
        {
            //richTextBox1.SelectionBackColor = Color.White;

            // string[] cc=new string[1000];

            bl = regexp(blankLines);
            bc = regexp(blockComments);
            lc = regexp(lineComments);
            //bo = regexp(braceOpen);
            bs = regexp(brace);
            eloc = c - bl - (bc + lc) - bs;

            MessageBox.Show("\n The Total Effective lines of Code is:" + eloc + "\n total lines: " + c + "\n number of comments: " + (bc + lc) + "\n number of blank lines: " + bl + "\n number of braces: " + bs);
        }
        
        void updateNumberLabel()
        {
            //we get index of first visible char and number of first visible line
            Point pos = new Point(0, 0);
            int firstIndex = richTextBox1.GetCharIndexFromPosition(pos);
            int firstLine = richTextBox1.GetLineFromCharIndex(firstIndex);

            //now we get index of last visible char and number of last visible line
            pos.X = ClientRectangle.Width;
            pos.Y = ClientRectangle.Height;
            int lastIndex = richTextBox1.GetCharIndexFromPosition(pos);
            int lastLine = richTextBox1.GetLineFromCharIndex(lastIndex);

            //this is point position of last visible char, we'll use its Y value for calculating numberLabel size
            pos = richTextBox1.GetPositionFromCharIndex(lastIndex);

           
            //finally, renumber label
            numberLabel.Text = "";
            for (int i = firstLine; i <= lastLine + 1; i++)
            {
                numberLabel.Text += i + 1 + "\n";
            }

        }

       

       

        private void richTextBox1_Resize(object sender, EventArgs e)
        {
            richTextBox1_VScroll(null, null);
        }

        private void richTextBox1_FontChanged(object sender, EventArgs e)
        {
            updateNumberLabel();
            richTextBox1_VScroll(null, null);
        }

        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            //move location of numberLabel for amount of pixels caused by scrollbar
            int d = richTextBox1.GetPositionFromCharIndex(0).Y % (richTextBox1.Font.Height + 1);
            numberLabel.Location = new Point(0, d);

            updateNumberLabel();
        }

        private void LLOC_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionBackColor=Color.White;
            lloc = 0;
            lloc = regexp(@";\s*$");
            MessageBox.Show("The Logical Lines of Code is: "+lloc);
        }

       

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

     

        private void kloc_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionBackColor = Color.White;
            kl = (double)sloc / 1000;
            MessageBox.Show("The Number Kilo Lines of code is: "+kl);
        }

        private void numberLabel_Click(object sender, EventArgs e)
        {

        }

        private void LOC_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kloc1_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionBackColor = Color.White;
            kl = (double)sloc / 1000;
            MessageBox.Show("The Number Kilo Lines of code is: " + kl);
        }

       

        
        
    }
}
