using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Arhiviranje_dokumenata.Helpers;
using BrightIdeasSoftware;

namespace Arhiviranje_dokumenata
{
    public partial class ZakljucaneBeleske : Form
    {
        private Form1 mainForm;
        private Predmet predmetForma = null;
        private List<Finansije> lista;
       
        private bool biloIzmena = false;
        private bool snimljeno = false;

        public ZakljucaneBeleske(Form1 glavnaForma, Predmet parentForma, List<Finansije> listaFinansija)
        {
            InitializeComponent();
            predmetForma = parentForma;
            mainForm = glavnaForma;

            lista = listaFinansija == null ? new List<Finansije>() : listaFinansija;

            biloIzmena = false;

            //farbanje placenih finansija
            olvFinansije.UseCellFormatEvents = true;
            olvFinansije.FormatRow += (sender, args) =>
            {
                Finansije evidencija = (Finansije)args.Model;

                if (evidencija.placeno)
                {
                    args.Item.BackColor = GlobalVariables.arhivirana_evidencija_color;
                }
            };

            popuniListu();
        }

        private void btnZatvori_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnPrintBeleske_Click(object sender, EventArgs e)
        {
            olvColumn3.IsVisible = false;
            olvFinansije.RebuildColumns();

            lvpFinansije.Header = DateTime.Now.ToString(GlobalVariables.date_time_string_pattern);
            BlockFormat hf = new BlockFormat();
            hf.TextColor = Color.Black;

            Font fFnt = new Font(olvFinansije.Font.FontFamily, 10, FontStyle.Regular);
            lvpFinansije.CellFormat.Font = fFnt;

            lvpFinansije.HeaderFormat = hf;

            lvpFinansije.PrintWithDialog();

            olvColumn3.IsVisible = true;
            olvFinansije.RebuildColumns();
        }

        private void ZakljucaneBeleske_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (biloIzmena && !snimljeno)
            {
                DialogResult dialogResult = MessageBox.Show("Da li želite da snimite izmene?", "Upozorenje!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    predmetForma.updateFinansije(lista);
                    snimljeno = true;
                    this.Close();
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.Close();
                }
            }
        }

        private void popuniListu() {
            olvFinansije.ClearObjects();
            var tempList = new List<Finansije>();
            
            foreach (Finansije item in lista)
            {
                var itemBackup = item.Clone();
                itemBackup.text = GlobalVariables.RTFToText(item.text);
                itemBackup.datumUnosaString = item.datumUnosa.ToString(GlobalVariables.date_string_pattern);
                tempList.Add(itemBackup);
            }
            olvFinansije.SetObjects(tempList);
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            FinansijeDetalji novPredmet = new FinansijeDetalji(this);
            novPredmet.ShowDialog();
            novPredmet.Dispose();
        }

        public void DodajNov(Finansije podaci)
        {
            lista.Add(podaci);
            popuniListu();
            biloIzmena = true;
        }

        public void OsveziStari(Finansije podaci)
        {
            Finansije element = lista.Find(m => m.Id == podaci.Id);
            if (element != null) {
                element = podaci;
            }
            popuniListu();
            biloIzmena = true;
        }

        public void Obrisi(Finansije podaci) {
            lista.Remove(podaci);
            popuniListu();
            biloIzmena = true;
        }

        private void olvFinansije_DoubleClick(object sender, EventArgs e)
        {
            if (olvFinansije.SelectedItems.Count == 1)
            {
                string id = olvFinansije.SelectedItems[0].SubItems[2].Text;
                Finansije element = lista.Find(m => m.Id.ToString() == id);
                if (element != null)
                {
                    FinansijeDetalji detalji = new FinansijeDetalji(this, element);
                    detalji.ShowDialog();
                    detalji.Dispose();
                }
            }
        }
    }
}
