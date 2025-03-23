using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gr1Lab1
{
    public abstract class Pracownik
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Stanowisko { get; set; }
        public decimal Wynagrodzenie { get; set; }

        public abstract void PokazInformacje();

        public virtual decimal ObliczDzienneWynagrodzenie(decimal IloscGodzinPracy)
        {
            return ((Wynagrodzenie / 21) / 8) * IloscGodzinPracy;
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

        // Przykład nadpisania metody
        public override decimal ObliczRoczneWynagrodzenie()
        {
            // Dodajemy bonus roczny za godziny nadliczbowe
            decimal bonus = (IloscGodzinPracy > 160 ? (IloscGodzinPracy - 160) * (Wynagrodzenie / 160) : 0);
            return base.ObliczRoczneWynagrodzenie() + bonus;
        }

        public override decimal ObliczMiesieczneWynagrodzenie()
        {

            decimal podstawoweWynagrodzenie = Wynagrodzenie;


            if (IloscGodzinPracy > 160)
            {
                decimal stawkaGodzinowa = Wynagrodzenie / 160;
                decimal wynagrodzenieDodatkowe = (IloscGodzinPracy - 160) * stawkaGodzinowa * 1.5m;
                return podstawoweWynagrodzenie + wynagrodzenieDodatkowe;
            }

            return podstawoweWynagrodzenie;
        }
    }
    public class Menedzer : Pracownik
    {
        public decimal BonusRoczny { get; set; }

        public override void PokazInformacje()
        {
            Console.WriteLine($"Menedżer: {Imie} {Nazwisko}, stanowisko: {Stanowisko}, wynagrodzenie miesięczne: {Wynagrodzenie}, bonus roczny: {BonusRoczny}");
        }

        public override decimal ObliczRoczneWynagrodzenie()
        {
            return base.ObliczRoczneWynagrodzenie() + BonusRoczny;
        }
        public override decimal ObliczMiesieczneWynagrodzenie()
        {
            return Wynagrodzenie;
        }

        public override decimal ObliczDzienneWynagrodzenie(decimal IloscGodzinPracy)
        {
            // Stawka dzienna dla menedżera może być wyższa ze względu na odpowiedzialność
            decimal podstawowaStawkaDzienna = base.ObliczDzienneWynagrodzenie(IloscGodzinPracy);
            decimal dodatekZaOdpowiedzialnosc = podstawowaStawkaDzienna;

            return podstawowaStawkaDzienna + dodatekZaOdpowiedzialnosc;
        }

    }

    public class PracownikZdalny : Pracownik
    {
        public int IloscDniZdalnych { get; set; }

        public override void PokazInformacje()
        {
            Console.WriteLine($"Pracownik zdalny: {Imie} {Nazwisko}, stanowisko: {Stanowisko}, wynagrodzenie miesięczne: {Wynagrodzenie}, ilość dni zdalnych: {IloscDniZdalnych}");
        }
        public override decimal ObliczMiesieczneWynagrodzenie()
        {
            return Wynagrodzenie;
        }

        public override decimal ObliczDzienneWynagrodzenie(decimal IloscGodzinPracy)
        {
            decimal podstawowaStawkaDzienna = base.ObliczDzienneWynagrodzenie(IloscGodzinPracy);

            return podstawowaStawkaDzienna;
        }

        // Pracownik zdalny może mieć inne kryteria dla bonusów itp., dlatego możemy tu dodać dodatkowe metody lub nadpisać istniejące.
    }
}