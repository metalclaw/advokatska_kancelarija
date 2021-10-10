namespace Arhiviranje_dokumenata
{
    partial class Pretraga
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
            this.lvRezultatiPretrage = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbPoslovniBroj = new System.Windows.Forms.RadioButton();
            this.btnTrazi = new System.Windows.Forms.Button();
            this.tbUpit = new System.Windows.Forms.TextBox();
            this.rbProtivnaStrana = new System.Windows.Forms.RadioButton();
            this.rbImeStranke = new System.Windows.Forms.RadioButton();
            this.rbBrojPredmeta = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvRezultatiPretrage
            // 
            this.lvRezultatiPretrage.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lvRezultatiPretrage.FullRowSelect = true;
            this.lvRezultatiPretrage.GridLines = true;
            this.lvRezultatiPretrage.HideSelection = false;
            this.lvRezultatiPretrage.Location = new System.Drawing.Point(12, 101);
            this.lvRezultatiPretrage.MultiSelect = false;
            this.lvRezultatiPretrage.Name = "lvRezultatiPretrage";
            this.lvRezultatiPretrage.Size = new System.Drawing.Size(660, 216);
            this.lvRezultatiPretrage.TabIndex = 0;
            this.lvRezultatiPretrage.UseCompatibleStateImageBehavior = false;
            this.lvRezultatiPretrage.View = System.Windows.Forms.View.Details;
            this.lvRezultatiPretrage.DoubleClick += new System.EventHandler(this.lvRezultatiPretrage_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Broj predmeta";
            this.columnHeader1.Width = 81;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Stranka";
            this.columnHeader2.Width = 200;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Suprotna strana";
            this.columnHeader3.Width = 207;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Vrsta predmeta";
            this.columnHeader4.Width = 88;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Poslovni broj";
            this.columnHeader5.Width = 80;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbPoslovniBroj);
            this.groupBox1.Controls.Add(this.btnTrazi);
            this.groupBox1.Controls.Add(this.tbUpit);
            this.groupBox1.Controls.Add(this.rbProtivnaStrana);
            this.groupBox1.Controls.Add(this.rbImeStranke);
            this.groupBox1.Controls.Add(this.rbBrojPredmeta);
            this.groupBox1.Location = new System.Drawing.Point(60, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(563, 83);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pretraži predmete po";
            // 
            // rbPoslovniBroj
            // 
            this.rbPoslovniBroj.AutoSize = true;
            this.rbPoslovniBroj.Location = new System.Drawing.Point(120, 58);
            this.rbPoslovniBroj.Name = "rbPoslovniBroj";
            this.rbPoslovniBroj.Size = new System.Drawing.Size(103, 17);
            this.rbPoslovniBroj.TabIndex = 5;
            this.rbPoslovniBroj.TabStop = true;
            this.rbPoslovniBroj.Text = "Poslovnom broju";
            this.rbPoslovniBroj.UseVisualStyleBackColor = true;
            // 
            // btnTrazi
            // 
            this.btnTrazi.Location = new System.Drawing.Point(459, 31);
            this.btnTrazi.Name = "btnTrazi";
            this.btnTrazi.Size = new System.Drawing.Size(75, 23);
            this.btnTrazi.TabIndex = 4;
            this.btnTrazi.Text = "Traži";
            this.btnTrazi.UseVisualStyleBackColor = true;
            this.btnTrazi.Click += new System.EventHandler(this.btnTrazi_Click);
            // 
            // tbUpit
            // 
            this.tbUpit.Location = new System.Drawing.Point(253, 34);
            this.tbUpit.Name = "tbUpit";
            this.tbUpit.Size = new System.Drawing.Size(200, 20);
            this.tbUpit.TabIndex = 3;
            this.tbUpit.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbUpit_KeyUp);
            // 
            // rbProtivnaStrana
            // 
            this.rbProtivnaStrana.AutoSize = true;
            this.rbProtivnaStrana.Location = new System.Drawing.Point(120, 35);
            this.rbProtivnaStrana.Name = "rbProtivnaStrana";
            this.rbProtivnaStrana.Size = new System.Drawing.Size(130, 17);
            this.rbProtivnaStrana.TabIndex = 2;
            this.rbProtivnaStrana.Text = "Imenu suprotne strane";
            this.rbProtivnaStrana.UseVisualStyleBackColor = true;
            // 
            // rbImeStranke
            // 
            this.rbImeStranke.AutoSize = true;
            this.rbImeStranke.Checked = true;
            this.rbImeStranke.Location = new System.Drawing.Point(18, 58);
            this.rbImeStranke.Name = "rbImeStranke";
            this.rbImeStranke.Size = new System.Drawing.Size(92, 17);
            this.rbImeStranke.TabIndex = 1;
            this.rbImeStranke.TabStop = true;
            this.rbImeStranke.Text = "Imenu stranke";
            this.rbImeStranke.UseVisualStyleBackColor = true;
            // 
            // rbBrojPredmeta
            // 
            this.rbBrojPredmeta.AutoSize = true;
            this.rbBrojPredmeta.Location = new System.Drawing.Point(18, 35);
            this.rbBrojPredmeta.Name = "rbBrojPredmeta";
            this.rbBrojPredmeta.Size = new System.Drawing.Size(96, 17);
            this.rbBrojPredmeta.TabIndex = 0;
            this.rbBrojPredmeta.Text = "Broju predmeta";
            this.rbBrojPredmeta.UseVisualStyleBackColor = true;
            // 
            // Pretraga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(684, 329);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lvRezultatiPretrage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Pretraga";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pretraga predmeta";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvRezultatiPretrage;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnTrazi;
        private System.Windows.Forms.TextBox tbUpit;
        private System.Windows.Forms.RadioButton rbProtivnaStrana;
        private System.Windows.Forms.RadioButton rbImeStranke;
        private System.Windows.Forms.RadioButton rbBrojPredmeta;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.RadioButton rbPoslovniBroj;
    }
}