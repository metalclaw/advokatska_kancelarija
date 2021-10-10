using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Arhiviranje_dokumenata.Helpers;
using BrightIdeasSoftware;

namespace Arhiviranje_dokumenata
{
    public partial class ListaPlacanjaDugovanja : Form
    {
        private Form1 glavnaForma;
        private Thread workerThread = null;
        private bool stopProcess = false;
        private List<ListaPlacanjaDugovanjaClass> spakovanoZaListu;

        public ListaPlacanjaDugovanja(Form1 mainForm)
        {
            InitializeComponent();
            glavnaForma = mainForm;

            //farbanje placenih finansija
            olvFinansije.UseCellFormatEvents = true;
            olvFinansije.FormatRow += (sender, args) =>
            {
                ListaPlacanjaDugovanjaClass evidencija = (ListaPlacanjaDugovanjaClass)args.Model;

                if (evidencija.placeno)
                {
                    args.Item.BackColor = GlobalVariables.arhivirana_evidencija_color;
                }
            };

            this.stopProcess = false;
            this.olvFinansije.EmptyListMsg = "Nema podataka";
            // Initialise and start worker thread
            this.workerThread = new Thread(new ThreadStart(this.fillList));
            this.workerThread.Start();
        }
       
        private void fillList()
        {
            List<PredmetData> sviPredmeti = DatabaseCommunication.getSviPredmeti();
            List<ListaPlacanjaDugovanjaClass> zaListu = new List<ListaPlacanjaDugovanjaClass>();
            foreach (PredmetData pd in sviPredmeti)
            {
                if (pd.finansije != null)
                {
                    foreach (Finansije item in pd.finansije)
                    {
                        zaListu.Add(new ListaPlacanjaDugovanjaClass
                        {
                            beleska = GlobalVariables.RTFToText(item.text),
                            brojPredmeta = GlobalVariables.spojBrojPredmeta(pd.brojPredmetaBr, pd.brojPredmetaGod),
                            ime = pd.stranka,
                            placeno = item.placeno
                        });
                    }
                }
            }
            spakovanoZaListu = zaListu;

            filter_CheckedChanged(null, null);
        }

        private void fillData(List<ListaPlacanjaDugovanjaClass> zaListu)
        {
            if (!olvFinansije.IsDisposed)
            {
                olvFinansije.ClearObjects();
                olvFinansije.SetObjects(zaListu);
            }

            hideProgress();
            this.workerThread.Abort();
        }

        private void btnZatvori_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnStampajListu_Click(object sender, EventArgs e)
        {
            olvColumn1.Text = "Br.";

            string filter = "Sve";

            if (rbPlaceno.Checked)
            {
                filter = "Plaćeno";
            }else if (rbNeplacene.Checked)
            {
                filter = "Neplaćeno";
            }

            lvpEvidencije.Header = DateTime.Now.ToString(GlobalVariables.date_time_string_pattern) + " - Filter: " + filter;
            BlockFormat hf = new BlockFormat();
            hf.TextColor = Color.Black;

            Font fFnt = new Font(olvFinansije.Font.FontFamily, 10, FontStyle.Regular);
            lvpEvidencije.CellFormat.Font = fFnt;

            lvpEvidencije.HeaderFormat = hf;
            lvpEvidencije.PrintWithDialog();

            olvColumn1.Text = "Broj predmeta";
        }

        private void olvEvidencije_DoubleClick(object sender, EventArgs e)
        {
            if (olvFinansije.SelectedItems.Count == 1)
            {
                MessageBox.Show(olvFinansije.SelectedItems[0].SubItems[2].Text);
            }
        }

        #region cross-thread

        delegate void hideProgressCallback();

        public void hideProgress()
        {
            if (IsHandleCreated)
            {
                if (InvokeRequired)
                {

                    hideProgressCallback d = new hideProgressCallback(hideProgress);
                    Invoke(d);
                }
                else
                {
                    pnlLoading.Visible = false;
                }
            }
        }

        #endregion

        private void filter_CheckedChanged(object sender, EventArgs e)
        {
            List<ListaPlacanjaDugovanjaClass> filteredList = spakovanoZaListu.FindAll(x => {

                if (rbNeplacene.Checked)
                {
                    return !x.placeno;
                }
                else if (rbPlaceno.Checked)
                {
                    return x.placeno;
                }
                else
                {
                    return true;
                }

            });

            fillData(filteredList);
        }
    }
}
