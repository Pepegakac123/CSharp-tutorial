using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram2
{
    public abstract class Pracownik
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Stanowisko { get; set; }
        public decimal Wynagrodzenie { get; set; }

        public abstract void PokazInformacje();
        public virtual decimal ObliczDzienneWynagrodzenie()
        {
            return Wynagrodzenie / 22;
        }

        public virtual decimal ObliczMiesieczneWynagrodzenie()
        {
            return Wynagrodzenie;
        }
        public virtual decimal ObliczRoczneWynagrodzenie()
        {
            return Wynagrodzenie * 12;
        }
    }

    public class PracownikBiurowy : Pracownik
    {
        public int IloscGodzinPracy { get; set; }

        public override void PokazInformacje()
        {
            Console.WriteLine($"Pracownik biurowy: {Imie} {Nazwisko}, stanowisko: {Stanowisko}, wynagrodzenie miesięczne: {Wynagrodzenie}, ilość godzin pracy: {IloscGodzinPracy}");
        }

        public override decimal ObliczDzienneWynagrodzenie()
        {
            decimal stawkaGodzinowa = Wynagrodzenie / IloscGodzinPracy;
            return stawkaGodzinowa * 8;
        }

        public override decimal ObliczMiesieczneWynagrodzenie()
        {
            decimal wynagrodzeniePodstawowe = Wynagrodzenie;
            decimal bonusZaNadgodziny = (IloscGodzinPracy > 160) ? (IloscGodzinPracy - 160) * (Wynagrodzenie / 160) : 0;
            return wynagrodzeniePodstawowe + bonusZaNadgodziny;
        }

        public override decimal ObliczRoczneWynagrodzenie()
        {
            return ObliczMiesieczneWynagrodzenie() * 12;
        }
    }
    public class Menedzer : Pracownik
    {
        public decimal BonusRoczny { get; set; }

        public override void PokazInformacje()
        {
            Console.WriteLine($"Menedżer: {Imie} {Nazwisko}, stanowisko: {Stanowisko}, wynagrodzenie miesięczne: {Wynagrodzenie}, bonus roczny: {BonusRoczny}");
        }

        public override decimal ObliczDzienneWynagrodzenie()
        {
            return Wynagrodzenie / 22;
        }

        public override decimal ObliczMiesieczneWynagrodzenie()
        {
            return Wynagrodzenie;
        }

        // Już istniejąca, ale zaktualizowana metoda obliczania rocznego wynagrodzenia
        public override decimal ObliczRoczneWynagrodzenie()
        {
            return ObliczMiesieczneWynagrodzenie() * 12 + BonusRoczny;
        }
    }

    public class PracownikZdalny : Pracownik
    {
        public int IloscDniZdalnych { get; set; }
        public bool Terminowosc { get; set; }

        public override void PokazInformacje()
        {
            Console.WriteLine($"Pracownik zdalny: {Imie} {Nazwisko}, stanowisko: {Stanowisko}, wynagrodzenie miesięczne: {Wynagrodzenie}, ilość dni zdalnych: {IloscDniZdalnych}");
        }

        public override decimal ObliczDzienneWynagrodzenie()
        {
            decimal podstawowaDniowka = Wynagrodzenie / IloscDniZdalnych;

            return Terminowosc ? podstawowaDniowka * 1.1m : podstawowaDniowka; //bonus 10%
        }

        public override decimal ObliczMiesieczneWynagrodzenie()
        {
            decimal podstawoweWynagrodzenie = Wynagrodzenie;

            return Terminowosc ? podstawoweWynagrodzenie * 1.15m : podstawoweWynagrodzenie; //bonus 15%
        }

        public override decimal ObliczRoczneWynagrodzenie()
        {
            decimal podstawoweRoczne = ObliczMiesieczneWynagrodzenie() * 12;
            return Terminowosc ? podstawoweRoczne + Wynagrodzenie : podstawoweRoczne;
        }
    }
}
