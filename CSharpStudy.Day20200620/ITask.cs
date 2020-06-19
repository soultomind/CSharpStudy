using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStudy.Day20200620
{
    interface ITask
    {
        void Start();
        void Work(object arg);
        void Finish();
    }
}
