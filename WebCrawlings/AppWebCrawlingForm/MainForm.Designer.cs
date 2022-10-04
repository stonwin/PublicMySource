
namespace AppWebCrawlingForm
{
    partial class MainForm
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
            this.btnDaum = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDaum
            // 
            this.btnDaum.Location = new System.Drawing.Point(23, 12);
            this.btnDaum.Name = "btnDaum";
            this.btnDaum.Size = new System.Drawing.Size(133, 71);
            this.btnDaum.TabIndex = 0;
            this.btnDaum.Text = "다음사이트";
            this.btnDaum.UseVisualStyleBackColor = true;
            this.btnDaum.Click += new System.EventHandler(this.btnDaum_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(162, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 71);
            this.button1.TabIndex = 1;
            this.button1.Text = "안양도시공사";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 349);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDaum);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDaum;
        private System.Windows.Forms.Button button1;
    }
}