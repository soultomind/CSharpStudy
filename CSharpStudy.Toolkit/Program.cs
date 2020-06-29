using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStudy.Toolkit
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessStart(@"C:\Workspace\DotNet\GitProject\CSharpStudy\CSharpStudy.Day20200704\bin\Debug\CSharpStudy.Day20200704.exe");
        }

        private static void ProcessStart(string fileName)
        {
            ProcessStartInfo psi = new ProcessStartInfo(fileName);
            //psi.WorkingDirectory = Path.GetDirectoryName(fileName);
            Process.Start(psi);
        }
    }
}
