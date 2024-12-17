using System;

class Program
{

    public static void Main(string[] args)
    {
        // int[] numbers = new int[3];
        // int suma = 0;
        // try
        // {
        //     for (int i = 0; i < 3; i++)
        //     {
        //         string input = Console.ReadLine();

        //         // Sprawdzenie czy input jest null lub pusty
        //         if (string.IsNullOrEmpty(input))
        //         {
        //             throw new ArgumentException();
        //         }

        //         // Dla liczb z wiodącymi zerami używamy podstawowego parsowania
        //         if (input.StartsWith("00"))
        //         {
        //             if (!int.TryParse(input, out numbers[i]))
        //             {
        //                 throw new FormatException();
        //             }
        //             continue;
        //         }

        //         // Dla pozostałych przypadków używamy long do sprawdzenia zakresu
        //         long longValue = long.Parse(input);
        //         if (longValue > int.MaxValue || longValue < int.MinValue)
        //         {
        //             throw new OverflowException();
        //         }
        //         numbers[i] = (int)longValue;
        //     }

        //     checked
        //     {
        //         suma = numbers[0] + numbers[1] + numbers[2];
        //     }
        //     Console.WriteLine(suma);
        // }
        // catch (FormatException)
        // {
        //     Console.WriteLine("format exception, exit");
        // }
        // catch (ArgumentException)
        // {
        //     Console.WriteLine("argument exception, exit");
        // }
        // catch (OverflowException)
        // {
        //     Console.WriteLine("overflow exception, exit");
        // }
        // catch (Exception)
        // {
        //     Console.WriteLine("non supported exception, exit");
        // }
        double srednia = Srednia(null);
    }
    public static void Wzorek(int n)
    {
        if (n % 2 == 0) n = n - 1; // Dostosowanie, aby n było nieparzyste

        for (int i = 0; i < n; i++) // Iteracja po wierszach
        {
            for (int j = 0; j < n; j++) // Iteracja po kolumnach
            {
                // Rysowanie gwiazdek na odpowiednich pozycjach
                if (i == 0) // Podstawa na górze
                {
                    Console.Write("*");
                }
                else if (i <= n / 2 && (j >= i && j <= n - i - 1)) // Skrócone boki
                {
                    Console.Write("*");
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine(); // Przejście do nowej linii po zakończeniu rysowania wiersza
        }
    }

    public static double TriangleArea(int a, int b, int c, int precision = 2)
    {
        if (a < 0 || b < 0 || c < 0 || precision < 2 || precision > 8)
        {
            throw new ArgumentOutOfRangeException("wrong arguments");
        }
        if (a + b <= c || a + c <= b || b + c <= a)
        {
            throw new ArgumentException("object not exist");
        }
        double obw = ((double)a + (double)b + (double)c) / 2;
        return Math.Round(Math.Sqrt(obw * (obw - a) * (obw - b) * (obw - c)), precision);
    }

    public static double Srednia(int[,] tab)
    {
        if (tab == null || tab.GetLength(0) == 0 || tab.GetLength(1) == 0)
        {
            return 0.00;
        }
        int suma = 0;
        int counter = 0;
        for (int i = 0; i < tab.GetLength(0); i++)
        {
            for (int j = 0; j < tab.GetLength(1); j++)
            {
                if (tab[i, j] > 0)
                {
                    suma += tab[i, j];
                    counter++;
                }
            }
        }
        if (counter == 0)
            return 0.00;
        double srednia = (double)suma / counter;
        return Math.Round(srednia, 2);

    }
}
