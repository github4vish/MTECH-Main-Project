using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Threading;


namespace MiniCompiler
{
    public partial class Form1 : Form
    {
        String t;

   CompilationClass k = new CompilationClass();
   List<string> AllAssemblyFiles;
        public Form1()
        {
            AllAssemblyFiles = k.GetAssembliesFile();
            InitializeComponent();
  
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string m in Environment.GetLogicalDrives())
            {


            
              
            }
        } 

        private void button1_Click(object sender, EventArgs e)
        {
            
           // button1.Enabled = false;
            Thread thread = new Thread(new ThreadStart(this.Compile));
            
            thread.Start();
            
        }
        delegate void UpdateList(List<Assembly> refAssemlies, string MainClass);
        public void UpdateUI(List<Assembly> refAssemblies,string MainClass)
        {
            label3.Text = MainClass;
            listBox1.Items.Clear();
            //listBox2.Items.Clear();
            foreach(Assembly r in refAssemblies)
            {
                listBox1.Items.Add(r.GetName());
        }


        }

        delegate void UpdateErrorList(List<string> errorlist);
        public void UpdateEList(List<string> errorlist)
        {
            //listBox2.Items.Clear();
            foreach (string error in errorlist)
            {
              //listBox2.Items.Add(error); 
            }
            button1.Enabled = false;
        }
        void k_CompilationError(object sender, ErrorEventArgs e)
        {
            //listBox2.Items.Clear();
            UpdateErrorList update = new UpdateErrorList(UpdateEList);
            this.Invoke(update, e.Errors);

        }

       
        private void Compile()
        {
            
            k.RefAssemblies.Clear();

            Assembly s = Assembly.LoadFile(@"C:\assemblies\mscorlib.dll");
            Assembly s1 = Assembly.LoadFile(@"C:\assemblies\System.Core.dll");
        
            k.RefAssemblies.Add(s);
          k.RefAssemblies.Add(s1);

            
            
                var AssembliesList = ManiPulateMainClass.GetAssemblyNames(t);
            

            

            
            //foreach (var assembly in AllAssemblyFiles)
            //{
            //    foreach (string CodeAssembly in AssembliesList)
            //    {

            //        if (CodeAssembly != "System")
            //        {
            //            if (assembly.Contains(CodeAssembly))
            //            {
            //                k.RefAssemblies.Add(Assembly.LoadFile(assembly));
            //            }
            //        }

            //    }
            //}
           
            UpdateList update = new UpdateList(UpdateUI);
            this.Invoke(update, k.RefAssemblies, CompilationClass.Classname(t));            

           // k.CompilationError += new ErrorEventHandler(k_CompilationError);
           // k.CompileCSharp(t, "Sample.exe");
          
        }


        


             
        private void button2_Click(object sender, EventArgs e)
        {

            UpdateList updatelist = new UpdateList(UpdateUI);
            this.Invoke(updatelist, AllAssemblyFiles);
            }


      

        

        private void Home_Click(object sender, EventArgs e)
        {
            StartForm s1 = new StartForm();
            s1.Show();
            this.Hide();
        }

        

        

        

        private void button2_Click1(object sender, EventArgs e)
        {
            MessageBox.Show("The Assembly Metric is Calculatd here");
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
            //button1.Enabled = true;
            
           // t = this.richTextBox1.Text;

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            t = this.richTextBox1.Text;
        }



       
           
        }
    }
