using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Delegaty
{
    class Program
    {

        static void Hello(string msg)
        {
            Console.WriteLine("Hello: " + msg);
        }

        static void Goodbye(string msg)
        {
            Console.WriteLine("Goodbye: " + msg);
        }

        delegate void odnosnik(string msg);
        static void Main(string[] args)
        {
            odnosnik wlascicielOdnosnika, wlascicielOdnosnika2, wlascicielOdnosnika3, wlascicielOdnosnika4;

            wlascicielOdnosnika = Hello;
            wlascicielOdnosnika2 = Goodbye;
            wlascicielOdnosnika3 = wlascicielOdnosnika + wlascicielOdnosnika2;
            wlascicielOdnosnika4 = wlascicielOdnosnika3 - wlascicielOdnosnika2;
            wlascicielOdnosnika("test");
        }
    }
}
