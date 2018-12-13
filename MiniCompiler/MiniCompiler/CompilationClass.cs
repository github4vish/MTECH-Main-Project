

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
namespace MiniCompiler
{
    
   
    public class ErrorEventArgs : EventArgs
    {
        
       public readonly List<string> Errors = new List<string>();
        public ErrorEventArgs(List<string> errors)
        {
            this.Errors = errors;
        }
    }
    public delegate void ErrorEventHandler(Object sender  , ErrorEventArgs e);
    

    class CompilationClass
    {
        
        [DllImport("Kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern Boolean ShowWindow(IntPtr hWnd, Int32 nCmdShow);


        public CompilationClass()
        {
             
            RefAssemblies = new List<Assembly>();
            
        }
        List<Assembly> assemblies = new List<Assembly>();
        public List<Assembly> RefAssemblies { get { return assemblies; } set { assemblies = value; } }

       
        private static IEnumerable<string> GetFilesRecursive(string  dirPath)
        {
            DirectoryInfo dinfo = new DirectoryInfo(dirPath);
            return GetFilesRecursive(dinfo, "*.dll*");
        }

        private static IEnumerable<string> GetFilesRecursive(DirectoryInfo dirInfo, string searchPattern)
        {
            foreach (DirectoryInfo di in dirInfo.GetDirectories())
                foreach (string fi in GetFilesRecursive(di, searchPattern))
                    yield return fi;

            foreach (FileInfo fi in dirInfo.GetFiles(searchPattern))
                yield return fi.FullName;
        }

        public List<string> AssemblyDirectories()
        {
            List<string> assemblies = new List<string>();
            Microsoft.Win32.RegistryKey key = Registry.LocalMachine;
            key = key.OpenSubKey(@"SOFTWARE\Microsoft\.NETFramework\AssemblyFolders");
            string[] SubKeynames = key.GetSubKeyNames();
            foreach (string subkeys in SubKeynames)
            {
                RegistryKey subkey = Registry.LocalMachine;
                subkey = subkey.OpenSubKey(@"SOFTWARE\Microsoft\.NETFramework\AssemblyFolders\" + subkeys);
                string[] l = subkey.GetValueNames();
                foreach (string v in l)
                {
                    if (subkey.GetValueKind(v) == RegistryValueKind.String)
                    {
                        string value = (string)subkey.GetValue(v);
                        assemblies.Add(value);
                    }
                }
          }
            //assemblies.Add( Environment.SystemDirectory+ @"..\..\Windows\Microsoft.NET\Framework\v2.0.50727");
          return assemblies;
        }
        public  List<string> GetAssembliesFile( )
        {
            List<string> refAssemblies = new List<string>();

            
            if (!File.Exists(Environment.CurrentDirectory+ "\\assemblies.sft"))
            {

             
                List<string> dirs = AssemblyDirectories();

                foreach (string dir in dirs)
                {


                    foreach (string assemblyfile in GetFilesRecursive(dir))
                    {


                        refAssemblies.Add(assemblyfile);

                    }
                }
                foreach (string assemblyfile in System.IO.Directory.GetFiles(@"C:\Windows\Microsoft.NET\Framework"))
                {
                    FileInfo fino = new FileInfo(assemblyfile);
                    refAssemblies.Add(fino.FullName);

                }
                SerializeAssemblyDirs(refAssemblies);
            }
            else
            {
                BinaryFormatter binary = new BinaryFormatter();
                Stream sreader = new FileStream(Environment.CurrentDirectory + "\\assemblies.sft", FileMode.Open);

                refAssemblies = (List<string>)binary.Deserialize(sreader);
                sreader.Close();
            }
            return refAssemblies;
       
        }
        public  void SerializeAssemblyDirs(List<string> assemblies )
        {

            Stream str = new FileStream(Environment.CurrentDirectory + "\\assemblies.sft", FileMode.OpenOrCreate);
            BinaryFormatter binary = new BinaryFormatter();
            binary.Serialize(str, assemblies);
        }

        public event ErrorEventHandler CompilationError;
        protected virtual void OnCompilationError(ErrorEventArgs e)
        {
            if (CompilationError != null)
            {
                CompilationError(this, e);
            }
        }
        public event EventHandler FileNotExists;
        protected virtual void OnFileNotExists(EventArgs e)
        {
            if (FileNotExists != null)
            {
                FileNotExists(this, e);
            }
        }
        public static string Classname(string code)
        {
            string mainNameSpace = string.Empty;
            List<string> classcode = new List<string>();
            List<string> classname = new List<string>();
            List<string> NamespaceCode = new List<string>();
            List<string> nmSpaceName;
            string mainClass = string.Empty;
            ManiPulateMainClass.ManipulateNameSpace(code, out NamespaceCode, out nmSpaceName);
            for (int x = 0; x < nmSpaceName.Count; x++)
            {
                ManiPulateMainClass.ManipulateClasses(NamespaceCode[x], out classname, out classcode);


                mainClass = ManiPulateMainClass.GetMainClass(classname, classcode);

                mainNameSpace = nmSpaceName[x];



            }
            string[] mainClassName = mainClass.Split(':');
            return string.Format("{0}.{1}", mainNameSpace.Trim(), mainClassName[0].Trim());
        }


       public long a;
        
        public  void CompileCSharp(string code,string outputassembly )
        {
       
            CSharpCodeProvider provider = new CSharpCodeProvider();
            ICodeCompiler compiler = provider.CreateCompiler();

          
          
      #region CompilerParameters
            CompilerParameters parameters = new CompilerParameters()
            {
                MainClass = Classname(code),
                GenerateExecutable = true,
                OutputAssembly = outputassembly,
                IncludeDebugInformation = true,
               
            };
        
            
            parameters.ReferencedAssemblies.Clear();
            foreach (Assembly asm in assemblies)
            {
            
                parameters.ReferencedAssemblies.Add(asm.Location);
            }


          
#endregion 
            CompilerResults result = compiler.CompileAssemblyFromSource(parameters, code);
            if (result.Errors.Count == 0)
            {
                    if (!System.IO.File.Exists(outputassembly))
                    {
                        OnFileNotExists(new EventArgs());
                        return;
                    }
                    ProcessStartInfo startInfo = new ProcessStartInfo();

                    startInfo.FileName =outputassembly;

                    startInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    startInfo.Arguments = "\\S";
                    Process process = new Process();

                    process.StartInfo = startInfo;

                    process.Start();
                    IntPtr hwnd = GetConsoleWindow();
                    ShowWindow(hwnd, 0);
                    process = Process.GetCurrentProcess();
      a=process.PrivateMemorySize64;
            }
            else
            {
           
                foreach (CompilerError er in result.Errors)
                {
          
           
                    List<string> cerrors = new List<string>();
                    cerrors.Clear();
                    cerrors.Add(er.ErrorText+ " Line ("+er.Line.ToString()+")  error  :"+er.ErrorNumber );
                    OnCompilationError(new ErrorEventArgs(cerrors));
                  
                }
                result.Errors.Clear();
            }
           

        }



        
    }
}
