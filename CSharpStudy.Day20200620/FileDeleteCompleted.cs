using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStudy.Day20200620
{
    class FileDeleteCompleted : IOnCompleted
    {
        public void Completed(object arg)
        {
            string directory = arg as string;
            Console.WriteLine(String.Format("{0} 디렉토리 안에 파일에 대한 삭제가 완료되었습니다.", directory));
        }
    }
}
