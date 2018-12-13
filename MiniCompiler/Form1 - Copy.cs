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
            Thread thread = new Thread(new ThreadStart(Compile));
            thread.Start();
        
        }
        delegate void UpdateList(List<Assembly> refAssemlies, string MainClass);
        public void UpdateUI(List<Assembly> refAssemblies,string MainClass)
        {
            label3.Text = MainClass;
            listBox1.Items.Clear();
            foreach(Assembly r in refAssemblies)
            {
                listBox1.Items.Add(r.GetName());
        }
        }
     
        private void Compile()
        {
            k.RefAssemblies.Clear();

            Assembly s = Assembly.LoadFile(@"C:\Windows\Microsoft.NET\Framework\v2.0.50727\mscorlib.dll");
            Assembly s1 = Assembly.LoadFile(@"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5\System.Core.dll");
        
            k.RefAssemblies.Add(s);
            k.RefAssemblies.Add(s1);
            var AssembliesList = ManiPulateMainClass.GetAssemblyNames(textBox1.Text);

            
            foreach (var assembly in AllAssemblyFiles)
            {
                foreach (string CodeAssembly in AssembliesList)
                {

                    if (CodeAssembly != "System")
                    {
                        if (assembly.Contains(CodeAssembly))
                        {
                            k.RefAssemblies.Add(Assembly.LoadFile(assembly));
                        }
                    }

                }
            }
           
            UpdateList update = new UpdateList(UpdateUI);
            this.Invoke(update, k.RefAssemblies, CompilationClass.Classname(textBox1.Text));
           

            k.CompilationError += new ErrorEventHandler(k_CompilationError);
            k.CompileCSharp(textBox1.Text, "Sample.exe");
            //button1.Enabled = true;
        }
        delegate void UpdateErrorList(List<string> errorlist);
        public void UpdateEList(List<string> errorlist)
        {
            listBox2.Items.Clear();
            foreach(string error in errorlist)
            {
                listBox2.Items.Add(error);
      }}
        void k_CompilationError(object sender, ErrorEventArgs e)
        {
            UpdateErrorList update = new UpdateErrorList(UpdateEList);
            this.Invoke(update,e.Errors);
        
        }
       
        private void button2_Click(object sender, EventArgs e)
        {

            UpdateList updatelist = new UpdateList(UpdateUI);
            this.Invoke(updatelist, AllAssemblyFiles);
            }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
         
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Home_Click(object sender, EventArgs e)
        {
            StartForm s1 = new StartForm();
            s1.Show();
            this.Hide();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {

        }
           
        }
    }
