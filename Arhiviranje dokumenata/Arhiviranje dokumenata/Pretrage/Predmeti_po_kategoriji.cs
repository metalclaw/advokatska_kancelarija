using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Arhiviranje_dokumenata.Helpers;

namespace Arhiviranje_dokumenata
{
    public partial class Predmeti_po_kategoriji : Form
    {
        Form1 mainForm = null;
        public Predmeti_po_kategoriji(Form1 mainFormReference)
        {
            InitializeComponent();
            BackColor = GlobalVariables.background_color;
            mainForm = mainFormReference;

            List<KategorijePredmeta> listaKategorija = DatabaseCommunication.getKategorijePredmeta();

            bool raznoPostoji = false;

            foreach (KategorijePredmeta item in listaKategorija)
            {
                cbKategorije.Items.Add(item.naziv);
                if (item.naziv == "Razno")
                {
                    raznoPostoji = true;
                }
            }

            if (!raznoPostoji)
            {
                DatabaseCommunication.upisNovuKategorijuPredmetaUBazu(mainForm, "Razno", false);
            }
        }

        private void btnZatvori_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void olvEvidencije_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (olvPredmeti.SelectedItems.Count == 1) {
                var brPredmeta = GlobalVariables.razbijBrojPredmeta(olvPredmeti.SelectedItems[0].SubItems[0].Text);
                Predmet predmet = new Predmet(brPredmeta[0], brPredmeta[1], mainForm);
                predmet.ShowDialog();
                predmet.Dispose();
            }
        }

        private void btnOdstampajEvidencije_Click(object sender, EventArgs e)
        {
            /*int oldCol1Width = olvColumn1.Width;
            int oldCol2Width = olvColumn2.Width;

            olvColumn1.Text = "Br.";

            olvColumn1.Width = 60;
            olvColumn2.Width = 400;*/

            listViewPrinter1.PrintWithDialog();

            /*olvColumn1.Text = "Broj predmeta";
            olvColumn1.Width = oldCol1Width;
            olvColumn2.Width = oldCol2Width;*/
        }

        private void cbKategorije_SelectedIndexChanged(object sender, EventArgs e)
        {
            popuniListu();
        }

        private void popuniListu() {
            if (cbKategorije.SelectedItem != null)
            {
                //moze se optimizovati da se filtrira pri pozivu na bazu al sad me mrzi...

                var listaPredmeta = DatabaseCommunication.getPredmetiByKategorija(cbKategorije.SelectedItem.ToString());

                List<ListaPredmetiPoKategorijama> zaOlv = new List<ListaPredmetiPoKategorijama>();

                foreach (PredmetData predmet in listaPredmeta)
                {
                    bool dodaj = false;

                    if (rbSvi.Checked)
                    {
                        dodaj = true;
                    }
                    else if (rbAktivni.Checked)
                    {
                        if (predmet.predmetJeAktivan)
                        {
                            dodaj = true;
                        }
                    }
                    else if (rbArhivirani.Checked)
                    {
                        if (!predmet.predmetJeAktivan)
                        {
                            dodaj = true;
                        }
                    }

                    if (dodaj)
                    {
                        zaOlv.Add(new ListaPredmetiPoKategorijama()
                        {
                            brojPredmeta = GlobalVariables.spojBrojPredmeta(predmet.brojPredmetaBr, predmet.brojPredmetaGod),
                            stranka = predmet.stranka,
                        });
                    }
                }

                olvPredmeti.SetObjects(zaOlv);
            }
        }

        private void rbSvi_CheckedChanged(object sender, EventArgs e)
        {
            //da bi se samo jednom pozvalo posto svi radio buttoni dele ovaj event
            if (((RadioButton)sender).Checked)
            {
                popuniListu();
            }
        }
    }
}
