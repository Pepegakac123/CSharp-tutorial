using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppThread
{
    public class LabWsei
    {
        // Funkcja do sprawdzania czy liczba jest pierwsza
        public static bool IsPrime(long number)
        {
            if (number <= 1)
            {
                return false;
            }
            if (number <= 3)
            {
                return true;
            }
            if (number % 2 == 0 || number % 3 == 0)
            {
                return false;
            }

            int i = 5;
            while (i * i <= number)
            {
                if (number % i == 0 || number % (i + 2) == 0)
                {
                    return false;
                }
                i += 6;
            }
            return true;
        }

        private static Object fileLock = new Object();
        public static void WriteInToFileLiczbyPierwsze(long liczba, int nrWatku)
        {
            lock (fileLock)
            {
                string textDoDodania = liczba.ToString() + " sprawdzone przez:" + nrWatku.ToString() + "\r\n";
                File.AppendAllText("znalezioneLiczbyPierwze.txt", textDoDodania);
            }
        }

        public static void WriteInToFileLiczbyPierwsze(long liczba)
        {
            lock (fileLock)
            {
                string textDoDodania = liczba.ToString() + "\r\n";
                File.AppendAllText("znalezioneLiczbyPierwze.txt", textDoDodania);
            }
        }

        public static long GetCountOfAllRunedThread()
        {
            Process currentProcess = Process.GetCurrentProcess();
            ProcessThreadCollection processCollection = currentProcess.Threads;
            return processCollection.Count;
        }
    }
}
