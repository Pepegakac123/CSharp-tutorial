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
            //     SamochodOsobowy myCar = new SamochodOsobowy
            //     {
            //         Marka = "BMW",
            //         TypSilnika = "Benzyna",
            //         MaksymalnaPrędkość = 200,
            //         Spalanie = 14,
            //         Ciezar = 1500,
            //         IloscDrzwi = 4,
            //         Szyberdach = true,
            //         CzyMozeCiagnacPrzyczepke = true
            //     };

            //     //wyswietl informacje
            //     myCar.WyswietlInformacje();

            //     decimal kosztPrzejazdu = myCar.KosztPrzejazduNa100km((decimal)5.98);

            //     Console.WriteLine($"Koszt przejazdu: {kosztPrzejazdu} zł");
            Pracownik biurowy = new PracownikBiurowy
            {
                Imie = "Jan",
                Nazwisko = "Kowalski",
                Stanowisko = "Księgowy",
                Wynagrodzenie = 4000m,
                IloscGodzinPracy = 170
            };

            Pracownik menedzer = new Menedzer
            {
                Imie = "Anna",
                Nazwisko = "Nowak",
                Stanowisko = "Menedżer Projektu",
                Wynagrodzenie = 8000m,
                BonusRoczny = 10000m
            };

            biurowy.PokazInformacje();
            Console.WriteLine($"Roczne wynagrodzenie: {biurowy.ObliczRoczneWynagrodzenie()} Miesieczne Wynagrodzenie {biurowy.ObliczMiesieczneWynagrodzenie()}");

            menedzer.PokazInformacje();
            Console.WriteLine($"Roczne wynagrodzenie: {menedzer.ObliczRoczneWynagrodzenie()}, Miesieczne Wynagrodzenie {menedzer.ObliczMiesieczneWynagrodzenie()}");


        }
    }
}