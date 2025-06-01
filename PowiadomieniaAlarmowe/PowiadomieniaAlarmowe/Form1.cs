using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowiadomieniaAlarmowe
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        delegate void deklaracjaZawiadomienia(string message);
        deklaracjaZawiadomienia zawiadomienie;

        private HashSet<string> wyslanePolicja = new HashSet<string>();
        private HashSet<string> wyslanePogotowie = new HashSet<string>();
        private HashSet<string> wyslaneStraz = new HashSet<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            if (zawiadomienie != null)
            {
                zawiadomienie(textBoxZawiadomienie.Text);
            }

        }
        void ZawiadomieniePolicji(string message)
        {
            if (!wyslanePolicja.Contains(message))
            {
                textBox_Policja.Text = message + "\r\n" + textBox_Policja.Text;
                wyslanePolicja.Add(message);
            }
        }

        void ZawiadomieniePogotowia(string message)
        {
            if (!wyslanePogotowie.Contains(message))
            {
                textBoxPogotowie.Text = message + "\r\n" + textBoxPogotowie.Text;
                wyslanePogotowie.Add(message);
            }
        }

        void ZawiadomienieStrazy(string message)
        {
            if (!wyslaneStraz.Contains(message))
            {
                textBoxStraz.Text = message + "\r\n" + textBoxStraz.Text;
                wyslaneStraz.Add(message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            zawiadomienie -= ZawiadomieniePolicji;
            zawiadomienie += ZawiadomieniePolicji;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            zawiadomienie -= ZawiadomieniePogotowia;
            zawiadomienie += ZawiadomieniePogotowia;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            zawiadomienie -= ZawiadomienieStrazy;
            zawiadomienie += ZawiadomienieStrazy;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            zawiadomienie -= ZawiadomieniePolicji;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            zawiadomienie -= ZawiadomieniePogotowia;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            zawiadomienie -= ZawiadomienieStrazy;
        }
    }
}
