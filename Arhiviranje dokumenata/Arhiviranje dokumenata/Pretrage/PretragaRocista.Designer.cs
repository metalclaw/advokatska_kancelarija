namespace Arhiviranje_dokumenata
{
    partial class PretragaRocista
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
            this.dtpPocetniDatum = new System.Windows.Forms.DateTimePicker();
            this.dtpZavrsniDatum = new System.Windows.Forms.DateTimePicker();
            this.btnStampajRocista = new System.Windows.Forms.Button();
            this.olvRocista = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1Rocista = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2Rocista = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3Rocista = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4Rocista = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5Rocista = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6Rocista = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPretrazi = new System.Windows.Forms.Button();
            this.listViewPrinter1 = new BrightIdeasSoftware.ListViewPrinter();
            ((System.ComponentModel.ISupportInitialize)(this.olvRocista)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpPocetniDatum
            // 
            this.dtpPocetniDatum.CustomFormat = "dd.MM.yyyy";
            this.dtpPocetniDatum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPocetniDatum.Location = new System.Drawing.Point(138, 36);
            this.dtpPocetniDatum.Name = "dtpPocetniDatum";
            this.dtpPocetniDatum.Size = new System.Drawing.Size(200, 20);
            this.dtpPocetniDatum.TabIndex = 0;
            // 
            // dtpZavrsniDatum
            // 
            this.dtpZavrsniDatum.CustomFormat = "dd.MM.yyyy";
            this.dtpZavrsniDatum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpZavrsniDatum.Location = new System.Drawing.Point(412, 36);
            this.dtpZavrsniDatum.Name = "dtpZavrsniDatum";
            this.dtpZavrsniDatum.Size = new System.Drawing.Size(200, 20);
            this.dtpZavrsniDatum.TabIndex = 1;
            // 
            // btnStampajRocista
            // 
            this.btnStampajRocista.Image = global::Arhiviranje_dokumenata.Properties.Resources.Print_16x;
            this.btnStampajRocista.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStampajRocista.Location = new System.Drawing.Point(291, 360);
            this.btnStampajRocista.Name = "btnStampajRocista";
            this.btnStampajRocista.Size = new System.Drawing.Size(182, 23);
            this.btnStampajRocista.TabIndex = 38;
            this.btnStampajRocista.Text = "Odštampaj listu ročišta";
            this.btnStampajRocista.UseVisualStyleBackColor = true;
            this.btnStampajRocista.Click += new System.EventHandler(this.btnStampajRocista_Click);
            // 
            // olvRocista
            // 
            this.olvRocista.AllColumns.Add(this.olvColumn1Rocista);
            this.olvRocista.AllColumns.Add(this.olvColumn2Rocista);
            this.olvRocista.AllColumns.Add(this.olvColumn3Rocista);
            this.olvRocista.AllColumns.Add(this.olvColumn4Rocista);
            this.olvRocista.AllColumns.Add(this.olvColumn5Rocista);
            this.olvRocista.AllColumns.Add(this.olvColumn6Rocista);
            this.olvRocista.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olvRocista.CellEditUseWholeCell = false;
            this.olvRocista.CheckedAspectName = "";
            this.olvRocista.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1Rocista,
            this.olvColumn2Rocista,
            this.olvColumn3Rocista,
            this.olvColumn4Rocista,
            this.olvColumn5Rocista,
            this.olvColumn6Rocista});
            this.olvRocista.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvRocista.FullRowSelect = true;
            this.olvRocista.GridLines = true;
            this.olvRocista.HideSelection = false;
            this.olvRocista.Location = new System.Drawing.Point(12, 79);
            this.olvRocista.Name = "olvRocista";
            this.olvRocista.ShowGroups = false;
            this.olvRocista.ShowHeaderInAllViews = false;
            this.olvRocista.ShowSortIndicators = false;
            this.olvRocista.Size = new System.Drawing.Size(737, 266);
            this.olvRocista.TabIndex = 37;
            this.olvRocista.UseCompatibleStateImageBehavior = false;
            this.olvRocista.View = System.Windows.Forms.View.Details;
            this.olvRocista.DoubleClick += new System.EventHandler(this.olvRocista_DoubleClick);
            // 
            // olvColumn1Rocista
            // 
            this.olvColumn1Rocista.AspectName = "brojPredmeta";
            this.olvColumn1Rocista.Hideable = false;
            this.olvColumn1Rocista.IsEditable = false;
            this.olvColumn1Rocista.Text = "Broj predmeta";
            this.olvColumn1Rocista.Width = 79;
            // 
            // olvColumn2Rocista
            // 
            this.olvColumn2Rocista.AspectName = "stranka";
            this.olvColumn2Rocista.Text = "Stranka";
            this.olvColumn2Rocista.Width = 156;
            // 
            // olvColumn3Rocista
            // 
            this.olvColumn3Rocista.AspectName = "tekstRocista";
            this.olvColumn3Rocista.Text = "Tekst ročišta";
            this.olvColumn3Rocista.Width = 270;
            // 
            // olvColumn4Rocista
            // 
            this.olvColumn4Rocista.AspectName = "sud";
            this.olvColumn4Rocista.Text = "Organ";
            this.olvColumn4Rocista.Width = 86;
            // 
            // olvColumn5Rocista
            // 
            this.olvColumn5Rocista.AspectName = "sudskiBroj";
            this.olvColumn5Rocista.Text = "Poslovni broj";
            this.olvColumn5Rocista.Width = 68;
            // 
            // olvColumn6Rocista
            // 
            this.olvColumn6Rocista.AspectName = "brojSudnice";
            this.olvColumn6Rocista.Text = "Broj sudnice";
            this.olvColumn6Rocista.Width = 75;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(200, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Početni datum";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(479, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "Završni datum";
            // 
            // btnPretrazi
            // 
            this.btnPretrazi.Location = new System.Drawing.Point(655, 33);
            this.btnPretrazi.Name = "btnPretrazi";
            this.btnPretrazi.Size = new System.Drawing.Size(75, 23);
            this.btnPretrazi.TabIndex = 41;
            this.btnPretrazi.Text = "Pretraži";
            this.btnPretrazi.UseVisualStyleBackColor = true;
            this.btnPretrazi.Click += new System.EventHandler(this.btnPretrazi_Click);
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
            this.listViewPrinter1.ListView = this.olvRocista;
            this.listViewPrinter1.WatermarkColor = System.Drawing.Color.Empty;
            // 
            // PretragaRocista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 392);
            this.Controls.Add(this.btnPretrazi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStampajRocista);
            this.Controls.Add(this.olvRocista);
            this.Controls.Add(this.dtpZavrsniDatum);
            this.Controls.Add(this.dtpPocetniDatum);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(777, 431);
            this.Name = "PretragaRocista";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pretraga ročišta";
            ((System.ComponentModel.ISupportInitialize)(this.olvRocista)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpPocetniDatum;
        private System.Windows.Forms.DateTimePicker dtpZavrsniDatum;
        private System.Windows.Forms.Button btnStampajRocista;
        private BrightIdeasSoftware.ObjectListView olvRocista;
        private BrightIdeasSoftware.OLVColumn olvColumn1Rocista;
        private BrightIdeasSoftware.OLVColumn olvColumn2Rocista;
        private BrightIdeasSoftware.OLVColumn olvColumn3Rocista;
        private BrightIdeasSoftware.OLVColumn olvColumn4Rocista;
        private BrightIdeasSoftware.OLVColumn olvColumn5Rocista;
        private BrightIdeasSoftware.OLVColumn olvColumn6Rocista;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPretrazi;
        private BrightIdeasSoftware.ListViewPrinter listViewPrinter1;
    }
}