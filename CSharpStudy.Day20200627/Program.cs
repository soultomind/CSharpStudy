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
            // 특정 구간에 대한 성능을 측정할 때 많이 사용된다.
            Stopwatch sw = new Stopwatch();
            sw.Start();

            ExamBitCoverter();
            Thread.Sleep(1500);

            sw.Stop();
            Console.WriteLine("Stopwatch.ElapsedMilliseconds={0}", sw.ElapsedMilliseconds / 1000);
            Console.ReadKey();
        }

        private static void ExamDateTime()
        {
            DateTime newDateTime = new DateTime(2020, 06, 27, 10, 0, 0);
            Console.WriteLine("CSharp Study Time={0}", newDateTime);

            DateTime now = DateTime.Now;
            DateTime utcNow = DateTime.UtcNow;
            Console.WriteLine(now.ToString("yyyy-MM-dd HH:mm:ss"));

            Console.WriteLine("Now.DayOfYear={0}", now.DayOfYear);
            Console.WriteLine("Now.DayOfWeek={0}.", now.DayOfWeek);

            Console.WriteLine("Now.AddDays(7)={0}", now.AddDays(7));
            foreach (string fileName in Directory.GetFiles("C:\\Temp1"))
            {
                FileInfo fi = new FileInfo(fileName);
                DateTime fileWriteTime = fi.LastWriteTime;

                // 1초(Second)는 1,000 밀리초(Millisecond)인데 이보다 정밀도가 높은 시간 값이 필요하다면 Ticks 속성을 이용
                // 1밀리초의 10,000분의 1에 해당하는 정밀도를 가지고 있다.
                TimeSpan time = new TimeSpan(now.Ticks - fileWriteTime.Ticks);

                if (time.TotalMinutes > 5)
                {
                    fi.Delete();
                    Console.WriteLine("{0} Delete File.", fi.Name);
                }
            }

            DateTime birthTime = new DateTime(1987, 9, 30);

            TimeSpan lifeTime = now.Subtract(birthTime);
            Console.WriteLine("LifeTime.Days={0}", lifeTime.Days);

            // 9시간 차이
            Console.WriteLine("DateTime.UtcNow={0}, Kind={1}", utcNow, utcNow.Kind);
            Console.WriteLine("DateTime.Now={0}, Kind={1}", now, now.Kind);

            TimeSpan gap = new DateTime(now.Year, 12, 31) - now;
            Console.WriteLine("Gap.TotalDays={0}", (int)gap.TotalDays);
            Console.WriteLine("Now.DayOfYear + Gap.Totaldays = {0}", now.DayOfYear + (int)gap.TotalDays);
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

            Console.WriteLine();
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
            Console.WriteLine("Encoding.Default={0}", Encoding.Default);
            foreach (EncodingInfo encodingInfo in Encoding.GetEncodings())
            {
                Console.WriteLine("EncodingInfo Name={0}, CodePage={1}", encodingInfo.Name, encodingInfo.CodePage);
            }

            string json = "{event:\"execute\"}";
            string encode = ConvertUtility.Base64Encode(json);
            Console.WriteLine(encode);
            string decode = ConvertUtility.Base64Decode(encode);
            Console.WriteLine(decode);
        }

        private static void ExamRegularExpression()
        {
            Console.WriteLine("ColorUtility.IsValidCssStyleRgba(\"#336655\")={0}", 
                ColorUtility.IsValidCssStyleSharpRgba("#336655"));
            Console.WriteLine("ColorUtility.IsValidCssStyleRgba(\"#336655FF\")={0}",
                ColorUtility.IsValidCssStyleSharpRgba("#336655FF"));
            Console.WriteLine("ColorUtility.IsValidCssStyleRgba(\"#336655FG\")={0}",
                ColorUtility.IsValidCssStyleSharpRgba("#336655FG"));
        }

        private static void PrintBytes(ref byte[] bytes)
        {
            foreach (byte @byte in bytes)
            {
                Console.Write(@byte);
            }
        }
        private static void ExamBitCoverter()
        {
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
            byte[] shortBytes = BitConverter.GetBytes((short)32);
            byte[] intBytes = BitConverter.GetBytes(32);

            PrintBytes(ref boolBytes); Console.WriteLine(" BoolBytes={0}", BitConverter.ToString(boolBytes));
            PrintBytes(ref shortBytes); Console.WriteLine(" ShortBytes={0}", BitConverter.ToString(shortBytes));
            PrintBytes(ref intBytes); Console.WriteLine(" IntBytes={0}", BitConverter.ToString(intBytes));
            // 1바이트는 8비트(00000000) 2바이트(00000000 00000000)

            Console.WriteLine(Convert.ToString(32, 2));

            string value = BitConverter.ToString(intBytes).Replace("-", "");
            int nTemp = Convert.ToInt32(value, 16);
            value = Convert.ToString(nTemp, 2);
            Console.WriteLine("Length={0}, {1}", value.Length, value);

            // 소켓 패킷 정의시 데이터에 대한 길이 설정 가능값
            Console.WriteLine(32000.ToString("D10"));
        }
    }
}
