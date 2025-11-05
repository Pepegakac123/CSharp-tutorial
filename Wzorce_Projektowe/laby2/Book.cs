using System;

namespace LibraryManagementSystem
{
    public class Book
    {

        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public DateTime Publication { get; set; }

        public Book()
        {
        }

        public Book(string title, string author, string isbn, DateTime publication)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            Publication = publication;
        }

        public void ShowDueDate()
        {
            DateTime dueDate = DateTime.Now.AddDays(14);
            Console.WriteLine($"Termin zwrotu '{Title}': {dueDate:dd/MM/yyyy}");
        }

        public void ReservationStatus()
        {
            Console.WriteLine($"Status '{Title}': Dostępna");
        }

        public void Feedback()
        {
            Console.WriteLine($"Dodano opinię o książce '{Title}'.");
        }

        public void BookRequest()
        {
            Console.WriteLine($"Żądanie wypożyczenia '{Title}' zostało złożone.");
        }

        public void RenewInfo()
        {
            Console.WriteLine($"Przedłużono wypożyczenie '{Title}'.");
        }
    }
}