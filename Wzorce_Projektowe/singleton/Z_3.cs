using System;
using System.Collections.Generic;

public class DatabaseConnectionManager
{
    /* Słownik przechowujący instancje połączeń do baz danych
     Twoja implementacja*/
    private static Dictionary<string, DatabaseConnectionManager> instances = new Dictionary<string, DatabaseConnectionManager>();



    public bool IsConnected { get; private set; }
    public string Database { get; private set; }
    private static readonly object lockObj = new object();

    /* Prywatny konstruktor
    Twoja implementacja*/
    private DatabaseConnectionManager(string database)
    {
        Database = database;
        IsConnected = false;
    }

    /* Metoda zwracająca połączenie do odpowiedniej bazy danych
    Twoja implementacja*/
    public static DatabaseConnectionManager GetConnection(string database)
    {
        if (!instances.ContainsKey(database))
        {
            lock (lockObj)
            {
                if (!instances.ContainsKey(database))
                {
                    instances[database] = new DatabaseConnectionManager(database);
                }
            }
        }
        return instances[database];
    }

    // Otwieranie połączenia
    public void OpenConnection()
    {
        Console.WriteLine($"Połączenie do bazy danych {Database} zostało otwarto.");
        IsConnected = true;
    }

    // Zamykanie połączenia
    public void CloseConnection()
    {
        Console.WriteLine($"Połączenie do bazy danych {Database} zostało zamknięte.");
        IsConnected = false;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- Test wzorca Multiton dla DatabaseConnectionManager ---");


        Console.WriteLine("\n--- Baza 1: MySQL ---");
        DatabaseConnectionManager mysql1 = DatabaseConnectionManager.GetConnection("MySQL");
        mysql1.OpenConnection();


        Console.WriteLine("\n--- Baza 2: Postgres ---");
        DatabaseConnectionManager postgres = DatabaseConnectionManager.GetConnection("Postgres");
        postgres.OpenConnection();


        Console.WriteLine("\n--- Baza 1 (Ponowne pobranie): MySQL ---");
        DatabaseConnectionManager mysql2 = DatabaseConnectionManager.GetConnection("MySQL");

        Console.WriteLine("\n--- WERYFIKACJA ---");

        // Test 1: Czy różne klucze dają różne instancje?
        bool differentInstances = !ReferenceEquals(mysql1, postgres);
        Console.WriteLine($"MySQL i Postgres to różne instancje? **{differentInstances}** (Oczekiwano: True)");
        Console.WriteLine($"Hash MySQL: {mysql1.GetHashCode()}, Hash Postgres: {postgres.GetHashCode()}");

        // Test 2: Czy ten sam klucz daje tę samą instancję?
        bool sameInstance = ReferenceEquals(mysql1, mysql2);
        Console.WriteLine($"mysql1 i mysql2 to ta sama instancja? **{sameInstance}** (Oczekiwano: True)");
        Console.WriteLine($"Hash mysql1: {mysql1.GetHashCode()}, Hash mysql2: {mysql2.GetHashCode()}");


        mysql1.CloseConnection();
        postgres.CloseConnection();

        Console.WriteLine($"\nStan mysql2 po zamknięciu mysql1: IsConnected = {mysql2.IsConnected}");
        // Oczekiwany wynik: IsConnected powinno być 'False' (ponieważ mysql1 i mysql2 to ten sam obiekt).
    }
}
