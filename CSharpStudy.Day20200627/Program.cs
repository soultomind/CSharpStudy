using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CSharpStudy.Day20200627
{
    class Program
    {
        static void Main(string[] args)
        {
            ExamCollections();
            Console.ReadKey();
        }

        private static void ExamDateTime()
        {
            Console.WriteLine("###################### DateTime");
            DateTime newDateTime = new DateTime(2020, 06, 27, 10, 0, 0);
            Console.WriteLine("CSharp Study Time={0}", newDateTime);

            DateTime now = DateTime.Now;
            DateTime utcNow = DateTime.UtcNow;
            Console.WriteLine(now.ToString("yyyy-MM-dd HH:mm:ss"));

            Console.WriteLine("Now.DayOfYear={0}", now.DayOfYear); 
            Console.WriteLine("Now.DayOfWeek={0}.", now.DayOfWeek);

            Console.WriteLine("Now.AddDays(7)={0}", now.AddDays(7));

            DateTime birthTime = new DateTime(1987, 9, 30);

            TimeSpan lifeTime = DateTime.Now.Subtract(birthTime);
            Console.WriteLine("LifeTime.Days={0}", lifeTime.Days);

            // 9시간 차이
            Console.WriteLine("DateTime.UtcNow={0}, Kind={1}", utcNow, utcNow.Kind);
            Console.WriteLine("DateTime.Now={0}, Kind={1}", now, now.Kind);
            Console.WriteLine("###################### DateTime");
        }

        private static void ExamTimeSpan()
        {
            Console.WriteLine("###################### ExamTimeSpan");
            TimeSpan gap = new DateTime(DateTime.Now.Year, 12, 31) - DateTime.Now;
            Console.WriteLine("Gap.TotalDays={0}", (int)gap.TotalDays);
            Console.WriteLine("Now.DayOfYear + Gap.Totaldays = {0}", DateTime.Now.DayOfYear + (int)gap.TotalDays);

            foreach (string fileName in Directory.GetFiles("C:\\Temp1"))
            {
                FileInfo fi = new FileInfo(fileName);
                DateTime fileWriteTime = fi.LastWriteTime;

                // 1초(Second)는 1,000 밀리초(Millisecond)인데 이보다 정밀도가 높은 시간 값이 필요하다면 Ticks 속성을 이용
                // 1밀리초의 10,000분의 1에 해당하는 정밀도를 가지고 있다.
                TimeSpan time = new TimeSpan(DateTime.Now.Ticks - fileWriteTime.Ticks);

                if (time.TotalMinutes > 1)
                {
                    fi.Delete();
                    Console.WriteLine("{0} Delete File.", fi.Name);
                }
            }
            Console.WriteLine("###################### ExamTimeSpan");
        }

        private static void ExamStopwatch()
        {
            Console.WriteLine("###################### ExamStopwatch");
            // 특정 구간에 대한 성능을 측정할 때 많이 사용된다.
            Stopwatch sw = new Stopwatch();
            sw.Start();

            Thread.Sleep(1000);

            sw.Stop();

            Console.WriteLine("Stopwatch.ElapsedMilliseconds={0}", sw.ElapsedMilliseconds / 1000);
            Console.WriteLine("###################### ExamStopwatch");
        }

        private static void ExamString()
        {
            Console.WriteLine("###################### String");
            // String 타입은 불변 객체 이기 때문에 모든 변환은 새로운 메모리 할당을 발생
            string word = "c#";
            Console.WriteLine("Word HashCode={0}", word.GetHashCode());
            Console.WriteLine("Word.ToLower() HashCode={0}", word.ToLower().GetHashCode());
            Console.WriteLine("Word.ToUpper() HashCode={0}", word.ToUpper().GetHashCode());

            // Alpha, Red, Green, Blue

            // 8비트 8자리 1,2,4,8,16,32,64,128=255 값으로 ARGB 의 값을 Int 형으로 표현 할 수 있다.
            // int nTemp =   0xFFFFFFFFF;
            
            // 11111111 00000000 00000000 00000000
            // 00000000 00000000 00000000 11111111

            int argb = 0x00FF0000; 
            Console.WriteLine("Argb=" + argb);
            // Alapha=00000000, Red=11111111, Green=00000000, Blue=00000000

            Color color = ColorUtility.FromArgb(argb);
            Console.WriteLine(color.ToString());
            Console.WriteLine("Color.ToArgb()={0}", color.ToArgb());

            argb = ColorUtility.ToCssStyleRgbaNumber("rgba(255, 0, 0, 0)");
            Console.WriteLine("ColorUtility.ToCssRgba(\"rgba(255, 0, 0, 0\")={0}", argb);

            string studyName = "C# Study";
            if (studyName.IndexOf(" ") != -1)
            {
                Console.WriteLine("Study!");
            }

            // StringComparison.OrdinalIgnoreCase 대소문자 구문 X
            // Equals, StartsWith, EndsWith, IndexOf ..

            // {1} 부터 시작 할경우 FormatException 발생
            string message = String.Format("{0} {1}", "C#", "Study");
            Console.WriteLine(message);

            string text = "text";
            if (text == "text")
            {
                Console.WriteLine("==");
            }

            if (text.Equals("TEXT", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Equals");
            }

            string format = "날짜: {0,-20:D}, 판매 수량: {1,15:N}";
            Console.WriteLine(format, DateTime.Now, 267);
            Console.WriteLine("###################### String");
        }

        private static void ExamStringBuilder()
        {
            Console.WriteLine("###################### StringBuilder");

            // Capacity는 기존 Capacity 만큼의 문자열이 찰 경우 해당 크기의 두 배로!
            StringBuilder builder = new StringBuilder();
            Console.WriteLine("Capacity={0}", builder.MaxCapacity);
            Console.WriteLine("Capacity={0}, Length={1}", builder.Capacity, builder.Length);

            // Append로 리턴되는 객체또한 자기자신 This 
            StringBuilder builderA = builder.Append("ABCDEFGHIJKLMNOP");
            Console.WriteLine("builderA={0}, builder={1}", builder.GetHashCode(), builderA.GetHashCode());
            Console.WriteLine("Capacity={0}, Length={1}", builder.Capacity, builder.Length);
            builder.Append("Q");
            Console.WriteLine("Capacity={0}, Length={1}", builder.Capacity, builder.Length);
            builder.Append(builder.ToString());
            Console.WriteLine("Capacity={0}, Length={1}", builder.Capacity, builder.Length);
            Console.WriteLine("###################### StringBuilder");
        }

        private static void ExamEncoding()
        {
            Console.WriteLine("###################### ExamEncoding");
            Console.WriteLine("Encoding.Default={0}", Encoding.Default);
            foreach (EncodingInfo encodingInfo in Encoding.GetEncodings())
            {
                Console.WriteLine("EncodingInfo Name={0}, CodePage={1}", encodingInfo.Name, encodingInfo.CodePage);
            }

            string json = "{event:\"execute\"}";
            Console.WriteLine(json);
            string encode = ConvertUtility.Base64Encode(json);
            Console.WriteLine(encode);
            string decode = ConvertUtility.Base64Decode(encode);
            Console.WriteLine(decode);
            Console.WriteLine("###################### ExamEncoding");
        }

        private static void ExamRegularExpression()
        {
            Console.WriteLine("###################### ExamRegularExpression");

            Console.WriteLine("ColorUtility.IsValidCssStyleRgba(\"#336655\")={0}", 
                ColorUtility.IsValidCssStyleSharpRgba("#336655"));
            Console.WriteLine("ColorUtility.IsValidCssStyleRgba(\"#336655FF\")={0}",
                ColorUtility.IsValidCssStyleSharpRgba("#336655FF"));
            Console.WriteLine("ColorUtility.IsValidCssStyleRgba(\"#336655FG\")={0}",
                ColorUtility.IsValidCssStyleSharpRgba("#336655FG"));

            string text = "Hello, World! Welcome to my world";
            Regex regex = new Regex("World", RegexOptions.IgnoreCase);
            string result = regex.Replace(text, new MatchEvaluator((match)=>
            {
                return "Universe";
            }));
            Console.WriteLine("Result={0}", result);

            Console.WriteLine("###################### ExamRegularExpression");
        }

        private static void ExamBitCoverter()
        {
            Console.WriteLine("###################### ExamBitCoverter");

            // 문자열 또한 byte 데이터이다.
            byte[] bytes = 
                new byte[] {
                    67,
                    (byte)'#',
                    (byte)' ', (byte)'S', (byte)'t', (byte)'u', (byte)'d', (byte)'y' };

            foreach (byte @byte in bytes)
            {
                Console.Write((char)@byte);
            }
            Console.WriteLine("");

            
            Console.WriteLine("BitConverter.IsLittleEndian={0}", BitConverter.IsLittleEndian);

            byte[] boolBytes = BitConverter.GetBytes(true);
            byte[] shortBytes = BitConverter.GetBytes((short)32000);
            byte[] intBytes = BitConverter.GetBytes(1652300);

            Console.WriteLine("BoolResult={0}", BitConverter.ToBoolean(boolBytes, 0));
            Console.WriteLine("ShortResult={0}", BitConverter.ToInt16(shortBytes, 0));
            Console.WriteLine("IntResult={0}", BitConverter.ToInt32(intBytes, 0));

            Console.WriteLine("BoolBytes={0}", BitConverter.ToString(boolBytes));
            Console.WriteLine("LittleEndian ShortBytes={0}, BigEndian=7D-00", BitConverter.ToString(shortBytes)); // 7D 00 <- 빅 엔디안
            Console.WriteLine("LittleEndian IntBytes={0}, BigEndian=00 19 36 4C", BitConverter.ToString(intBytes)); // 00 19 36 4C

            byte[] binaryData = new byte[] { 0x4C, 0x36, 0x19, 0x00 };
            int result = BitConverter.ToInt32(binaryData, 0);
            Console.WriteLine("Result={0}", result);

            Console.WriteLine("###################### ExamBitCoverter");
        }

        private static void ExamMemoryStream()
        {
            Console.WriteLine("###################### ExamMemoryStream");

            int nValue = 1652300;
            Console.WriteLine("Value={0}", nValue);
            byte[] intBytes = BitConverter.GetBytes(nValue);
            MemoryStream ms = new MemoryStream();
            ms.Write(intBytes, 0, intBytes.Length);

            // 저장된 바이트 배열을 읽기 위해서는 처음 부터 읽어야 하기 때문에 포지션에 대한값을 0으로 설정이 필요
            ms.Position = 0;

            // 자료형에 맞는 바이트수로 할당 필요
            byte[] intOutBytes = new byte[4];
            ms.Read(intOutBytes, 0, intOutBytes.Length);
            int intResult = BitConverter.ToInt32(intOutBytes, 0);
            if (nValue == intResult)
            {
                Console.WriteLine("IntResult={0}", intResult);
            }

            string strValue = "C#";
            Console.WriteLine("value={0}", strValue);
            byte[] strBytes = Encoding.UTF8.GetBytes(strValue);
            ms.Write(strBytes, 0, strBytes.Length);

            byte[] strOutBytes = new byte[strBytes.Length];
            ms.Position = intOutBytes.Length;
            ms.Read(strOutBytes, 0, strBytes.Length);
            string strResult = Encoding.UTF8.GetString(strOutBytes);
            Console.WriteLine("StrResult={0}", strResult);

            byte[] buffer = ms.ToArray();
            intResult = BitConverter.ToInt32(buffer, 0);
            Console.WriteLine("IntResult={0}", intResult);

            // - 구분자로 16진수 43-23 -> 10진수 67-35
            strResult = BitConverter.ToString(buffer, 4);

            //strResult = Convert.ToString(buffer[4]);
            //strResult += Convert.ToString(buffer[5]);
            Console.WriteLine("StrResult={0}", strResult);

            Console.WriteLine("###################### ExamMemoryStream");
        }

        private static void ExamStreamWriterAndReader()
        {
            Console.WriteLine("###################### ExamStreamWriterAndReader");

            try
            {
                using (MemoryStream ms = new MemoryStream())
                using (StreamWriter sw = new StreamWriter(ms, Encoding.UTF8))
                {
                    //byte[] buffer = Encoding.UTF8.GetBytes("Hello, World!");
                    //ms.Write(buffer, 0, buffer.Length);

                    sw.WriteLine("Hello, World!");
                    sw.WriteLine("Anderson");
                    sw.Write(32000);
                    sw.Flush();
                    ms.Position = 0;

                    StreamReader sr = new StreamReader(ms);
                    string data = sr.ReadToEnd();
                    Console.WriteLine("Data={0}", data);

                    sr.Dispose();
                }

                using (StreamReader sr = new StreamReader("Config.json", Encoding.UTF8))
                {
                    string data = sr.ReadToEnd();
                    Console.WriteLine("Config.json={0}", data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            Console.WriteLine("###################### ExamStreamWriterAndReader");
        }

        private static void ExamBinaryWriterAndReader()
        {
            Console.WriteLine("###################### ExamBinaryWriterAndReader");
            try
            {
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    bw.Write("Hello World" + Environment.NewLine);
                    bw.Write("Anderson" + Environment.NewLine);
                    bw.Write(32000);
                    bw.Flush();

                    ms.Position = 0;

                    // 2진파일 중간에 잘못된 데이터가 써질경우 그 이후의 데이터는 손실된다.

                    BinaryReader br = new BinaryReader(ms);
                    string value1 = br.ReadString();
                    string value2 = br.ReadString();
                    int value3 = br.ReadInt32();
                    Console.WriteLine("{0}{1}{2}", value1, value2, value3);
                    br.Dispose();
                    br.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            Console.WriteLine("###################### ExamBinaryWriterAndReader");
        }

        private static void ExamBinaryFormatter()
        {
            Console.WriteLine("###################### ExamBinaryFormatter");

            Study study = new Study("C# Study", "강남");

            BinaryFormatter bf = new BinaryFormatter();

            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, study);

            ms.Position = 0;

            Study clone = bf.Deserialize(ms) as Study;

            ms.Dispose();
            ms.Close();
            

            Console.WriteLine("Clone={0}", clone);

            Console.WriteLine("###################### ExamBinaryFormatter");
        }

        private static void ExamXmlSerializer()
        {
            Console.WriteLine("###################### ExamXmlSerializer");

            Person person = new Person("이바우", 25);

            MemoryStream ms = new MemoryStream();

            XmlSerializer xs = new XmlSerializer(typeof(Person));
            xs.Serialize(ms, person);

            ms.Position = 0;

            Person clone = xs.Deserialize(ms) as Person;

            ms.Dispose();
            ms.Close();

            Console.WriteLine("Clone={0}", clone);

            string xml = Encoding.UTF8.GetString(ms.ToArray());
            Console.WriteLine("Xml={0}", xml);



            Console.WriteLine("###################### ExamXmlSerializer");
        }

        private static void ExamDataContractJsonSerializer()
        {
            Console.WriteLine("###################### ExamDataContractJsonSerializer");

            Person person = new Person("이바우", 25);

            MemoryStream ms = new MemoryStream();

            DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(Person));
            dcjs.WriteObject(ms, person);

            ms.Position = 0;

            Person clone = dcjs.ReadObject(ms) as Person;

            Console.WriteLine("Clone={0}", clone);

            ms.Dispose();
            ms.Close();

            Console.WriteLine("###################### ExamDataContractJsonSerializer");
        }

        private static void ExamCollections()
        {
            const int COUNT = 10000000;
            Stopwatch sw = new Stopwatch();
            {
                IList list = new ArrayList(10);
                Console.WriteLine("List Type={0} HashCode={1}", list.GetType(), list.GetHashCode());
                Console.WriteLine("ArrayList.Capacity={0}", ((ArrayList)list).Capacity);
                
                sw.Start();
                for (int index = 0; index < COUNT; index++)
                {
                    list.Add(index);
                }
                sw.Stop();
                Console.WriteLine("Stopwatch {0},{1}", sw.ElapsedTicks, sw.ElapsedMilliseconds / 1000);

                // Thread Safe ArrayList
                IList syncList = ArrayList.Synchronized(list);
                Console.WriteLine("SyncList Type={0} HashCode={1}", syncList.GetType(), syncList.GetHashCode());

                sw.Reset();
                sw.Start();
                for (int index = 0; index < COUNT; index++)
                {
                    syncList.Add(index);
                }
                sw.Stop();
                Console.WriteLine("Stopwatch {0},{1}", sw.ElapsedTicks, sw.ElapsedMilliseconds / 1000);

                sw.Reset();
                sw.Start();

                int nTemp = (int)list[COUNT - 500];

                sw.Stop();
                Console.WriteLine("Stopwatch {0},{1}", sw.ElapsedTicks, sw.ElapsedMilliseconds / 1000);
            }

            // System.Collections.Generic
            {
                // ICollection

                // 배열 형태
                List<int> list = new List<int>();
                
                // 객체 참조 형태 
                LinkedList<int> linkedList = new LinkedList<int>();
            }

            {
                Hashtable hashTable = new Hashtable();
                for (int index = 0; index < COUNT; index++)
                {
                    hashTable.Add(index, index);
                }

                sw.Reset();
                int nTemp = (int)hashTable[COUNT - 500];
                Console.WriteLine("Stopwatch {0},{1}", sw.ElapsedTicks, sw.ElapsedMilliseconds / 1000);
                sw.Stop();

                // ArgumentException
                // hashTable.Add(COUNT - 1, COUNT - 1);
            }

            {
                SortedList sortedList = new SortedList();
                sortedList.Add(17, "Sammy");
                sortedList.Add(27, "Paul");
                sortedList.Add(32, "Cooper");
                sortedList.Add(56, "Anderson");

                Console.WriteLine("KeyValues");
                foreach (int key in sortedList.GetKeyList())
                {
                    Console.WriteLine("{0}={1}", key, sortedList[key]);
                }

                // Generic 형태가 아니라 오류가 발생 하지 않음 
                Console.WriteLine("Values");
                foreach (string value in sortedList.GetValueList())
                {
                    Console.WriteLine("{0}", value);
                }
            }

            {
                SortedList<int, string> sortedList = new SortedList<int, string>();
                sortedList.Add(17, "Sammy");
                sortedList.Add(27, "Paul");
                sortedList.Add(32, "Cooper");
                sortedList.Add(56, "Anderson");

                Console.WriteLine("KeyValues");
                foreach (int key in sortedList.Keys)
                {
                    Console.WriteLine("{0}={1}", key, sortedList[key]);
                }

                // Generic 형태라 오류 발생
                Console.WriteLine("Values");
                foreach (string value in sortedList.Values)
                {
                    Console.WriteLine("{0}", value);
                }
            }

            {
                Stack<int> stack = new Stack<int>();
                stack.Push(10);
                stack.Push(20);

                Console.WriteLine("Stack.Pop={0}", stack.Pop());
                Console.WriteLine("Stack.Pop={0}", stack.Pop());
                
                // InvalidOperationException 발생
                //Console.WriteLine("Stack.Pop={0}", stack.Pop());

                if (stack.Count > 0)
                {
                    Console.WriteLine("Stack.Pop={0}", stack.Pop());
                }
                else
                {
                    Console.WriteLine("failed Stack.Pop()");
                }

                stack.Push(10);
                stack.Push(20);
                stack.Clear();
                Console.WriteLine("Stack.Count={0}", stack.Count);
            }

            {
                Queue<int> queue = new Queue<int>();
                queue.Enqueue(1000);
                queue.Enqueue(1001);
                queue.Enqueue(1002);

                Console.WriteLine("Queue.Dequeue={0}", queue.Dequeue());
                Console.WriteLine("Queue.Dequeue={0}", queue.Dequeue());

                if (queue.Count > 0)
                {
                    Console.WriteLine("Queue.Dequeue={0}", queue.Dequeue());
                }
                else
                {
                    Console.WriteLine("failed Queue.Dequeue()");
                }

                queue.Enqueue(10);
                queue.Clear();
                Console.WriteLine("Queue.Count={0}", queue.Count);
            }
        }
    }
}
