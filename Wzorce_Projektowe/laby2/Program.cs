using System;

namespace LibraryManagementSystem
{
    class Program
    {
        // Wedle diagramu metody np taka jak Search() w przypadku librarian nie miały podanych parametrów
        // Powoduje to ograniczenie niektórych funkcjonalności
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔═══════════════════════════════════════════════════╗");
            Console.WriteLine("║      SYSTEM ZARZĄDZANIA BIBLIOTEKĄ               ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════╝\n");

            // Inicjalizacja systemu
            var system = new LibraryManagementSystem();

            // Tworzenie bazy danych
            var database = new LibraryDatabase();

            // Dodanie książek do bazy
            Console.WriteLine(">>> Dodawanie książek do bazy danych:\n");
            var book1 = new Book("Władca Pierścieni", "J.R.R. Tolkien",
                "978-83-7469-123-4", new DateTime(1954, 7, 29));
            var book2 = new Book("Harry Potter i Kamień Filozoficzny", "J.K. Rowling",
                "978-83-7278-162-3", new DateTime(1997, 6, 26));
            var book3 = new Book("Wiedźmin: Ostatnie życzenie", "Andrzej Sapkowski",
                "978-83-7469-652-9", new DateTime(1993, 1, 1));
            var book4 = new Book("1984", "George Orwell",
                "978-83-7469-891-2", new DateTime(1949, 6, 8));
            var book5 = new Book("Czysty Kod", "Robert C. Martin",
                "978-83-246-1610-0", new DateTime(2008, 8, 1));

            database.Add(book1);
            database.Add(book2);
            database.Add(book3);
            database.Add(book4);
            database.Add(book5);

            // Wyświetlenie wszystkich książek
            Console.WriteLine("\n>>> Wszystkie książki w bibliotece:");
            database.Display();

            // Tworzenie bibliotekarza
            Console.WriteLine("\n>>> Tworzenie konta bibliotekarza:\n");
            var librarian = new Librarian("Anna Kowalska", "LIB001", "admin123");
            Console.WriteLine($"Utworzono konto bibliotekarza: {librarian.Name}");

            // Weryfikacja bibliotekarza
            Console.WriteLine("\n>>> Weryfikacja bibliotekarza:\n");
            librarian.VerifyLibrarian();

            // Tworzenie studentów
            Console.WriteLine("\n>>> Rejestracja studentów:\n");
            var student1 = new Student("Jan Kowalski", "STU001", "Informatyka");
            Console.WriteLine($"Zarejestrowano studenta: {student1.Name}, kierunek: {student1.Class}");

            var student2 = new Student("Maria Nowak", "STU002", "Matematyka");
            Console.WriteLine($"Zarejestrowano studenta: {student2.Name}, kierunek: {student2.Class}");

            // Tworzenie pracowników
            Console.WriteLine("\n>>> Rejestracja pracowników:\n");
            var staff1 = new Staff("Piotr Wiśniewski", "STA001", "Wydział Informatyki");
            Console.WriteLine($"Zarejestrowano pracownika: {staff1.Name}, wydział: {staff1.Dept}");

            var staff2 = new Staff("Katarzyna Lewandowska", "STA002", "Wydział Matematyki");
            Console.WriteLine($"Zarejestrowano pracownika: {staff2.Name}, wydział: {staff2.Dept}");

            // Logowanie studenta
            Console.WriteLine("\n>>> Logowanie użytkownika:\n");
            system.UserType = "Student";
            system.Username = "jan.kowalski";
            system.Password = "haslo123";
            system.Login();

            // Weryfikacja studenta
            Console.WriteLine("\n>>> Weryfikacja użytkownika:\n");
            student1.Verify();

            // Wyszukiwanie książek
            Console.WriteLine("\n>>> Wyszukiwanie książek przez studenta:");
            database.Search("Władca");

            // Bibliotekarz wyszukuje książki
            Console.WriteLine("\n>>> Bibliotekarz przeszukuje system:\n");
            librarian.Search();

            // Wypożyczenie książek przez studenta
            Console.WriteLine("\n>>> Student wypożycza książki:\n");
            book1.BookRequest();
            student1.Account.NoBorrowedBooks++;

            book2.BookRequest();
            student1.Account.NoBorrowedBooks++;

            // Sprawdzenie terminu zwrotu
            Console.WriteLine("\n>>> Sprawdzanie terminów zwrotu:\n");
            book1.ShowDueDate();
            book2.ShowDueDate();

            // Status rezerwacji książek
            Console.WriteLine("\n>>> Status książek:\n");
            book1.ReservationStatus();
            book3.ReservationStatus();

            // Sprawdzenie stanu konta studenta
            Console.WriteLine("\n>>> Sprawdzenie stanu konta:\n");
            student1.CheckAccount();

            // Informacje o wypożyczonych książkach
            Console.WriteLine("\n>>> Informacje o wypożyczonych książkach:\n");
            student1.GetBookInfo();

            // Pracownik wypożycza książkę
            Console.WriteLine("\n>>> Pracownik wypożycza książkę:\n");
            book3.BookRequest();
            staff1.Account.NoBorrowedBooks++;
            staff1.CheckAccount();

            // Przedłużenie wypożyczenia
            Console.WriteLine("\n>>> Student przedłuża wypożyczenie:\n");
            book1.RenewInfo();

            // Dodanie opinii o książce
            Console.WriteLine("\n>>> Dodawanie opinii o książce:\n");
            book1.Feedback();
            book2.Feedback();

            // Symulacja opóźnienia - naliczanie kary
            Console.WriteLine("\n>>> Obliczanie kary za opóźnienie:\n");
            student1.Account.CalculateFine();

            // Ponowne sprawdzenie konta po karze
            Console.WriteLine("\n>>> Stan konta po naliczeniu kary:\n");
            student1.CheckAccount();

            // Zwrot książki
            Console.WriteLine("\n>>> Student zwraca książkę:\n");
            student1.Account.NoBorrowedBooks--;
            student1.Account.NoReturnedBooks++;
            Console.WriteLine($"Zwrócono książkę: {book1.Title}");
            student1.CheckAccount();

            // Aktualizacja informacji o książce
            Console.WriteLine("\n>>> Bibliotekarz aktualizuje informacje o książce:\n");
            var updatedBook = new Book("Władca Pierścieni - Wydanie Specjalne",
                "J.R.R. Tolkien", "978-83-7469-123-4", new DateTime(1954, 7, 29));
            database.Update(updatedBook);

            // Dodanie nowej książki
            Console.WriteLine("\n>>> Dodanie nowej książki:\n");
            var newBook = new Book("Hobbit", "J.R.R. Tolkien",
                "978-83-7469-555-5", new DateTime(1937, 9, 21));
            database.Add(newBook);

            // Wyszukiwanie książek autora
            Console.WriteLine("\n>>> Wyszukiwanie książek Tolkiena:");
            database.Search("Tolkien");

            // Drugi student loguje się i wypożycza książkę
            Console.WriteLine("\n>>> Drugi student loguje się:\n");
            system.Username = "maria.nowak";
            system.Login();
            student2.Verify();

            Console.WriteLine("\n>>> Drugi student wypożycza książkę:\n");
            book4.BookRequest();
            student2.Account.NoBorrowedBooks++;
            book4.ShowDueDate();

            // Student rezerwuje książkę
            Console.WriteLine("\n>>> Rezerwacja książki:\n");
            student2.Account.NoReservedBooks++;
            Console.WriteLine($"Student {student2.Name} zarezerwował książkę");
            student2.CheckAccount();

            // Wyświetlenie aktualnego stanu bazy
            Console.WriteLine("\n>>> Aktualny stan biblioteki:");
            database.Display();

            // Usunięcie książki z bazy
            Console.WriteLine("\n>>> Usunięcie książki z bazy:\n");
            database.Delete(book5);

            // Finalna lista książek
            Console.WriteLine("\n>>> Finalna lista książek:");
            database.Display();

            // Wylogowanie
            Console.WriteLine("\n>>> Wylogowanie użytkownika:\n");
            system.Logout();

            // Nowa sesja - pracownik
            Console.WriteLine("\n>>> Nowa sesja - logowanie pracownika:\n");
            system.UserType = "Staff";
            system.Username = "piotr.wisniewski";
            system.Password = "haslo456";
            system.Login();

            staff1.Verify();
            staff1.GetBookInfo();

            // Zgłoszenie zguby książki
            Console.WriteLine("\n>>> Zgłoszenie zagubienia książki:\n");
            staff1.Account.NoLostBooks++;
            staff1.Account.NoBorrowedBooks--;
            Console.WriteLine($"Pracownik zgłosił zgubienie książki");
            staff1.CheckAccount();

            // Obliczenie kary za zgubienie
            Console.WriteLine("\n>>> Naliczanie kary za zgubioną książkę:\n");
            staff1.Account.CalculateFine();
            staff1.CheckAccount();

            // Rejestracja nowego użytkownika
            Console.WriteLine("\n>>> Rejestracja nowego użytkownika:\n");
            var newSystem = new LibraryManagementSystem();
            newSystem.UserType = "Student";
            newSystem.Username = "adam.nowak";
            newSystem.Register();

            // Finalne wylogowanie
            Console.WriteLine("\n>>> Zakończenie sesji:\n");
            system.Logout();

            Console.WriteLine("\n╔═══════════════════════════════════════════════════╗");
            Console.WriteLine("║           SESJA ZAKOŃCZONA                       ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════╝");

            Console.WriteLine("\nNaciśnij Enter aby zakończyć...");
            Console.ReadLine();
        }
    }
}