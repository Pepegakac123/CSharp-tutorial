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
        // Stała epsilon do porównań zmiennoprzecinkowych
        private const double EPSILON = 1e-10;

        static void Main(string[] args)
        {
            try
            {
                // Rozszerzona obsługa wejścia z dodatkową walidacją
                string[] input = Console.ReadLine().Split(" ");
                
                // Sprawdzenie poprawności liczby argumentów
                if (input.Length != 2)
                {
                    throw new ArgumentException("ujemny argument");
                }

                decimal R = decimal.Parse(input[0]);
                decimal L = decimal.Parse(input[1]);

                // Rozszerzone walidacje
                ValidateInput(R, L);

                decimal V;
                decimal volume = (decimal)ObliczObjetoscStozka(R, L, out V);

                // Zaawansowane szacowanie wartości
                int[] szacowaneWartosci = SzacowanieStozka(volume);

                Console.WriteLine($"{szacowaneWartosci[0]} {szacowaneWartosci[1]}");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("ujemny argument");
            }
            catch (OverflowException)
            {
                Console.WriteLine("obiekt nie istnieje");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // Walidacja danych wejściowych
        static void ValidateInput(decimal R, decimal L)
        {
            // Sprawdzenie wartości ujemnych
            if (R < 0 || L < 0)
            {
                throw new ArgumentException("ujemny argument");
            }

            // Sprawdzenie warunku istnienia stożka
            if (L < R)
            {
                throw new ArgumentException("obiekt nie istnieje");
            }

            // Zabezpieczenie przed bardzo małymi wartościami
            if (R < (decimal)EPSILON || L < (decimal)EPSILON)
            {
                throw new ArgumentException("obiekt nie istnieje");
            }
        }

        // Ulepszona metoda obliczania objętości stożka
        static double ObliczObjetoscStozka(decimal R, decimal L, out decimal V)
        {
            // Obliczenie wysokości z zabezpieczeniami
            decimal H = (decimal)Math.Sqrt((double)(L * L - R * R));

            // Zabezpieczenie przed błędami numerycznymi
            if (H<0 || (H == 0))
            {
                throw new ArithmeticException("ujemny argument");
            }

            // Dokładne obliczenie objętości z użyciem decimal
            V = (1.0m / 3.0m) * (decimal)Math.PI * R * R * H;

            return (double)V;
        }

        // Ulepszone szacowanie wartości
        static int[] SzacowanieStozka(decimal V)
        {
            // Dodatkowe sprawdzenia
            if (V < (decimal)EPSILON)
            {
                return new int[] { 0, 0 };
            }

            // Dokładniejsze zaokrąglanie z małą tolerancją
            int lowerLimit = (int)Math.Floor((double)V + EPSILON);
            int upperLimit = (int)Math.Ceiling((double)V - EPSILON);

            // Dodatkowe zabezpieczenie przed przepełnieniem
            if (lowerLimit < 0 || upperLimit < 0)
            {
                throw new OverflowException("ujemny argument");
            }

            return new int[] { lowerLimit, upperLimit };
        }
    }
}