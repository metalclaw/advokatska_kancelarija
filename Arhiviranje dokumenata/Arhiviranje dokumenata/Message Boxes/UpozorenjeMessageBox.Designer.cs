namespace Arhiviranje_dokumenata
{
    partial class UpozorenjeMessageBox
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
            this.btnZatvori = new System.Windows.Forms.Button();
            this.btnSnimi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(266, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Izmene nisu snimljenje, da li ste sigurni da želite \r\nda zatvorite predmet bez pr" +
    "ethodnog snimanja izmena?";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnZatvori
            // 
            this.btnZatvori.Location = new System.Drawing.Point(20, 87);
            this.btnZatvori.Name = "btnZatvori";
            this.btnZatvori.Size = new System.Drawing.Size(132, 23);
            this.btnZatvori.TabIndex = 2;
            this.btnZatvori.Text = "Zatvori bez snimanja";
            this.btnZatvori.UseVisualStyleBackColor = true;
            this.btnZatvori.Click += new System.EventHandler(this.btnZatvori_Click);
            // 
            // btnSnimi
            // 
            this.btnSnimi.Location = new System.Drawing.Point(199, 87);
            this.btnSnimi.Name = "btnSnimi";
            this.btnSnimi.Size = new System.Drawing.Size(132, 23);
            this.btnSnimi.TabIndex = 1;
            this.btnSnimi.Text = "Snimi pre zatvaranja";
            this.btnSnimi.UseVisualStyleBackColor = true;
            this.btnSnimi.Click += new System.EventHandler(this.btnSnimi_Click);
            // 
            // UpozorenjeMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(359, 132);
            this.Controls.Add(this.btnSnimi);
            this.Controls.Add(this.btnZatvori);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpozorenjeMessageBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Upozorenje!";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnZatvori;
        private System.Windows.Forms.Button btnSnimi;
    }
}