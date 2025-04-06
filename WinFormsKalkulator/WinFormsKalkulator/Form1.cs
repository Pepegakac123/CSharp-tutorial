namespace WinFormsKalkulator
{
    public partial class Form1 : Form
    {
        string value = "";
        double result = 0;
        string currentOperation = "";
        bool isNewOperation = true;
        bool isAfterCalculation = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isAfterCalculation)
            {
                value = "";
                isAfterCalculation = false;
            }
            value = value + "2";
            textBox1.Text = value;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (isAfterCalculation)
            {
                value = "";
                isAfterCalculation = false;
            }
            value = value + "9";
            textBox1.Text = value;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (isAfterCalculation)
            {
                value = "";
                isAfterCalculation = false;
            }
            value = value + "0";
            textBox1.Text = value;
        }

        private void plus(object sender, EventArgs e)
        {
            PerformPendingOperation();
            currentOperation = "+";
            isNewOperation = false;
            isAfterCalculation = true;
        }

        private void divide(object sender, EventArgs e)
        {
            PerformPendingOperation();
            currentOperation = "/";
            isNewOperation = false;
            isAfterCalculation = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isAfterCalculation)
            {
                value = "";
                isAfterCalculation = false;
            }
            value = value + "1";
            textBox1.Text = value;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (isAfterCalculation)
            {
                value = "";
                isAfterCalculation = false;
            }
            value = value + "3";
            textBox1.Text = value;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (isAfterCalculation)
            {
                value = "";
                isAfterCalculation = false;
            }
            value = value + "4";
            textBox1.Text = value;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (isAfterCalculation)
            {
                value = "";
                isAfterCalculation = false;
            }
            value = value + "5";
            textBox1.Text = value;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (isAfterCalculation)
            {
                value = "";
                isAfterCalculation = false;
            }
            value = value + "6";
            textBox1.Text = value;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (isAfterCalculation)
            {
                value = "";
                isAfterCalculation = false;
            }
            value = value + "7";
            textBox1.Text = value;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (isAfterCalculation)
            {
                value = "";
                isAfterCalculation = false;
            }
            value = value + "8";
            textBox1.Text = value;
        }

        private void minus(object sender, EventArgs e)
        {
            PerformPendingOperation();
            currentOperation = "-";
            isNewOperation = false;
            isAfterCalculation = true;
        }

        private void multiply(object sender, EventArgs e)
        {
            PerformPendingOperation();
            currentOperation = "*";
            isNewOperation = false;
            isAfterCalculation = true;
        }



        private void PerformPendingOperation()
        {
            if (!string.IsNullOrEmpty(value))
            {
                double inputValue = double.Parse(value);

                if (isNewOperation)
                {
                    result = inputValue;
                    isNewOperation = false;
                }
                else
                {
                    switch (currentOperation)
                    {
                        case "+":
                            result += inputValue;
                            break;
                        case "-":
                            result -= inputValue;
                            break;
                        case "*":
                            result *= inputValue;
                            break;
                        case "/":
                            if (inputValue != 0)
                            {
                                result /= inputValue;
                            }
                            else
                            {
                                MessageBox.Show("Nie mo¿na dzieliæ przez zero!", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            break;
                    }
                }

                value = "";
                textBox1.Text = result.ToString();
            }
        }

        private void equals(object sender, EventArgs e)
        {
            PerformPendingOperation();
            currentOperation = "";
            isNewOperation = true;
            isAfterCalculation = true;
        }
    }
}