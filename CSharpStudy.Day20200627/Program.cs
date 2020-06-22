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

            ExamString();
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
            // Alpha, Red, Green, Blue

            // 8비트 8자리 1,2,4,8,16,32,64,128=255 값으로 ARGB 의 값을 Int 형으로 표현 할 수 있다.
            int argb = 0x00FF0000;
            Console.WriteLine("Argb=" + argb);
            // Alapha=00000000, Red=11111111, Green=00000000, Blue=00000000

            Color color = ColorUtility.FromArgb(argb);
            Console.WriteLine(color.ToString());
            Console.WriteLine("Color.ToArgb()={0}", color.ToArgb());

            argb = ColorUtility.ToCssRgba("rgba(255, 0, 0, 0)");
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

            // String 타입은 불변 객체 이기 때문에 모든 변환은 새로운 메모리 할당을 발생

            // Capacity는 기존 Capacity 만큼의 문자열이 찰 경우 해당 크기의 두 배로!
            StringBuilder builder = new StringBuilder();
            Console.WriteLine("Capacity={0}", builder.MaxCapacity);
            Console.WriteLine("Capacity={0}, Length={1}", builder.Capacity, builder.Length);
            builder.Append("ABCDEFGHIJKLMNOP");
            Console.WriteLine("Capacity={0}, Length={1}", builder.Capacity, builder.Length);
            builder.Append("Q");
            Console.WriteLine("Capacity={0}, Length={1}", builder.Capacity, builder.Length);
            builder.Append(builder.ToString());
            Console.WriteLine("Capacity={0}, Length={1}", builder.Capacity, builder.Length);
        }
    }
}
