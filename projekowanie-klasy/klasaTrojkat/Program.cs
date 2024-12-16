using System;

namespace klasaTrojkat
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Podaj boki trojkata: ");
            string[] boki = Console.ReadLine().Split(' ');
            try
            {
                Trojkat t = new Trojkat(double.Parse(boki[0]), double.Parse(boki[1]), double.Parse(boki[2]));
                t.wypiszInfo();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    public class Trojkat
    {
        public double A { get; set; } = 1;
        public double B { get; set; } = 1;
        public double C { get; set; } = 1;

        public Trojkat()
        {

        }

        public Trojkat(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
                throw new ArgumentOutOfRangeException("Boki muszą być większe od zera");
            if (a + b <= c || a + c <= b || b + c <= a)
                throw new ArgumentException("Wszystkie boki muszą być mniejsze od sumy innych boków");
            A = a;
            B = b;
            C = c;
        }

        public double ObliczPole()
        {
            double p = (A + B + C) / 2;
            return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
        }

        public double ObliczObwod()
        {
            return A + B + C;
        }
        public string SprawdzRodzajKatow()
        {
            double[] boki = new[] { A, B, C };
            Array.Sort(boki);

            // Twierdzenie cosinusów: cos(kąta) = (a² + b² - c²) / (2ab)
            double cosKata = (Math.Pow(boki[0], 2) + Math.Pow(boki[1], 2) - Math.Pow(boki[2], 2))
                             / (2 * boki[0] * boki[1]);

            if (Math.Abs(cosKata) < 0.000001) // prawie 0 - kąt prosty
                return "prostokątny";
            else if (cosKata < 0)
                return "rozwartokątny";
            else
                return "ostrokątny";
        }
        public string SprawdzRodzajBokow()
        {
            if (Math.Abs(A - B) < 0.000001 && Math.Abs(B - C) < 0.000001)
                return "równoboczny";
            else if (Math.Abs(A - B) < 0.000001 || Math.Abs(B - C) < 0.000001 || Math.Abs(A - C) < 0.000001)
                return "równoramienny";
            else
                return "różnoboczny";
        }

        public void wypiszInfo()
        {
            Console.WriteLine($"Boki: {A}, {B}, {C}");
            Console.WriteLine($"Pole: {ObliczPole()}");
            Console.WriteLine($"Obwod: {ObliczObwod()}");
            Console.WriteLine($"Rodzaj katow: {SprawdzRodzajKatow()}");
            Console.WriteLine($"Rodzaj bokow: {SprawdzRodzajBokow()}");
        }

    }
}