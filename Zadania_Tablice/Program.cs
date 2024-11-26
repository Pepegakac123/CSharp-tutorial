using System;

namespace Zadania_Tablice
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new int[] {0, 1, 1, 2, 3, 3, 3};
            var b = new int[] {0, 1, 2, 3, 3};
            Print(a, b);
        }

        // public static void Print(int[] a, int[] b)
        // {
        //     int[] temp = new int[a.Length];
        //     int count = 0;

        //     for (int i = 0; i < a.Length; i++)
        //     {
        //         bool exists = false;

        //         for (int j = 0; j < b.Length; j++)
        //         {
        //             if (a[i] == b[j])
        //             {
        //                 exists = true;
        //                 break;
        //             }
        //         }

        //         if (!exists)
        //         {
        //             bool alreadyAdded = false;
        //             for (int k = 0; k < count; k++)
        //             {
        //                 if (temp[k] == a[i])
        //                 {
        //                     alreadyAdded = true;
        //                     break;
        //                 }
        //             }

        //             if (!alreadyAdded)
        //             {
        //                 temp[count] = a[i];
        //                 count++;
        //             }
        //         }
        //     }
        //     if (count == 0)
        //     {
        //         Console.WriteLine("empty");
        //         return;
        //     }

        //     for (int i = 0; i < count - 1; i++)
        //     {
        //         for (int j = 0; j < count - i - 1; j++)
        //         {
        //             if (temp[j] > temp[j + 1])
        //             {
        //                 int tmp = temp[j];
        //                 temp[j] = temp[j + 1];
        //                 temp[j + 1] = tmp;
        //             }
        //         }
        //     }
        //     for (int i = 0; i < count; i++)
        //     {
        //         Console.Write(temp[i] + " ");
        //     }
        //     Console.WriteLine();
        // }

        // Part 3

        public static void Print(int[] a, int[] b)
        {
            int[] result = new int[b.Length + a.Length];
            int count = 0;

            for (int i = 0; i < a.Length; i++)
            {
                if (!Array.Exists(b, x => x == a[i]))
                {
                    // Sprawdzanie, czy element już został dodany do wyniku
                    bool alreadyAdded = false;
                    for (int j = 0; j < count; j++)
                    {
                        if (result[j] == a[i])
                        {
                            alreadyAdded = true;
                            break;
                        }
                    }

                    if (!alreadyAdded)
                    {
                        result[count] = a[i];
                        count++;
                    }
                }
            }

             for (int i = 0; i < b.Length; i++)
            {
                if (!Array.Exists(a, x => x == b[i]))
                {
                    // Sprawdzanie, czy element już został dodany do wyniku
                    bool alreadyAdded = false;
                    for (int j = 0; j < count; j++)
                    {
                        if (result[j] == b[i])
                        {
                            alreadyAdded = true;
                            break;
                        }
                    }

                    if (!alreadyAdded)
                    {
                        result[count] = b[i];
                        count++;
                    }
                }
            }

            if (count == 0)
            {
                Console.WriteLine("empty");
                return;
            }
            Array.Resize(ref result, count);

            Array.Sort(result);
            Console.WriteLine(string.Join(" ", result));
        }
    }
}
