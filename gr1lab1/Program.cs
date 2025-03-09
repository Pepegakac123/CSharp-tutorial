using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Gr1Lab1
{
    class Ulamek : IComparable<Ulamek>
    {
        public int licznik;
        public int mianownik;

        public Ulamek(int inlicznik, int inMianownik)
        {
            if (inMianownik == 0)
            {
                throw new ArgumentException("nie moze byc zero");
            }

            // Assign values first
            licznik = inlicznik;
            mianownik = inMianownik;

            // Handle negative denominators
            if (mianownik < 0)
            {
                licznik = -licznik;
                mianownik = -mianownik;
            }

            // Simplify the fraction
            Uproscic();
        }

        private int NajwiekszyWspolnyDzielnik(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }


        private void Uproscic()
        {

            int nwd = NajwiekszyWspolnyDzielnik(Math.Abs(licznik), Math.Abs(mianownik));

            if (nwd > 1)
            {
                licznik /= nwd;
                mianownik /= nwd;
            }
        }


        public int CompareTo(Ulamek other)
        {
            if (other == null) return 1;

            // Mnożymy krzyżowo aby porównywać ułamki
            // a/b < c/d jeśli a*d < c*b
            return (this.licznik * other.mianownik).CompareTo(other.licznik * this.mianownik);
        }





        public override string ToString()
        {
            return licznik.ToString() + "/" + mianownik.ToString();
        }

        public static Ulamek operator *(Ulamek a, Ulamek b)
        {
            int licznik = a.licznik * b.licznik;
            int mianownik = a.mianownik * b.mianownik;
            return (new Ulamek(licznik, mianownik));
        }

        public static Ulamek operator +(Ulamek a, Ulamek b)
        {

            int nowy_licznik = a.licznik * b.mianownik + b.licznik * a.mianownik;
            int nowy_mianownik = a.mianownik * b.mianownik;
            return (new Ulamek(nowy_licznik, nowy_mianownik));
        }

        public static Ulamek operator -(Ulamek a, Ulamek b)
        {

            int nowy_licznik = a.licznik * b.mianownik - b.licznik * a.mianownik;
            int nowy_mianownik = a.mianownik * b.mianownik;
            return (new Ulamek(nowy_licznik, nowy_mianownik));
        }

        public static Ulamek operator /(Ulamek a, Ulamek b)
        {
            int licznik = a.licznik * b.mianownik;
            int mianownik = a.mianownik * b.licznik;

            if (mianownik == 0)
                throw new DivideByZeroException("Division by zero fraction");

            return (new Ulamek(licznik, mianownik));
        }

        public static bool operator >(Ulamek a, Ulamek b)
        {
            return a.licznik * b.mianownik > b.licznik * a.mianownik;
        }

        public static bool operator <(Ulamek a, Ulamek b)
        {
            return a.licznik * b.mianownik < b.licznik * a.mianownik;
        }

        public static explicit operator double(Ulamek a)
        {
            return (double)a.licznik / (double)a.mianownik;
        }

        public static bool operator ==(Ulamek a, Ulamek b)
        {
            // Sprawdzamy, czy oba obiekty są null
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            // Sprawdzamy, czy którykolwiek z obiektów jest null
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            // Porównujemy wartości ułamków bezpośrednio
            // (liczniki i mianowniki powinny być już uproszczone)
            return a.licznik == b.licznik && a.mianownik == b.mianownik;
        }

        public static bool operator !=(Ulamek a, Ulamek b)
        {
            // Używamy negacji operatora równości
            return !(a == b);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Ulamek polowa = new Ulamek(1, 2);
            Ulamek cwierc = new Ulamek(1, 4);

            Console.WriteLine(polowa);
            Console.WriteLine(cwierc);

            Ulamek pomnozony = polowa * cwierc;
            Console.WriteLine("pomnozony " + pomnozony);

            Ulamek dodany = polowa + cwierc;
            Console.WriteLine("dodany " + dodany);

            Ulamek odjety = polowa - cwierc;
            Console.WriteLine("odjety " + odjety);

            Ulamek podzielony = polowa / cwierc;
            Console.WriteLine("podzielony " + podzielony);


                


            if (polowa > cwierc)
            {
                Console.WriteLine("tak jest wieksze");
            }
            else
            {
                Console.WriteLine("nie jest wieksze");
            }

            // Test 1: Dwa identyczne ułamki
            Ulamek u1 = new Ulamek(1, 2);
            Ulamek u2 = new Ulamek(1, 2);
            Console.WriteLine($"Test 1: {u1} == {u2} ? {u1 == u2}");  // Powinno być true

            // Test 2: Dwa różne ułamki
            Ulamek u3 = new Ulamek(1, 3);
            Console.WriteLine($"Test 2: {u1} == {u3} ? {u1 == u3}");  // Powinno być false
            Console.WriteLine($"Test 3: {u1} != {u3} ? {u1 != u3}");  // Powinno być true

            double inDoublePolowa = (double)polowa;
            Console.WriteLine("polowa in double " + inDoublePolowa);

            Ulamek[] tablica = { new Ulamek(1, 5), new Ulamek(4, 5), new Ulamek(3, 5), new Ulamek(2, 5), new Ulamek(2,8) };

            Console.WriteLine("tablica przed sortowaniem");
            foreach (Ulamek temp in tablica)
            {
                Console.WriteLine(temp);
            }

            //posortujemy
            Array.Sort(tablica);

            Console.WriteLine("tablica po sortowaniem");
            foreach (Ulamek temp in tablica)
            {
                Console.WriteLine(temp);
            }
            Console.ReadLine();
        }
    }
}