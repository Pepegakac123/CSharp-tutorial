using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restauracja
{
    public partial class Form1 : Form
    {
        string pathToOrderedFile = @"D:\Users\kacper.adamczyk.WSEI\CSharp-tutorial\RestauracjaDoDokonczenia\ordered.xml";
        List<OrderedProducts> orderedProducts = new List<OrderedProducts>();
        public Form1()
        {
            InitializeComponent();
            try
            {
                orderedProducts = OrderedProductsXmlSerializer.DeserializeListFromXml(pathToOrderedFile);
                RefreshProductList();
            }
            catch { }
        }
        
        void RefreshProductList()
        {
            listBoxOrdered.Items.Clear();
            foreach (OrderedProducts product in orderedProducts)
            {
                string strToAdd = product.tableNr.ToString() + ".   " + product.productName + ":    " + product.price.ToString() + "zł";
                listBoxOrdered.Items.Add(strToAdd);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrderedProducts productToAdd;
            productToAdd.price = numericUpDownPrice.Value;
            productToAdd.tableNr = (uint)numericUpDownTableNr.Value;
            productToAdd.productName = textBoxName.Text;
            orderedProducts.Add(productToAdd);

            RefreshProductList();

            OrderedProductsXmlSerializer.SerializeListToXml(orderedProducts,pathToOrderedFile);
        }


        List<OrderedProducts> readyProducts = new List<OrderedProducts>();

        void RefreshReadyProductList()
        {
            listBoxReady.Items.Clear();
            foreach (OrderedProducts product in readyProducts)
            {
                string strToAdd = product.tableNr.ToString() + ".   " + product.productName + ":    " + product.price.ToString() + "zł";
                listBoxReady.Items.Add(strToAdd);
            }
        }

        string pathToReadyFile = @"D:\Users\kacper.adamczyk.WSEI\CSharp-tutorial\RestauracjaDoDokonczenia\ready.xml";

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                readyProducts = OrderedProductsXmlSerializer.DeserializeListFromXml(pathToReadyFile);
                RefreshReadyProductList();
            }
            catch { }
        }


        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            uint selectedTable = (uint)numericUpDown1.Value;
            decimal totalForTable = CalculateTotalForTable(selectedTable);

            MessageBox.Show(
                $"Suma rachunku dla stolika {selectedTable}: {totalForTable}zł",
                "Rachunek",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            uint selectedTable = (uint)numericUpDown1.Value;
            decimal totalForTable = CalculateTotalForTable(selectedTable);
            decimal paymentAmount = numericUpDown2.Value;

            if (paymentAmount < totalForTable)
            {
                MessageBox.Show(
                    $"Wprowadzona kwota ({paymentAmount}zł) jest mniejsza niż suma rachunku ({totalForTable}zł).\n" +
                    $"Brakuje: {totalForTable - paymentAmount}zł",
                    "Niewystarczająca kwota",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return; 
            }
            DialogResult result = MessageBox.Show(
                $"Czy na pewno opłacić rachunek dla stolika {selectedTable} w wysokości {totalForTable}zł?",
                "Potwierdzenie płatności",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                orderedProducts.RemoveAll(p => p.tableNr == selectedTable);

                readyProducts.RemoveAll(p => p.tableNr == selectedTable);
                OrderedProductsXmlSerializer.SerializeListToXml(orderedProducts, pathToOrderedFile);
                OrderedProductsXmlSerializer.SerializeListToXml(readyProducts, pathToReadyFile);


                RefreshProductList();
                RefreshReadyProductList();

                MessageBox.Show(
                    $"Rachunek dla stolika {selectedTable} w wysokości {totalForTable}zł został opłacony.",
                    "Rachunek opłacony",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            uint selectedTable = (uint)numericUpDown1.Value;
            decimal totalForTable = CalculateTotalForTable(selectedTable);
            decimal paymentAmount = numericUpDown2.Value;
        }

        private decimal CalculateTotalForTable(uint tableNumber)
        {
            decimal total = 0;

            foreach (OrderedProducts product in orderedProducts)
            {
                if (product.tableNr == tableNumber)
                {
                    total += product.price;
                }
            }

            foreach (OrderedProducts product in readyProducts)
            {
                if (product.tableNr == tableNumber)
                {
                    total += product.price;
                }
            }

            return total;
        }

    }
}
