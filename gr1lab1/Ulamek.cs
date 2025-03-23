// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Net.Http.Headers;
// using System.Text;
// using System.Threading.Tasks;

// namespace Gr1Lab1
// {
//     class Program
//     {

//         static void Main(string[] args)
//         {
//             Ulamek polowa = new Ulamek(1, 2);
//             Ulamek cwierc = new Ulamek(1, 4);

//             Console.WriteLine($"Test 1: {polowa} * {cwierc} = {polowa * cwierc}");
//             Console.WriteLine($"Test 2: {polowa} + {cwierc} = {polowa + cwierc}");
//             Console.WriteLine($"Test 3: {polowa} - {cwierc} = {polowa - cwierc}");
//             Console.WriteLine($"Test 4: {polowa} / {cwierc} = {polowa / cwierc}");

//             Console.WriteLine($"Test 5: {polowa} > {cwierc} ? {polowa > cwierc}");  // Powinno być true
//             Console.WriteLine($"Test 6: {polowa} < {cwierc} ? {polowa < cwierc}");  // Powinno być false
//             Console.WriteLine($"Test 7: {polowa} >= {cwierc} ? {polowa >= cwierc}"); // Powinno być true
//             Console.WriteLine($"Test 8: {polowa} <= {cwierc} ? {polowa <= cwierc}"); // Powinno być false

//             Console.WriteLine($"Test 9: (double){polowa} = {(double)polowa}"); // Rzutowanie na double

//             // Test tablicy przed i po sortowaniu
//             Ulamek[] tablica = { new Ulamek(1, 5), new Ulamek(4, 5), new Ulamek(3, 5), new Ulamek(2, 5), new Ulamek(2, 8) };

//             Console.WriteLine("Test 10: Tablica przed sortowaniem:");
//             Console.WriteLine(string.Join(", ", tablica.Select(u => u.ToString())));

//             Array.Sort(tablica);

//             Console.WriteLine("Test 11: Tablica po sortowaniu:");
//             Console.WriteLine(string.Join(", ", tablica.Select(u => u.ToString())));
//         }
//         public static void WypiszTablice(Ulamek[] tablica)
//         {
//             Console.WriteLine(string.Join(", ", tablica.Select(u => u.ToString())));
//         }
//     }


//     class Ulamek : IComparable<Ulamek>, IEquatable<Ulamek>
//     {
//         public int licznik;
//         public int mianownik;

//         public Ulamek(int inLicznik, int inMianownik)
//         {
//             if (inMianownik == 0)
//             {
//                 throw new ArgumentException("nie moze byc zero");
//             }

//             licznik = inLicznik;
//             mianownik = inMianownik;

//             if (mianownik < 0)
//             {
//                 licznik = -licznik;
//                 mianownik = -mianownik;
//             }
//             Uproscic();
//         }

//         public static int NajwiekszyWspolnyDzielnik(int a, int b) //Znajdujemy nwd potrzebne do upraszczania
//         {
//             int i = 0;
//             while (b != 0)
//             {
//                 ++i;
//                 int temp = b;
//                 b = a % b;
//                 a = temp;
//             }
//             return a;
//         }


//         private void Uproscic()
//         {

//             int nwd = NajwiekszyWspolnyDzielnik(Math.Abs(licznik), Math.Abs(mianownik));

//             if (nwd > 1)
//             {
//                 licznik /= nwd;
//                 mianownik /= nwd;
//             }
//         }


//         public int CompareTo(Ulamek? other) // Implementacja metody IComparable
//         {
//             if (other == null) return 1;

//             // Mnożymy krzyżowo aby porównywać ułamki
//             // a/b < c/d jeśli a*d < c*b
//             return (licznik * other.mianownik).CompareTo(other.licznik * mianownik);
//         }

//         public override string ToString() => $"{licznik}/{mianownik}";


//         // Implementacja operatorów
//         public static Ulamek operator *(Ulamek a, Ulamek b)
//         {
//             int licznik = a.licznik * b.licznik;
//             int mianownik = a.mianownik * b.mianownik;
//             return new Ulamek(licznik, mianownik);
//         }

//         public static Ulamek operator +(Ulamek a, Ulamek b)
//         {

//             int nowy_licznik = a.licznik * b.mianownik + b.licznik * a.mianownik;
//             int nowy_mianownik = a.mianownik * b.mianownik;
//             return new Ulamek(nowy_licznik, nowy_mianownik);
//         }

//         public static Ulamek operator -(Ulamek a, Ulamek b)
//         {

//             int nowy_licznik = a.licznik * b.mianownik - b.licznik * a.mianownik;
//             int nowy_mianownik = a.mianownik * b.mianownik;
//             return new Ulamek(nowy_licznik, nowy_mianownik);
//         }

//         public static Ulamek operator /(Ulamek a, Ulamek b)
//         {
//             int licznik = a.licznik * b.mianownik;
//             int mianownik = a.mianownik * b.licznik;

//             if (mianownik == 0)
//                 throw new DivideByZeroException("Nie można dzielić przez zero");

//             return new Ulamek(licznik, mianownik);
//         }

//         public static bool operator >(Ulamek a, Ulamek b) => a.licznik * b.mianownik > b.licznik * a.mianownik;
//         public static bool operator >=(Ulamek a, Ulamek b) => a.licznik * b.mianownik >= b.licznik * a.mianownik;
//         public static bool operator <(Ulamek a, Ulamek b) => a.licznik * b.mianownik < b.licznik * a.mianownik;
//         public static bool operator <=(Ulamek a, Ulamek b) => a.licznik * b.mianownik <= b.licznik * a.mianownik;


//         public static bool operator ==(Ulamek? a, Ulamek? b)
//         {
//             if (a is null && b is null) return true;
//             if (a is null || b is null) return false;

//             return a.licznik == b.licznik && a.mianownik == b.mianownik;
//         }

//         public static bool operator !=(Ulamek? a, Ulamek? b) => !(a == b);

//         public static explicit operator double(Ulamek a) => (double)a.licznik / (double)a.mianownik;

//         // Jeśli klasa przeciąża operatory == i !=, należy również nadpisać Equals() oraz GetHashCode()
//         // Implementacja interfejsu IEquatable
//         public bool Equals(Ulamek? other)
//         {
//             if (other is null) return false;
//             return licznik == other.licznik && mianownik == other.mianownik;
//         }

//         public override bool Equals(object? obj)
//         {
//             if (obj is Ulamek other) return Equals(other);
//             return false;
//         }

//         public override int GetHashCode()
//         {
//             return HashCode.Combine(licznik, mianownik);
//         }


//     }
// }