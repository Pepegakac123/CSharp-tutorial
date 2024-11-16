using System;

class Program
{
    static void Main()
    {
        // Wczytanie danych wejściowych
        // string[] input = Console.ReadLine().Split(' ');
        // int x = int.Parse(input[0]);
        // int y = int.Parse(input[1]);
        // int z = int.Parse(input[2]);

        // Start:
        //     if(x>0){
        //         if(y>0){
        //             x = x-1;
        //             y = y-1;
        //             Console.Write("C");
        //             goto Start;
        //         }else{
        //             Console.Write("D");
        //             if(z>0){
        //                 goto End;
        //             }
        //             goto Breakpoint;
        //         }
        //     }else{
        //         Console.Write("E");
        //         goto Breakpoint;
        //     }
        // Breakpoint:
        //         Console.Write("G");
        // End:
        //     Console.WriteLine("");

         // Wczytanie danych wejściowych
  string[] input = Console.ReadLine().Split(' ');
        int x = int.Parse(input[0]);
        int y = int.Parse(input[1]);
        int z = int.Parse(input[2]);

        // Pętla główna
    //     while (x > 0)
    //     {
    //         if (y > 0)
    //         {
    //             x = x - 1;
    //             y = y - 1;
    //             Console.Write("C");
    //             continue; // Wracamy na początek pętli
    //         }
    //         else
    //         {
    //             Console.Write("D");
    //             if (z > 0)
    //             {
    //                 Console.WriteLine(""); // Kończymy działanie
    //                 return;
    //             }
    //             Console.Write("G");
    //             Console.WriteLine(""); // Kończymy działanie
    //             return;
    //         }
    //     }
    //     Console.Write("E");
    //     Console.Write("G");
    //     Console.WriteLine("");
    // }
        for (int i = 0; x>0; i++){
            if(y > 0){
                x = x - 1;
                y = y-1;
                Console.Write("C");
                continue;
            }
            else{
                Console.Write("D");
                if(z>0){
                    Console.WriteLine("");
                    return;
                }
                Console.Write("G");
                Console.WriteLine("");
                return;
            }
        }
        Console.Write("E");
        Console.Write("G");
        Console.WriteLine("");
    }
}