// using System;

// class Program
// {
//     static readonly string[][] InitializePatterns()
//     {
//         return new string[][] {
//             new string[] {" _ ", "| |", "|_|"}, // 0
//             new string[] {"   ", "  |", "  |"}, // 1
//             new string[] {" _ ", " _|", "|_ "}, // 2
//             new string[] {" _ ", " _|", " _|"}, // 3
//             new string[] {"   ", "|_|", "  |"}, // 4
//             new string[] {" _ ", "|_ ", " _|"}, // 5
//             new string[] {" _ ", "|_ ", "|_|"}, // 6
//             new string[] {" _ ", "  |", "  |"}, // 7
//             new string[] {" _ ", "|_|", "|_|"}, // 8
//             new string[] {" _ ", "|_|", "  |"}  // 9
//         };
//     }

//     public static void Main(string[] args)
//     {
//         string liczba = Console.ReadLine();
//         string[][] digitalPatterns = InitializePatterns();

//         for (int row = 0; row < 3; row++) //Iteracaj po wierszach
//         {
//             for (int i = 0; i < liczba.Length; i++)
//             {
//                 int number = int.Parse(liczba[i].ToString());
//                 Console.Write(digitalPatterns[number][row]);
//             }
//             Console.WriteLine(); //Przeskod do nowej lini/wiersza
//         }
//     }
// }

// Zadanie SPOJ - odwrotne
using System;
using System.Text;

class Program
{
    static readonly string[][] PATTERNS = {
        new[] {" _ ", "| |", "|_|"}, // 0
        new[] {"   ", "  |", "  |"}, // 1
        new[] {" _ ", " _|", "|_ "}, // 2
        new[] {" _ ", " _|", " _|"}, // 3
        new[] {"   ", "|_|", "  |"}, // 4
        new[] {" _ ", "|_ ", " _|"}, // 5
        new[] {" _ ", "|_ ", "|_|"}, // 6
        new[] {" _ ", "  |", "  |"}, // 7
        new[] {" _ ", "|_|", "|_|"}, // 8
        new[] {" _ ", "|_|", "  |"}  // 9
    };

    public static void Main()
    {
        string line;
        while ((line = Console.ReadLine()) != null && !string.IsNullOrEmpty(line))
        {
            var result = new StringBuilder();
            // Tworzony wzorzec cyfry z inputów użytkownika
            var pattern = new[] { line, Console.ReadLine(), Console.ReadLine() };

            for (int i = 0; i < line.Length; i += 3)
            {
                // Wyycinamy wzór dla pojedynczej cyfry aby móc go później porównać
                var digit = new[] {
                    pattern[0].Substring(i, 3),
                    pattern[1].Substring(i, 3),
                    pattern[2].Substring(i, 3)
            };

                // Znajdujemy matching cyfre
                for (int j = 0; j < PATTERNS.Length; j++)
                {
                    if (digit[0] == PATTERNS[j][0] &&
                        digit[1] == PATTERNS[j][1] &&
                        digit[2] == PATTERNS[j][2])
                    {
                        result.Append(j);
                        break;
                    }
                }
            }

            Console.WriteLine(result);
        }
    }
}