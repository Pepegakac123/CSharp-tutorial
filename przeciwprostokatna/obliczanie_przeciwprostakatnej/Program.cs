using System;

class obliczanie_przeciwprostakatnej{
    public static void Main(String[] args){
        // Pobrac wartosc double a i b
        // obliczyc pierwiastek kwadratu tych wartosci
        // wyswietlic wynik

        Console.WriteLine("Podaj pierwszą liczbę");

        double a = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Podaj drugą liczbę");

        double b = Convert.ToDouble(Console.ReadLine());

        double c = Math.Round(Math.Sqrt(a*a + b*b),2);

        Console.WriteLine($"Przeciwprostokątna trojkąta o bokach a={a}, b={b} wynosi {c}");
        

    }
}