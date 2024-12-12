using System;
using System.Collections.Generic;
namespace RabatyLiniLotniczej
{

    public static class FlightTicketSystem
    {
        public static void Main(string[] args)
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
                    string[] dateOfBirthInfo = Console.ReadLine().Split(".");
                    // Sprawdzenie czy format daty urodzenia jest poprawny
                    if (dateOfBirthInfo.Length != 3)
                        throw new FormatException("Podana data urodzenia jest niepoprawna. Podaj datę w formacie RRRR.MM.DD np: 2004.12.22");

                    // Sprawdzenie czy podane dane są poprawne i da się przekonwertować je na liczby
                    if (!int.TryParse(dateOfBirthInfo[0], out int yearBirth) ||
                        !int.TryParse(dateOfBirthInfo[1], out int monthBirth) ||
                        !int.TryParse(dateOfBirthInfo[2], out int dayBirth))
                        throw new FormatException("Data urodzenia zawiera nieprawidłowe liczby");

                    dateOfBirth = new DateOnly(yearBirth, monthBirth, dayBirth);

                    Console.WriteLine("Podaj date lotu w formacie RRRR.MM.DD: ");
                    string[] dateOfFlightInfo = Console.ReadLine().Split(".");
                    // Sprawdzenie czy format daty lotu jest poprawny
                    if (dateOfFlightInfo.Length != 3)
                        throw new FormatException("Podana data lotu jest niepoprawna. Podaj datę w formacie RRRR.MM.DD np: 2024.12.22");

                    // Sprawdzenie czy podane dane są poprawne i da się przekonwertować je na liczby
                    if (!int.TryParse(dateOfFlightInfo[0], out int yearFlight) ||
                        !int.TryParse(dateOfFlightInfo[1], out int monthFlight) ||
                        !int.TryParse(dateOfFlightInfo[2], out int dayFlight))
                        throw new FormatException("Data lotu zawiera nieprawidłowe liczby");

                    dateOfFlight = new DateOnly(yearFlight, monthFlight, dayFlight);

                    // Sprawdzamy czy data lotu nie jest wcześniejsza niż dzisiejsza
                    if (dateOfFlight < DateOnly.FromDateTime(DateTime.Now))
                        throw new ArgumentException("Data lotu nie może być wcześniejsza niż dzisiejsza");

                    // Sprawdzamy czy data urodzenia nie jest późniejsza niż dzisiejsza
                    if (dateOfBirth > DateOnly.FromDateTime(DateTime.Now))
                        throw new ArgumentException("Data urodzenia nie może być późniejsza niż dzisiejsza");

                    Console.WriteLine("Czy lot jest krajowy (T/N)? ");
                    string domesticInput = Console.ReadLine().Trim().ToUpper();
                    if (domesticInput != "T" && domesticInput != "N")
                        throw new ArgumentException("Odpowiedź musi być T lub N");
                    isDomesticFlight = domesticInput == "T";

                    Console.WriteLine("Czy jesteś stałym klientem (T/N)? ");
                    string regularInput = Console.ReadLine().Trim().ToUpper();
                    if (regularInput != "T" && regularInput != "N")
                        throw new ArgumentException("Odpowiedź musi być T lub N");
                    isRegularCustomer = regularInput == "T";

                    success = true;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Błąd formatu: {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Błąd danych: {ex.Message}");
                }
                catch (Exception)
                {
                    Console.WriteLine("Wystąpił nieoczekiwany błąd. Spróbuj ponownie.");
                }
            }

            PrintTicketInfo(dateOfBirth.Value, dateOfFlight.Value, isDomesticFlight, isRegularCustomer);
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
