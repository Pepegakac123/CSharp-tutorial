using System;

class rysowanieProstokata{
public static void Main(string[] args){
    int rows = 0;
    int cols = 0;
    string symbol = "";

    while(symbol == "" || rows <= 0 || cols <=0){
    System.Console.WriteLine("The number of rows:");
    rows = Convert.ToInt32(Console.ReadLine());
    System.Console.WriteLine("The number of columns:");
    cols = Convert.ToInt32(Console.ReadLine());
    System.Console.WriteLine("The symbol:");
    symbol = Console.ReadLine();
    }
    Console.WriteLine("Projecting The Shape");
        for (int i = 1; i <= rows; i++){
        for(int j = 1; j <= cols; j++){
            System.Console.Write(symbol);
        }
        Console.WriteLine("");
    }
}
}



