using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace Gr1Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            SamochodOsobowy myCar = new SamochodOsobowy
            {
                Marka = "BMW",
                TypSilnika = "Benzyna",
                MaksymalnaPrędkość = 200,
                Spalanie = 14,
                Ciezar = 1500,
                IloscDrzwi = 4,
                Szyberdach = true,
                CzyMozeCiagnacPrzyczepke = true
            };

            //wyswietl informacje
            myCar.WyswietlInformacje();

            decimal kosztPrzejazdu = myCar.KosztPrzejazduNa100km((decimal)5.98);

            Console.WriteLine($"Koszt przejazdu: {kosztPrzejazdu} zł");
        }
    }
}