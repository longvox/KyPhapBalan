using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyPhapBaLan
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(KyPhap.Calc(KyPhap.ConvertToPostfix("6/2(1+2)")));
        }


    }
}
