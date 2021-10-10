using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Arhiviranje_dokumenata.Helpers;

namespace Arhiviranje_dokumenata
{
    public partial class Evidencije_za_dan : Form
    {
        Form1 mainForm = null;
        public Evidencije_za_dan(DateTime datum, Form1 mainFormReference)
        {
            InitializeComponent();
            BackColor = GlobalVariables.background_color;
            mainForm = mainFormReference;
            var listaEvidencija = DatabaseCommunication.getDanasnjeEvidencije(datum);

            List<DanasnjeEvidencijeOLV> zaOlv = new List<DanasnjeEvidencijeOLV>();

            foreach (ListaDanasnjihEvidencija evidencija in listaEvidencija)
            {
                zaOlv.Add(new DanasnjeEvidencijeOLV()
                {
                    brojPredmeta = evidencija.brojPredmeta,
                    stranka = evidencija.stranka,
                    tekstEvidencije = evidencija.tekstEvidencije
                });
            }

            olvEvidencije.SetObjects(zaOlv);

            this.Text = "Evidencije za dan " + datum.Date.ToString(GlobalVariables.date_string_pattern);
        }

        private void btnZatvori_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void olvEvidencije_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (olvEvidencije.SelectedItems.Count == 1) {
                var brPredmeta = GlobalVariables.razbijBrojPredmeta(olvEvidencije.SelectedItems[0].SubItems[0].Text);
                Predmet predmet = new Predmet(brPredmeta[0], brPredmeta[1], mainForm);
                predmet.ShowDialog();
                predmet.Dispose();
            }
        }

        private void btnOdstampajEvidencije_Click(object sender, EventArgs e)
        {
            int oldCol1Width = olvColumn1.Width;
            int oldCol2Width = olvColumn2.Width;
            int oldCol3Width = olvColumn3.Width;

            olvColumn1.Text = "Br.";

            olvColumn1.Width = 60;
            olvColumn2.Width = 400;
            olvColumn3.Width = 600;

            listViewPrinter1.PrintWithDialog();

            olvColumn1.Text = "Broj predmeta";
            olvColumn1.Width = oldCol1Width;
            olvColumn2.Width = oldCol2Width;
            olvColumn3.Width = oldCol3Width;
        }
    }
}
