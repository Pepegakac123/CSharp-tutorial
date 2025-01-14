public class Lista {
    public static void Main(string[] args) {

        List<string> lista = new List<string>();
        lista.Add("kot");
        lista.Add("pies");
        lista.Add("krowa");
        lista.Add("szpak");
        foreach (var element in lista) {
            Console.WriteLine(element);
        }
        Console.WriteLine("Usuniecie elementow 2 i ostatniego");
        lista.RemoveAt(1);
        lista.RemoveAt(lista.Count - 1);
        foreach (var element in lista)
        {
            Console.WriteLine(element);
        }
        lista.Add("mucha");
        lista.Insert(0, "gazela");
        Console.WriteLine("Dodanie elementu na poczatek i na koniec");
        foreach (var element in lista)
        {
            Console.WriteLine(element);
        }
        Console.WriteLine(lista.Contains("krowa"));
        Console.WriteLine(lista.IndexOf("mucha"));

        Console.WriteLine("Odwrocenie kolejnosci listy");
        List<string> odwrocona = new List<string>(lista);
        odwrocona.Reverse();
        foreach (var element in odwrocona)
        {
            Console.WriteLine(element);
        }

        lista.Sort();

        Console.WriteLine("Sortowanie listy malejąco");
        lista.Sort((x,y) => y.CompareTo(x));

        Console.WriteLine("Sortowanie po długości napisów");
        lista.Sort((x, y) =>
        {
            int porownanieDlugosci = x.Length.CompareTo(y.Length);
            return porownanieDlugosci != 0 ? porownanieDlugosci : x.CompareTo(y);
        });


    }
}
