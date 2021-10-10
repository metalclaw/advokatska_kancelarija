namespace Arhiviranje_dokumenata
{
    partial class Predmeti_po_kategoriji
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
            this.btnZatvori = new System.Windows.Forms.Button();
            this.olvPredmeti = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnOdstampajEvidencije = new System.Windows.Forms.Button();
            this.cbKategorije = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbAktivni = new System.Windows.Forms.RadioButton();
            this.rbArhivirani = new System.Windows.Forms.RadioButton();
            this.rbSvi = new System.Windows.Forms.RadioButton();
            this.listViewPrinter1 = new BrightIdeasSoftware.ListViewPrinter();
            ((System.ComponentModel.ISupportInitialize)(this.olvPredmeti)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnZatvori
            // 
            this.btnZatvori.Image = global::Arhiviranje_dokumenata.Properties.Resources.Close_16x;
            this.btnZatvori.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnZatvori.Location = new System.Drawing.Point(388, 345);
            this.btnZatvori.Name = "btnZatvori";
            this.btnZatvori.Size = new System.Drawing.Size(69, 23);
            this.btnZatvori.TabIndex = 0;
            this.btnZatvori.Text = "Zatvori";
            this.btnZatvori.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnZatvori.UseVisualStyleBackColor = true;
            this.btnZatvori.Click += new System.EventHandler(this.btnZatvori_Click);
            // 
            // olvPredmeti
            // 
            this.olvPredmeti.AllColumns.Add(this.olvColumn1);
            this.olvPredmeti.AllColumns.Add(this.olvColumn2);
            this.olvPredmeti.CellEditUseWholeCell = false;
            this.olvPredmeti.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2});
            this.olvPredmeti.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvPredmeti.FullRowSelect = true;
            this.olvPredmeti.GridLines = true;
            this.olvPredmeti.Location = new System.Drawing.Point(12, 82);
            this.olvPredmeti.Name = "olvPredmeti";
            this.olvPredmeti.ShowGroups = false;
            this.olvPredmeti.ShowSortIndicators = false;
            this.olvPredmeti.Size = new System.Drawing.Size(522, 255);
            this.olvPredmeti.TabIndex = 2;
            this.olvPredmeti.UseCompatibleStateImageBehavior = false;
            this.olvPredmeti.View = System.Windows.Forms.View.Details;
            this.olvPredmeti.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.olvEvidencije_MouseDoubleClick);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "brojPredmeta";
            this.olvColumn1.Text = "Broj predmeta";
            this.olvColumn1.Width = 116;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "stranka";
            this.olvColumn2.Text = "Stranka";
            this.olvColumn2.Width = 400;
            // 
            // btnOdstampajEvidencije
            // 
            this.btnOdstampajEvidencije.Image = global::Arhiviranje_dokumenata.Properties.Resources.Print_16x;
            this.btnOdstampajEvidencije.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOdstampajEvidencije.Location = new System.Drawing.Point(46, 345);
            this.btnOdstampajEvidencije.Name = "btnOdstampajEvidencije";
            this.btnOdstampajEvidencije.Size = new System.Drawing.Size(134, 23);
            this.btnOdstampajEvidencije.TabIndex = 8;
            this.btnOdstampajEvidencije.Text = "Odštampaj evidencije";
            this.btnOdstampajEvidencije.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOdstampajEvidencije.UseVisualStyleBackColor = true;
            this.btnOdstampajEvidencije.Click += new System.EventHandler(this.btnOdstampajEvidencije_Click);
            // 
            // cbKategorije
            // 
            this.cbKategorije.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKategorije.FormattingEnabled = true;
            this.cbKategorije.Location = new System.Drawing.Point(12, 26);
            this.cbKategorije.Name = "cbKategorije";
            this.cbKategorije.Size = new System.Drawing.Size(276, 21);
            this.cbKategorije.TabIndex = 9;
            this.cbKategorije.SelectedIndexChanged += new System.EventHandler(this.cbKategorije_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(126, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Kategorija";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbSvi);
            this.groupBox1.Controls.Add(this.rbArhivirani);
            this.groupBox1.Controls.Add(this.rbAktivni);
            this.groupBox1.Location = new System.Drawing.Point(310, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 66);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vrsta predmeta";
            // 
            // rbAktivni
            // 
            this.rbAktivni.AutoSize = true;
            this.rbAktivni.Location = new System.Drawing.Point(6, 20);
            this.rbAktivni.Name = "rbAktivni";
            this.rbAktivni.Size = new System.Drawing.Size(57, 17);
            this.rbAktivni.TabIndex = 0;
            this.rbAktivni.Text = "Aktivni";
            this.rbAktivni.UseVisualStyleBackColor = true;
            this.rbAktivni.CheckedChanged += new System.EventHandler(this.rbSvi_CheckedChanged);
            // 
            // rbArhivirani
            // 
            this.rbArhivirani.AutoSize = true;
            this.rbArhivirani.Location = new System.Drawing.Point(6, 43);
            this.rbArhivirani.Name = "rbArhivirani";
            this.rbArhivirani.Size = new System.Drawing.Size(68, 17);
            this.rbArhivirani.TabIndex = 1;
            this.rbArhivirani.Text = "Arhivirani";
            this.rbArhivirani.UseVisualStyleBackColor = true;
            this.rbArhivirani.CheckedChanged += new System.EventHandler(this.rbSvi_CheckedChanged);
            // 
            // rbSvi
            // 
            this.rbSvi.AutoSize = true;
            this.rbSvi.Checked = true;
            this.rbSvi.Location = new System.Drawing.Point(120, 20);
            this.rbSvi.Name = "rbSvi";
            this.rbSvi.Size = new System.Drawing.Size(40, 17);
            this.rbSvi.TabIndex = 2;
            this.rbSvi.TabStop = true;
            this.rbSvi.Text = "Svi";
            this.rbSvi.UseVisualStyleBackColor = true;
            this.rbSvi.CheckedChanged += new System.EventHandler(this.rbSvi_CheckedChanged);
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
            // Predmeti_po_kategoriji
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(546, 380);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbKategorije);
            this.Controls.Add(this.btnOdstampajEvidencije);
            this.Controls.Add(this.olvPredmeti);
            this.Controls.Add(this.btnZatvori);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Predmeti_po_kategoriji";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Predmeti po kategoriji";
            ((System.ComponentModel.ISupportInitialize)(this.olvPredmeti)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnZatvori;
        private BrightIdeasSoftware.ObjectListView olvPredmeti;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private System.Windows.Forms.Button btnOdstampajEvidencije;
        private BrightIdeasSoftware.ListViewPrinter listViewPrinter1;
        private System.Windows.Forms.ComboBox cbKategorije;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSvi;
        private System.Windows.Forms.RadioButton rbArhivirani;
        private System.Windows.Forms.RadioButton rbAktivni;
    }
}