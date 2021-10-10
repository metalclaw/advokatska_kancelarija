namespace Arhiviranje_dokumenata
{
    partial class PredmetiPoRadniku
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
            this.lblRadnik = new System.Windows.Forms.Label();
            this.cbRadnici = new System.Windows.Forms.ComboBox();
            this.olvPredmeti = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnZatvori = new System.Windows.Forms.Button();
            this.cbPrikaziArhivirane = new System.Windows.Forms.CheckBox();
            this.btnOdstampajListu = new System.Windows.Forms.Button();
            this.listViewPrinter1 = new BrightIdeasSoftware.ListViewPrinter();
            ((System.ComponentModel.ISupportInitialize)(this.olvPredmeti)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRadnik
            // 
            this.lblRadnik.AutoSize = true;
            this.lblRadnik.Location = new System.Drawing.Point(221, 18);
            this.lblRadnik.Name = "lblRadnik";
            this.lblRadnik.Size = new System.Drawing.Size(53, 13);
            this.lblRadnik.TabIndex = 14;
            this.lblRadnik.Text = "Zaposleni";
            // 
            // cbRadnici
            // 
            this.cbRadnici.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRadnici.FormattingEnabled = true;
            this.cbRadnici.Location = new System.Drawing.Point(107, 34);
            this.cbRadnici.Name = "cbRadnici";
            this.cbRadnici.Size = new System.Drawing.Size(276, 21);
            this.cbRadnici.TabIndex = 13;
            this.cbRadnici.SelectedIndexChanged += new System.EventHandler(this.cbRadnici_SelectedIndexChanged);
            // 
            // olvPredmeti
            // 
            this.olvPredmeti.AllColumns.Add(this.olvColumn1);
            this.olvPredmeti.AllColumns.Add(this.olvColumn2);
            this.olvPredmeti.AllColumns.Add(this.olvColumn3);
            this.olvPredmeti.AllColumns.Add(this.olvColumn4);
            this.olvPredmeti.CellEditUseWholeCell = false;
            this.olvPredmeti.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4});
            this.olvPredmeti.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvPredmeti.FullRowSelect = true;
            this.olvPredmeti.GridLines = true;
            this.olvPredmeti.Location = new System.Drawing.Point(12, 73);
            this.olvPredmeti.Name = "olvPredmeti";
            this.olvPredmeti.ShowGroups = false;
            this.olvPredmeti.ShowSortIndicators = false;
            this.olvPredmeti.Size = new System.Drawing.Size(522, 255);
            this.olvPredmeti.TabIndex = 12;
            this.olvPredmeti.UseCompatibleStateImageBehavior = false;
            this.olvPredmeti.View = System.Windows.Forms.View.Details;
            this.olvPredmeti.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.olvPredmeti_MouseDoubleClick);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "brojPredmeta";
            this.olvColumn1.Text = "Broj predmeta";
            this.olvColumn1.Width = 80;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "stranka";
            this.olvColumn2.Text = "Stranka";
            this.olvColumn2.Width = 180;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "textEvidencije";
            this.olvColumn3.Text = "Tekst evidencije";
            this.olvColumn3.Width = 150;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "datumEvidencije";
            this.olvColumn4.Text = "Datum evidencije";
            this.olvColumn4.Width = 105;
            // 
            // btnZatvori
            // 
            this.btnZatvori.Image = global::Arhiviranje_dokumenata.Properties.Resources.Close_16x;
            this.btnZatvori.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnZatvori.Location = new System.Drawing.Point(420, 346);
            this.btnZatvori.Name = "btnZatvori";
            this.btnZatvori.Size = new System.Drawing.Size(69, 23);
            this.btnZatvori.TabIndex = 11;
            this.btnZatvori.Text = "Zatvori";
            this.btnZatvori.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnZatvori.UseVisualStyleBackColor = true;
            this.btnZatvori.Click += new System.EventHandler(this.btnZatvori_Click);
            // 
            // cbPrikaziArhivirane
            // 
            this.cbPrikaziArhivirane.AutoSize = true;
            this.cbPrikaziArhivirane.Location = new System.Drawing.Point(420, 29);
            this.cbPrikaziArhivirane.Name = "cbPrikaziArhivirane";
            this.cbPrikaziArhivirane.Size = new System.Drawing.Size(114, 30);
            this.cbPrikaziArhivirane.TabIndex = 15;
            this.cbPrikaziArhivirane.Text = "Prikaži i arhivirane \r\npredmete";
            this.cbPrikaziArhivirane.UseVisualStyleBackColor = true;
            this.cbPrikaziArhivirane.CheckedChanged += new System.EventHandler(this.cbPrikaziArhivirane_CheckedChanged);
            // 
            // btnOdstampajListu
            // 
            this.btnOdstampajListu.Image = global::Arhiviranje_dokumenata.Properties.Resources.Print_16x;
            this.btnOdstampajListu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOdstampajListu.Location = new System.Drawing.Point(60, 346);
            this.btnOdstampajListu.Name = "btnOdstampajListu";
            this.btnOdstampajListu.Size = new System.Drawing.Size(110, 23);
            this.btnOdstampajListu.TabIndex = 16;
            this.btnOdstampajListu.Text = "Odštampaj listu";
            this.btnOdstampajListu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOdstampajListu.UseVisualStyleBackColor = true;
            this.btnOdstampajListu.Click += new System.EventHandler(this.btnOdstampajListu_Click);
            // 
            // listViewPrinter1
            // 
            // 
            // 
            // 
            this.listViewPrinter1.CellFormat.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.listViewPrinter1.CellFormat.BottomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.listViewPrinter1.CellFormat.BottomBorderWidth = 0.5F;
            this.listViewPrinter1.CellFormat.CanWrap = true;
            this.listViewPrinter1.CellFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.listViewPrinter1.CellFormat.LeftBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.listViewPrinter1.CellFormat.LeftBorderWidth = 0.5F;
            this.listViewPrinter1.CellFormat.MinimumTextHeight = 0F;
            this.listViewPrinter1.CellFormat.RightBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.listViewPrinter1.CellFormat.RightBorderWidth = 0.5F;
            this.listViewPrinter1.CellFormat.TextColor = System.Drawing.Color.Empty;
            this.listViewPrinter1.CellFormat.TopBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.listViewPrinter1.CellFormat.TopBorderWidth = 0.5F;
            // 
            // 
            // 
            this.listViewPrinter1.FooterFormat.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.listViewPrinter1.FooterFormat.BottomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.listViewPrinter1.FooterFormat.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Italic);
            this.listViewPrinter1.FooterFormat.LeftBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.listViewPrinter1.FooterFormat.MinimumTextHeight = 0F;
            this.listViewPrinter1.FooterFormat.RightBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.listViewPrinter1.FooterFormat.TextColor = System.Drawing.Color.Black;
            this.listViewPrinter1.FooterFormat.TopBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.listViewPrinter1.FooterFormat.TopBorderWidth = 0.5F;
            // 
            // 
            // 
            this.listViewPrinter1.GroupHeaderFormat.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.listViewPrinter1.GroupHeaderFormat.BottomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.listViewPrinter1.GroupHeaderFormat.BottomBorderWidth = 3F;
            this.listViewPrinter1.GroupHeaderFormat.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.listViewPrinter1.GroupHeaderFormat.LeftBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.listViewPrinter1.GroupHeaderFormat.MinimumTextHeight = 0F;
            this.listViewPrinter1.GroupHeaderFormat.RightBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.listViewPrinter1.GroupHeaderFormat.TextColor = System.Drawing.Color.Black;
            this.listViewPrinter1.GroupHeaderFormat.TopBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            this.listViewPrinter1.HeaderFormat.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.listViewPrinter1.HeaderFormat.BottomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.listViewPrinter1.HeaderFormat.Font = new System.Drawing.Font("Verdana", 24F);
            this.listViewPrinter1.HeaderFormat.LeftBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.listViewPrinter1.HeaderFormat.MinimumTextHeight = 0F;
            this.listViewPrinter1.HeaderFormat.RightBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.listViewPrinter1.HeaderFormat.TextColor = System.Drawing.Color.WhiteSmoke;
            this.listViewPrinter1.HeaderFormat.TopBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.listViewPrinter1.IsListHeaderOnEachPage = false;
            this.listViewPrinter1.IsShrinkToFit = true;
            this.listViewPrinter1.ListFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.listViewPrinter1.ListGridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.listViewPrinter1.ListHeaderFormat.BackgroundColor = System.Drawing.Color.LightGray;
            this.listViewPrinter1.ListHeaderFormat.BottomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.listViewPrinter1.ListHeaderFormat.BottomBorderWidth = 1.5F;
            this.listViewPrinter1.ListHeaderFormat.CanWrap = true;
            this.listViewPrinter1.ListHeaderFormat.Font = new System.Drawing.Font("Verdana", 12F);
            this.listViewPrinter1.ListHeaderFormat.LeftBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.listViewPrinter1.ListHeaderFormat.LeftBorderWidth = 1.5F;
            this.listViewPrinter1.ListHeaderFormat.MinimumTextHeight = 0F;
            this.listViewPrinter1.ListHeaderFormat.RightBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.listViewPrinter1.ListHeaderFormat.RightBorderWidth = 1.5F;
            this.listViewPrinter1.ListHeaderFormat.TextColor = System.Drawing.Color.Black;
            this.listViewPrinter1.ListHeaderFormat.TopBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.listViewPrinter1.ListHeaderFormat.TopBorderWidth = 1.5F;
            this.listViewPrinter1.ListView = this.olvPredmeti;
            this.listViewPrinter1.WatermarkColor = System.Drawing.Color.Empty;
            // 
            // PredmetiPoRadniku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 381);
            this.Controls.Add(this.btnOdstampajListu);
            this.Controls.Add(this.cbPrikaziArhivirane);
            this.Controls.Add(this.lblRadnik);
            this.Controls.Add(this.cbRadnici);
            this.Controls.Add(this.olvPredmeti);
            this.Controls.Add(this.btnZatvori);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PredmetiPoRadniku";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pretraga predmeta po imenu zaposlenog";
            ((System.ComponentModel.ISupportInitialize)(this.olvPredmeti)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRadnik;
        private System.Windows.Forms.ComboBox cbRadnici;
        private BrightIdeasSoftware.ObjectListView olvPredmeti;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private System.Windows.Forms.Button btnZatvori;
        private System.Windows.Forms.CheckBox cbPrikaziArhivirane;
        private System.Windows.Forms.Button btnOdstampajListu;
        private BrightIdeasSoftware.ListViewPrinter listViewPrinter1;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
    }
}