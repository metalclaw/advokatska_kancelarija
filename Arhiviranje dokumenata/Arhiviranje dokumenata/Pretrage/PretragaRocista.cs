using System;
using System.Windows.Forms;
using Arhiviranje_dokumenata.Helpers;

namespace Arhiviranje_dokumenata
{
    public partial class PretragaRocista : Form
    {
        Form1 parent = null;

        public PretragaRocista(Form1 parent)
        {
            InitializeComponent();
        }

        private void btnPretrazi_Click(object sender, EventArgs e)
        {
            if (dtpPocetniDatum.Value.Date >= dtpZavrsniDatum.Value.Date) {
                MessageBox.Show("Početni datum mora biti manji od završnog datuma!", "Upozorenje!");
            } else {
                olvRocista.ClearObjects();
                olvRocista.SetObjects(DatabaseCommunication.getRocistaZaVremensiRaspon(dtpPocetniDatum.Value, dtpZavrsniDatum.Value));
            }
        }

        private void btnStampajRocista_Click(object sender, EventArgs e)
        {
            int oldCol1Width = olvColumn1Rocista.Width;
            int oldCol2Width = olvColumn2Rocista.Width;
            int oldCol3Width = olvColumn3Rocista.Width;
            int oldCol4Width = olvColumn4Rocista.Width;
            int oldCol5Width = olvColumn5Rocista.Width;
            int oldCol6Width = olvColumn6Rocista.Width;

            olvColumn1Rocista.Text = "Br.";

            olvColumn1Rocista.Width = 60;
            olvColumn2Rocista.Width = 300;
            olvColumn3Rocista.Width = 600;
            olvColumn4Rocista.Width = 200;
            olvColumn5Rocista.Width = 100;
            olvColumn6Rocista.Width = 100;

            listViewPrinter1.PrintWithDialog();

            olvColumn1Rocista.Text = "Broj predmeta";
            olvColumn1Rocista.Width = oldCol1Width;
            olvColumn2Rocista.Width = oldCol2Width;
            olvColumn3Rocista.Width = oldCol3Width;
            olvColumn4Rocista.Width = oldCol4Width;
            olvColumn5Rocista.Width = oldCol5Width;
            olvColumn6Rocista.Width = oldCol6Width;
        }

        private void olvRocista_DoubleClick(object sender, EventArgs e)
        {
            if (olvRocista.SelectedItems.Count == 1)
            {
                string brPredmeta = olvRocista.SelectedItems[0].SubItems[0].Text;
                var brojPredmeta = GlobalVariables.razbijBrojPredmeta(brPredmeta);
                Predmet predmet = new Predmet(brojPredmeta[0], brojPredmeta[1], parent);
                predmet.ShowDialog();
                predmet.Dispose();
            }
        }
    }
}
