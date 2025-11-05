using System;

namespace LibraryManagementSystem
{
    public class Account
    {

        public int NoBorrowedBooks { get; set; }
        public int NoReservedBooks { get; set; }
        public int NoReturnedBooks { get; set; }
        public int NoLostBooks { get; set; }
        public decimal FineAmount { get; set; }


        public Account()
        {
            NoBorrowedBooks = 0;
            NoReservedBooks = 0;
            NoReturnedBooks = 0;
            NoLostBooks = 0;
            FineAmount = 0;
        }


        public void CalculateFine()
        {
            decimal finePerDay = 2.0m;
            int daysOverdue = 5;
            FineAmount += daysOverdue * finePerDay;
            Console.WriteLine($"Obliczono karę: {FineAmount} zł");
        }
    }
}