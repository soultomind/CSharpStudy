using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStudy.Day20200620
{
    class FileDeleteCompleted : ITaskCompleted
    {
        public void Completed(object arg)
        {
            string path = arg as string;
            Console.WriteLine(String.Format("{0} 파일 삭제.", path));
        }
    }
}
