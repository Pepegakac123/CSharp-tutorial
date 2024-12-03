// using System;

// namespace objetosc_stozka
// {
//     class Program{
       
//         static void Main(string[] args){
//              try{
//                    string[] input = Console.ReadLine().Split(" ");
//         double R = double.Parse(input[0]);
//         double L = double.Parse(input[1]);
//         if(L<R){throw new ArgumentException("obiekt nie istnieje");}
//         if(R < 0 || L < 0){throw new ArgumentException("ujemny argument");}
//         double V;
//         int[] szacowaneWartosci = SzacowanieStozka(ObjetoscStozka(R, L, out V));
//         if(szacowaneWartosci[0] < 0 || szacowaneWartosci[1] < 0){throw new OverflowException("ujemny argument");}
//         Console.WriteLine($"{szacowaneWartosci[0]} {szacowaneWartosci[1]}");
//             }catch(ArgumentException e){
//                 Console.WriteLine(e.Message);
//             }catch(OverflowException e){
//                 Console.WriteLine(e.Message);
//             }
        
//         }


//         static double ObjetoscStozka(double R, double L, out double V){
//             double H = Math.Sqrt(Math.Pow(L, 2) - Math.Pow(R, 2));
//             V = (1.0 / 3.0) * Math.PI * Math.Pow(R, 2) * H;
//             return V;
//         }

//         static int[] SzacowanieStozka(double V){
//             int lowerLimit = (int)Math.Floor(V);
//             int upperLimit = (int)Math.Ceiling(V);
//             if (V < 1e-6) // Bardzo mała objętość
//             {
//                 lowerLimit = 0;
//                 upperLimit = 0;
//             }
//             return new int[] {lowerLimit, upperLimit};

//         } 
//     }
// }

using System;

namespace objetosc_stozka
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                checked
                {
                    // Wczytanie danych wejściowych
                    string[] input = Console.ReadLine().Split(" ");

                    if (input.Length != 2)
                    {
                        throw new Exception();
                    }

                    // Parsowanie danych wejściowych
                    if (!long.TryParse(input[0], out long R) || !long.TryParse(input[1], out long L))
                    {
                        throw new Exception();
                    }

                    // Sprawdzenie warunku na argumenty ujemne
                    if (R < 0 || L < 0)
                    {
                        Console.WriteLine("ujemny argument");
                        return;
                    }

                    // Sprawdzenie, czy możliwe jest skonstruowanie stożka
                    if (L < R)
                    {
                        Console.WriteLine("obiekt nie istnieje");
                        return;
                    }

                    // Obliczanie objętości stożka
                    decimal volume = CalculateConeVolume(R, L);

                    // Szacowanie dolnej i górnej granicy objętości
                    long lowerBound = (long)Math.Floor(volume);
                    long upperBound = (long)Math.Ceiling(volume);

                    // Wypisanie wyniku
                    Console.WriteLine($"{lowerBound} {upperBound}");
                }
            }
            catch
            {
                Console.WriteLine("ujemny argument");
            }
        }

        /// <summary>
        /// Funkcja obliczająca objętość stożka.
        /// </summary>
        static decimal CalculateConeVolume(long R, long L)
        {
            checked
            {
                // Rzutowanie wartości na decimal
                decimal decimalR = (decimal)R;
                decimal decimalL = (decimal)L;

                // Obliczanie wysokości stożka
                decimal height = (decimal)Math.Sqrt((double)(decimalL * decimalL - decimalR * decimalR));

                // Obliczanie objętości stożka
                return (1.0m / 3.0m) * (decimal)Math.PI * decimalR * decimalR * height;
            }
        }
    }
}
