using System;

class Program
{
    static void Main()
    {
        // try
        // {
        //     // Wczytanie trzech napisów
        //     string input1 = Console.ReadLine();
        //     string input2 = Console.ReadLine();
        //     string input3 = Console.ReadLine();

        //     // Sprawdzenie czy któryś z inputów nie jest pusty
        //     if (string.IsNullOrEmpty(input1) || string.IsNullOrEmpty(input2) || string.IsNullOrEmpty(input3))
        //     {
        //         throw new ArgumentException();
        //     }

        //     // Konwersja na liczby całkowite
        //     int num1 = int.Parse(input1);
        //     int num2 = int.Parse(input2);
        //     int num3 = int.Parse(input3);

        //     // Znalezienie najmniejszej i największej wartości
        //     int min = Math.Min(Math.Min(num1, num2), num3);
        //     int max = Math.Max(Math.Max(num1, num2), num3);

        //     // Obliczenie i wyświetlenie rozstępu
        //     Console.WriteLine(max - min);
        // }
        // catch (ArgumentException)
        // {
        //     Console.WriteLine("argument exception, exit");
        // }
        // catch (FormatException)
        // {
        //     Console.WriteLine("format exception, exit");
        // }
        // catch (OverflowException)
        // {
        //     Console.WriteLine("overflow exception, exit");
        // }
        // catch (Exception)
        // {
        //     Console.WriteLine("non supported exception, exit");
        // }
        // Test 1: tablica z wartościami [1,1,1], [2,2], [3,3,3,3,3]
        int[][] test1 = new int[][]
        {
    new int[] { 1, 1, 1,1 },
    new int[] { 2, 2 },
    new int[] { 3, 3, 3, 3, 3,3 }
        };

        // Test 2: tablica z wartościami [1,2,3]
        int[][] test2 = new int[][]
        {
    new int[] { 1, 2, 3 }
        };

        // Test 3: tablica z wartościami [-1], [1], [1]
        int[][] test3 = new int[][]
        {
    new int[] { -1 },
    new int[] { 1 },
    new int[] { 1 }
        };

        // Test 4: tablica z wartościami [1,1], [2,-1]
        int[][] test4 = new int[][]
        {
    new int[] { 1, 1 },
    new int[] { 2, -1 }
        };

        // Test 5: tablica z wartościami [-1,-2], [-3,-4]
        int[][] test5 = new int[][]
        {
    new int[] { -1, -2 },
    new int[] { -3, -4 }
        };

        // Przykład użycia:
        Console.WriteLine(Srednia(test1)); // Powinno zwrócić 2.17
        Console.WriteLine(Srednia(test2)); // Powinno zwrócić 2.00
        Console.WriteLine(Srednia(test3)); // Powinno zwrócić 1.00
        Console.WriteLine(Srednia(test4)); // Powinno zwrócić 1.33
        Console.WriteLine(Srednia(test5)); // Powinno zwrócić 0.00
    }
    public static double Srednia(int[][] tab)
    {
        if (tab == null || tab.Length == 0)
        {
            return 0.00;
        }

        double suma = 0;
        int counter = 0;

        for (int i = 0; i < tab.Length; i++)
        {
            if (tab[i] == null) continue;

            for (int j = 0; j < tab[i].Length; j++)
            {
                if (tab[i][j] > 0)
                {
                    suma += tab[i][j];
                    counter++;
                }
            }
        }

        if (counter == 0) return 0.00;

        double srednia = suma / counter;
        return Math.Round(srednia, 2);
    }
}