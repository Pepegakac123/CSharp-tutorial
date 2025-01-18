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
        //    HashSet<int> setAExceptB = new HashSet<int>(a);
        //    setAExceptB.ExceptWith(b);
        //    HashSet<int> setBExceptA = new HashSet<int>(b);
        //    setBExceptA.ExceptWith(a);

        //    int[] result = new int[setAExceptB.Count + setBExceptA.Count];


        //    setAExceptB.CopyTo(result, 0);
        //    setBExceptA.CopyTo(result, exceptA.Count);


        //    Array.Sort(result);


        //    Console.WriteLine(result.Length > 0 ? string.Join(" ", result) : "empty");
        //}
        public static void Print(int[] a, int[] b)
        {
            HashSet<int> result = new HashSet<int>(a);
            result.SymmetricExceptWith(b); 

            
            int[] sorted = new int[result.Count];
            result.CopyTo(sorted);
            Array.Sort(sorted);

            // Wyświetlamy wynik
            Console.WriteLine(sorted.Length > 0 ? string.Join(" ", sorted) : "empty");
        }
    };

}