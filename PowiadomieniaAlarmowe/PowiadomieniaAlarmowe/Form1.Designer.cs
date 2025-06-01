namespace PowiadomieniaAlarmowe
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxZawiadomienie = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox_Policja = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.textBoxPogotowie = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.textBoxStraz = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxZawiadomienie
            // 
            this.textBoxZawiadomienie.Location = new System.Drawing.Point(13, 13);
            this.textBoxZawiadomienie.Multiline = true;
            this.textBoxZawiadomienie.Name = "textBoxZawiadomienie";
            this.textBoxZawiadomienie.Size = new System.Drawing.Size(825, 157);
            this.textBoxZawiadomienie.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 176);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(825, 29);
            this.button1.TabIndex = 1;
            this.button1.Text = "Wyslij Powiadomienie";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox_Policja
            // 
            this.textBox_Policja.Location = new System.Drawing.Point(13, 355);
            this.textBox_Policja.Multiline = true;
            this.textBox_Policja.Name = "textBox_Policja";
            this.textBox_Policja.Size = new System.Drawing.Size(271, 225);
            this.textBox_Policja.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 288);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "+ Policja";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(13, 317);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "- Policja";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(290, 317);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "- Pogotowei";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(290, 288);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 6;
            this.button5.Text = "+ Pogotowie";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBoxPogotowie
            // 
            this.textBoxPogotowie.Location = new System.Drawing.Point(290, 355);
            this.textBoxPogotowie.Multiline = true;
            this.textBoxPogotowie.Name = "textBoxPogotowie";
            this.textBoxPogotowie.Size = new System.Drawing.Size(271, 225);
            this.textBoxPogotowie.TabIndex = 5;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(567, 317);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(113, 23);
            this.button6.TabIndex = 10;
            this.button6.Text = "- Straz Pozarna";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(567, 288);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(113, 23);
            this.button7.TabIndex = 9;
            this.button7.Text = "+ Straz Pozarna";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // textBoxStraz
            // 
            this.textBoxStraz.Location = new System.Drawing.Point(567, 355);
            this.textBoxStraz.Multiline = true;
            this.textBoxStraz.Name = "textBoxStraz";
            this.textBoxStraz.Size = new System.Drawing.Size(271, 225);
            this.textBoxStraz.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 592);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.textBoxStraz);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.textBoxPogotowie);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox_Policja);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxZawiadomienie);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxZawiadomienie;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox_Policja;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBoxPogotowie;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox textBoxStraz;
    }
}

