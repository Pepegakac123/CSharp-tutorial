namespace WinForms_Lab1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBoxDataIn1 = new TextBox();
            textBoxDataIn2 = new TextBox();
            textBoxDataOut = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // textBoxDataIn1
            // 
            textBoxDataIn1.Location = new Point(55, 35);
            textBoxDataIn1.Name = "textBoxDataIn1";
            textBoxDataIn1.Size = new Size(100, 23);
            textBoxDataIn1.TabIndex = 0;
            // 
            // textBoxDataIn2
            // 
            textBoxDataIn2.Location = new Point(55, 75);
            textBoxDataIn2.Name = "textBoxDataIn2";
            textBoxDataIn2.Size = new Size(100, 23);
            textBoxDataIn2.TabIndex = 1;
            // 
            // textBoxDataOut
            // 
            textBoxDataOut.Location = new Point(599, 50);
            textBoxDataOut.Name = "textBoxDataOut";
            textBoxDataOut.Size = new Size(100, 23);
            textBoxDataOut.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(272, 15);
            button1.Name = "button1";
            button1.Size = new Size(23, 24);
            button1.TabIndex = 3;
            button1.Text = "+";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button_plus;
            // 
            // button2
            // 
            button2.Location = new Point(272, 45);
            button2.Name = "button2";
            button2.Size = new Size(23, 24);
            button2.TabIndex = 4;
            button2.Text = "-";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button_minus;
            // 
            // button3
            // 
            button3.Location = new Point(272, 75);
            button3.Name = "button3";
            button3.Size = new Size(23, 24);
            button3.TabIndex = 5;
            button3.Text = "*";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button_multiply;
            // 
            // button4
            // 
            button4.Location = new Point(272, 105);
            button4.Name = "button4";
            button4.Size = new Size(23, 24);
            button4.TabIndex = 6;
            button4.Text = "/";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button_divide;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBoxDataOut);
            Controls.Add(textBoxDataIn2);
            Controls.Add(textBoxDataIn1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxDataIn1;
        private TextBox textBoxDataIn2;
        private TextBox textBoxDataOut;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}
