using System;
using System.Collections.Generic;
using System.ComponentModel;
using Arhiviranje_dokumenata.Helpers;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Management;
using System.Globalization;
using BrightIdeasSoftware;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Collections;
using Arhiviranje_dokumenata.Message_Boxes;

namespace Arhiviranje_dokumenata
{
    public partial class Form1 : Form
    {
        DropboxCommunication dropbox = new DropboxCommunication();
        System.Timers.Timer timer = new System.Timers.Timer(1000);
        System.Timers.Timer timer30sec = new System.Timers.Timer(30000);
        int numberOfPages = 1;
        int currentPage = 1;
        string beleskeText = "";
        bool uploadingExcelToDropbox = false;
        bool evidencijePomerene = false;
        string brPredmetakliknutaUTaskManageru = "";
        string imeKorisnika = "";
        bool closeForm = false;
        bool startupFinished = false;
        public bool podesavanjaOtvorena = false;

        private List<ListaDanasnjihEvidencija> tempDanasnjeEvidencije = new List<ListaDanasnjihEvidencija>();
        private int tempAktivniPredmetiBr = 0;
        private int tempArhiviraniPredmetiBr = 0;
        private List<ListaRocista> tempRocistaZaListu = new List<ListaRocista>();
        private List<ListaEvidencijaZaNarednih15Dana> tempEvidencijeZaNaredneDane = new List<ListaEvidencijaZaNarednih15Dana>();
        private DnevneBeleske tempBeleske = new DnevneBeleske();

        private PrintDocument printBeleske = new PrintDocument();
        private int checkPrint = 0;

        public Form1()
        {
            InitializeComponent();

            printBeleske.BeginPrint += printBeleske_BeginPrint;
            printBeleske.PrintPage += printBeleske_PrintPage;

            if ((string)Properties.Settings.Default["imeKorisnika"] == "korisnik")
            {
                imeKorisnika = "korisnik-" + Guid.NewGuid();
                Properties.Settings.Default["imeKorisnika"] = imeKorisnika;
            }
            else {
                imeKorisnika = (string)Properties.Settings.Default["imeKorisnika"];
            }

            BackColor = GlobalVariables.background_color;

            if (BackColor == Color.Thistle)
            {
                tbBeleske.BackColor = Color.FromKnownColor(KnownColor.Info);
            }
            else {
                tbBeleske.BackColor = Color.FromKnownColor(KnownColor.White);
            }

            ObjectListView.IgnoreMissingAspects = true;

            if (!DatabaseCommunication.testDbConnection(this, null, null))
            {
                MessageBox.Show("Problem sa vezom ka bazi podataka! Program ne može da nastavi sa radom bez baze, podesite vezu!");

                Podesavanja settings = new Podesavanja(this);
                settings.ShowDialog();
                settings.Dispose();
            }

            if (GlobalVariables.IsOnline())
            {
                GoogleCalendarCommunication.createConnection(this);
            }

            bool mongo_is_installed = false;

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Service");
            ManagementObjectCollection collection = searcher.Get();

            foreach (ManagementObject obj in collection)
            {
                string name = obj["Name"] as string;
                string pathName = obj["PathName"] as string;

                if (pathName.Contains("mongod")) {
                    mongo_is_installed = true;
                }
            }

            GlobalVariables.mongo_is_installed = mongo_is_installed;

            pnlBackup.Visible = false;

            timer.Elapsed += async (sender, e) => await refreshList();
            timer30sec.Elapsed += async (sender, e) => await refreshDanasnjeEvidencije();
            timer.Stop();
            timer30sec.Stop();
        }

        public event EventHandler LoadCompleted;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.OnLoadCompleted(EventArgs.Empty);
        }
        protected virtual void OnLoadCompleted(EventArgs e)
        {
            LoadCompleted?.Invoke(this, e);
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            startup();
        }

        public void startup()
        {
            List<KategorijePredmeta> listaKategorija = DatabaseCommunication.getKategorijePredmeta();

            bool raznoPostoji = false;

            foreach (KategorijePredmeta item in listaKategorija)
            {
                if (item.naziv == "Razno")
                {
                    raznoPostoji = true;
                    break;
                }
            }

            if (!raznoPostoji)
            {
                DatabaseCommunication.upisNovuKategorijuPredmetaUBazu(this, "Razno", false);
                List<PredmetData> sviPredmeti = DatabaseCommunication.getSviPredmeti();

                foreach (PredmetData predmet in sviPredmeti)
                {
                    if (string.IsNullOrEmpty(predmet.kategorija))
                    {
                        predmet.kategorija = "Razno";
                        DatabaseCommunication.updatePredmet(this, predmet, false);
                    }
                }
            }
            //rtf ima svoje stilove pa ga ne mozemo ostaviti praznog il ce brisati beleske
            beleskeText = tbBeleske.Rtf;

            //pomeri sve evidencije koje nisu pomerene od prethodnih dana za danas
            pomeriStaraRocistaZaDanas();
            pomeriStareEvidencijeZaDanas();
            evidencijePomerene = true;
            
            tempAktivniPredmetiBr = DatabaseCommunication.getBrojArhiviranihIliAktivnihPredmetaUBazi(true);
            tempArhiviraniPredmetiBr = DatabaseCommunication.getBrojArhiviranihIliAktivnihPredmetaUBazi(false);
            tempDanasnjeEvidencije = DatabaseCommunication.getDanasnjeEvidencije(DateTime.Now);

            tempRocistaZaListu = DatabaseCommunication.getRocistaZaVremensiRaspon(DateTime.Now.FirstDayOfWeek(), DateTime.Now.LastDayOfWeek());

            tempEvidencijeZaNaredneDane = DatabaseCommunication.getBrojEvidencijaZaNaredneDane();
            tempBeleske = DatabaseCommunication.getDnevneBeleske();

            if (GlobalVariables.mongo_is_installed)
            {
                btnAdminOtkljucajBeleske.Visible = true;
            }
        }

        public void proveriTimer() {
            //  try
            //    {
            //ako timeri ne rade upali ih
                if (!timer.Enabled)
                {
                    timer.Start();
                }

                if (!timer30sec.Enabled)
                {
                    timer30sec.Start();
                }
         /*   }
            catch (ObjectDisposedException ex) {

            }*/
        }

        public void osveziKorisnika() {
            imeKorisnika = (string)Properties.Settings.Default["imeKorisnika"];
        }

        public void osveziBrojArhiviranihIAktivnihPredmeta(bool startupCall = false)
        {
            if (lblBrAktivnihPredmeta.InvokeRequired || lblBrojArhiviranihPredmeta.InvokeRequired || groupBox1.InvokeRequired)
            {
                if (lblBrAktivnihPredmeta.IsHandleCreated && lblBrojArhiviranihPredmeta.IsHandleCreated && groupBox1.IsHandleCreated)
                {
                    OsveziBrojArhiviranihIAktivnihPredmetaCallback d = new OsveziBrojArhiviranihIAktivnihPredmetaCallback(osveziBrojArhiviranihIAktivnihPredmeta);
                    Invoke(d, startupCall);
                }
            }
            else
            {
                int aktivni = startupCall ? tempAktivniPredmetiBr : DatabaseCommunication.getBrojArhiviranihIliAktivnihPredmetaUBazi(true);
                int arhivirani = startupCall ? tempArhiviraniPredmetiBr : DatabaseCommunication.getBrojArhiviranihIliAktivnihPredmetaUBazi(false);
                lblBrAktivnihPredmeta.Text = "Aktivni = " + aktivni.ToString();
                lblBrojArhiviranihPredmeta.Text = "Arhivirani = " + arhivirani.ToString();
                groupBox1.Text = "Broj predmeta = " + (aktivni + arhivirani).ToString();
            }
        }

        private void pomeriStareEvidencijeZaDanas() {
            List<PredmetData> predmetiSaStarimEvidencijama = DatabaseCommunication.getAktivniPredmetiSaEvidencijamaZaPrethodneDane();
            List<ListaEvidencija> evidencijeZaPostavitiNaKalendaru = new List<ListaEvidencija>();

            foreach (PredmetData predmet in predmetiSaStarimEvidencijama)
            {
                foreach (ListaEvidencija evidencija in predmet.listaEvidencija)
                {
                    if (evidencija.datum < DateTime.Now)
                    {
                        evidencija.datum = DateTime.Now;
                    }
                }

                DatabaseCommunication.databaseFixUpdatePredmet(predmet);
                PredmetData newPredmet =
                    DatabaseCommunication.getPredmetByBrojPredmeta(predmet.brojPredmetaBr, predmet.brojPredmetaGod);

                string predmetIStranka = GlobalVariables.spojBrojPredmeta(newPredmet.brojPredmetaBr, newPredmet.brojPredmetaGod) + " " + newPredmet.stranka;

                List<ListaEvidencija> evidencijeZaKalendar = newPredmet.listaEvidencija.ConvertAll(evidencija => new ListaEvidencija {
                    datum = evidencija.datum,
                    id = evidencija.id,
                    imaEventNaGoogleKalendaru = evidencija.imaEventNaGoogleKalendaru,
                    prioritet = evidencija.prioritet,
                    radnikId = evidencija.radnikId,
                    tekstEvidencije = predmetIStranka + " " + evidencija.tekstEvidencije
                });
                evidencijeZaPostavitiNaKalendaru.AddRange(evidencijeZaKalendar);
            }

            if (predmetiSaStarimEvidencijama.Count > 0) {
                MessageBox.Show(this, "Pronađeno je " + predmetiSaStarimEvidencijama.Count.ToString() + " starih evidencija i pomereno za danas.", "Obaveštenje!");
                GoogleCalendarCommunication.updateEvidencijeAkoPostojeNaKalendaru(this, evidencijeZaPostavitiNaKalendaru, true);
            }
        }

        private void pomeriStaraRocistaZaDanas() {
            List<PredmetData> predmetiSaStarimRocistima = DatabaseCommunication.getAktivniPredmetiSaRocistimaZaPrethodneDane();
            List<Rociste> rocistaZaUpdate = new List<Rociste>();

            foreach (PredmetData predmet in predmetiSaStarimRocistima)
            {
                foreach (Rociste rociste in predmet.listaRocista)
                {
                    if (rociste.datum < DateTime.Now)
                    {
                        rociste.datum = DateTime.Now;
                    }
                }

                DatabaseCommunication.databaseFixUpdatePredmet(predmet);

                PredmetData newPredmet =
                    DatabaseCommunication.getPredmetByBrojPredmeta(predmet.brojPredmetaBr, predmet.brojPredmetaGod);

                string predmetIStranka = GlobalVariables.spojBrojPredmeta(newPredmet.brojPredmetaBr, newPredmet.brojPredmetaGod) + " " + newPredmet.stranka;

                List<Rociste> rocistaZaKalendar = newPredmet.listaRocista.ConvertAll(rociste => new Rociste
                {
                    datum = rociste.datum,
                    id = rociste.id,
                    Id = rociste.Id,
                    text = predmetIStranka + " " + rociste.text
                });
                rocistaZaUpdate.AddRange(rocistaZaKalendar);
            }

            if (predmetiSaStarimRocistima.Count > 0)
            {
                MessageBox.Show(this, "Pronađeno je " + predmetiSaStarimRocistima.Count.ToString() + " starih ročišta i pomereno za danas.", "Obaveštenje!");

                GoogleCalendarCommunication.updateRocistaAkoPostojeNaKalendaru(this, rocistaZaUpdate, true);
            }
        }

        private void ubaciDanasnjeEvidencijeUListu(List<ListaDanasnjihEvidencija> listaEvidencija)
        {
            olvEvidencije.ClearObjects();
            //dodaj podatke u listu
            olvEvidencije.SetObjects(listaEvidencija);

            //ofarbaj redove po prioritetu
            olvEvidencije.UseCellFormatEvents = true;
            olvEvidencije.FormatRow += (sender, args) =>
            {
                ListaDanasnjihEvidencija evidencija = (ListaDanasnjihEvidencija)args.Model;

                switch (evidencija.prioritet)
                {
                    case 1:
                        args.Item.BackColor = GlobalVariables.evidencija_priority_1;
                        break;
                    case 2:
                        args.Item.BackColor = GlobalVariables.evidencija_priority_2;
                        break;
                    case 3:
                        args.Item.BackColor = GlobalVariables.evidencija_priority_3;
                        break;
                    default:
                        args.Item.BackColor = GlobalVariables.evidencija_priority_default;
                        break;
                }
            };

            //sortiraj po prioritetu
            olvEvidencije.Sort(olvColumn13, SortOrder.Ascending);

            lblDanasnjeEvidencijeOsvezavanje.Text = "Zadnji put osveženo " + DateTime.Now.ToString("hh:mm:ss");
        }

        private void ubaciRocistaUListu(List<ListaRocista> listaRocista) {
            olvRocista.ClearObjects();
            olvRocista.SetObjects(listaRocista);
        }

        private void ubaciEvidencijeZaNarednih15DanaUListu(List<ListaEvidencijaZaNarednih15Dana> listaEvidencija)
        {
            if (olvBrojEvidencijaZaNaredneDane.InvokeRequired)
            {
                if (olvBrojEvidencijaZaNaredneDane.IsHandleCreated)
                {
                    ubaciEvidencijeZaNarednih15DanaUListuCallback d = new ubaciEvidencijeZaNarednih15DanaUListuCallback(ubaciEvidencijeZaNarednih15DanaUListu);
                    Invoke(d, listaEvidencija);
                }
            }
            else
            {
                List<ListaEvidencijaZaNarednih15DanaOLV> zaOlv = new List<ListaEvidencijaZaNarednih15DanaOLV>();

                foreach (var item in listaEvidencija)
                {

                    zaOlv.Add(new ListaEvidencijaZaNarednih15DanaOLV()
                    {
                        datum = item.datum.ToString(GlobalVariables.date_string_pattern),
                        brojEvidencija = item.brojEvidencija
                    });
                    //item.datum = ;
                }
                olvBrojEvidencijaZaNaredneDane.ClearObjects();
                olvBrojEvidencijaZaNaredneDane.SetObjects(zaOlv);
            }
        }

        private void btnOdstampajDanasnjeEvidencije_Click(object sender, EventArgs e)
        {
            int oldCol1Width = olvColumn1.Width;
            int oldCol2Width = olvColumn2.Width;
            int oldCol3Width = olvColumn3.Width;
            int oldCol14Width = olvColumn14.Width;

            olvColumn1.Text = "Br.";

            olvColumn1.Width = 100;
            olvColumn2.Width = 600;
            olvColumn3.Width = 400;
            olvColumn14.Width = 400;
            lvpEvidencije.Header = DateTime.Now.ToString(GlobalVariables.date_time_string_pattern);
            BlockFormat hf = new BlockFormat();
            hf.TextColor = Color.Black;

            Font fFnt = new Font(olvEvidencije.Font.FontFamily, 14, FontStyle.Regular);
            lvpEvidencije.CellFormat.Font = fFnt;

            lvpEvidencije.HeaderFormat = hf;
            lvpEvidencije.PrintWithDialog();

            olvColumn1.Text = "Broj predmeta";
            olvColumn1.Width = oldCol1Width;
            olvColumn2.Width = oldCol2Width;
            olvColumn3.Width = oldCol3Width;
            olvColumn14.Width = oldCol14Width;
        }

        #region button events
        private void btnPodesavanjaPrograma_Click(object sender, EventArgs e)
        {
            Podesavanja podesavanja = new Arhiviranje_dokumenata.Podesavanja(this);
            podesavanja.ShowDialog();
            podesavanja.Dispose();
        }
        private void btnDodajNovPremdet_Click(object sender, EventArgs e)
        {
            Predmet novPredmet = new Predmet(-1, -1, this);
            novPredmet.ShowDialog();
            novPredmet.Dispose();
        }

        private void btnPretraziPredmete_Click(object sender, EventArgs e)
        {
            Pretraga pretraga = new Pretraga(this);
            pretraga.ShowDialog();
            pretraga.Dispose();
        }
        #endregion

        private void btnSnimiKaoExcel_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            saveFileDialog1.Dispose();
        }

        private void exportToExcel(string fileName, bool deleteFile)
        {
            if (DatabaseCommunication.testDbConnection(this, null, null))
            {
                Type officeType = Type.GetTypeFromProgID("Excel.Application");
                if (officeType != null && !uploadingExcelToDropbox)
                {
                    try
                    {
                        lockAll();
                        uploadingExcelToDropbox = true;
                        Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                        app.Visible = false;
                        Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(1);
                        Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[1];

                        var listOfDataForExcel = DatabaseCommunication.getAllDataForExcel();

                        //write column names in first row
                        ws.Cells[1, 1] = "Broj predmeta";
                        ws.Cells[1, 2] = "Stranka";
                        ws.Cells[1, 3] = "Suprotna strana";
                        ws.Cells[1, 4] = "Vrsta spora";
                        ws.Cells[1, 5] = "Sudski broj";
                        ws.Cells[1, 6] = "Ime sudije";
                        ws.Cells[1, 7] = "Datum kreiranja predmeta";
                        ws.Cells[1, 8] = "Broj telefona";
                        ws.Cells[1, 9] = "Evidencije";
                        ws.Cells[1, 10] = "Ročišta";

                        Microsoft.Office.Interop.Excel.Range rng = ws.Range[ws.Cells[1, 1], ws.Cells[1, 10]];

                        //bold first row
                        rng.Font.Bold = true;
                        //change background color of first row
                        rng.Interior.Color = ColorTranslator.ToOle(Color.LightGray);

                        int i = 2;//start writing from the second row

                        foreach (PredmetData predmet in listOfDataForExcel)
                        {
                            ws.Cells[i, 1] = predmet.brojPredmetaBr.ToString() + "/" + predmet.brojPredmetaGod.ToString();
                            ws.Cells[i, 2] = predmet.stranka;
                            ws.Cells[i, 3] = predmet.suprotnaStrana;
                            ws.Cells[i, 4] = predmet.vrstaPredmeta;
                            ws.Cells[i, 5] = predmet.poslovniBroj;
                            ws.Cells[i, 6] = predmet.imeSudije;
                            ws.Cells[i, 7] = predmet.datumFormiranja.ToString(GlobalVariables.date_string_pattern);
                            ws.Cells[i, 8] = predmet.brTelefona;

                            string evidencije = "";
                            int index = 1;

                            if (predmet.listaEvidencija is IEnumerable)
                            {
                                foreach (ListaEvidencija evidencija in predmet.listaEvidencija)
                                {
                                    evidencije += "\n" + index++.ToString() + " - " + evidencija.tekstEvidencije + ". Datum - " + evidencija.datum.ToString(GlobalVariables.date_string_pattern);
                                }
                            }
                            ws.Cells[i, 9] = evidencije;

                            string rocista = "";
                            index = 1;
                            if (predmet.listaRocista is IEnumerable)
                            {
                                foreach (Rociste rociste in predmet.listaRocista)
                                {
                                    rocista += "\n" + index++.ToString() + " - " + rociste.text + ". Datum - " + rociste.datum.ToString(GlobalVariables.date_string_pattern);
                                }
                            }
                            ws.Cells[i, 10] = rocista;
                            i++;
                        }

                        //automatically sets width of columns so that the cell content doesnt overlap
                        ws.Columns.AutoFit();

                        wb.SaveAs(fileName);

                        app.Quit();

                        if (GlobalVariables.IsOnline())
                        {
                            Task.Run(() => dropbox.uploadExcel(this, fileName, deleteFile));
                        }
                        else {
                            dropboxUploadDone(deleteFile, fileName);
                        }
                    }
                    catch (Exception e) {
                        //MessageBox.Show("Došlo je do problema pri snimanju excel tabele: " + e.Message + "\n\nPogasite sve otvorene excel dokumente i pokusajte ponovo.");
                        unlockAll();
                    }
                }
                else {
                    MessageBox.Show(this, "Na ovom računaru nije instaliran Microsoft Excel. Program ne može da napravi Excel tabelu bez Excel-a.", "Greška!");
                }
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show(this, "Veza sa bazom je izgubljena. \nDa li želite da pokušate ponovo ili da ugasite program? \n(Yes pokušava ponovo, No gasi program)", "Greška!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    exportToExcel(fileName, deleteFile);
                }
                else if (dialogResult == DialogResult.No)
                {
                    Process.GetCurrentProcess().Kill();
                }
            }
        }

        private void lockAll() {
            foreach (Control ctrl in Controls) {
                if (ctrl is Button || ctrl is ObjectListView || ctrl is RichTextBox)
                {
                    ctrl.Enabled = false;
                }
            }
        }

        private void unlockAll() {
            foreach (Control ctrl in Controls)
            {
                if (ctrl is Button || ctrl is ObjectListView || ctrl is RichTextBox)
                {
                    ctrl.Enabled = true;
                }
            }
        }

        public void backupDatabaseAsExcelToDropbox() {
            string filePath = @"C:\automatskiExcelBackup.xlsx";
            exportToExcel(filePath, true);
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (saveFileDialog1.FileName != "")
            {
                exportToExcel(saveFileDialog1.FileName, false);
            }
        }

        private void olvEvidencije_DoubleClick(object sender, EventArgs e)
        {
            if (olvEvidencije.SelectedItems.Count == 1)
            {
                string brPredmeta = olvEvidencije.SelectedItems[0].SubItems[0].Text;
                var brojPredmeta = GlobalVariables.razbijBrojPredmeta(brPredmeta);
                Predmet predmet = new Predmet(brojPredmeta[0], brojPredmeta[1], this);
                predmet.ShowDialog();
                predmet.Dispose();
            }
        }

        public void reloadDanasnjeEvidencije() {
            if (olvEvidencije.InvokeRequired)
            {
                if (olvEvidencije.IsHandleCreated)
                {
                    ReloadBeleskeIRocistaCallback d = new ReloadBeleskeIRocistaCallback(reloadDanasnjeEvidencije);
                    Invoke(d);
                }
            }
            else
            {
                try
                {
                    if (!olvEvidencije.Focused)
                    {
                        ubaciDanasnjeEvidencijeUListu(DatabaseCommunication.getDanasnjeEvidencije(DateTime.Now));
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(this, "Došlo je do greške pri čitanju današnjih evidencija: " + e.Message, "Greška!");
                }
            }
        }

        public void reloadRocista()
        {
            if (olvRocista.InvokeRequired)
            {
                if (olvRocista.IsHandleCreated)
                {
                    ReloadBeleskeIRocistaCallback d = new ReloadBeleskeIRocistaCallback(reloadRocista);
                    Invoke(d);
                }
            }
            else
            {
                if (!olvRocista.Focused)
                {
                    try
                    {
                        ubaciRocistaUListu(DatabaseCommunication.getRocistaZaVremensiRaspon(DateTime.Now.FirstDayOfWeek(), DateTime.Now.LastDayOfWeek()));
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(this, "Došlo je do greške pri čitanju ročišta: " + e.Message, "Greška!");
                    }
                }
            }
        }

        public void reloadData(bool startupCall = false)
        {
            if(olvEvidencije.InvokeRequired || olvBrojEvidencijaZaNaredneDane.InvokeRequired || tbBeleske.InvokeRequired || olvRocista.InvokeRequired)
            {
                if (olvEvidencije.IsHandleCreated && olvBrojEvidencijaZaNaredneDane.IsHandleCreated && tbBeleske.IsHandleCreated && olvRocista.IsHandleCreated)
                {
                    ReloadDataCallback d = new ReloadDataCallback(reloadData);
                    Invoke(d, startupCall);
                }
            }
            else
            {
                if (!olvBrojEvidencijaZaNaredneDane.Focused)
                {
                    try
                    {
                        ubaciEvidencijeZaNarednih15DanaUListu(startupCall ? tempEvidencijeZaNaredneDane : DatabaseCommunication.getBrojEvidencijaZaNaredneDane());
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(this, "Došlo je do greške pri čitanju evidencija za 15 dana: " + e.Message, "Greška!");
                    }
                }


                if (tbBeleske.ReadOnly && !tbBeleske.Focused)
                {
                    try
                    {
                        DnevneBeleske beleske = startupCall ? tempBeleske : DatabaseCommunication.getDnevneBeleske();

                        if (GlobalVariables.IsValidRtf(beleske.textBeleske))
                        {
                            tbBeleske.Rtf = beleske.textBeleske;
                        }
                        else
                        {
                            tbBeleske.Text = beleske.textBeleske;
                        }

                        if (beleske.zakljucano)
                        {
                            if (beleske.zakljucaoIme != imeKorisnika)
                            {
                                if (!lblBeleskeZakljucano.Visible || !lblBeleskeZakljucao.Visible)
                                {
                                    lblBeleskeZakljucano.Visible = true;
                                    lblBeleskeZakljucao.Visible = true;
                                    lblBeleskeZakljucao.Text = beleske.zakljucaoIme;
                                    tbBeleske.ReadOnly = true;
                                }
                            }
                        }
                        else
                        {
                            if (lblBeleskeZakljucano.Visible || lblBeleskeZakljucao.Visible)
                            {
                                lblBeleskeZakljucano.Visible = false;
                                lblBeleskeZakljucao.Visible = false;
                            }
                            btnMenjajBeleske.Text = "Izmeni beleške";
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(this, "Došlo je do greške pri čitanju beleški: " + e.Message, "Greška!");
                    }
                }
                
            }
        }

        private void btnNapraviBackupBaze_Click(object sender, EventArgs e)
        {
            bekapujBazu();
        }

        public bool bekapujBazu(bool uploadToDropbox = true) {

            if (GlobalVariables.mongo_is_installed)
            {
                try
                {
                    updateBackupInfo("Pravim lokalni bekap");
                    pnlBackup.Visible = true;

                    lockAll();

                    string path = AppDomain.CurrentDomain.BaseDirectory;//path to application directory
                    string host = "127.0.0.1";
                    string newFolderName = DateTime.Now.ToString(GlobalVariables.database_backup_name_string_pattern);
                    string fullPath = "\"" + path + "mongo_backup\\" + newFolderName + "\"";
                    // /c da se sam gasi /k da se ne gasi
                    Process backupProcess = Process.Start("cmd", "/c mongodump --host " + host + " --port 27017 --db Arhiviranje_Dokumenata --out " + fullPath);

                    backupProcess.WaitForExit();

                    //check if backup was created
                    fullPath = fullPath.Substring(1, fullPath.Length - 2);


                    bool beleskeBackupDone = true;

                    var beleske = DatabaseCommunication.getDnevneBeleske();

                    if (!String.IsNullOrWhiteSpace(beleske.textBeleske))
                    {
                        beleskeBackupDone = File.Exists(fullPath + "\\Arhiviranje_Dokumenata\\beleske.bson");
                    }

                    if (File.Exists(fullPath + "\\Arhiviranje_Dokumenata\\predmet.bson") && beleskeBackupDone)
                    {
                        MessageBox.Show(this, "Lokalni bekap je uspešno napravljen!", "Uspeh!");

                        //if we want to upload to dropbox
                        if (uploadToDropbox)
                        {
                            if (GlobalVariables.IsOnline())
                            {
                                updateBackupInfo("Uploadujem backup na dropbox");

                                Task.Run(() => dropbox.startBackup(this, fullPath + "\\Arhiviranje_Dokumenata\\", newFolderName));
                            }
                            else
                            {
                                backupUploadDone();
                            }
                        }
                        else {
                            backupUploadDone();
                        }
                        DatabaseCommunication.cleanBackup(this);
                    }
                    else
                    {
                        MessageBox.Show(this, "Došlo je do greške pri pravljenju bekapa!", "Greška!");
                        unlockAll();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Došlo je do greške pri pravljenju bekapa: " + ex.Message);
                    backupUploadDone();
                }
            }

            return true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!closeForm)
            {
                if (!tbBeleske.ReadOnly)
                {
                    DatabaseCommunication.upisiDnevneBeleskeUBazu(this, tbBeleske.Rtf, false, "");
                }
                timer.Dispose(); //stops auto update of lvSviPredmeti
                timer30sec.Dispose();

                closeForm = true;
                e.Cancel = true;
                btnNapraviBackupBaze.PerformClick();
            }
        }

        private void updatePaginationInformation()
        {
            lblStrana.Text = currentPage.ToString() + "/" + numberOfPages.ToString();
        }

        private void btnSledecaStrana_Click(object sender, EventArgs e)
        {
            if (currentPage < numberOfPages) {
                updateLVSviPredmeti(DatabaseCommunication.getPage(++currentPage));
            }
        }

        private void btnPrethodnaStrana_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                updateLVSviPredmeti(DatabaseCommunication.getPage(--currentPage));
            }
        }

        private async Task refreshDanasnjeEvidencije() {
            if (DatabaseCommunication.testDbConnection(this, null, null))
            {
                reloadDanasnjeEvidencije();
            }
            else
            {
                timer30sec.Stop();
                timer.Stop();

                if (!podesavanjaOtvorena)
                {
                    podesavanjaOtvorena = true;
                    GreskaPonudiOpcijeIliGasenje greska = new GreskaPonudiOpcijeIliGasenje(this);
                    greska.ShowDialog();
                    greska.Dispose();
                }
            }
        }

        private async Task refreshList(bool startupCall = false)
        {
            if (DatabaseCommunication.testDbConnection(this, null, null))
            {
                try
                {
                    updateLVSviPredmeti(DatabaseCommunication.getPage(currentPage));
                    reloadData(startupCall);
                    osveziBrojArhiviranihIAktivnihPredmeta();
                    reloadRocista();
                    if (!evidencijePomerene)
                    {
                        pomeriStareEvidencijeZaDanas();
                    }
                }
                catch (Exception e) {
                    MessageBox.Show(this, "Došlo je do greške pri osvežavanju podataka: " + e.Message, "Greška!");
                }
            }
            else
            {
                timer.Stop();
                timer30sec.Stop();

                if (!podesavanjaOtvorena)
                {
                    podesavanjaOtvorena = true;
                    GreskaPonudiOpcijeIliGasenje greska = new GreskaPonudiOpcijeIliGasenje(this);
                    greska.ShowDialog();
                    greska.Dispose();
                }
            }
        }

        delegate void UpdateListViewCallback(PaginacijaPodaci podaci);
        delegate void ReloadDataCallback(bool startupCall);
        delegate void ReloadBeleskeIRocistaCallback();
        delegate void OsveziBrojArhiviranihIAktivnihPredmetaCallback(bool startupCall);
        delegate void ubaciEvidencijeZaNarednih15DanaUListuCallback(List<ListaEvidencijaZaNarednih15Dana> listaEvidencija);

        private void updateLVSviPredmeti(PaginacijaPodaci podaci)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (olvSviPredmeti.InvokeRequired)
            {
                if (olvSviPredmeti.IsHandleCreated)
                {
                    UpdateListViewCallback d = new UpdateListViewCallback(updateLVSviPredmeti);
                    Invoke(d, new object[] { podaci });
                }
            }
            else
            {
                if (!olvSviPredmeti.Focused)
                {
                    List<PredmetDataOlv> zaOlv = new List<PredmetDataOlv>();

                    foreach (var item in podaci.items)
                    {

                        zaOlv.Add(new PredmetDataOlv()
                        {
                            brojPredmeta = GlobalVariables.spojBrojPredmeta(item.brojPredmetaBr, item.brojPredmetaGod),
                            stranka = item.stranka,
                            vrstaPredmeta = item.vrstaPredmeta,
                            poslovniBroj = item.poslovniBroj,
                            imeSudije = item.imeSudije,
                            suprotnaStrana = item.suprotnaStrana,
                            datumFormiranja = item.datumFormiranja.ToString(GlobalVariables.date_string_pattern),
                            predmetJeAktivan = item.predmetJeAktivan.ToString()
                        });
                    }
                    olvSviPredmeti.ClearObjects();
                    olvSviPredmeti.SetObjects(zaOlv);

                    //farbanje arhiviranih predmeta
                    olvSviPredmeti.UseCellFormatEvents = true;
                    olvSviPredmeti.FormatRow += (sender, args) =>
                    {
                        PredmetDataOlv evidencija = (PredmetDataOlv)args.Model;

                        if (evidencija.predmetJeAktivan == "False")
                        {
                            args.Item.BackColor = GlobalVariables.arhivirana_evidencija_color;
                        }
                    };

                    numberOfPages = podaci.maxPages;
                    updatePaginationInformation();
                }
            }
        }

        private void lvSviPredmeti_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (olvSviPredmeti.SelectedItems.Count == 1)
            {
                var brPredmeta = GlobalVariables.razbijBrojPredmeta(olvSviPredmeti.SelectedItems[0].SubItems[0].Text);

                Predmet predmet = new Predmet(brPredmeta[0], brPredmeta[1], this);
                predmet.ShowDialog();
                predmet.Dispose();
            }
        }

        private void lvBrojEvidencijaZaNaredneDane_DoubleClick(object sender, EventArgs e)
        {
            if (olvBrojEvidencijaZaNaredneDane.SelectedItems.Count == 1) {
                DateTime datum = DateTime.ParseExact(olvBrojEvidencijaZaNaredneDane.SelectedItems[0].SubItems[0].Text, GlobalVariables.date_string_pattern, CultureInfo.InvariantCulture);
                Evidencije_za_dan forma = new Evidencije_za_dan(datum, this);
                forma.ShowDialog();
                forma.Dispose();
            }
        }

        private void olvBrojEvidencijaZaNaredneDane_DoubleClick(object sender, EventArgs e)
        {
            if (olvBrojEvidencijaZaNaredneDane.SelectedItems.Count == 1)
            {
                DateTime datum = DateTime.ParseExact(olvBrojEvidencijaZaNaredneDane.SelectedItems[0].SubItems[0].Text, GlobalVariables.date_string_pattern, CultureInfo.InvariantCulture);
                Evidencije_za_dan forma = new Evidencije_za_dan(datum, this);
                forma.ShowDialog();
                forma.Dispose();
            }
        }

        private void olvSviPredmeti_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (olvSviPredmeti.SelectedItems.Count == 1)
            {
                var brPredmeta = GlobalVariables.razbijBrojPredmeta(olvSviPredmeti.SelectedItems[0].SubItems[0].Text);
                Predmet predmet = new Predmet(brPredmeta[0], brPredmeta[1], this);
                predmet.ShowDialog();
                predmet.Dispose();
            }
        }

        private void otvoriPredmetPoEvidencijiTM(object sender, EventArgs e)
        {
            var brojPredmeta = GlobalVariables.razbijBrojPredmeta(brPredmetakliknutaUTaskManageru);
            Predmet predmet = new Predmet(brojPredmeta[0], brojPredmeta[1], this);
            predmet.ShowDialog();
            predmet.Dispose();
        }

        private void btnStampajTM_Click(object sender, EventArgs e)
        {
            lvpRocista.PrintWithDialog();
        }

        private void btnPrvaStrana_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            updateLVSviPredmeti(DatabaseCommunication.getPage(currentPage));
        }

        private void btnPoslednjaStrana_Click(object sender, EventArgs e)
        {
            currentPage = numberOfPages;
            updateLVSviPredmeti(DatabaseCommunication.getPage(currentPage));
        }

        private void btnPredmetiPoKategoriji_Click(object sender, EventArgs e)
        {
            Predmeti_po_kategoriji predmeti = new Predmeti_po_kategoriji(this);
            predmeti.ShowDialog();
            predmeti.Dispose();
        }

        private void btnPredmetiPoRadniku_Click_1(object sender, EventArgs e)
        {
            PredmetiPoRadniku predmeti = new PredmetiPoRadniku(this);
            predmeti.ShowDialog();
            predmeti.Dispose();
        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            if (!tbBeleske.ReadOnly)
            {
                if (!tbBeleske.SelectionFont.Bold)
                {
                    tbBeleske.SelectionFont = new Font(tbBeleske.Font, FontStyle.Bold);
                    tbBeleske.SelectionStart = tbBeleske.SelectionStart + tbBeleske.SelectionLength;
                    tbBeleske.SelectionLength = 0;
                    // Set font immediately after selection
                    tbBeleske.SelectionFont = tbBeleske.Font;
                }
                else
                {
                    tbBeleske.SelectionFont = tbBeleske.Font;
                    tbBeleske.SelectionStart = tbBeleske.SelectionStart + tbBeleske.SelectionLength;
                    tbBeleske.SelectionLength = 0;
                }
            }
        }

        private void olvRocista_DoubleClick(object sender, EventArgs e)
        {
            if (olvRocista.SelectedItems.Count == 1)
            {
                string brPredmeta = olvRocista.SelectedItems[0].SubItems[0].Text;
                var brojPredmeta = GlobalVariables.razbijBrojPredmeta(brPredmeta);
                Predmet predmet = new Predmet(brojPredmeta[0], brojPredmeta[1], this);
                predmet.ShowDialog();
                predmet.Dispose();
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

            olvColumn1Rocista.Width = 100;
            olvColumn2Rocista.Width = 300;
            olvColumn3Rocista.Width = 600;
            olvColumn4Rocista.Width = 200;
            olvColumn5Rocista.Width = 100;
            olvColumn6Rocista.Width = 100;

            Font fFnt = new Font(olvRocista.Font.FontFamily, 14, FontStyle.Regular);
            lvpRocista.CellFormat.Font = fFnt;

            lvpRocista.PrintWithDialog();

            olvColumn1Rocista.Text = "Broj predmeta";
            olvColumn1Rocista.Width = oldCol1Width;
            olvColumn2Rocista.Width = oldCol2Width;
            olvColumn3Rocista.Width = oldCol3Width;
            olvColumn4Rocista.Width = oldCol4Width;
            olvColumn5Rocista.Width = oldCol5Width;
            olvColumn6Rocista.Width = oldCol6Width;
        }

        private void btnPretraziRocista_Click(object sender, EventArgs e)
        {
             PretragaRocista pretragaRocista = new PretragaRocista(this);
             pretragaRocista.ShowDialog();
             pretragaRocista.Dispose();
        }

        private void btnPrintBeleske_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == DialogResult.OK) { 
                printBeleske.Print();
            }
        }

        private void printBeleske_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Print the content of RichTextBox. Store the last character printed.
            checkPrint = tbBeleske.Print(checkPrint, tbBeleske.TextLength, e);

            // Check for more pages
            e.HasMorePages = checkPrint < tbBeleske.TextLength;
        }

        private void printBeleske_BeginPrint(object sender, PrintEventArgs e)
        {
            checkPrint = 0;
        }

        private void btnMenjajBeleske_Click(object sender, EventArgs e)
        {
            if (tbBeleske.ReadOnly)
            {
                //vidi da li moze da se otkljuca za izmene
                DnevneBeleske beleske = DatabaseCommunication.getDnevneBeleske();
                if (!beleske.zakljucano || beleske.zakljucaoIme == imeKorisnika)
                {
                    if (GlobalVariables.IsValidRtf(beleske.textBeleske))
                    {
                        tbBeleske.Rtf = beleske.textBeleske;
                    }
                    else
                    {
                        tbBeleske.Text = beleske.textBeleske;
                    }

                    tbBeleske.ReadOnly = false;

                    DatabaseCommunication.upisiDnevneBeleskeUBazu(this, tbBeleske.Rtf, true, imeKorisnika);

                    btnMenjajBeleske.Text = "Snimi izmene";
                }
            }
            else {
                //snimi izmene
                DatabaseCommunication.upisiDnevneBeleskeUBazu(this, tbBeleske.Rtf, false, "");
                tbBeleske.ReadOnly = true;
            }
        }

        private void btnAdminOtkljucajBeleske_Click(object sender, EventArgs e)
        {
            DatabaseCommunication.upisiDnevneBeleskeUBazu(this, tbBeleske.Rtf, false, "");
        }

        private void btnListaPlacanjaDugovanja_Click(object sender, EventArgs e)
        {
            TraziSifru traziSifru = new TraziSifru(this, null);
            traziSifru.ShowDialog();
            traziSifru.Dispose();
        }

        public void otvoriListuPlacanjaDugovanja()
        {
            System.Timers.Timer providedTimer = new System.Timers.Timer(200);
            providedTimer.Elapsed += async (sender, e) => await otvoriPlacanjaDugovanja(providedTimer, this);
            providedTimer.Start();
        }

        private async Task otvoriPlacanjaDugovanja(System.Timers.Timer providedTimer, Form1 mainThread)
        {
            providedTimer.Stop();
            providedTimer.Dispose();
            mainThread.openPlacanjaDugovanja();
        }

        #region cross-thread stuff

        delegate void updateBackupInfoCallback(string text);

        public void updateBackupInfo(string text) {
            if (lblBackupInfo.IsHandleCreated)
            {
                if (lblBackupInfo.InvokeRequired)
                {

                    updateBackupInfoCallback d = new updateBackupInfoCallback(updateBackupInfo);
                    Invoke(d, text);
                }
                else
                {
                    lblBackupInfo.Text = text;
                }
            }
        }

        delegate void backupUploadDoneCallback();

        public void backupUploadDone()
        {
            if (pnlBackup.IsHandleCreated)
            {
                if (InvokeRequired)
                {

                    backupUploadDoneCallback d = new backupUploadDoneCallback(backupUploadDone);
                    Invoke(d);
                }
                else
                {
                    pnlBackup.Visible = false;
                    unlockAll();

                    if (closeForm)
                    {
                        Application.Exit();
                    }
                }
            }
        }

        delegate void showMessageCallback(string text);

        public void showMessage(string text)
        {
            if (IsHandleCreated)
            {
                if (InvokeRequired)
                {

                    showMessageCallback d = new showMessageCallback(showMessage);
                    Invoke(d, text);
                }
                else
                {
                    MessageBox.Show(this, text);
                }
            }
        }

        delegate void dropboxUploadDoneCallback(bool deleteFile, string fileName);

        public void dropboxUploadDone(bool deleteFile, string fileName)
        {
            if (IsHandleCreated)
            {
                if (InvokeRequired)
                {

                    dropboxUploadDoneCallback d = new dropboxUploadDoneCallback(dropboxUploadDone);
                    Invoke(d, deleteFile, fileName);

                }
                else
                {
                    uploadingExcelToDropbox = false;

                    if (deleteFile)
                    {
                        File.Delete(fileName);
                    }

                    unlockAll();
                }
            }
        }

        delegate void openPlacanjDugovanjaCallback();

        public void openPlacanjaDugovanja()
        {
            if (IsHandleCreated)
            {
                if (InvokeRequired)
                {

                    openPlacanjDugovanjaCallback d = new openPlacanjDugovanjaCallback(openPlacanjaDugovanja);
                    Invoke(d);
                }
                else
                {
                    ListaPlacanjaDugovanja beleske = new ListaPlacanjaDugovanja(this);
                    beleske.ShowDialog();
                    beleske.Dispose();
                }
            }
        }
        #endregion

        private void Form1_Activated(object sender, EventArgs e)
        {
            if (!startupFinished)
            {
                if(!olvEvidencije.Focused)
                {
                    if (tempDanasnjeEvidencije.Count > 0)
                    {
                        ubaciDanasnjeEvidencijeUListu(tempDanasnjeEvidencije);
                    }
                    else {
                        ubaciDanasnjeEvidencijeUListu(DatabaseCommunication.getDanasnjeEvidencije(DateTime.Now));
                    }
                }
                osveziBrojArhiviranihIAktivnihPredmeta(true);
                ubaciRocistaUListu(tempRocistaZaListu);
                reloadData(true);
                refreshList(true);
                proveriTimer();
                startupFinished = true;
            }
        }

        private void tbBeleske_KeyDown(object sender, KeyEventArgs e)
        {
            if (!tbBeleske.ReadOnly)
            {
                //check if ctrl+b and bold if yes
                GlobalVariables.boldShortcutKey_KeyDown(sender, e);
                //check if ctrl+v or shift+insert and paste only text without formatting
                GlobalVariables.ignoreFormattingWhenPasting_KeyDown(sender, e);
            }
        }
    }
}

#region extenzije

public static partial class DateTimeExtensions
{
    public static DateTime FirstDayOfWeek(this DateTime dt)
    {
        var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
        var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
        if (diff < 0)
            diff += 7;
        return dt.AddDays(-diff).Date;
    }

    public static DateTime LastDayOfWeek(this DateTime dt)
    {
        return dt.FirstDayOfWeek().AddDays(6);
    }

    public static DateTime FirstDayOfMonth(this DateTime dt)
    {
        return new DateTime(dt.Year, dt.Month, 1);
    }

    public static DateTime LastDayOfMonth(this DateTime dt)
    {
        return dt.FirstDayOfMonth().AddMonths(1).AddDays(-1);
    }

    public static DateTime FirstDayOfNextMonth(this DateTime dt)
    {
        return dt.FirstDayOfMonth().AddMonths(1);
    }
}

namespace System.Windows.Forms
{
    public class RichTextBoxPrintCtrl : RichTextBox
    {
        //Convert the unit used by the .NET framework (1/100 inch) 
        //and the unit used by Win32 API calls (twips 1/1440 inch)
        private const double anInch = 14.4;

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CHARRANGE
        {
            public int cpMin;         //First character of range (0 for start of doc)
            public int cpMax;           //Last character of range (-1 for end of doc)
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct FORMATRANGE
        {
            public IntPtr hdc;             //Actual DC to draw on
            public IntPtr hdcTarget;       //Target DC for determining text formatting
            public RECT rc;                //Region of the DC to draw to (in twips)
            public RECT rcPage;            //Region of the whole DC (page size) (in twips)
            public CHARRANGE chrg;         //Range of text to draw (see earlier declaration)
        }

        private const int WM_USER = 0x0400;
        private const int EM_FORMATRANGE = WM_USER + 57;

        [DllImport("USER32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        // Render the contents of the RichTextBox for printing
        //  Return the last character printed + 1 (printing start from this point for next page)
        public int Print(int charFrom, int charTo, PrintPageEventArgs e)
        {
            //Calculate the area to render and print
            RECT rectToPrint;
            rectToPrint.Top = (int)(e.MarginBounds.Top * anInch);
            rectToPrint.Bottom = (int)(e.MarginBounds.Bottom * anInch);
            rectToPrint.Left = (int)(e.MarginBounds.Left * anInch);
            rectToPrint.Right = (int)(e.MarginBounds.Right * anInch);

            //Calculate the size of the page
            RECT rectPage;
            rectPage.Top = (int)(e.PageBounds.Top * anInch);
            rectPage.Bottom = (int)(e.PageBounds.Bottom * anInch);
            rectPage.Left = (int)(e.PageBounds.Left * anInch);
            rectPage.Right = (int)(e.PageBounds.Right * anInch);

            IntPtr hdc = e.Graphics.GetHdc();

            FORMATRANGE fmtRange;
            fmtRange.chrg.cpMax = charTo;               //Indicate character from to character to 
            fmtRange.chrg.cpMin = charFrom;
            fmtRange.hdc = hdc;                    //Use the same DC for measuring and rendering
            fmtRange.hdcTarget = hdc;              //Point at printer hDC
            fmtRange.rc = rectToPrint;             //Indicate the area on page to print
            fmtRange.rcPage = rectPage;            //Indicate size of page

            IntPtr res = IntPtr.Zero;

            IntPtr wparam = IntPtr.Zero;
            wparam = new IntPtr(1);

            //Get the pointer to the FORMATRANGE structure in memory
            IntPtr lparam = IntPtr.Zero;
            lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange));
            Marshal.StructureToPtr(fmtRange, lparam, false);

            //Send the rendered data for printing 
            res = SendMessage(Handle, EM_FORMATRANGE, wparam, lparam);

            //Free the block of memory allocated
            Marshal.FreeCoTaskMem(lparam);

            //Release the device context handle obtained by a previous call
            e.Graphics.ReleaseHdc(hdc);

            //Return last + 1 character printer
            return res.ToInt32();
        }
    }
}
#endregion