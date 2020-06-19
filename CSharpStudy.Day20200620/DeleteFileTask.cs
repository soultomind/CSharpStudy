using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStudy.Day20200620
{
    class DeleteFileTask : ITask
    {
        public void Start()
        {
            
        }

        public void Work(object arg)
        {
            string path = arg as string;
            if (path != null)
            {
                Console.WriteLine(String.Format("{0} 파일 삭제.", path));
            }
        }

        public void Finish()
        {
            
        }
    }
}
