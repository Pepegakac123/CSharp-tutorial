﻿namespace Kuchnia
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
            this.listBoxOrdered = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // listBoxOrdered
            // 
            this.listBoxOrdered.FormattingEnabled = true;
            this.listBoxOrdered.ItemHeight = 20;
            this.listBoxOrdered.Location = new System.Drawing.Point(33, 75);
            this.listBoxOrdered.Name = "listBoxOrdered";
            this.listBoxOrdered.Size = new System.Drawing.Size(764, 564);
            this.listBoxOrdered.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(876, 252);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(249, 39);
            this.button1.TabIndex = 9;
            this.button1.Text = "GotoweDoWydania";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1226, 691);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBoxOrdered);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxOrdered;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
    }
}

