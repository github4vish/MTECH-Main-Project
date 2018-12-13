using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniCompiler
{
    public partial class StartForm : Form
    {

        //public StartForm s1 = new StartForm();
        public StartForm()
        {
            InitializeComponent();
            
             
        
        }

       
        private void Test_the_code_Click(object sender, EventArgs e)
        {

            
            TestForm f1 = new TestForm();
            f1.RefToForm1 = this;
            this.Hide();
            f1.Show();
            
        }

        private void Apply_Wyline_technique_Click(object sender, EventArgs e)
        {
            //Form1 f1 = new Form1();
            //f1.Show();
           // this.Hide();
        }

        private void Performance_of_code_Click(object sender, EventArgs e)
        {
            
            Performance_of_code p1= new Performance_of_code();
            p1.RefToForm1 = this;
            this.Hide();
            p1.Show();
          
        }

        private void Code_Comparision_Click(object sender, EventArgs e)
        {
            Code_Comparision c1 = new Code_Comparision();
            //c1.RefToForm1 = this;
            c1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            HalsteadForm h1 = new HalsteadForm();
            h1.RefToForm1 = this;
            this.Hide();
            h1.Show();
            
        }

        private void LOC_Click(object sender, EventArgs e)
        {
            
            LOC l1 = new LOC();
            l1.RefToForm1 = this;
            this.Hide();
            l1.Show();
        }

        private void StartForm_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            CyclomaticForm c1 = new CyclomaticForm();            
            c1.RefToForm1 = this;
            this.Hide();
            c1.Show();
        }

        private void FunctionalPoints_Click(object sender, EventArgs e)
        {

            
            FunctionalPoints fp = new FunctionalPoints();
            fp.RefToForm1 = this;
            this.Hide();
            fp.Show();
        }

        private void HalsteadcomlpexityMetrics_Click(object sender, EventArgs e)
        {
            
            HalsteadForm h1 = new HalsteadForm();
            h1.RefToForm1 = this;
            this.Hide();
            h1.Show();
        }

        private void FunctionalPointAnalysis_Click(object sender, EventArgs e)
        {
            
            FunctionalPoints fp = new FunctionalPoints();
            fp.RefToForm1 = this;
            this.Hide();
            fp.Show();
                
        }

        

        
    }
}
