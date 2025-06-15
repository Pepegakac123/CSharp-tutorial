using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WatkiLaby
{
    class Program
    {
        private static void CialoFunkcjiWatku(object o)
        {
            string infoToPrint = (string)o;
            for(int x =0; x < 10; x++)
            {
                Console.WriteLine($"{infoToPrint} {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(10);
            }
        }
        static void Main(string[] args)
        {
            ParameterizedThreadStart operation = new ParameterizedThreadStart(CialoFunkcjiWatku);

            Thread thread1 = new Thread(operation);
            thread1.Start("Dziendobry");

            Thread thread2 = new Thread(operation);
            thread2.Start("Dowidzenia");

            Console.ReadKey();

        }
    }
}
