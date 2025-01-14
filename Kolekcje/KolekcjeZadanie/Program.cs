namespace KolekcjeZadanie
{
public class KolekcjeZadanie
{
    public static void Main(string[] args)
    {
int[] a = new int[] { -2, -1, 0, 1, 4 };
            int[] b = new int[] { -3, -2, -1, 1, 2, 3 };
            Print(a, b);
    }
        //public static void Print(int[] a, int[] b)
        //{
        //    HashSet<int> setA = new HashSet<int>(a);
        //    HashSet<int> setB = new HashSet<int>(b);
        //    HashSet<int> exceptA = new HashSet<int>(setA);
        //    HashSet<int> exceptB = new HashSet<int>(setB);
        //    exceptA.ExceptWith(setB);
        //    exceptB.ExceptWith(setA);


        //    int[] result = new int[exceptA.Count + exceptB.Count];


        //    exceptA.CopyTo(result, 0);
        //    exceptB.CopyTo(result, exceptA.Count);


        //    Array.Sort(result);


        //    Console.WriteLine(result.Length > 0 ? string.Join(" ", result) : "empty");
        //}
        public static void Print(int[] a, int[] b)
        {
            HashSet<int> result = new HashSet<int>(a);
            result.SymmetricExceptWith(b);  // znajduje elementy które są w A lub B, ale nie w obu

            // Konwertujemy na posortowaną tablicę
            int[] sorted = new int[result.Count];
            result.CopyTo(sorted);
            Array.Sort(sorted);

            // Wyświetlamy wynik
            Console.WriteLine(sorted.Length > 0 ? string.Join(" ", sorted) : "empty");
        }
    };

}