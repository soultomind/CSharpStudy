using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStudy.Day20200704
{
    class Program
    {
        static void Main(string[] args)
        {
            // 해당 프로그램이 다른 프로세스를 통하여 실행 가능성이 있을 경우에는 
            // CurrentDirectory에 대한 설정을 해두는 습관이 필요하다.
            Process process = Process.GetCurrentProcess();

            // 해당 코드 설정 시 Environment.CurrentDirectory는 현재 실행 파일이 있는 기준 디렉토리로 설정된다.
            //Directory.SetCurrentDirectory(Path.GetDirectoryName(process.MainModule.FileName));

            ExamFileAndFIleInfo();
            Console.ReadKey();
        }

        private static void ExamFileStream()
        {
            Console.WriteLine("##################################### ExamFileStream");
            // 파일이 없을 경우 : 생성후 데이터 추가
            // 파일이 있을 경우 : 데이터 포지션의 위치를 마지막으로 이동후 데이터 추가

            Console.WriteLine("Test1");
            using (FileStream fs = new FileStream("Test1.log", FileMode.Create))
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("Hello, World!");
                sw.WriteLine("Anderson");
                sw.WriteLine(2020);
                sw.Flush();

                Console.WriteLine("CanWrite={0}", fs.CanWrite);
                Console.WriteLine("CanRead={0}", fs.CanRead);
            }

            Console.WriteLine("Test2");
            byte[] buffer = BitConverter.GetBytes(32000);
            using (FileStream fs = new FileStream("Test2.log", FileMode.Create))
            {
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write("Hello World" + Environment.NewLine);
                bw.Write("Anderson" + Environment.NewLine);
                bw.Write(32000);
                bw.Write(buffer);
                bw.Flush();

                fs.Position = 0;

                // 2진 데이터로 출력된 "00 7D 00 00" 부분을 문자열로 취급하기 때문에 발생하는 현상
                BinaryReader br = new BinaryReader(fs);
                Console.WriteLine(br.ReadString());
                Console.WriteLine(br.ReadString());
                Console.WriteLine(br.ReadInt32());
                Console.WriteLine(br.ReadInt32());
            }

            Console.WriteLine("Test3");
            using (FileStream fs = new FileStream("Test3.log", FileMode.Append))
            using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
            {
                sw.WriteLine("Hello World");
                Console.ReadLine();
            }

            Console.WriteLine("Test4");
            // Test4.log 파일 실행
            using (FileStream fs = new FileStream("Test4.log", FileMode.Append, FileAccess.Write, FileShare.None))
            using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
            {
                sw.WriteLine("Hello World");
                Console.ReadLine();
            }

            Console.WriteLine("Environment.CurrentDirectory={0}", Environment.CurrentDirectory);

            Environment.CurrentDirectory = @"C:\Temp";
            Console.WriteLine("Directory.GetCurrentDirectory()={0}", Directory.GetCurrentDirectory());
            Console.WriteLine("Test5");
            // Test4.log 파일 실행

            // Environment.CurrentDirectory 디렉토리 존재하지않을 경우 DirectoryNotFoundException 예외 발생
            using (FileStream fs = new FileStream("Test5.log", FileMode.Append, FileAccess.Write, FileShare.None))
            using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
            {
                sw.WriteLine("Hello World");
                Console.ReadLine();
            }

            if (Directory.Exists(Environment.CurrentDirectory))
            {
                
            }
            
            // 해당 위치에서 비로서 Test4.log 파일에 대한 실행이 가능하다.
            Console.WriteLine("##################################### ExamFileStream");
        }

        private static void ExamFileAndFIleInfo()
        {
            Console.WriteLine("##################################### ExamFileAndFIleInfo");



            Console.WriteLine("##################################### ExamFileAndFIleInfo");
        }
    }
}
