using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gr1Lab1
{
    public abstract class Pojazd
    {
        public string Marka { get; set; }
        public string TypSilnika { get; set; } // np. benzyna, diesel, elektryczny
        public int MaksymalnaPrędkość { get; set; }
        public double Spalanie { get; set; } // litry na 100 km
        public double Ciezar { get; set; } // w kg
        public decimal Cena { get; set; } // Cena pojazdu

        public abstract void WyswietlInformacje();
        public virtual decimal KosztPrzejazduNa100km(decimal cenaPaliwa)
        {
            return (decimal)Spalanie * cenaPaliwa;
        }
    }

    public class SamochodOsobowy : Pojazd
    {
        public int IloscDrzwi { get; set; }
        public bool Szyberdach { get; set; }
        public bool CzyMozeCiagnacPrzyczepke { get; set; }

        public override void WyswietlInformacje()
        {
            Console.WriteLine($"Samochód osobowy: {Marka}, Typ silnika: {TypSilnika}, Maks. prędkość: {MaksymalnaPrędkość} km/h, Spalanie: {Spalanie} l/100km, Ciężar: {Ciezar} kg, Cena: {Cena}, Drzwi: {IloscDrzwi}, Szyberdach: {Szyberdach}, Przyczepka: {CzyMozeCiagnacPrzyczepke}");
        }
    }

    public class Motocykl : Pojazd
    {
        public bool MaKufry { get; set; } // Czy motocykl jest wyposażony w kufry

        public override void WyswietlInformacje()
        {
            Console.WriteLine($"Motocykl: {Marka}, Typ silnika: {TypSilnika}, Maks. prędkość: {MaksymalnaPrędkość} km/h, Spalanie: {Spalanie} l/100km, Ciężar: {Ciezar} kg, Cena: {Cena}, Kufry: {MaKufry}");
        }
    }

    public class Ciezarowka : Pojazd
    {
        public double Ladownosc { get; set; } // Maksymalna ładowność

        public override void WyswietlInformacje()
        {
            Console.WriteLine($"Ciężarówka: {Marka}, Typ silnika: {TypSilnika}, Maks. prędkość: {MaksymalnaPrędkość} km/h, Spalanie: {Spalanie} l/100km, Ciężar: {Ciezar} kg, Cena: {Cena}, Ładowność: {Ladownosc}");
        }
    }
}