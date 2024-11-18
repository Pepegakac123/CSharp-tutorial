using System;
using System.IO.Compression;

class Program {

const char CHAR = '*';
static void Star() => Console.Write(CHAR);
static void StarLn() => Console.WriteLine(CHAR);
static void Space() => Console.Write(" ");
static void SpaceLn() => Console.WriteLine(" ");
static void NewLine() => Console.WriteLine();

static void LiteraX(int n)
{
    if (n < 3) throw new ArgumentException("zbyt mały rozmiar");
    if (n % 2 == 0) n = n + 1;

    //górna połówka
    for (int i = 0; i < n; i++){
        for (int j = 0; j < n; j++){
            if(j==i || j==n-i-1){  
              Star();
            }else{
                Space();
            }

        }
         NewLine();
    }
}

static void odwroconaLiteraZ(int n){
    if (n < 3) throw new ArgumentException("zbyt mały rozmiar");

    for (int i = 0; i<n; i++){
        for(int j = 0; j<n; j++){
            if(i==0 || i==n-1 || i==j){
                Star();
            }else{
            Space();
            }
        }
        NewLine();
    }

}

static void LiteraZ(int n){
    if (n < 3) throw new ArgumentException("zbyt mały rozmiar");

    for (int i = 0; i<n; i++){
        for(int j = 0; j<n; j++){
            if(i==0 || i==n-1){
                Star();
            }else if(j==n-i-1){
                Star();
            }else{
            Space();
            }
        }
        NewLine();
    }

}

public static void Klepsydra (int n){
    if (n < 3) throw new ArgumentException("zbyt mały rozmiar");

    for(int i=0; i<n; i++){
        for(int j=0; j<n; j++){
            if(i==0 || i==n-1){
                Star();
            }else if(i==j || j==n-i-1){
                Star();
            }else{
                Space();
            }
        }
        NewLine();
    }

}

public static void TrojkatPusty1(int n){
    if (n < 3) throw new ArgumentException("zbyt mały rozmiar");
    if (n % 2 == 0) n = n - 1;

    for(int i=0; i<n; i++){
        for (int j = 0; j < n; j++) // Iteracja po kolumnach
        {
            // Rysowanie gwiazdek na odpowiednich pozycjach:
            if (i == n/2 || (j >= n / 2 - i && j <= n / 2 + i && i<=n/2)) // Podstawa i boki trójkąta
                Star();
            else
                Space();
        }
        NewLine();
    }
}
public static void TrojkatPusty2(int n)
{
    if (n < 5) throw new ArgumentException("zbyt mały rozmiar");
    if (n % 2 == 0) n = n - 1; // Dostosowanie, aby n było nieparzyste

    for (int i = 0; i < n; i++) // Iteracja po wierszach
    {
        for (int j = 0; j < n; j++) // Iteracja po kolumnach
        {
            // Rysowanie gwiazdek na odpowiednich pozycjach
            if (i == 0) // Podstawa na górze
            {
                Star();
            }
            else if (i <= n/2 && (j >= i && j <= n - i - 1)) // Skrócone boki
            {
                Star();
            }
            else
            {
                Space();
            }
        }
        NewLine(); // Przejście do nowej linii po zakończeniu rysowania wiersza
    }
}



static void Main(string[] args)
{
    // LiteraX(3);
    // odwroconaLiteraZ(8);
    // LiteraZ(4);
    // Klepsydra(7);
    TrojkatPusty1(10);
    Console.WriteLine("------------");
    TrojkatPusty2(7);
}
}
