namespace Arhiviranje_dokumenata.Message_Boxes
{
    partial class TraziSifru
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.btnPrihvati = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ovo je deo programa je zaključan. \r\nUnesite šifru da bi mu pristupili";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbPass
            // 
            this.tbPass.Location = new System.Drawing.Point(42, 75);
            this.tbPass.Name = "tbPass";
            this.tbPass.PasswordChar = '*';
            this.tbPass.Size = new System.Drawing.Size(237, 20);
            this.tbPass.TabIndex = 1;
            this.tbPass.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbPass_KeyUp);
            // 
            // btnPrihvati
            // 
            this.btnPrihvati.Location = new System.Drawing.Point(42, 113);
            this.btnPrihvati.Name = "btnPrihvati";
            this.btnPrihvati.Size = new System.Drawing.Size(75, 23);
            this.btnPrihvati.TabIndex = 2;
            this.btnPrihvati.Text = "Prihvati";
            this.btnPrihvati.UseVisualStyleBackColor = true;
            this.btnPrihvati.Click += new System.EventHandler(this.btnPrihvati_Click);
            // 
            // btnOdustani
            // 
            this.btnOdustani.Location = new System.Drawing.Point(204, 113);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(75, 23);
            this.btnOdustani.TabIndex = 3;
            this.btnOdustani.Text = "Odustani";
            this.btnOdustani.UseVisualStyleBackColor = true;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // TraziSifru
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 154);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnPrihvati);
            this.Controls.Add(this.tbPass);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TraziSifru";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unesite šifru";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPass;
        private System.Windows.Forms.Button btnPrihvati;
        private System.Windows.Forms.Button btnOdustani;
    }
}