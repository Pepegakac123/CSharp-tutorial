using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem
{
    public class LibraryDatabase
    {

        public List<Book> ListOfBooks { get; set; }

        // Konstruktor
        public LibraryDatabase()
        {
            ListOfBooks = new List<Book>();
        }

        // Metody
        public void Add(Book book)
        {
            if (book != null)
            {
                ListOfBooks.Add(book);
                Console.WriteLine($"Dodano: {book.Title}");
            }
        }

        public void Delete(Book book)
        {
            if (book != null && ListOfBooks.Remove(book))
            {
                Console.WriteLine($"Usunięto: {book.Title}");
            }
        }

        public void Update(Book book)
        {
            if (book != null)
            {
                var existing = ListOfBooks.FirstOrDefault(b => b.ISBN == book.ISBN);
                if (existing != null)
                {
                    existing.Title = book.Title;
                    existing.Author = book.Author;
                    existing.Publication = book.Publication;
                    Console.WriteLine($"Zaktualizowano: {book.Title}");
                }
            }
        }

        public void Display()
        {
            Console.WriteLine("\n=== Lista książek ===");
            foreach (var book in ListOfBooks)
            {
                Console.WriteLine($"- {book.Title} | {book.Author} | ISBN: {book.ISBN}");
            }
        }

        public void Search(string searchTerm)
        {
            Console.WriteLine($"\nWyszukiwanie: '{searchTerm}'");
            var results = ListOfBooks.Where(b =>
                b.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                b.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            ).ToList();

            if (results.Any())
            {
                foreach (var book in results)
                {
                    Console.WriteLine($"- {book.Title} ({book.Author})");
                }
            }
            else
            {
                Console.WriteLine("Nie znaleziono książek.");
            }
        }
    }
}