using Restauracja;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kuchnia
{
    public partial class Form1 : Form
    {
        string pathToOrderedFile = @"D:\Users\kacper.adamczyk.WSEI\CSharp-tutorial\RestauracjaDoDokonczenia\ordered.xml";
        string pathToReadyFile = @"D:\Users\kacper.adamczyk.WSEI\CSharp-tutorial\RestauracjaDoDokonczenia\ready.xml";
        List<OrderedProducts> orderedProducts = new List<OrderedProducts>();
        List<OrderedProducts> readyProducts = new List<OrderedProducts>();

        public Form1()
        {
            InitializeComponent();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                orderedProducts = OrderedProductsXmlSerializer.DeserializeListFromXml(pathToOrderedFile);
                RefreshProductList();
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index=listBoxOrdered.SelectedIndex;
            if (index>=0)
            {
                
                try
                {
                    readyProducts = OrderedProductsXmlSerializer.DeserializeListFromXml(pathToReadyFile);
                }
                catch
                {
                    readyProducts = new List<OrderedProducts>();
                }
                OrderedProducts selectedProduct = orderedProducts[index];
                readyProducts.Add(selectedProduct);

                orderedProducts.RemoveAt(index);

                OrderedProductsXmlSerializer.SerializeListToXml(orderedProducts, pathToOrderedFile);
                OrderedProductsXmlSerializer.SerializeListToXml(readyProducts, pathToReadyFile);

               
            }
            RefreshProductList();
        }

    }
}
