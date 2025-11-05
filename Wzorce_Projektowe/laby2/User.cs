using System;

namespace LibraryManagementSystem
{
    public class User
    {

        public string Name { get; set; }
        public string ID { get; set; }

        // Kompozycja - User zawiera Account
        public Account Account { get; private set; }

        // Konstruktor
        public User()
        {
            Account = new Account();
        }

        public User(string name, string id)
        {
            Name = name;
            ID = id;
            Account = new Account();
        }


        public void Verify()
        {
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(ID))
            {
                Console.WriteLine($"Użytkownik {Name} (ID: {ID}) zweryfikowany.");
            }
            else
            {
                Console.WriteLine("Weryfikacja nie powiodła się.");
            }
        }

        public void CheckAccount()
        {
            Console.WriteLine($"\n=== Konto użytkownika: {Name} ===");
            Console.WriteLine($"Wypożyczone: {Account.NoBorrowedBooks}");
            Console.WriteLine($"Zarezerwowane: {Account.NoReservedBooks}");
            Console.WriteLine($"Zwrócone: {Account.NoReturnedBooks}");
            Console.WriteLine($"Zgubione: {Account.NoLostBooks}");
            Console.WriteLine($"Kary: {Account.FineAmount} zł");
        }

        public void GetBookInfo()
        {
            Console.WriteLine($"Książki użytkownika {Name}:");
            Console.WriteLine($"Liczba wypożyczonych: {Account.NoBorrowedBooks}");
        }
    }
}