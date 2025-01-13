using System;
using BibliotekaKolekcji;

public class Program
{
    public static void Main(string[] args)
    {
        Stos<int> stos = new Stos<int>();
        stos.Push(1);
        stos.Push(2);
        stos.Push(3);

        foreach (int element in stos.ToArray())
        {
            Console.WriteLine(element);
        }
    } 
}