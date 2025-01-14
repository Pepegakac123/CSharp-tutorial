public class Program
{
    public static void Main(string[] args)
    {
        HashSet<int> A = new HashSet<int>() { 1, 2, 5, 2, 7, 8 };
        HashSet<int> B = new HashSet<int>() { 2, 1, 5, 3, 9, 6 };
        HashSet<int> C = new HashSet<int>(A);
        C.UnionWith(B);

        Console.WriteLine("Średnia zbioru C:");
        Average(C);

        Console.WriteLine("\nCzęść wspólna zbiorów A i B:");
        HashSet<int> czesćWspólna = new HashSet<int>(A);
        czesćWspólna.IntersectWith(B);
        DisplaySet(czesćWspólna);

        Console.WriteLine("\nSprawdzanie podzbioru w różnicy symetrycznej:");
        HashSet<int> roznicaSymetryczna = new HashSet<int>(A);
        roznicaSymetryczna.SymmetricExceptWith(B);
        Console.WriteLine("Różnica Symetryczna");
        DisplaySet(roznicaSymetryczna);
        HashSet<int> zbiorDoSprawdzenia = new HashSet<int>() { 6, 9 };
        bool czyPodzbiór = roznicaSymetryczna.IsSupersetOf(zbiorDoSprawdzenia);
        Console.WriteLine($"Czy {{{6,9}}} jest podzbiorem różnicy symetrycznej: {czyPodzbiór}");
    }

    public static void DisplaySet<T>(ISet<T> set)
    {
        Console.WriteLine(string.Join(", ", set));
    }

    public static void Average(HashSet<int> set)
    {
        int sum = 0;
        foreach (int x in set)
        {
            sum += x;
        }
        Console.WriteLine(sum / set.Count);
    }
}