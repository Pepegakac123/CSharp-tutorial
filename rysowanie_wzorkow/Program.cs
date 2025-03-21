﻿using System;
using System.IO.Compression;

class Program
{

    const char CHAR = '*';
    static void Star() => Console.Write(CHAR);
    static void StarLn() => Console.WriteLine(CHAR);
    static void Space() => Console.Write(" ");
    static void SpaceLn() => Console.WriteLine(" ");
    static void NewLine() => Console.WriteLine();

    static void LiteraX(int n)
    {
        if (n < 3) throw new ArgumentException("zbyt mały rozmiar");
        if (n % 2 == 0) n = n + 1;

        //górna połówka
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (j == i || j == n - i - 1)
                {
                    Star();
                }
                else
                {
                    Space();
                }

            }
            NewLine();
        }
    }

    static void odwroconaLiteraZ(int n)
    {
        if (n < 3) throw new ArgumentException("zbyt mały rozmiar");

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i == 0 || i == n - 1 || i == j)
                {
                    Star();
                }
                else
                {
                    Space();
                }
            }
            NewLine();
        }

    }

    static void LiteraZ(int n)
    {
        if (n < 3) throw new ArgumentException("zbyt mały rozmiar");

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i == 0 || i == n - 1)
                {
                    Star();
                }
                else if (j == n - i - 1)
                {
                    Star();
                }
                else
                {
                    Space();
                }
            }
            NewLine();
        }

    }

    public static void Klepsydra(int n)
    {
        if (n < 3) throw new ArgumentException("zbyt mały rozmiar");

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i == 0 || i == n - 1)
                {
                    Star();
                }
                else if (i == j || j == n - i - 1)
                {
                    Star();
                }
                else
                {
                    Space();
                }
            }
            NewLine();
        }

    }

    public static void TrojkatWypelniony1(int n)
    {
        if (n < 3) throw new ArgumentException("zbyt mały rozmiar");
        if (n % 2 == 0) n = n - 1;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++) // Iteracja po kolumnach
            {
                // Rysowanie gwiazdek na odpowiednich pozycjach:
                if (i == n / 2 || (j >= n / 2 - i && j <= n / 2 + i && i <= n / 2)) // Podstawa i boki trójkąta
                    Star();
                else
                    Space();
            }
            NewLine();
        }
    }
    public static void TrojkatWypelniony2(int n)
    {
        if (n < 5) throw new ArgumentException("zbyt mały rozmiar");
        if (n % 2 == 0) n = n - 1; // Dostosowanie, aby n było nieparzyste

        for (int i = 0; i < n; i++) // Iteracja po wierszach
        {
            for (int j = 0; j < n; j++) // Iteracja po kolumnach
            {
                // Rysowanie gwiazdek na odpowiednich pozycjach
                if (i == 0) // Podstawa na górze
                {
                    Star();
                }
                else if (i <= n / 2 && (j >= i && j <= n - i - 1)) // Skrócone boki
                {
                    Star();
                }
                else
                {
                    Space();
                }
            }
            NewLine(); // Przejście do nowej linii po zakończeniu rysowania wiersza
        }
    }

    public static void TrojkatPusty1(int n)
    {
        if (n < 3) throw new ArgumentException("zbyt mały rozmiar");
        if (n % 2 == 0) n = n - 1; // Dostosowanie, aby n było nieparzyste

        for (int i = 0; i < n; i++) // Iteracja po wierszach
        {
            for (int j = 0; j < n; j++) // Iteracja po kolumnach
            {
                // Rysowanie gwiazdek na odpowiednich pozycjach
                if (i == n / 2 + 1) // Podstawa na dole
                {
                    Star();
                }
                else if (i <= n / 2 && (j == n / 2 - i || j == n / 2 + i)) // Skrócone boki
                {
                    Star();
                }
                else
                {
                    Space();
                }
            }
            NewLine(); // Przejście do nowej linii po zakończeniu rysowania wiersza
        }
    }
    public static void TrojkatPusty2(int n)
    {
        if (n < 3) throw new ArgumentException("zbyt mały rozmiar");
        if (n % 2 == 0) n = n - 1; // Dostosowanie, aby n było nieparzyste

        for (int i = 0; i < n; i++) // Iteracja po wierszach
        {
            for (int j = 0; j < n; j++) // Iteracja po kolumnach
            {
                // Rysowanie gwiazdek na odpowiednich pozycjach
                if (i == 0) // Podstawa na górze
                {
                    Star();
                }
                else if (i <= n / 2 && (j == i || j == n - i - 1)) // Skrócone boki
                {
                    Star();
                }
                else
                {
                    Space();
                }
            }
            NewLine(); // Przejście do nowej linii po zakończeniu rysowania wiersza
        }
    }

    public static void rombPustoPelny(int n)
    {
        if (n < 3) throw new ArgumentException("zbyt mały rozmiar");
        if (n % 2 == 0) n = n - 1; // Dostosowanie, aby n było nieparzyste

        for (int i = 0; i < n; i++) // Iteracja po wierszach
        {
            for (int j = 0; j < n; j++) // Iteracja po kolumnach
            {
                // Rysowanie gwiazdek na odpowiednich pozycjach
                if (i == n / 2) // Podstawa na dole
                {
                    Star();
                }
                else if (i <= n / 2 && (j == n / 2 - i || j == n / 2 + i)) // Skrócone boki
                {
                    Star();
                }
                else if (i > n / 2 && (j >= i - n / 2 && j < n - (i - n / 2))) // Skrócone boki
                {
                    Star();
                }
                else
                {
                    Space();
                }
            }
            NewLine(); // Przejście do nowej linii po zakończeniu rysowania wiersza
        }
    }

    public static void LiteraN(int n)
    {
        if (n < 3) throw new ArgumentException("zbyt mały rozmiar");

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (j == 0 || j == n - 1)
                {
                    Star();
                }
                else if (i == j)
                {
                    Star();
                }
                else
                {
                    Space();
                }
            }
            NewLine();
        }
    }
    public static void OdwroconaLiteraN(int n)
    {
        if (n < 3) throw new ArgumentException("zbyt mały rozmiar");

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (j == 0 || j == n - 1)
                {
                    Star();
                }
                else if (j == n - i - 1)
                {
                    Star();
                }
                else
                {
                    Space();
                }
            }
            NewLine();
        }
    }

    public static void Szachownica(int n)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if ((i % 2 == 0 && j < n / 2) || (i % 2 != 0 && j < n / 2 + 1 && j != 0))
                {
                    Star();
                    Space();
                }
                else
                {
                    Space();
                }
            }
            NewLine();
        }
    }


    static void Main(string[] args)
    {
        // LiteraX(3);
        // odwroconaLiteraZ(8);
        // LiteraZ(4);
        // Klepsydra(7);
        // TrojkatWypelniony1(10);
        // Console.WriteLine("------------");
        // TrojkatWypelniony2(7);
        // TrojkatPusty1(7);
        // rombPustoPelny(7);
        // LiteraN(7);
        // OdwroconaLiteraN(12);
        Szachownica(8);
    }
}
