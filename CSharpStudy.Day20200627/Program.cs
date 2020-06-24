using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpStudy.Day20200627
{
    class Program
    {
        static void Main(string[] args)
        {
            ExamRegularExpression();
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
    }
}
