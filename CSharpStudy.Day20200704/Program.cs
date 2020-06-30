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
            string currentDirectory = Path.GetDirectoryName(process.MainModule.FileName);
            //Directory.SetCurrentDirectory(currentDirectory);
            //Environment.CurrentDirectory = currentDirectory;

            ExamDirectoryAndDirectoryInfo();
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

            
            Console.WriteLine("Test5");
            // Test4.log 파일 실행
            Environment.CurrentDirectory = "C:\\Temp2";
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

            Console.WriteLine("File.WriteAllText / File.ReadAllText");
            if (Directory.Exists(Environment.CurrentDirectory))
            {
                string path = Environment.CurrentDirectory + "\\Config.json";
                Newtonsoft.Json.Linq.JObject dataJson = new Newtonsoft.Json.Linq.JObject();
                dataJson.Add("Build", "Any CPU");

                string contents = dataJson.ToString();
                File.WriteAllText(path, contents, Encoding.UTF8);
                Console.WriteLine("File.WriteAllText");

                contents = File.ReadAllText(path, Encoding.UTF8);
                Console.WriteLine("File.ReadAllText");
                Console.WriteLine(contents);

                Console.WriteLine("File Create");

                string fileName1 = Environment.CurrentDirectory + "\\Env1.json";

                // 파일 생성후 FileStream Dispose 호출 필요!
                using (FileStream fs = File.Create(fileName1))
                {
                    if (File.Exists(fileName1))
                    {
                        Console.WriteLine(fileName1 + " 파일 생성 성공");
                    }
                    else
                    {
                        Console.WriteLine(fileName1 + " 파일 생성 실패");
                    }
                }

                string fileName2 = Environment.CurrentDirectory + "\\Env2.json";
                FileInfo fi = new FileInfo(fileName2);
                using (FileStream fs = fi.Create())
                {
                    if (fi.Exists)
                    {
                        Console.WriteLine(fileName2 + " 파일 생성 성공");
                    }
                    else
                    {
                        Console.WriteLine(fileName2 + " 파일 생성 실패");
                    }
                }

                if (File.Exists(fileName2))
                {
                    // 이름 바꾸기
                    string sourceFileName = fileName2;
                    string destFileName = Environment.CurrentDirectory + "\\Env3.json";

                    // 파일이 없을 시 파일명 변경 변경해야하는 파일이 있을 경우 예외 발생(System.IO.IOException)
                    if (!File.Exists(destFileName))
                    {
                        File.Move(sourceFileName, destFileName);
                        Console.WriteLine(sourceFileName + " 파일을 " + destFileName + " 이름으로 변경 하였습니다.");
                    }
                    
                }

                Console.WriteLine("Environment.CurrentDirectory={0}", Environment.CurrentDirectory);

                Environment.CurrentDirectory = @"C:\Temp";
                Console.WriteLine("Directory.GetCurrentDirectory()={0}", Directory.GetCurrentDirectory());
            }

            Console.WriteLine("##################################### ExamFileAndFIleInfo");
        }

        private static void ExamDirectoryAndDirectoryInfo()
        {
            Console.WriteLine("##################################### ExamDirectoryAndDirectoryInfo");

            Process process = Process.GetCurrentProcess();
            string currentDirectory = Path.GetDirectoryName(process.MainModule.FileName);
            Directory.SetCurrentDirectory(currentDirectory);

            DirectoryInfo di = new DirectoryInfo(currentDirectory + "\\Temp");
            di.Create();

            if (di.Exists)
            {
                Console.WriteLine(currentDirectory + " 디렉터리 생성에 성공 하였습니다.");
            }
            else
            {
                Console.WriteLine(currentDirectory + " 디렉터리 생성에 실패 하였습니다.");
            }

            string directoryRoot = Directory.GetDirectoryRoot(di.FullName);
            Console.WriteLine("{0} directoryRoot={1}", di.FullName, directoryRoot);

            string sourceDirName = di.FullName;
            string destDirName = currentDirectory + "\\Temp1";

            if (!Directory.Exists(destDirName))
            {
                Directory.Move(sourceDirName, destDirName);
                Console.WriteLine("{0} 디렉터리가 {1} 변경되었습니다.", sourceDirName, destDirName);
            }

            // 디렉터리 안에 디렉터리 및 파일 까지 모두 삭제 후 해당 디렉터리 삭제
            Directory.Delete(destDirName, true);

            // EveryThing!
            foreach (string path in Directory.GetFiles(currentDirectory, "*.log", SearchOption.TopDirectoryOnly))
            {
                File.Delete(path);
                Console.WriteLine("{0} 파일이 삭제 되었습니다.", path);
            }

            Console.WriteLine("##################################### ExamFileAndFIleInfo");
        }
    }
}
