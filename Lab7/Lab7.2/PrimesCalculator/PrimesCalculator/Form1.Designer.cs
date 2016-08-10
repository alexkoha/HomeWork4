namespace PrimesCalculator
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
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.first = new System.Windows.Forms.TextBox();
            this.last = new System.Windows.Forms.TextBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Calculate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 101);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(151, 316);
            this.listBox1.TabIndex = 1;
            // 
            // first
            // 
            this.first.Location = new System.Drawing.Point(13, 13);
            this.first.Name = "first";
            this.first.Size = new System.Drawing.Size(100, 20);
            this.first.TabIndex = 2;
            this.first.Text = "First ";
            // 
            // last
            // 
            this.last.Location = new System.Drawing.Point(12, 40);
            this.last.Name = "last";
            this.last.Size = new System.Drawing.Size(100, 20);
            this.last.TabIndex = 3;
            this.last.Text = "Last";
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(95, 66);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(68, 23);
            this.Cancel.TabIndex = 4;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(175, 429);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.last);
            this.Controls.Add(this.first);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox first;
        private System.Windows.Forms.TextBox last;
        private System.Windows.Forms.Button Cancel;
    }
}

