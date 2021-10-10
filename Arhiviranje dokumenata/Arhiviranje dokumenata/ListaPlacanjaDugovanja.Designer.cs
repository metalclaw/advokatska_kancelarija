namespace Arhiviranje_dokumenata
{
    partial class ListaPlacanjaDugovanja
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
            this.olvFinansije = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnZatvori = new System.Windows.Forms.Button();
            this.btnStampajListu = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pnlLoading = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.rbPlaceno = new System.Windows.Forms.RadioButton();
            this.rbNeplacene = new System.Windows.Forms.RadioButton();
            this.rbSve = new System.Windows.Forms.RadioButton();
            this.lvpEvidencije = new BrightIdeasSoftware.ListViewPrinter();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.olvFinansije)).BeginInit();
            this.pnlLoading.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // olvFinansije
            // 
            this.olvFinansije.AllColumns.Add(this.olvColumn1);
            this.olvFinansije.AllColumns.Add(this.olvColumn2);
            this.olvFinansije.AllColumns.Add(this.olvColumn3);
            this.olvFinansije.CellEditUseWholeCell = false;
            this.olvFinansije.CheckedAspectName = "";
            this.olvFinansije.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3});
            this.olvFinansije.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvFinansije.FullRowSelect = true;
            this.olvFinansije.GridLines = true;
            this.olvFinansije.HideSelection = false;
            this.olvFinansije.Location = new System.Drawing.Point(12, 64);
            this.olvFinansije.MultiSelect = false;
            this.olvFinansije.Name = "olvFinansije";
            this.olvFinansije.ShowGroups = false;
            this.olvFinansije.ShowHeaderInAllViews = false;
            this.olvFinansije.ShowSortIndicators = false;
            this.olvFinansije.Size = new System.Drawing.Size(619, 436);
            this.olvFinansije.TabIndex = 3;
            this.olvFinansije.UseCompatibleStateImageBehavior = false;
            this.olvFinansije.View = System.Windows.Forms.View.Details;
            this.olvFinansije.DoubleClick += new System.EventHandler(this.olvEvidencije_DoubleClick);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "brojPredmeta";
            this.olvColumn1.Hideable = false;
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Text = "Broj predmeta";
            this.olvColumn1.Width = 86;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "ime";
            this.olvColumn2.Text = "Ime";
            this.olvColumn2.Width = 104;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "beleska";
            this.olvColumn3.Text = "Beleška";
            this.olvColumn3.Width = 424;
            // 
            // btnZatvori
            // 
            this.btnZatvori.Location = new System.Drawing.Point(556, 527);
            this.btnZatvori.Name = "btnZatvori";
            this.btnZatvori.Size = new System.Drawing.Size(75, 23);
            this.btnZatvori.TabIndex = 4;
            this.btnZatvori.Text = "Zatvori";
            this.btnZatvori.UseVisualStyleBackColor = true;
            this.btnZatvori.Click += new System.EventHandler(this.btnZatvori_Click);
            // 
            // btnStampajListu
            // 
            this.btnStampajListu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStampajListu.Image = global::Arhiviranje_dokumenata.Properties.Resources.Print_16x;
            this.btnStampajListu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStampajListu.Location = new System.Drawing.Point(12, 506);
            this.btnStampajListu.Name = "btnStampajListu";
            this.btnStampajListu.Size = new System.Drawing.Size(132, 23);
            this.btnStampajListu.TabIndex = 37;
            this.btnStampajListu.Text = "Odštampaj listu";
            this.btnStampajListu.UseVisualStyleBackColor = true;
            this.btnStampajListu.Click += new System.EventHandler(this.btnStampajListu_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(28, 34);
            this.progressBar1.MarqueeAnimationSpeed = 20;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(284, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 38;
            // 
            // pnlLoading
            // 
            this.pnlLoading.Controls.Add(this.label1);
            this.pnlLoading.Controls.Add(this.progressBar1);
            this.pnlLoading.Location = new System.Drawing.Point(143, 240);
            this.pnlLoading.Name = "pnlLoading";
            this.pnlLoading.Size = new System.Drawing.Size(341, 71);
            this.pnlLoading.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Podaci se učitavaju";
            // 
            // rbPlaceno
            // 
            this.rbPlaceno.AutoSize = true;
            this.rbPlaceno.Location = new System.Drawing.Point(86, 19);
            this.rbPlaceno.Name = "rbPlaceno";
            this.rbPlaceno.Size = new System.Drawing.Size(64, 17);
            this.rbPlaceno.TabIndex = 40;
            this.rbPlaceno.Text = "Plaćeno";
            this.rbPlaceno.UseVisualStyleBackColor = true;
            this.rbPlaceno.CheckedChanged += new System.EventHandler(this.filter_CheckedChanged);
            // 
            // rbNeplacene
            // 
            this.rbNeplacene.AutoSize = true;
            this.rbNeplacene.Location = new System.Drawing.Point(177, 19);
            this.rbNeplacene.Name = "rbNeplacene";
            this.rbNeplacene.Size = new System.Drawing.Size(77, 17);
            this.rbNeplacene.TabIndex = 41;
            this.rbNeplacene.Text = "Neplaćeno";
            this.rbNeplacene.UseVisualStyleBackColor = true;
            this.rbNeplacene.CheckedChanged += new System.EventHandler(this.filter_CheckedChanged);
            // 
            // rbSve
            // 
            this.rbSve.AutoSize = true;
            this.rbSve.Checked = true;
            this.rbSve.Location = new System.Drawing.Point(6, 19);
            this.rbSve.Name = "rbSve";
            this.rbSve.Size = new System.Drawing.Size(44, 17);
            this.rbSve.TabIndex = 42;
            this.rbSve.TabStop = true;
            this.rbSve.Text = "Sve";
            this.rbSve.UseVisualStyleBackColor = true;
            this.rbSve.CheckedChanged += new System.EventHandler(this.filter_CheckedChanged);
            // 
            // lvpEvidencije
            // 
            // 
            // 
            // 
            this.lvpEvidencije.CellFormat.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpEvidencije.CellFormat.BottomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.lvpEvidencije.CellFormat.BottomBorderWidth = 0.5F;
            this.lvpEvidencije.CellFormat.CanWrap = true;
            this.lvpEvidencije.CellFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lvpEvidencije.CellFormat.LeftBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.lvpEvidencije.CellFormat.LeftBorderWidth = 0.5F;
            this.lvpEvidencije.CellFormat.MinimumTextHeight = 0F;
            this.lvpEvidencije.CellFormat.RightBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.lvpEvidencije.CellFormat.RightBorderWidth = 0.5F;
            this.lvpEvidencije.CellFormat.TextColor = System.Drawing.Color.Empty;
            this.lvpEvidencije.CellFormat.TopBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.lvpEvidencije.CellFormat.TopBorderWidth = 0.5F;
            // 
            // 
            // 
            this.lvpEvidencije.FooterFormat.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpEvidencije.FooterFormat.BottomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpEvidencije.FooterFormat.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Italic);
            this.lvpEvidencije.FooterFormat.LeftBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpEvidencije.FooterFormat.MinimumTextHeight = 0F;
            this.lvpEvidencije.FooterFormat.RightBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpEvidencije.FooterFormat.TextColor = System.Drawing.Color.Black;
            this.lvpEvidencije.FooterFormat.TopBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lvpEvidencije.FooterFormat.TopBorderWidth = 0.5F;
            // 
            // 
            // 
            this.lvpEvidencije.GroupHeaderFormat.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpEvidencije.GroupHeaderFormat.BottomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpEvidencije.GroupHeaderFormat.BottomBorderWidth = 3F;
            this.lvpEvidencije.GroupHeaderFormat.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.lvpEvidencije.GroupHeaderFormat.LeftBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpEvidencije.GroupHeaderFormat.MinimumTextHeight = 0F;
            this.lvpEvidencije.GroupHeaderFormat.RightBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpEvidencije.GroupHeaderFormat.TextColor = System.Drawing.Color.Black;
            this.lvpEvidencije.GroupHeaderFormat.TopBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            this.lvpEvidencije.HeaderFormat.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpEvidencije.HeaderFormat.BottomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpEvidencije.HeaderFormat.Font = new System.Drawing.Font("Verdana", 24F);
            this.lvpEvidencije.HeaderFormat.LeftBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpEvidencije.HeaderFormat.MinimumTextHeight = 0F;
            this.lvpEvidencije.HeaderFormat.RightBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpEvidencije.HeaderFormat.TextColor = System.Drawing.Color.WhiteSmoke;
            this.lvpEvidencije.HeaderFormat.TopBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvpEvidencije.IsListHeaderOnEachPage = false;
            this.lvpEvidencije.IsShrinkToFit = true;
            this.lvpEvidencije.ListFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lvpEvidencije.ListGridColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            // 
            // 
            // 
            this.lvpEvidencije.ListHeaderFormat.BackgroundColor = System.Drawing.Color.LightGray;
            this.lvpEvidencije.ListHeaderFormat.BottomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.lvpEvidencije.ListHeaderFormat.BottomBorderWidth = 1.5F;
            this.lvpEvidencije.ListHeaderFormat.CanWrap = true;
            this.lvpEvidencije.ListHeaderFormat.Font = new System.Drawing.Font("Verdana", 12F);
            this.lvpEvidencije.ListHeaderFormat.LeftBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.lvpEvidencije.ListHeaderFormat.LeftBorderWidth = 1.5F;
            this.lvpEvidencije.ListHeaderFormat.MinimumTextHeight = 0F;
            this.lvpEvidencije.ListHeaderFormat.RightBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.lvpEvidencije.ListHeaderFormat.RightBorderWidth = 1.5F;
            this.lvpEvidencije.ListHeaderFormat.TextColor = System.Drawing.Color.Black;
            this.lvpEvidencije.ListHeaderFormat.TopBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.lvpEvidencije.ListHeaderFormat.TopBorderWidth = 1.5F;
            this.lvpEvidencije.ListView = this.olvFinansije;
            this.lvpEvidencije.WatermarkColor = System.Drawing.Color.Empty;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbPlaceno);
            this.groupBox1.Controls.Add(this.rbSve);
            this.groupBox1.Controls.Add(this.rbNeplacene);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 46);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // ListaPlacanjaDugovanja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 562);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlLoading);
            this.Controls.Add(this.btnStampajListu);
            this.Controls.Add(this.btnZatvori);
            this.Controls.Add(this.olvFinansije);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ListaPlacanjaDugovanja";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista finansija";
            ((System.ComponentModel.ISupportInitialize)(this.olvFinansije)).EndInit();
            this.pnlLoading.ResumeLayout(false);
            this.pnlLoading.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView olvFinansije;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private System.Windows.Forms.Button btnZatvori;
        private System.Windows.Forms.Button btnStampajListu;
        private BrightIdeasSoftware.ListViewPrinter lvpEvidencije;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel pnlLoading;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbPlaceno;
        private System.Windows.Forms.RadioButton rbNeplacene;
        private System.Windows.Forms.RadioButton rbSve;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}