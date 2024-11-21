// using System;

// namespace zglaszanie_i_przechwytywanie_wyjatkow
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             System.Console.WriteLine("--test: start");
//             double result = TrianglePerimeter(1, 1, 1, 3);
//             System.Console.WriteLine(result);
//             System.Console.WriteLine("--test: stop");
//         }

//         public static double TrianglePerimeter(int a, int b, int c, int precision = 2)
//         {
//             double wynik;
//             if(precision < 2 || precision > 8 || a<0 || b<0 || c<0) throw new ArgumentOutOfRangeException("wrong arguments");
//             if(a > b + c || b > a + c || c > a + b) throw new ArgumentException("object not exist");
            
//             wynik = a + b + c;
//             return Math.Round(wynik,precision);
//         }
//     }
// }

// Part 2

using System;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Wczytanie danych ze standardowego wejścia
            int a = int.Parse(Console.ReadLine());
            int b = int.Parse(Console.ReadLine());
            int c = int.Parse(Console.ReadLine());

            // Obliczenie iloczynu trzech liczb
            int iloczyn = checked(a * b * c);
            Console.WriteLine(iloczyn);
        }
        catch (ArgumentException)
        {
            // Wyjątek, gdy podano nieprawidłowy argument
            Console.WriteLine("argument exception, exit");
        }
        catch (FormatException)
        {
            // Wyjątek, gdy dane wejściowe nie są liczbami całkowitymi
            Console.WriteLine("format exception, exit");
        }
        catch (OverflowException)
        {
            // Wyjątek, gdy liczba przekracza zakres typu int
            Console.WriteLine("overflow exception, exit");
        }
        catch (Exception)
        {
            // Obsługa pozostałych nieobsługiwanych wyjątków
            Console.WriteLine("non supported exception, exit");
        }
    }
}
