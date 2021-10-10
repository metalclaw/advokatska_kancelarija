using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Arhiviranje_dokumenata.Helpers;

namespace Arhiviranje_dokumenata
{
    public partial class Pretraga : Form
    {
        Form1 mainForm = null;
        public Pretraga(Form1 mainFormInstance)
        {
            mainForm = mainFormInstance;
            InitializeComponent();
            BackColor = GlobalVariables.background_color;
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            string query = tbUpit.Text.ToLower();

            if (!String.IsNullOrWhiteSpace(query))
            {
                var rezultat = (List <PredmetData>)null;
                if (DatabaseCommunication.testDbConnection(mainForm, null, null))
                {
                    if (rbBrojPredmeta.Checked)
                    {
                        rezultat = DatabaseCommunication.getPredmetiByBrojPredmeta(query);
                    }
                    else if (rbImeStranke.Checked)
                    {
                        rezultat = DatabaseCommunication.getPredmetiByImeStranke(query);
                    }
                    else if (rbProtivnaStrana.Checked)
                    {
                        rezultat = DatabaseCommunication.getPredmetiByProtivnaStrana(query);
                    }
                    else if (rbPoslovniBroj.Checked) {
                        rezultat = DatabaseCommunication.getPredmetiByPoslovniBroj(query);
                    }

                    if (rezultat.Count > 0)
                    {
                        popuniListu(rezultat);
                    }
                    else
                    {
                        MessageBox.Show("Nema rezultata.");
                        lvRezultatiPretrage.Items.Clear();
                    }
                }
            }
            else {
                MessageBox.Show("Polje za pretragu je prazno!");
            }
        }

        private void popuniListu(List<PredmetData> rezultat) {
            lvRezultatiPretrage.Items.Clear();

            foreach (PredmetData item in rezultat)
            {
                ListViewItem rezultatPretrage = new ListViewItem(GlobalVariables.spojBrojPredmeta(item.brojPredmetaBr, item.brojPredmetaGod));
                rezultatPretrage.SubItems.Add(item.stranka);
                rezultatPretrage.SubItems.Add(item.suprotnaStrana);
                rezultatPretrage.SubItems.Add(item.vrstaPredmeta);
                rezultatPretrage.SubItems.Add(item.poslovniBroj);

                if (!item.predmetJeAktivan){
                    rezultatPretrage.BackColor = GlobalVariables.arhivirana_evidencija_color;
                }

                lvRezultatiPretrage.Items.Add(rezultatPretrage);
            }
        }

        private void lvRezultatiPretrage_DoubleClick(object sender, EventArgs e)
        {
            if (lvRezultatiPretrage.SelectedItems.Count == 1) {
                var brPredmeta = GlobalVariables.razbijBrojPredmeta(lvRezultatiPretrage.SelectedItems[0].SubItems[0].Text);

                Predmet predmet = new Predmet(brPredmeta[0], brPredmeta[1], mainForm);
                predmet.ShowDialog();
                predmet.Dispose();
            }
        }

        private void tbUpit_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTrazi.PerformClick();
            }
        }
    }
}
