
public class KolejkaIStos
{
    public static void Main(string[] args)
    {
        Stack<string> stack = new Stack<string>();
        Queue<string> kolejka = new Queue<string>();

        stack.Push("1");
        stack.Push("2");
        stack.Push("3");
        stack.Push("4");

        kolejka.Enqueue("a");
        kolejka.Enqueue("b");
        kolejka.Enqueue("c");
        kolejka.Enqueue("d");

        WypiszStos(stack);
        WypiszKolejke(kolejka);

        var stosel1 = stack.Pop();
        var stosel2 = stack.Pop();

        kolejka.Enqueue(stosel1);
        kolejka.Enqueue(stosel2);

        WypiszStos(stack);
        WypiszKolejke(kolejka);

        Console.WriteLine(stack.Peek());
        Console.WriteLine(kolejka.Peek());
    }
    public static void WypiszKolejke(Queue<string> kolejka)
    {
        Console.WriteLine("Kolejka:");
        foreach (string element in kolejka)
        {
            Console.WriteLine(element);
        }
    }
    public static void WypiszStos(Stack<string> stos)
    {
        Console.WriteLine("Stos:");

        foreach (string element in stos)
        {
            Console.WriteLine(element);
        }
    }
}