using System;

namespace objetosc_stozka
{
    class Program{
       
        static void Main(string[] args){
             try{
                   string[] input = Console.ReadLine().Split(" ");
        double R = double.Parse(input[0]);
        double L = double.Parse(input[1]);
        if(L<R){throw new ArgumentException("obiekt nie istnieje");}
        if(R < 0 || L < 0){throw new ArgumentException("ujemny argument");}
        double V;
        int[] szacowaneWartosci = SzacowanieStozka(ObjetoscStozka(R, L, out V));
        if(szacowaneWartosci[0] < 0 || szacowaneWartosci[1] < 0){throw new OverflowException("ujemny argument");}
        Console.WriteLine($"{szacowaneWartosci[0]} {szacowaneWartosci[1]}");
            }catch(ArgumentException e){
                Console.WriteLine(e.Message);
            }catch(OverflowException e){
                Console.WriteLine(e.Message);
            }
        
        }


        static double ObjetoscStozka(double R, double L, out double V){
            double H = Math.Sqrt(Math.Pow(L, 2) - Math.Pow(R, 2));
            V = (1.0 / 3.0) * Math.PI * Math.Pow(R, 2) * H;
            return V;
        }

        static int[] SzacowanieStozka(double V){
            int lowerLimit = (int)Math.Floor(V);
            int upperLimit = (int)Math.Ceiling(V);
            if (V < 1e-6) // Bardzo mała objętość
            {
                lowerLimit = 0;
                upperLimit = 0;
            }
            return new int[] {lowerLimit, upperLimit};

        } 
    }
}