using System;
// static char[][] ReadJaggedArrayFromStdInput() - z pierwszej linii standardowego wejścia wczytuje liczbę wierszy (<10), następnie wczytuje kolejne wiersze składające się ze znaków oddzielonych pojedynczą spacją. Jako wynik swojego działania zwraca wczytane dane w formie tablicy postrzępionej (patrz sygnatura),
// static char[][] TransposeJaggedArray(char[][] tab) - transponuje tablicę, zwracając nową, w której kolumny stają się wierszami, zaś wiersze kolumnami,
// static void PrintJaggedArrayToStdOutput(char[][] tab) - wypisuje na standardowe wyjście tablicę wierszami w kolejnych liniach.
// Przy implementacji sugeruj się podanymi przykładami.

// Możesz utworzyć inne metody, które uznasz za pomocne, ale musisz zaprogramować podane powyżej dokładnie z taką sygnaturą.

// Twoje implementacje metod testowane będą w metodzie Main (nie pisz jej!!) w następujący sposób:

// 3
// a b c d e
// a b
// a b c
namespace zadanie_tablice_2d
{
    public static class Program{
        public static void Main(string[] args){
                char[][] jagged = ReadJaggedArrayFromStdInput();
                PrintJaggedArrayToStdOutput(jagged);
                jagged = TransposeJaggedArray(jagged);
                Console.WriteLine();
                PrintJaggedArrayToStdOutput(jagged);
        }

        static char[][] ReadJaggedArrayFromStdInput()
        {
            int rows = int.Parse(Console.ReadLine());
            char[][] jaggedArray = new char[rows][]; // Poprawiona deklaracja
            
            for (int i = 0; i < rows; i++)
            {
                // Wczytujemy linię i dzielimy ją spacjami
                string[] line = Console.ReadLine().Split(' ');
                jaggedArray[i] = new char[line.Length];
                
                // Kopiujemy znaki do tablicy
                for (int j = 0; j < line.Length; j++)
                {
                    jaggedArray[i][j] = line[j][0];
                }
            }
            return jaggedArray;
        }

        static void PrintJaggedArrayToStdOutput(char[][] tab)
        {
            for(int i = 0; i < tab.Length; i++)
            {
                if(tab[i] == null) continue;
                
                for(int j = 0; j < tab[i].Length; j++)
                {
                    if(tab[i][j] == '\0') continue;
                    Console.Write(tab[i][j]);
                    if (j < tab[i].Length - 1) 
                        Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        static char[][] TransposeJaggedArray(char[][] tab)
{
    int maxColumn = 0;
    // Znajdujemy najdłuższy wiersz
    for (int i = 0; i < tab.Length; i++)
        maxColumn = Math.Max(maxColumn, tab[i].Length);

    char[][] transposed = new char[maxColumn][];
    for (int i = 0; i < maxColumn; i++)
        transposed[i] = new char[tab.Length];

    // Wypełniamy pustymi znakami
    for (int i = 0; i < maxColumn; i++)
        for (int j = 0; j < tab.Length; j++)
            transposed[i][j] = ' ';

    // Przepisujemy istniejące elementy
    for (int i = 0; i < tab.Length; i++)
        for (int j = 0; j < tab[i].Length; j++)
            transposed[j][i] = tab[i][j];

    return transposed;
}
    }
}
