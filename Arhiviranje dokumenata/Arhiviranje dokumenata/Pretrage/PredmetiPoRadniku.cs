using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Arhiviranje_dokumenata.Helpers;

namespace Arhiviranje_dokumenata
{
    public partial class PredmetiPoRadniku : Form
    {
        Form1 parent = null;

        public PredmetiPoRadniku(Form1 _parent)
        {
            InitializeComponent();
            parent = _parent;

            BackColor = GlobalVariables.background_color;

            List<Radnik> listaRadnika = DatabaseCommunication.getRadnici();

            foreach (Radnik item in listaRadnika)
            {
                cbRadnici.Items.Add(item.ime);
            }
        }

        private void btnZatvori_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void olvPredmeti_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (olvPredmeti.SelectedItems.Count == 1)
            {
                var brPredmeta = GlobalVariables.razbijBrojPredmeta(olvPredmeti.SelectedItems[0].SubItems[0].Text);
                Predmet predmet = new Predmet(brPredmeta[0], brPredmeta[1], parent);
                predmet.ShowDialog();
                predmet.Dispose();
                popuniListu();
            }
        }

        private void popuniListu()
        {
            if (cbRadnici.SelectedItem != null)
            {
                //moze se optimizovati da se filtrira pri pozivu na bazu al sad me mrzi...

                var listaPredmeta = DatabaseCommunication.getPredmetiByRadnik(cbRadnici.SelectedItem.ToString());

                List<ListaPredmetiPoRadnicima> zaOlv = new List<ListaPredmetiPoRadnicima>();

                foreach (PredmetData predmet in listaPredmeta)
                {
                    bool dodaj = false;

                    if (cbPrikaziArhivirane.Checked) {
                        dodaj = true;
                    }
                    else if (predmet.predmetJeAktivan)
                    {
                        dodaj = true;
                    }

                    if (dodaj)
                    {
                        string textEvidencijeZaUpis = "";
                        string datumEvidencijeZaUpis = "";

                        if (predmet.listaEvidencija.Count > 0) {
                            textEvidencijeZaUpis = predmet.listaEvidencija[0].tekstEvidencije;
                            datumEvidencijeZaUpis = predmet.listaEvidencija[0].datum.ToString(GlobalVariables.date_string_pattern);
                        }
                        
                        zaOlv.Add(new ListaPredmetiPoRadnicima()
                        {
                            brojPredmeta = GlobalVariables.spojBrojPredmeta(predmet.brojPredmetaBr, predmet.brojPredmetaGod),
                            stranka = predmet.stranka,
                            predmetJeAktivan = predmet.predmetJeAktivan,
                            textEvidencije = textEvidencijeZaUpis,
                            datumEvidencije = datumEvidencijeZaUpis
                        });
                    }
                }

                olvPredmeti.SetObjects(zaOlv);

                //farbanje arhiviranih predmeta
                olvPredmeti.UseCellFormatEvents = true;
                olvPredmeti.FormatRow += (sender, args) =>
                {
                    ListaPredmetiPoRadnicima evidencija = (ListaPredmetiPoRadnicima)args.Model;

                    if (!evidencija.predmetJeAktivan)
                    {
                        args.Item.BackColor = GlobalVariables.arhivirana_evidencija_color;
                    }
                };
            }
        }

        private void cbRadnici_SelectedIndexChanged(object sender, EventArgs e)
        {
            popuniListu();
        }

        private void cbPrikaziArhivirane_CheckedChanged(object sender, EventArgs e)
        {
            popuniListu();
        }

        private void btnOdstampajListu_Click(object sender, EventArgs e)
        {
            int oldCol1Width = olvColumn1.Width;
            int oldCol3Width = olvColumn3.Width;

           // olvColumn1.Text = "Br.";

            olvColumn1.Width = 100;
            olvColumn3.Width = 400;

            listViewPrinter1.PrintWithDialog();

      //      olvColumn1.Text = "Broj predmeta";
            olvColumn1.Width = oldCol1Width;
            olvColumn3.Width = oldCol3Width;
        }
    }
}
