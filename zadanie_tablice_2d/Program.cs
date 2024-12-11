using System;

namespace zadanie_tablice_2d
{
    public static class Program{
    // Zadanie 1:
    // Napisz program, który wczyta ze standardowego wejścia tablicę liczb całkowitych i wypisze na standardowe wyjście jej transpozycję (zamiana wierszy na kolumny).
    // Na wejściu, w pierwszej linii podane będą dwie dodatnie liczby n oraz m oddzielone pojedynczą spacją, oznaczające odpowiednio liczbę wierszy oraz liczbę kolumn wczytywanej tablicy. Liczby n oraz m nie przekroczą wartości 10.
    // W kolejnych liniach podane będą ciągi liczb całkowitych oddzielonych pojedynczą spacją, oznaczające kolejne wiersze wczytywanej tablicy.
    // Na wyjściu wypisz, w kolejnych liniach, ciągi liczb oddzielonych pojedynczą spacją, będące kolejnymi wierszami tablicy po transpozycji.
        // public static void Main(string[] args)
        // {
        //     string[] matrixInfo = Console.ReadLine().Split(" ");
        //     int numberOfRows = int.Parse(matrixInfo[0]);
        //     int numberOfColumns = int.Parse(matrixInfo[1]);
        //     int[,] matrix = new int[numberOfRows, numberOfColumns];
        //     for (int i = 0; i < numberOfRows; i++)
        //     {
        //         string[] row = Console.ReadLine().Split(" ");
        //         for (int j = 0; j < numberOfColumns; j++)
        //         {
        //             matrix[i,j] = int.Parse(row[j]);
        //         } 
        //     }
        //     printMatrix(transposeMatrix(matrix)); 
        // }

        // public static int[,] transposeMatrix(int[,] matrix)
        // { 
        //     int rows = matrix.GetLength(0);
        //     int cols = matrix.GetLength(1);
        //     int[,] transposedMatrix = new int[cols, rows]; // zamiana kolejności
            
        //     for(int i = 0; i < cols; i++)
        //     {
        //         for (int j = 0; j < rows; j++)
        //         {
        //             transposedMatrix[i,j] = matrix[j,i]; // zamiana indeksów
        //         }
        //     }
        //     return transposedMatrix;
        // }
        // public static void printMatrix(int[,] matrix){
        //     int rows = matrix.GetLength(0);
        //     int cols = matrix.GetLength(1);
        //     for (int i = 0; i<rows; i++){
        //         for (int j = 0; j<cols; j++){
        //             Console.Write(matrix[i,j] + " ");
        //         }
        //         Console.WriteLine();
        //     }
        // }

        //ZADANIE 2: 
        // Napisz program, który wczyta ze standardowego wejścia dwie tablice liczb całkowitych (A i B) i wypisze na standardowe wyjście ich iloczyn macierzowy (A x B) (https://pl.wikipedia.org/wiki/Mno%C5%BCenie_macierzy).

        // Na wejściu, w pierwszej linii podane są dwie dodatnie liczby n1 oraz m1 oddzielone pojedynczą spacją, oznaczające odpowiednio liczbę wierszy oraz liczbę kolumn wczytywanej macierzy A. Liczby n1 oraz m1 nie przekroczą wartości 10.
        // W drugiej linii podane są wartości macierzy A (liczby jednocyfrowe oddzielone pojedynczą spacją).
        // W trzeciej linii podane są dwie dodatnie liczby n2 oraz m2 oddzielone pojedynczą spacją, oznaczające odpowiednio liczbę wierszy oraz liczbę kolumn wczytywanej macierzy B. Liczby n2 oraz m2 nie przekroczą wartości 10.
        // W czwartej linii podane są wartości macierzy B (liczby jednocyfrowe oddzielone pojedynczą spacją).
        // Na wyjściu wypisz, w kolejnych liniach, ciągi liczb oddzielonych pojedynczą spacją, będące kolejnymi wierszami iloczynu macierzy A x B. Jeśli operacja mnożenia nie będzie możliwa, wypisz napis impossible.

        // public static void Main(string[] args)
        // {
        //     string[] firstMatrix = Console.ReadLine().Split(" ");
        //     var matrixA = assignMatrix(firstMatrix);
        //     string[] secondMatrix = Console.ReadLine().Split(" ");
        //     var matrixB = assignMatrix(secondMatrix);

        //     int rowsNumberOfA = matrixA.GetLength(0);
        //     int colsNumberOfA = matrixA.GetLength(1);

        //     int rowsNumberOfB = matrixB.GetLength(0);
        //     int colsNumberOfB = matrixB.GetLength(1);

        //     if(colsNumberOfA != rowsNumberOfB){
        //         Console.WriteLine("impossible");
        //         return;
        //         // 1 3
        //         // 3 4 5
        //         // 1 2
        //         // 8 9
        //     }
        //     int[,] matrixC = new int[rowsNumberOfA, colsNumberOfB];
        //     matrixC = multiplyMatrices(matrixA, matrixB, matrixC);
        //     printMatrix(matrixC);
        //     // 2 2
        //     // 1 2 3 4
        //     // 2 2
        //     // 1 1 1 1
        // }

        // public static int[,] assignMatrix(string[] input){
        //     int numberOfRows = int.Parse(input[0]);
        //     int numberOfColumns = int.Parse(input[1]);
        //     int[,] matrix = new int[numberOfRows, numberOfColumns];
        //     // 1 2 3 4
        //     string[]matrixData = Console.ReadLine().Split(" ");
        //     for (int i = 0; i < numberOfRows; i++){
        //         for (int j = 0; j < numberOfColumns; j++){
        //             // element [0,1] będzie na pozycji 0 * 2 + 1 = 1
        //             // element [1,0] będzie na pozycji 1 * 2 + 0 = 2
        //             matrix[i,j] = int.Parse(matrixData[i * numberOfColumns + j]);
        //         }
        //     }
        //     return matrix; 
        // }
        // public static int[,] multiplyMatrices(int[,] matrixA, int[,] matrixB, int[,] result)
        // {
        //     int rowsA = matrixA.GetLength(0);    // m
        //     int colsB = matrixB.GetLength(1);    // p
        //     int colsA = matrixA.GetLength(1);    // n (= rowsB)

        //     // Dla każdego elementu wynikowej macierzy
        //     for (int i = 0; i < rowsA; i++)
        //     {
        //         for (int j = 0; j < colsB; j++)
        //         {
        //             int sum = 0;
        //             // Sumowanie iloczynów
        //             for (int k = 0; k < colsA; k++)
        //             {
        //                 sum += matrixA[i,k] * matrixB[k,j];
        //             }
        //             result[i,j] = sum;
        //         }
        //     }
        //     return result;
        // }

        // public static void printMatrix(int[,] matrix){
        //     int rows = matrix.GetLength(0);
        //     int cols = matrix.GetLength(1);
        //     for (int i = 0; i<rows; i++){
        //         for (int j = 0; j<cols; j++){
        //             Console.Write(matrix[i,j] + " ");
        //         }
        //         Console.WriteLine();
        //     }
        // }

        // Zadanie 3

        // Napisz program, który wczyta ze standardowego wejścia prostokątną tablicę liczb całkowitych, a następnie wypisze indeks kolumny o największej sumie. Jeśli kolumn z największą sumą będzie więcej, wypisze indeks najmniejszy. Przyjmujemy 0-based indexing.

        // Na wejściu, w kolejnych liniach (wartość nieokreślona) podane będą jednakowej długości ciągi liczb całkowitych (liczby jednocyfrowe, oddzielone jedną spacją).
        // Na wyjściu: jedna liczba całkowita określająca indeks kolumny o największej sumie (pierwszy).
        // Podpowiedź:

        // 1) ponieważ nie wiesz, ile wierszy będzie liczyła tablica danych, musisz skorzystać z dynamicznej struktury danych. Najwygodniejszą wydaje się lista tablic List<int[]>, której później można używać w podobny sposób, jak dla tablic dwuwymiarowych. Odczytując kolejne wiersze możesz je splitować do tablicy liczb całkowitych, a następnie tę tablicę doklejać do listy (Add). Gdy wczytasz wszystkie dane możesz odwoływać się do jej konkretnych elementów za pomocą indeksowania [i][j].

       public static void Main(string[] args)
{
        List<int[]> data = new List<int[]>();
        string line;
    
        while ((line = Console.ReadLine()) != null && line != "")
        {
            int[] row = line.Split(' ')
                        .Select(int.Parse)
                        .ToArray();
            data.Add(row);
        }

        int numberOfColumns = data[0].Length;
        int[] columnSums = new int[numberOfColumns];

        // Obliczanie sum dla każdej kolumny
        for (int col = 0; col < numberOfColumns; col++)
        {
            for (int row = 0; row < data.Count; row++)
            {
                columnSums[col] += data[row][col];
            }
        }

        // Znajdowanie indeksu największej sumy
        int maxSum = columnSums[0];
        int biggestIndex = 0;

        for (int i = 1; i < columnSums.Length; i++)
        {
            if (columnSums[i] > maxSum)
            {
                maxSum = columnSums[i];
                biggestIndex = i;
            }
        }

        Console.WriteLine(biggestIndex);
}
}
}
