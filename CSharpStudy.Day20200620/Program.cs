using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpStudy.Day20200620
{
    class Program
    {
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            Console.WriteLine("AppDomain.CurrentDomain.UnhandledException");
            Console.WriteLine(ex.ToString());
        }

        [STAThread]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            InterfaceCallback();

            Console.WriteLine("Press any key to continue ...");
            Console.ReadLine();
        }

        private static void CheckedAndUnChecked()
        {
            short s = 32767;
            checked
            {
                s++;
            }
        }

        private static void InterfaceCallback()
        {
            string path = @"C:\Temp1";
            string classFullName = ConfigurationManager.AppSettings["ClassFullName"];

            Assembly assembly = Assembly.GetEntryAssembly();
            Type type = assembly.GetType(classFullName);
            object obj = Activator.CreateInstance(type);
            DeleteDirectoryInFiles(path, obj as ITaskCompleted);
        }

        private static void DeleteDirectoryInFiles(string directory, ITaskCompleted taskCompleted)
        {
            foreach (string path in Directory.GetFiles(directory))
            {
                File.Delete(path);
                taskCompleted.Completed(path);
            }
        }
    }
}
