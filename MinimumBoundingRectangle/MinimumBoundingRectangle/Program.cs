using System;
using System.Collections.Generic;

namespace MinimumBoundingRectangle
{
    class Program
    {
        static Prostokat MinimumBoundingRectangle(IList<IFigura> listaFigur)
        {
            if (listaFigur == null || listaFigur.Count == 0)
                return null;

            
            var pierwszy = listaFigur[0].GetBoundingRectangle();
            int minX = pierwszy.X1;
            int minY = pierwszy.Y1;
            int maxX = pierwszy.X2;
            int maxY = pierwszy.Y2;

            foreach (var figura in listaFigur)
            {
                var prostokat = figura.GetBoundingRectangle();
                minX = Math.Min(minX, prostokat.X1);
                minY = Math.Min(minY, prostokat.Y1);
                maxX = Math.Max(maxX, prostokat.X2);
                maxY = Math.Max(maxY, prostokat.Y2);
            }

            return new Prostokat(minX, minY, maxX, maxY);
        }

        static void Main(string[] args)
        {

            int t = int.Parse(Console.ReadLine()); //liczba testó

            for (int i = 0; i < t; i++)
            {
                
                int n = int.Parse(Console.ReadLine()); // liczba obiektów
                var figury = new List<IFigura>();

                
                for (int j = 0; j < n; j++)
                {
                    string[] dane = Console.ReadLine().Split();
                    char typ = Convert.ToChar(dane[0]);

                    switch (typ)
                    {
                        case 'p': // punkt
                            figury.Add(new Punkt(
                                int.Parse(dane[1]),
                                int.Parse(dane[2])));
                            break;

                        case 'c': // koło
                            figury.Add(new Kolo(
                                int.Parse(dane[1]),
                                int.Parse(dane[2]),
                                int.Parse(dane[3])));
                            break;

                        case 'l': // odcinek
                            figury.Add(new Odcinek(
                                int.Parse(dane[1]),
                                int.Parse(dane[2]),
                                int.Parse(dane[3]),
                                int.Parse(dane[4])));
                            break;
                    }
                }

                
                var wynik = MinimumBoundingRectangle(figury);
                Console.WriteLine($"{wynik.X1} {wynik.Y1} {wynik.X2} {wynik.Y2}");

                // Wczytujemy pustą linie jeżeli testy jeszce nie doszł do końca    
                if (i < t - 1)
                    Console.ReadLine();
            }
        }
    }

    public class Prostokat
    {
        public int X1 { get; set; }  // x lewego dolnego rogu
        public int Y1 { get; set; }  // y lewego dolnego rogu
        public int X2 { get; set; }  // x prawego górnego rogu
        public int Y2 { get; set; }  // y prawego górnego rogu

        public Prostokat(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }
    }
    public interface IFigura
    {
        Prostokat GetBoundingRectangle();
    }

    public class Punkt: IFigura
    {
        public int X { get; set; }
        public int Y { get; set; }
        
        public Punkt(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Prostokat GetBoundingRectangle()
        {
           return new Prostokat(X, Y, X, Y);
        }

    }

    public class Kolo : IFigura {
        public int X { get; set; }
        public int Y { get; set; }
        public int R { get; set; }
        public Kolo(int x, int y, int r)
        {
            X = x;
            Y = y;
            R = r;
        }
        public Prostokat GetBoundingRectangle()
        {
            return new Prostokat(X - R, Y - R, X + R, Y + R);
        }
    }

    public class Odcinek : IFigura { 
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public Odcinek(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }
        public Prostokat GetBoundingRectangle()
        {
            return new Prostokat(Math.Min(X1, X2), Math.Min(Y1, Y2), Math.Max(X1, X2), Math.Max(Y1, Y2));
        }
    }
}