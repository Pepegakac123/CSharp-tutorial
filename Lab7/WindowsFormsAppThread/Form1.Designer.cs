namespace WindowsFormsAppThread
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBarhighestNumberChecked = new System.Windows.Forms.TrackBar();
            this.textBoxNumberOfThreads = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxhighestNumberChecked = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarhighestNumberChecked)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(-22, -46);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(2);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(52, 45);
            this.trackBar1.TabIndex = 0;
            // 
            // trackBarhighestNumberChecked
            // 
            this.trackBarhighestNumberChecked.Location = new System.Drawing.Point(42, 77);
            this.trackBarhighestNumberChecked.Margin = new System.Windows.Forms.Padding(2);
            this.trackBarhighestNumberChecked.Maximum = 20;
            this.trackBarhighestNumberChecked.Name = "trackBarhighestNumberChecked";
            this.trackBarhighestNumberChecked.Size = new System.Drawing.Size(209, 45);
            this.trackBarhighestNumberChecked.TabIndex = 1;
            this.trackBarhighestNumberChecked.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // textBoxNumberOfThreads
            // 
            this.textBoxNumberOfThreads.Location = new System.Drawing.Point(27, 31);
            this.textBoxNumberOfThreads.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxNumberOfThreads.Name = "textBoxNumberOfThreads";
            this.textBoxNumberOfThreads.ReadOnly = true;
            this.textBoxNumberOfThreads.Size = new System.Drawing.Size(71, 20);
            this.textBoxNumberOfThreads.TabIndex = 3;
            this.textBoxNumberOfThreads.TextChanged += new System.EventHandler(this.textBoxNumberOfThreads_TextChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Number Of Threads";
            // 
            // textBoxhighestNumberChecked
            // 
            this.textBoxhighestNumberChecked.Location = new System.Drawing.Point(243, 31);
            this.textBoxhighestNumberChecked.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxhighestNumberChecked.Name = "textBoxhighestNumberChecked";
            this.textBoxhighestNumberChecked.ReadOnly = true;
            this.textBoxhighestNumberChecked.Size = new System.Drawing.Size(71, 20);
            this.textBoxhighestNumberChecked.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Highest Number Checked";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 194);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxhighestNumberChecked);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxNumberOfThreads);
            this.Controls.Add(this.trackBarhighestNumberChecked);
            this.Controls.Add(this.trackBar1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarhighestNumberChecked)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar trackBarhighestNumberChecked;
        private System.Windows.Forms.TextBox textBoxNumberOfThreads;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxhighestNumberChecked;
        private System.Windows.Forms.Label label2;
    }
}

