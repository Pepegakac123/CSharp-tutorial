using System;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace WindowsFormsAppThread
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<Thread> listaWatkow = new List<Thread>();
        private List<bool> flagiZatrzymania = new List<bool>();
        private static Object lockForWatki = new Object();

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            iloscWatkowZadana = trackBarhighestNumberChecked.Value;

            lock (lockForWatki)
            {

                if (iloscWatkowZadana > iloscWatkowUruchomiona)
                {
                    int watkowDoUruchomienia = iloscWatkowZadana - iloscWatkowUruchomiona;

                    for (int i = 0; i < watkowDoUruchomienia; i++)
                    {

                        flagiZatrzymania.Add(false);


                        int bazaNumerow = iloscWatkowUruchomiona;
                        int numerWatku = bazaNumerow + i + 1;
                        // Przekazujemy: numerWatku (do identyfikacji) i indeks flagi
                        Thread nowyWatek = new Thread(() => CialoFunkcjiWatku(numerWatku, flagiZatrzymania.Count - 1));
                        nowyWatek.Start();

                        listaWatkow.Add(nowyWatek);
                        iloscWatkowUruchomiona++;
                    }
                }
                else if (iloscWatkowZadana < iloscWatkowUruchomiona)
                {
                    int watkowDoZatrzymania = iloscWatkowUruchomiona - iloscWatkowZadana;

                    for (int i = 0; i < watkowDoZatrzymania; i++)
                    {
                        if (iloscWatkowUruchomiona > 0)
                        {
                            flagiZatrzymania[iloscWatkowUruchomiona - 1] = true;
                            iloscWatkowUruchomiona--;
                        }
                    }
                }
            }
        }

        static long highestNumberChecked = 1;
        private static Object lockForNr = new Object();

        private void button1_Click(object sender, EventArgs e)
        {
            if (LabWsei.IsPrime(highestNumberChecked) == true)
            {
                LabWsei.WriteInToFileLiczbyPierwsze(highestNumberChecked, 0);
            }
            highestNumberChecked++;
        }

        int iloscWatkowUruchomiona = 0;
        int iloscWatkowZadana = 0;

        private void CialoFunkcjiWatku(int numerWatku, int indeksFlagI)
        {
            long lokalnyNumer;

            while (indeksFlagI < flagiZatrzymania.Count && !flagiZatrzymania[indeksFlagI])
            {
                lock (lockForNr)
                {
                    lokalnyNumer = highestNumberChecked;
                    highestNumberChecked++; 
                }

                if (LabWsei.IsPrime(lokalnyNumer))
                {
                    LabWsei.WriteInToFileLiczbyPierwsze(lokalnyNumer, numerWatku);
                }

                Thread.Sleep(1);
                if (indeksFlagI < flagiZatrzymania.Count && flagiZatrzymania[indeksFlagI])
                    break;
            }

            lock (lockForWatki)
            {
                Thread currentThread = Thread.CurrentThread;
                int index = listaWatkow.IndexOf(currentThread);
                if (index >= 0)
                {
                    listaWatkow.RemoveAt(index);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Wyświetlamy naszą kontrolowaną liczbę wątków
            textBoxNumberOfThreads.Text = iloscWatkowUruchomiona.ToString();
            lock (lockForNr)
            {
                textBoxhighestNumberChecked.Text = highestNumberChecked.ToString();
            }
        }
        private void textBoxNumberOfThreads_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
