using System;
using System.Collections.Generic;
namespace RabatyLiniLotniczej
{

    public static class FlightTicketSystem
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Witamy w systemie rabatów lini lotniczych!");
            Console.WriteLine("------------------------------------------\n");
            var (dateOfBirth, dateOfFlight, isDomesticFlight, isRegularCustomer) = GetUserInput();
            PrintTicketInfo(dateOfBirth, dateOfFlight, isDomesticFlight, isRegularCustomer);
        }
        private static int CalculateDiscount(dateOfBirth, dateOfFlight, isDomesticFlight, isRegularCustomer)
        {

            double discount = 0;
            const double maxBabyDiscount = 0.8;
            const double maxRegularDiscount = 0.3;
            bool isFlightInSeason = CheckIsFlightInSeason(dateOfFlight);
            bool isEarlyReservation = CheckEarlyReservation(dateOfFlight);
            // Rabaty wiekowe
            if (dateOfBirth.Year - DateTime.Now.Year < 2)
            {
                isDomesticFlight? discount += 0.8 : discount += 0.7;
                isEarlyReservation? discount += 0.1 : discount += 0;
                if (discount > maxBabyDiscount) return maxBabyDiscount;

                return discount;
            }
            else if (isFlightInSeason)
            {

            }
            /*
        SYSTEM RABATÓW LINII LOTNICZEJ

        1. RABATY WIEKOWE:
        - Niemowlęta (< 2 lat):
            * 80% - loty krajowe
            * 70% - loty międzynarodowe
        - Młodzież (2-16 lat):
            * 10% - wszystkie loty

        2. RABATY DODATKOWE:
        - Wczesna rezerwacja (5 miesięcy przed): 10%
        - Poza sezonem (tylko loty międzynarodowe): 15%
        - Stały klient (tylko 18+ lat): 15%

        3. OGRANICZENIA:
        - Loty międzynarodowe: rabaty tylko dla niemowląt lub lotów poza sezonem
        - Max łączny rabat:
            * 80% - niemowlęta
            * 30% - pozostali pasażerowie

        4. SEZONY (okresy bez zniżek):
        - 20.12 - 10.01
        - 20.03 - 10.04
        - 01.07 - 31.08
        */
        }
        private static bool CheckIsFlightInSeason(DateOnly dateOfFlight)
        {
            // Okres świąteczno-noworoczny (20.12 - 10.01)
            if ((dataLotu.Month == 12 && dataLotu.Day >= 20) ||
                (dataLotu.Month == 1 && dataLotu.Day <= 10))
                return true;

            // Okres wiosenny (20.03 - 10.04)
            if ((dataLotu.Month == 3 && dataLotu.Day >= 20) ||
                (dataLotu.Month == 4 && dataLotu.Day <= 10))
                return true;

            // Okres wakacyjny (lipiec i sierpień)
            if (dataLotu.Month == 7 || dataLotu.Month == 8)
                return true;

            return false;
        }
        private static bool CheckEarlyReservation(DateOnly dateOfFlight)
        {
            // Obliczamy różnicę w miesiącach
            int MonthDiff = ((dateOfFlight.Year - DateOnly.Now.Year) * 12) + (dateOfFlight.Month - DateOnly.Now.Month);
            return MonthDiff >= 5;
        }
        private static (DateOnly dateOfBirth, DateOnly dateOfFlight, bool isDomesticFlight, bool isRegularCustomer) GetUserInput()
        {
            DateOnly? dateOfBirth = null;
            DateOnly? dateOfFlight = null;
            bool success = false;
            bool isDomesticFlight = false;
            bool isRegularCustomer = false;

            while (!success)
            {
                try
                {
                    Console.WriteLine("Podaj swoją date urodzenia w formacie RRRR.MM.DD: ");
                    dateOfBirth = ValidateAndParseData(Console.ReadLine().Split("."), "urodzenia");

                    Console.WriteLine("Podaj date lotu w formacie RRRR.MM.DD: ");
                    dateOfFlight = ValidateAndParseData(Console.ReadLine().Split("."), "lotu", true);

                    Console.WriteLine("Czy lot jest krajowy (T/N)? ");
                    isDomesticFlight = ValidateAndParseYesNo(Console.ReadLine(), "czy lot krajowy?");

                    Console.WriteLine("Czy jesteś stałym klientem (T/N)? ");
                    isRegularCustomer = ValidateAndParseYesNo(Console.ReadLine(), "czy stały klient?");
                    if (isRegularCustomer)
                        validateIsRegularCustomerCorrect(dateOfBirth);

                    success = true;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Błąd formatu: {ex.Message}");
                }
                catch (ArgumentOutOfRangeException) // Jeżeli data jest niezgodna z realnym formatem
                {
                    Console.WriteLine("Błąd: Podana data zawiera nieprawidłowe wartości (miesiąc 1-12, dzień 1-31)");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Błąd danych: {ex.Message}");
                }
                catch (Exception)
                {
                    Console.WriteLine("Wystąpił nieoczekiwany błąd. Spróbuj ponownie.");
                }
            }
            return (dateOfBirth.Value, dateOfFlight.Value, isDomesticFlight, isRegularCustomer);
        }
        private static DateOnly ValidateAndParseData(string[] dateInfo, string dateType, bool isFlightDate = false)
        {
            // Sprawdzenie czy format daty jest poprawny
            if (dateInfo.Length != 3)
                throw new FormatException($"Podana data {dateType} jest niepoprawna. Podaj datę w formacie RRRR.MM.DD");

            // Sprawdzenie czy podane dane są poprawne i da się przekonwertować je na liczby
            if (!int.TryParse(dateInfo[0], out int year) ||
                !int.TryParse(dateInfo[1], out int month) ||
                !int.TryParse(dateInfo[2], out int day))
                throw new FormatException("Data urodzenia zawiera nieprawidłowe liczby");

            DateOnly date = new DateOnly(year, month, day);

            // Jeżeli jest to data lotu, sprawdzamy czy jest wcześniej niż dzisiejsza
            if (isFlightDate && date < DateOnly.FromDateTime(DateTime.Now))
            {
                throw new ArgumentException("Data lotu nie może być wcześniejsza niż dzisiejsza");
            }
            // Jeżeli jest to data urodzenia, sprawdzamy czy ktoś nie urodził się w przyszłości
            else if (!isFlightDate && (date > DateOnly.FromDateTime(DateTime.Now)) || date.Year < 1945)
            {
                throw new ArgumentException("Data urodzenia nie może być wcześniejsza niż dzisiejsza");
            }
            return date;
        }

        private static bool ValidateAndParseYesNo(string input, string questionType)
        {
            string trimmedInput = input.Trim().ToUpper();
            if (trimmedInput != "T" && trimmedInput != "N")
                throw new ArgumentException($"Odpowiedź dla '{questionType}' musi być T lub N");

            return trimmedInput == "T" ? true : false;
        }

        private static void validateIsRegularCustomerCorrect(DateOnly dateOfBirth)
        {
            int YearsDiff = OnlyDate.Now.Year - dateOfBirth.Year;
            if (YearsDiff < 18) throw new ArgumentException($"Aby być stałym klientem musisz mieć ukończone 18 lat"); ;
        }

        private static void PrintTicketInfo(DateOnly dateOfBirth, DateOnly dateOfFlight, bool isDomesticFlight, bool isRegularCustomer)
        {
            Console.WriteLine("\n=== Do obliczeń przyjęto ===");
            Console.WriteLine($"Data urodzenia: {dateOfBirth}");
            Console.WriteLine($"Data lotu: {FormatDate(dateOfFlight)}");
            Console.WriteLine($"{(isDomesticFlight ? "Lot krajowy" : "Lot międzynarodowy")}");
            Console.WriteLine($"Stały klient: {(isRegularCustomer ? "Tak" : "Nie")}");
            Console.WriteLine("=========================\n");
        }

        private static string FormatDate(DateOnly date)
        {
            // Zamienia fornat na np:  wtorek, 4 lipca 2023
            return date.ToString("dddd, d MMMM yyyy", new System.Globalization.CultureInfo("pl-PL"));
        }
    }
    /*
SYSTEM RABATÓW LINII LOTNICZEJ

1. RABATY WIEKOWE:
- Niemowlęta (< 2 lat):
    * 80% - loty krajowe
    * 70% - loty międzynarodowe
- Młodzież (2-16 lat):
    * 10% - wszystkie loty

2. RABATY DODATKOWE:
- Wczesna rezerwacja (5 miesięcy przed): 10%
- Poza sezonem (tylko loty międzynarodowe): 15%
- Stały klient (tylko 18+ lat): 15%

3. OGRANICZENIA:
- Loty międzynarodowe: rabaty tylko dla niemowląt lub lotów poza sezonem
- Max łączny rabat:
    * 80% - niemowlęta
    * 30% - pozostali pasażerowie

4. SEZONY (okresy bez zniżek):
- 20.12 - 10.01
- 20.03 - 10.04
- 01.07 - 31.08
*/

    //     Podaj swoją datę urodzenia w formacie RRRR-MM-DD: 2011-01-01
    // Podaj datę lotu w formacie RRRR-MM-DD: 2023-07-04
    // Czy lot jest krajowy (T/N)? t
    // Czy jesteś stałym klientem (T/N)? t

    // === Do obliczeń przyjęto:
    //  * Data urodzenia: 01.01.2011
    //  * Data lotu: wtorek, 4 lipca 2023. Lot w sezonie
    //  * Lot krajowy
    //  * Stały klient: Tak
}
