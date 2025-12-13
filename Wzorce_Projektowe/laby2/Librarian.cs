using System;

namespace LibraryManagementSystem
{
    public class Librarian
    {
        // Atrybuty
        public string Name { get; set; }
        public string ID { get; set; }
        public string Password { get; set; }

        // Konstruktor
        public Librarian()
        {
        }

        public Librarian(string name, string id, string password)
        {
            Name = name;
            ID = id;
            Password = password;
        }

        // Metody
        public void VerifyLibrarian()
        {
            if (!string.IsNullOrEmpty(ID) && !string.IsNullOrEmpty(Password))
            {
                Console.WriteLine($"Bibliotekarz {Name} zweryfikowany.");
            }
            else
            {
                Console.WriteLine("Weryfikacja nie powiodła się.");
            }
        }

        public void Search()
        {
            Console.WriteLine($"Bibliotekarz {Name} wyszukuje książki...");
        }
    }
}