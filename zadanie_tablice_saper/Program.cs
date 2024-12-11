using System;
using System.Collections.Generic;

namespace zadanie_tablice_saper
{
    public class Program
    {
        // 4 5
        //.*.*.
        // ..*..
        // ..*..
        // .....
        public static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split(" ");
            char[,] saperBoard = CreateBoard(input);
            PrintBoard(saperBoard);
        }

        public static char[,] CreateBoard(string[] dimensions)
        {
            char[,] saperBoard = new char[int.Parse(dimensions[0]), int.Parse(dimensions[1])];

            // Wczytujemy planszę wiersz po wierszu
            for (int rows = 0; rows < saperBoard.GetLength(0); rows++)
            {
                string line = Console.ReadLine(); // Wczytujemy cały wiersz
                for (int cols = 0; cols < saperBoard.GetLength(1); cols++)
                {
                    saperBoard[rows, cols] = line[cols]; // Pobieramy konkretny znak z wiersza
                }
            }
            return saperBoard;
        }
        public static void PrintBoard(char[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == '*')
                    {
                        Console.Write('*');
                        continue;
                    }

                    int counter = 0;
                    // Sprawdzamy wszystkie sąsiednie pola (8 kierunków)
                    for (int di = -1; di <= 1; di++)
                    {
                        for (int dj = -1; dj <= 1; dj++)
                        {
                            // Pomijamy aktualne pole
                            if (di == 0 && dj == 0) continue;

                            // Sprawdzamy czy sąsiednie pole mieści się w granicach planszy
                            int newI = i + di;
                            int newJ = j + dj;
                            if (newI >= 0 && newI < board.GetLength(0) &&
                                newJ >= 0 && newJ < board.GetLength(1))
                            {
                                if (board[newI, newJ] == '*')
                                {
                                    counter++;
                                }
                            }
                        }
                    }

                    // Wyświetlamy wynik
                    Console.Write(counter > 0 ? counter.ToString() : ".");
                }
                Console.WriteLine();
            }

        }
    }
}