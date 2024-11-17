using System;
using System.Collections.Generic; // Dodano dla List<T>
using System.Linq; // Dodano dla Take() i TakeLast()

namespace Zadania_petle
{
    class Program
    {
        static void Main()
        {
            // Wczytywanie i parsowanie danych wejściowych
            string wejscie = Console.ReadLine();
            short[] dane = Array.ConvertAll<string, short>(wejscie.Split(" "), short.Parse);
            short min = dane[0];
            short max = dane[1];
            short divider = dane[2];
            if(dane[0] > dane[1])
            {
                min = dane[1];
                max = dane[0];
            }

            List<short> allNumbers = new List<short>();
            for(short i = (short)(min+1); i < max; i++)
            {
                allNumbers.Add(i);
            }

            allNumbers.RemoveAll(x => x % divider != 0);
            if(allNumbers.Count <= 0){
                Console.WriteLine("empty");
                return;
            }
            string result;
            if(allNumbers.Count > 10)
            {
                var firstThreeValues = allNumbers.Take(3).ToArray();
                var twoLastValues = allNumbers.TakeLast(2).ToArray();
                result = string.Join(", ", firstThreeValues) + ", ..., " + string.Join(", ", twoLastValues);
            }
            else
            {
                result = string.Join(", ", allNumbers);
            }
            
            Console.WriteLine(result);
        }
    }
}