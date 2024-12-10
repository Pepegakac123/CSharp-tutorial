using System;
// using System.IO;
using System.Collections.Generic;
namespace zadanie_tablice_sudoku
{
    // Napisz program, który sprawdzi czy podana tabela 9x9 jednocyfrowych liczb całkowitych jest prawidłowym rozwiązaniem łamigłówki Sudoku.

    // Wejście:

    // 9 linii, w każdej linii 9 cyfr (od 1 do 9) oddzielonych pojedynczą spacją
    // Wyjście:

    // napis yes jeśli podany zestaw stanowi poprawne rozwiązanie Sudoku, w przeciwnym przypadku napis no
    // UWAGA: piszesz cały program, odczytujący ze standardowego wejścia i piszący na standardowe wyjście.
    // 2 5 1 7 6 9 3 4 8
    // 9 8 6 3 4 5 2 7 1
    // 3 7 4 8 2 1 6 9 5
    // 4 2 9 6 3 8 5 1 7
    // 8 6 3 5 1 7 9 2 4
    // 5 1 7 4 9 2 8 3 6
    // 7 9 5 1 8 3 4 6 2
    // 1 4 2 9 5 6 7 8 3
    // 6 3 8 2 7 4 1 5 9


    public static class Program
    {
        public static void Main(string[] args)
        {
            int[,] sudoku = new int[9, 9];

            // string[] lines = File.ReadAllLines("sudoku.txt");

            // for (int i = 0; i < sudoku.GetLength(0); i++)
            // {
            //     string[] input = lines[i].Split(' ');
            //     for (int j = 0; j < input.Length; j++)
            //     {
            //         sudoku[i, j] = int.Parse(input[j]);
            //     }
            // }
            for (int i = 0; i < sudoku.GetLength(0); i++)
            {
                string[] input = Console.ReadLine().Split(' ');
                for (int j = 0; j < sudoku.GetLength(1); j++)
                {
                    sudoku[i, j] = int.Parse(input[j]);
                }
            }
            bool isRowsCorrect = CheckRows(sudoku);
            bool isColumnsCorrect = CheckColumns(sudoku);
            bool isSquareCorrect = CheckSquares(sudoku);
            if (isRowsCorrect && isColumnsCorrect && isSquareCorrect)
            {
                Console.WriteLine("yes");
                return;
            }
            Console.WriteLine("no");
        }
        public static bool CheckRows(int[,] sudoku)
        {
            HashSet<int> uniqueNumbers = new HashSet<int>();
            for (int i = 0; i < sudoku.GetLength(0); i++)
            {
                for (int j = 0; j < sudoku.GetLength(1); j++)
                {
                    uniqueNumbers.Add(sudoku[i, j]);
                }
                if (uniqueNumbers.Count != 9)
                {
                    return false;
                }
                uniqueNumbers.Clear();
            }
            return true;
        }
        public static bool CheckColumns(int[,] sudoku)
        {
            HashSet<int> uniqueNumbers = new HashSet<int>();
            for (int i = 0; i < sudoku.GetLength(0); i++)
            {
                for (int j = 0; j < sudoku.GetLength(1); j++)
                {
                    uniqueNumbers.Add(sudoku[j, i]);
                }
                if (uniqueNumbers.Count != 9)
                {
                    return false;
                }
                uniqueNumbers.Clear();
            }
            return true;
        }

        public static bool CheckSquares(int[,] sudoku)
        {
            HashSet<int> uniqueNumbers = new HashSet<int>();

            // Iteracja po kwadratach (3 rzędy kwadratów, 3 kolumny kwadratów)
            for (int blockRow = 0; blockRow < 3; blockRow++)
            {
                for (int blockCol = 0; blockCol < 3; blockCol++)
                {
                    // Iteracja po komórkach wewnątrz kwadratu 3x3
                    for (int cellRow = 0; cellRow < 3; cellRow++)
                    {
                        for (int cellCol = 0; cellCol < 3; cellCol++)
                        {
                            // Obliczanie rzeczywistego indeksu w tablicy sudoku
                            int actualRow = (blockRow * 3) + cellRow;
                            int actualCol = (blockCol * 3) + cellCol;

                            // Kolejność sprawdzania:
                            // cellRow=0, cellCol=0 -> sudoku[0,0] = 2
                            // cellRow=0, cellCol=1 -> sudoku[0,1] = 5  

                            uniqueNumbers.Add(sudoku[actualRow, actualCol]);
                        }
                    }

                    // Sprawdzenie czy kwadrat zawiera 9 unikalnych liczb
                    if (uniqueNumbers.Count != 9)
                    {
                        return false;
                    }
                    uniqueNumbers.Clear();
                }
            }

            return true;
        }
    }
}