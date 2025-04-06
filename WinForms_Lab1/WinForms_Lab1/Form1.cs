namespace WinForms_Lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_plus(object sender, EventArgs e)
        {
            if (double.TryParse(textBoxDataIn1.Text, out double data1))
            {
                if (double.TryParse(textBoxDataIn2.Text, out double data2))
                {
                    double result = data1 + data2;

                    textBoxDataOut.Text = result.ToString();
                    return;
                }
            }
            textBoxDataOut.Text = "Error";
        }


        private void button_minus(object sender, EventArgs e)
        {
            if (double.TryParse(textBoxDataIn1.Text, out double data1))
            {
                if (double.TryParse(textBoxDataIn2.Text, out double data2))
                {
                    double result = data1 - data2;

                    textBoxDataOut.Text = result.ToString();
                    return;
                }
            }
            textBoxDataOut.Text = "Error";
        }

        private void button_multiply(object sender, EventArgs e)
        {
            if (double.TryParse(textBoxDataIn1.Text, out double data1))
            {
                if (double.TryParse(textBoxDataIn2.Text, out double data2))
                {
                    double result = data1 * data2;

                    textBoxDataOut.Text = result.ToString();
                    return;
                }
            }
            textBoxDataOut.Text = "Error";
        }

        private void button_divide(object sender, EventArgs e)
        {
            if (double.TryParse(textBoxDataIn1.Text, out double data1))
            {
                if (double.TryParse(textBoxDataIn2.Text, out double data2))
                {
                    double result = data1 / data2;

                    textBoxDataOut.Text = result.ToString();
                    return;
                }
            }
            textBoxDataOut.Text = "Error";
        }
    }
}
