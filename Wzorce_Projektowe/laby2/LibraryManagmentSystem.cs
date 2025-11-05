using System;
using System.Collections.Generic;

namespace LibraryManagementSystem
{
    public class LibraryManagementSystem
    {
        public string UserType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // Kompozycja - LibraryManagementSystem zawiera User, Book i Librarian
        private List<User> users;
        private List<Book> books;
        private List<Librarian> librarians;

        public LibraryManagementSystem()
        {
            users = new List<User>();
            books = new List<Book>();
            librarians = new List<Librarian>();
        }

        public void Login()
        {
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                Console.WriteLine($"Zalogowano: {Username} jako {UserType}");
            }
            else
            {
                Console.WriteLine("Błąd logowania.");
            }
        }

        public void Register()
        {
            if (!string.IsNullOrEmpty(Username))
            {
                Console.WriteLine($"Zarejestrowano: {Username} ({UserType})");
            }
            else
            {
                Console.WriteLine("Błąd rejestracji.");
            }
        }

        public void Logout()
        {
            Console.WriteLine($"Wylogowano: {Username}");
            Username = null;
            Password = null;
            UserType = null;
        }
    }
}