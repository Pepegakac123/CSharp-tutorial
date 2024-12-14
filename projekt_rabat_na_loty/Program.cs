using System;
using System.Collections.Generic;
namespace RabatyLiniLotniczej
{

    public static class FlightTicketSystem
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Witamy w systemie rabatów lini lotniczych!");
            Console.WriteLine("------------------------------------------");
            var (dateOfBirth, dateOfFlight, isDomesticFlight, isRegularCustomer) = GetUserInput();
            PrintTicketInfo(dateOfBirth, dateOfFlight, isDomesticFlight, isRegularCustomer);
            Console.WriteLine("------------------------------------------");
            PrintDiscountInfo(CalculateDiscount(dateOfBirth, dateOfFlight, isDomesticFlight, isRegularCustomer));
            CalculateDiscount(dateOfBirth, dateOfFlight, isDomesticFlight, isRegularCustomer);
        }

        /// <summary>
        /// Oblicza końcowy rabat na podstawie danych pasażera i lotu.
        /// </summary>
        /// <param name="dateOfBirth">Data urodzenia pasażera</param>
        /// <param name="dateOfFlight">Data lotu</param>
        /// <param name="isDomesticFlight">Czy lot jest krajowy</param>
        /// <param name="isRegularCustomer">Czy pasażer jest stałym klientem</param>
        /// <returns>Wartość rabatu w procentach (0-30 dla regularnych, 0-80 dla niemowląt)</returns>
        private static int CalculateDiscount(DateOnly dateOfBirth, DateOnly dateOfFlight, bool isDomesticFlight, bool isRegularCustomer)
        {
            double discount = 0;
            const double maxBabyDiscount = 0.8;
            const double maxRegularDiscount = 0.3;
            bool isFlightInSeason = CheckIsFlightInSeason(dateOfFlight);
            bool isEarlyReservation = CheckEarlyReservation(dateOfFlight);
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            // czy pasażer jest niemowlęciem
            bool isInfant = dateOfBirth > today.AddYears(-2);
            // czy pasażer jest młodzieżą (2-16 lat)
            bool isYouth = dateOfBirth <= today.AddYears(-2) && dateOfBirth > today.AddYears(-16);

            // Przypadek 1: Niemowlęta
            if (isInfant)
            {
                discount = isDomesticFlight ? 0.8 : 0.7;
                if (isEarlyReservation) discount += 0.1;
                return (int)(Math.Min(discount, maxBabyDiscount) * 100);
            }

            // Przypadek 2: Lot krajowy
            if (isDomesticFlight)
            {
                if (isYouth) discount += 0.1;
                if (isRegularCustomer) discount += 0.15;
                if (isEarlyReservation) discount += 0.1;
            }
            // Przypadek 3: Lot międzynarodowy
            else
            {
                // Tylko jeśli lot jest poza sezonem
                if (!isFlightInSeason)
                {
                    discount += 0.15; // Podstawowy rabat za lot międzynarodowy poza sezonem
                    if (isYouth) discount += 0.1;
                    if (isRegularCustomer) discount += 0.15;
                    if (isEarlyReservation) discount += 0.1;
                }
                // Jeśli lot w sezonie - brak rabatów
            }

            //Zwracanie wartości rabatu w procentach, jeżeli jest ona mniejsza od maksymalnego rabatu, w innym przypadku zwraca sie maksymalny rabat dla danej grupy
            return (int)(Math.Min(discount, maxRegularDiscount) * 100);
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

        /// <summary>
        /// Sprawdza czy data lotu wypada w sezonie (okres świąteczny, wiosenny lub wakacyjny).
        /// </summary>
        /// <param name="dateOfFlight">Data lotu do sprawdzenia</param>
        /// <returns>true jeśli lot jest w sezonie, false w przeciwnym razie</returns>
        private static bool CheckIsFlightInSeason(DateOnly dateOfFlight)
        {
            // Okres świąteczno-noworoczny (20.12 - 10.01)
            if ((dateOfFlight.Month == 12 && dateOfFlight.Day >= 20) ||
                (dateOfFlight.Month == 1 && dateOfFlight.Day <= 10))
                return true;

            // Okres wiosenny (20.03 - 10.04)
            if ((dateOfFlight.Month == 3 && dateOfFlight.Day >= 20) ||
                (dateOfFlight.Month == 4 && dateOfFlight.Day <= 10))
                return true;

            // Okres wakacyjny (lipiec i sierpień)
            if (dateOfFlight.Month == 7 || dateOfFlight.Month == 8)
                return true;

            return false;
        }

        /// <summary>
        /// Sprawdza czy rezerwacja jest dokonana z 5-miesięcznym wyprzedzeniem.
        /// </summary>
        /// <param name="dateOfFlight">Data lotu</param>
        /// <returns>true jeśli rezerwacja jest wczesna, false w przeciwnym razie</returns>
        private static bool CheckEarlyReservation(DateOnly dateOfFlight)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            // Obliczamy różnicę w miesiącach
            int MonthDiff = ((dateOfFlight.Year - today.Year) * 12) + (dateOfFlight.Month - today.Month);
            return MonthDiff >= 5 ? true : false;
        }

        /// <summary>
        /// Pobiera i waliduje wszystkie dane wejściowe od użytkownika.
        /// </summary>
        /// <returns>Krotka zawierająca zwalidowane dane użytkownika (data urodzenia, data lotu, czy lot krajowy, czy stały klient)</returns>
        /// <exception cref="FormatException">Rzucany gdy format wprowadzonych danych jest nieprawidłowy</exception>
        /// <exception cref="ArgumentException">Rzucany gdy wprowadzone dane nie spełniają wymagań biznesowych</exception>
        /// <exception cref="ArgumentOutOfRangeException">Rzucany gdy data zawiera nieprawidłowe wartości (np. miesiąc > 12)</exception>
        private static (DateOnly dateOfBirth, DateOnly dateOfFlight, bool isDomesticFlight, bool isRegularCustomer) GetUserInput()
        {
            DateOnly? dateOfBirth = null;
            DateOnly? dateOfFlight = null;
            bool success = false;
            bool isDomesticFlight = false;
            bool isRegularCustomer = false;

            // Pobranie danych od użytkownika do skutku.
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
                        ValidateIsRegularCustomerCorrect(dateOfBirth.Value);

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

        /// <summary>
        /// Waliduje i przetwarza dane daty wprowadzone przez użytkownika.
        /// </summary>
        /// <param name="dateInfo">Tablica zawierająca części daty utworzona z inputu użytkownika</param>
        /// <param name="dateType">Typ daty ("urodzenia", "lotu")</param>
        /// <param name="isFlightDate">Czy data dotyczy lotu</param>
        /// <returns>Zwalidowany obiekt DateOnly</returns>
        /// <exception cref="FormatException">Rzucany gdy tablica jest nieprawidłowej długości lub dane nie są liczbami</exception>
        /// <exception cref="ArgumentException">Rzucany gdy data jest nieprawidłowa logicznie (np. data lotu w przeszłości lub data urodzenia w przyszłości)</exception>
        /// <exception cref="ArgumentOutOfRangeException">Rzucany gdy wartości daty są spoza zakresu (np. miesiąc > 12)</exception>
        /// <remarks>
        /// Metoda obecnie przyjmuje string jako dateType, ale w przyszłości można to zamienić na enum:
        /// <code>
        /// public enum DateType
        /// {
        ///     Birthday,
        ///     Flight
        /// }
        /// </code>
        /// </remarks>
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
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            // Jeżeli jest to data lotu, sprawdzamy czy jest wcześniej niż dzisiejsza
            if (isFlightDate && date < today)
            {
                throw new ArgumentException("Data lotu nie może być wcześniejsza niż dzisiejsza");
            }
            // Jeżeli jest to data urodzenia, sprawdzamy czy ktoś nie urodził się w przyszłości
            else if (!isFlightDate && (date > today || date.Year < 1945))
            {
                throw new ArgumentException("Data urodzenia nie może być wcześniejsza niż dzisiejsza");
            }
            return date;
        }

        /// <summary>
        /// Waliduje odpowiedź Tak/Nie od użytkownika.
        /// </summary>
        /// <param name="input">Wprowadzona odpowiedź</param>
        /// <param name="questionType">Typ pytania (czy lot krajowy, czy stały klient)</param>
        /// <returns>true dla 'T', false dla 'N'</returns>
        /// <exception cref="ArgumentException">Rzucany gdy odpowiedź nie jest 'T' ani 'N'</exception>
        /// <remarks>
        /// Metoda obecnie przyjmuje string jako questionType, ale w przyszłości można to zamienić na enum:
        /// <code>
        /// public enum questionType
        /// {
        ///     IsDomesticFlight,
        ///     IsRegularCustomer
        /// }
        /// </code>
        /// </remarks>
        private static bool ValidateAndParseYesNo(string input, string questionType)
        {
            string trimmedInput = input.Trim().ToUpper();
            if (trimmedInput != "T" && trimmedInput != "N")
                throw new ArgumentException($"Odpowiedź dla '{questionType}' musi być T lub N");

            return trimmedInput == "T" ? true : false;
        }

        /// <summary>
        /// Sprawdza czy użytkownik spełnia wymagania wiekowe dla statusu stałego klienta.
        /// </summary>
        /// <param name="dateOfBirth">Data urodzenia użytkownika</param>
        /// <exception cref="ArgumentException">Rzucany gdy użytkownik nie ma 18 lat</exception>
        private static void ValidateIsRegularCustomerCorrect(DateOnly dateOfBirth)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            DateOnly minimumBirthDate = today.AddYears(-18); // Data 18 lat temu

            if (dateOfBirth > minimumBirthDate)
                throw new ArgumentException("Aby być stałym klientem musisz mieć ukończone 18 lat");
        }

        /// <summary>
        /// Wyświetla informację o przyznanym rabacie.
        /// </summary>
        /// <param name="discount">Wartość rabatu w procentach</param>
        private static void PrintDiscountInfo(int discount)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            Console.WriteLine($"Przysługuje Ci rabat w wysokości: {discount}%");
            Console.WriteLine($"Data wygenerowania raportu: {today}");

            // Przysługuje Ci rabat w wysokości: 10%
            // Data wygenerowania raportu: 2023-02-01 12:03:23
        }

        /// <summary>
        /// Wyświetla podsumowanie wprowadzonych danych biletu.
        /// </summary>
        private static void PrintTicketInfo(DateOnly dateOfBirth, DateOnly dateOfFlight, bool isDomesticFlight, bool isRegularCustomer)
        {
            Console.WriteLine("\n=== Do obliczeń przyjęto ===");
            Console.WriteLine($"Data urodzenia: {dateOfBirth}");
            Console.WriteLine($"Data lotu: {FormatDate(dateOfFlight)}");
            Console.WriteLine($"{(isDomesticFlight ? "Lot krajowy" : "Lot międzynarodowy")}");
            Console.WriteLine($"Stały klient: {(isRegularCustomer ? "Tak" : "Nie")}");
            Console.WriteLine("=========================");
        }

        /// <summary>
        /// Formatuje datę do polskiego formatu tekstowego.
        /// </summary>
        /// <param name="date">Data do sformatowania</param>
        /// <returns>Data w formacie "dddd, d MMMM yyyy" po polsku</returns>
        private static string FormatDate(DateOnly date)
        {
            // Zamienia fornat na np:  wtorek, 4 lipca 2023
            return date.ToString("dddd, d MMMM yyyy", new System.Globalization.CultureInfo("pl-PL"));
        }
    }

}
