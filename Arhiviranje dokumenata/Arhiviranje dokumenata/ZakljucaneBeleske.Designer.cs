namespace Arhiviranje_dokumenata
{
    partial class ZakljucaneBeleske
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
            this.btnDodaj = new System.Windows.Forms.Button();
            this.olvFinansije = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnPrintBeleske = new System.Windows.Forms.Button();
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.lvpFinansije = new BrightIdeasSoftware.ListViewPrinter();
            ((System.ComponentModel.ISupportInitialize)(this.olvFinansije)).BeginInit();
            this.SuspendLayout();
            // 
            // btnZatvori
            // 
            this.btnZatvori.Location = new System.Drawing.Point(248, 474);
            this.btnZatvori.Name = "btnZatvori";
            this.btnZatvori.Size = new System.Drawing.Size(75, 23);
            this.btnZatvori.TabIndex = 22;
            this.btnZatvori.Text = "Zatvori";
            this.btnZatvori.UseVisualStyleBackColor = true;
            this.btnZatvori.Click += new System.EventHandler(this.btnZatvori_Click);
            // 
            // btnDodaj
            // 
            this.btnDodaj.Location = new System.Drawing.Point(12, 416);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(136, 23);
            this.btnDodaj.TabIndex = 42;
            this.btnDodaj.Text = "Dodaj";
            this.btnDodaj.UseVisualStyleBackColor = true;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // olvFinansije
            // 
            this.olvFinansije.AllColumns.Add(this.olvColumn2);
            this.olvFinansije.AllColumns.Add(this.olvColumn1);
            this.olvFinansije.AllColumns.Add(this.olvColumn3);
            this.olvFinansije.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olvFinansije.CellEditUseWholeCell = false;
            this.olvFinansije.CheckedAspectName = "";
            this.olvFinansije.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn2,
            this.olvColumn1,
            this.olvColumn3});
            this.olvFinansije.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvFinansije.FullRowSelect = true;
            this.olvFinansije.GridLines = true;
            this.olvFinansije.HideSelection = false;
            this.olvFinansije.Location = new System.Drawing.Point(12, 53);
            this.olvFinansije.Name = "olvFinansije";
            this.olvFinansije.ShowGroups = false;
            this.olvFinansije.ShowHeaderInAllViews = false;
            this.olvFinansije.ShowSortIndicators = false;
            this.olvFinansije.Size = new System.Drawing.Size(569, 357);
            this.olvFinansije.TabIndex = 46;
            this.olvFinansije.UseCompatibleStateImageBehavior = false;
            this.olvFinansije.View = System.Windows.Forms.View.Details;
            this.olvFinansije.DoubleClick += new System.EventHandler(this.olvFinansije_DoubleClick);
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "datumUnosaString";
            this.olvColumn2.Text = "Datum";
            this.olvColumn2.Width = 160;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "text";
            this.olvColumn1.Hideable = false;
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Text = "Tekst";
            this.olvColumn1.Width = 564;
            // 
            // btnPrintBeleske
            // 
            this.btnPrintBeleske.Image = global::Arhiviranje_dokumenata.Properties.Resources.Print_16x;
            this.btnPrintBeleske.Location = new System.Drawing.Point(557, 21);
            this.btnPrintBeleske.Name = "btnPrintBeleske";
            this.btnPrintBeleske.Size = new System.Drawing.Size(24, 23);
            this.btnPrintBeleske.TabIndex = 40;
            this.btnPrintBeleske.UseVisualStyleBackColor = true;
            this.btnPrintBeleske.Click += new System.EventHandler(this.btnPrintBeleske_Click);
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Id";
            this.olvColumn3.Text = "id";
            // 
            // lvpFinansije
            // 
            // 
            // 
            // 
            this.lvpFinansije.CellFormat.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpFinansije.CellFormat.BottomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.lvpFinansije.CellFormat.BottomBorderWidth = 0.5F;
            this.lvpFinansije.CellFormat.CanWrap = true;
            this.lvpFinansije.CellFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lvpFinansije.CellFormat.LeftBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.lvpFinansije.CellFormat.LeftBorderWidth = 0.5F;
            this.lvpFinansije.CellFormat.MinimumTextHeight = 0F;
            this.lvpFinansije.CellFormat.RightBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.lvpFinansije.CellFormat.RightBorderWidth = 0.5F;
            this.lvpFinansije.CellFormat.TextColor = System.Drawing.Color.Empty;
            this.lvpFinansije.CellFormat.TopBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.lvpFinansije.CellFormat.TopBorderWidth = 0.5F;
            // 
            // 
            // 
            this.lvpFinansije.FooterFormat.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpFinansije.FooterFormat.BottomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpFinansije.FooterFormat.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Italic);
            this.lvpFinansije.FooterFormat.LeftBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpFinansije.FooterFormat.MinimumTextHeight = 0F;
            this.lvpFinansije.FooterFormat.RightBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpFinansije.FooterFormat.TextColor = System.Drawing.Color.Black;
            this.lvpFinansije.FooterFormat.TopBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lvpFinansije.FooterFormat.TopBorderWidth = 0.5F;
            // 
            // 
            // 
            this.lvpFinansije.GroupHeaderFormat.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpFinansije.GroupHeaderFormat.BottomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpFinansije.GroupHeaderFormat.BottomBorderWidth = 3F;
            this.lvpFinansije.GroupHeaderFormat.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.lvpFinansije.GroupHeaderFormat.LeftBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpFinansije.GroupHeaderFormat.MinimumTextHeight = 0F;
            this.lvpFinansije.GroupHeaderFormat.RightBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpFinansije.GroupHeaderFormat.TextColor = System.Drawing.Color.Black;
            this.lvpFinansije.GroupHeaderFormat.TopBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            this.lvpFinansije.HeaderFormat.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpFinansije.HeaderFormat.BottomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpFinansije.HeaderFormat.Font = new System.Drawing.Font("Verdana", 24F);
            this.lvpFinansije.HeaderFormat.LeftBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpFinansije.HeaderFormat.MinimumTextHeight = 0F;
            this.lvpFinansije.HeaderFormat.RightBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpFinansije.HeaderFormat.TextColor = System.Drawing.Color.WhiteSmoke;
            this.lvpFinansije.HeaderFormat.TopBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpFinansije.IsListHeaderOnEachPage = false;
            this.lvpFinansije.IsShrinkToFit = true;
            this.lvpFinansije.ListFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lvpFinansije.ListGridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.lvpFinansije.ListHeaderFormat.BackgroundColor = System.Drawing.Color.LightGray;
            this.lvpFinansije.ListHeaderFormat.BottomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.lvpFinansije.ListHeaderFormat.BottomBorderWidth = 1.5F;
            this.lvpFinansije.ListHeaderFormat.CanWrap = true;
            this.lvpFinansije.ListHeaderFormat.Font = new System.Drawing.Font("Verdana", 12F);
            this.lvpFinansije.ListHeaderFormat.LeftBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.lvpFinansije.ListHeaderFormat.LeftBorderWidth = 1.5F;
            this.lvpFinansije.ListHeaderFormat.MinimumTextHeight = 0F;
            this.lvpFinansije.ListHeaderFormat.RightBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.lvpFinansije.ListHeaderFormat.RightBorderWidth = 1.5F;
            this.lvpFinansije.ListHeaderFormat.TextColor = System.Drawing.Color.Black;
            this.lvpFinansije.ListHeaderFormat.TopBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.lvpFinansije.ListHeaderFormat.TopBorderWidth = 1.5F;
            this.lvpFinansije.ListView = this.olvFinansije;
            this.lvpFinansije.WatermarkColor = System.Drawing.Color.Empty;
            // 
            // ZakljucaneBeleske
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 509);
            this.Controls.Add(this.olvFinansije);
            this.Controls.Add(this.btnDodaj);
            this.Controls.Add(this.btnPrintBeleske);
            this.Controls.Add(this.btnZatvori);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ZakljucaneBeleske";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Finansije";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ZakljucaneBeleske_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.olvFinansije)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnZatvori;
        private System.Windows.Forms.Button btnPrintBeleske;
        private System.Windows.Forms.Button btnDodaj;
        private BrightIdeasSoftware.ObjectListView olvFinansije;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.ListViewPrinter lvpFinansije;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
    }
}