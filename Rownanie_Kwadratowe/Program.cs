using System;
using System.Collections.Generic;

namespace Rownanie_Kwadratowe
{
    class Program
    {
public static void QuadraticEquation(int a, int b, int c)
{
    if (a == 0 && b == 0 && c == 0)
    {
        Console.WriteLine("infinity");
        return;
    }
    if (a == 0 && b == 0)
    {
        Console.WriteLine("empty");
        return;
    }
    if (a == 0)
    {
        // bx + c = 0
        double x = Math.Round(-((double)c / b), 2);
        Console.WriteLine($"x={x:0.00}");
        return;
    }


    double delta = b * b - 4.0 * a * c;
    Console.WriteLine(delta);
    if (delta > 0)
    {
        double sqrtDelta = Math.Sqrt(delta);
        double x1 = Math.Round((-b + sqrtDelta) / (2 * a), 2);
        double x2 = Math.Round((-b - sqrtDelta) / (2 * a), 2);


        if (x1 < x2)
        {
            Console.WriteLine($"x1={x1:0.00}");
            Console.WriteLine($"x2={x2:0.00}");
        }
        else
        {
            Console.WriteLine($"x1={x2:0.00}");
            Console.WriteLine($"x2={x1:0.00}");
        }
    }
    else if (delta == 0)
    {
        double x = Math.Round((double)-b / (2 * a), 2);
        Console.WriteLine($"x={x:0.00}");
    }
    else
    {
        Console.WriteLine("empty");
    }
}

        static void Main(string[] args)
        {
            // Przykłady użycia funkcji
            // QuadraticEquation(1, 3, 2);  // Oczekiwane: x1=-2.00, x2=-1.00
            // QuadraticEquation(1, -2, 1); // Oczekiwane: x=1.00
            // QuadraticEquation(1, 1, 1);  // Oczekiwane: empty
            // QuadraticEquation(0, 0, 0);  // Oczekiwane: infinity
            // QuadraticEquation(0, 2, 1);  // Oczekiwane: 0.50
            QuadraticEquation(1000000,6000,-300295);
        }
    }
}
