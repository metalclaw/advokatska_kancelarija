namespace Arhiviranje_dokumenata
{
    partial class FinansijeDetalji
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FinansijeDetalji));
            this.btnObrisi = new System.Windows.Forms.Button();
            this.btnSnimi = new System.Windows.Forms.Button();
            this.btnZatvori = new System.Windows.Forms.Button();
            this.btnPrintBeleske = new System.Windows.Forms.Button();
            this.btnBold = new System.Windows.Forms.Button();
            this.cbPlaceno = new System.Windows.Forms.CheckBox();
            this.rtbBeleske = new System.Windows.Forms.RichTextBoxPrintCtrl();
            this.SuspendLayout();
            // 
            // btnObrisi
            // 
            this.btnObrisi.Enabled = false;
            this.btnObrisi.Location = new System.Drawing.Point(246, 406);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(112, 23);
            this.btnObrisi.TabIndex = 50;
            this.btnObrisi.Text = "Obriši";
            this.btnObrisi.UseVisualStyleBackColor = true;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // btnSnimi
            // 
            this.btnSnimi.Location = new System.Drawing.Point(12, 406);
            this.btnSnimi.Name = "btnSnimi";
            this.btnSnimi.Size = new System.Drawing.Size(112, 23);
            this.btnSnimi.TabIndex = 47;
            this.btnSnimi.Text = "Snimi";
            this.btnSnimi.UseVisualStyleBackColor = true;
            this.btnSnimi.Click += new System.EventHandler(this.btnSnimi_Click);
            // 
            // btnZatvori
            // 
            this.btnZatvori.Location = new System.Drawing.Point(505, 406);
            this.btnZatvori.Name = "btnZatvori";
            this.btnZatvori.Size = new System.Drawing.Size(75, 23);
            this.btnZatvori.TabIndex = 51;
            this.btnZatvori.Text = "Zatvori";
            this.btnZatvori.UseVisualStyleBackColor = true;
            this.btnZatvori.Click += new System.EventHandler(this.btnZatvori_Click);
            // 
            // btnPrintBeleske
            // 
            this.btnPrintBeleske.Image = global::Arhiviranje_dokumenata.Properties.Resources.Print_16x;
            this.btnPrintBeleske.Location = new System.Drawing.Point(527, 25);
            this.btnPrintBeleske.Name = "btnPrintBeleske";
            this.btnPrintBeleske.Size = new System.Drawing.Size(24, 23);
            this.btnPrintBeleske.TabIndex = 52;
            this.btnPrintBeleske.UseVisualStyleBackColor = true;
            this.btnPrintBeleske.Click += new System.EventHandler(this.btnPrintBeleske_Click);
            // 
            // btnBold
            // 
            this.btnBold.Image = ((System.Drawing.Image)(resources.GetObject("btnBold.Image")));
            this.btnBold.Location = new System.Drawing.Point(557, 25);
            this.btnBold.Name = "btnBold";
            this.btnBold.Size = new System.Drawing.Size(24, 23);
            this.btnBold.TabIndex = 48;
            this.btnBold.UseVisualStyleBackColor = true;
            // 
            // cbPlaceno
            // 
            this.cbPlaceno.AutoSize = true;
            this.cbPlaceno.Location = new System.Drawing.Point(22, 25);
            this.cbPlaceno.Name = "cbPlaceno";
            this.cbPlaceno.Size = new System.Drawing.Size(65, 17);
            this.cbPlaceno.TabIndex = 53;
            this.cbPlaceno.Text = "Plaćeno";
            this.cbPlaceno.UseVisualStyleBackColor = true;
            // 
            // rtbBeleske
            // 
            this.rtbBeleske.Location = new System.Drawing.Point(12, 54);
            this.rtbBeleske.Name = "rtbBeleske";
            this.rtbBeleske.Size = new System.Drawing.Size(569, 346);
            this.rtbBeleske.TabIndex = 46;
            this.rtbBeleske.Text = "";
            this.rtbBeleske.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbBeleske_KeyDown);
            // 
            // FinansijeDetalji
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 450);
            this.Controls.Add(this.cbPlaceno);
            this.Controls.Add(this.btnPrintBeleske);
            this.Controls.Add(this.btnZatvori);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.btnBold);
            this.Controls.Add(this.btnSnimi);
            this.Controls.Add(this.rtbBeleske);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FinansijeDetalji";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalji";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.Button btnBold;
        private System.Windows.Forms.Button btnSnimi;
        private System.Windows.Forms.RichTextBoxPrintCtrl rtbBeleske;
        private System.Windows.Forms.Button btnZatvori;
        private System.Windows.Forms.Button btnPrintBeleske;
        private System.Windows.Forms.CheckBox cbPlaceno;
    }
}